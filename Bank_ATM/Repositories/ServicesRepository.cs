using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class ServicesRepository
    {
        private readonly string _connectionString;

        public ServicesRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public IEnumerable<ServiceDto> GetActiveServices()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ServiceDto>(@"
                    SELECT
                        id,
                        service_name as ServiceName,
                        category as Category,
                        account_hint as AccountHint,
                        is_active as IsActive,
                        (
                            SELECT COUNT(*)
                            FROM service_accounts sa
                            WHERE sa.service_id = services.id
                              AND sa.is_active = 1
                        ) as ValidReferenceCount,
                        created_at as CreatedAt
                    FROM services
                    WHERE is_active = 1
                    ORDER BY category, service_name");
            }
        }

        public IEnumerable<ServiceDto> GetAllServices()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ServiceDto>(@"
                    SELECT
                        id,
                        service_name as ServiceName,
                        category as Category,
                        account_hint as AccountHint,
                        is_active as IsActive,
                        (
                            SELECT COUNT(*)
                            FROM service_accounts sa
                            WHERE sa.service_id = services.id
                              AND sa.is_active = 1
                        ) as ValidReferenceCount,
                        created_at as CreatedAt
                    FROM services
                    ORDER BY category, service_name").ToList();
            }
        }

        public ServiceDto GetServiceById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<ServiceDto>(@"
                    SELECT
                        id,
                        service_name as ServiceName,
                        category as Category,
                        account_hint as AccountHint,
                        is_active as IsActive,
                        (
                            SELECT COUNT(*)
                            FROM service_accounts sa
                            WHERE sa.service_id = services.id
                              AND sa.is_active = 1
                        ) as ValidReferenceCount,
                        created_at as CreatedAt
                    FROM services
                    WHERE id = @Id", new { Id = id });
            }
        }

        public int CreateService(ServiceDto service)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int id = db.QuerySingle<int>(@"
                    INSERT INTO services (service_name, category, account_hint, is_active)
                    VALUES (@ServiceName, @Category, @AccountHint, @IsActive);
                    SELECT CAST(SCOPE_IDENTITY() as int);",
                    new
                    {
                        service.ServiceName,
                        service.Category,
                        service.AccountHint,
                        service.IsActive
                    });
                service.Id = id;
                return id;
            }
        }

        public void UpdateService(ServiceDto service)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(@"
                    UPDATE services
                    SET service_name = @ServiceName,
                        category = @Category,
                        account_hint = @AccountHint,
                        is_active = @IsActive
                    WHERE id = @Id",
                    new
                    {
                        service.Id,
                        service.ServiceName,
                        service.Category,
                        service.AccountHint,
                        service.IsActive
                    });
            }
        }

        public void DeleteService(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("UPDATE services SET is_active = 0 WHERE id = @Id", new { Id = id });
            }
        }

        public IEnumerable<ServiceAccountDto> GetServiceAccounts(int serviceId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<ServiceAccountDto>(@"
                    SELECT
                        sa.id,
                        sa.service_id as ServiceId,
                        sa.user_id as UserId,
                        sa.reference_number as ReferenceNumber,
                        sa.customer_name as CustomerName,
                        u.full_name as UserFullName,
                        u.username as Username,
                        sa.is_active as IsActive,
                        sa.created_at as CreatedAt
                    FROM service_accounts sa
                    LEFT JOIN users u ON u.id = sa.user_id
                    WHERE sa.service_id = @ServiceId
                    ORDER BY sa.is_active DESC, u.full_name, sa.reference_number",
                    new { ServiceId = serviceId }).ToList();
            }
        }

        public ServiceAccountDto GetActiveServiceAccount(int serviceId, string referenceNumber)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<ServiceAccountDto>(@"
                    SELECT
                        id,
                        service_id as ServiceId,
                        user_id as UserId,
                        reference_number as ReferenceNumber,
                        customer_name as CustomerName,
                        is_active as IsActive,
                        created_at as CreatedAt
                    FROM service_accounts
                    WHERE service_id = @ServiceId
                      AND reference_number = @ReferenceNumber
                      AND is_active = 1",
                    new { ServiceId = serviceId, ReferenceNumber = referenceNumber });
            }
        }

        public void ReplaceServiceAccounts(int serviceId, IEnumerable<ServiceAccountDto> accounts)
        {
            var accountList = accounts
                .Where(a => !string.IsNullOrWhiteSpace(a.ReferenceNumber))
                .GroupBy(a => a.ReferenceNumber.Trim())
                .Select(g => g.First())
                .ToList();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        db.Execute(
                            "UPDATE service_accounts SET is_active = 0 WHERE service_id = @ServiceId",
                            new { ServiceId = serviceId },
                            trans);

                        foreach (var account in accountList)
                        {
                            db.Execute(@"
                                IF EXISTS (
                                    SELECT 1 FROM service_accounts
                                    WHERE service_id = @ServiceId
                                      AND reference_number = @ReferenceNumber
                                )
                                BEGIN
                                    UPDATE service_accounts
                                    SET user_id = @UserId,
                                        customer_name = @CustomerName,
                                        is_active = 1
                                    WHERE service_id = @ServiceId
                                      AND reference_number = @ReferenceNumber
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO service_accounts (service_id, user_id, reference_number, customer_name, is_active)
                                    VALUES (@ServiceId, @UserId, @ReferenceNumber, @CustomerName, 1)
                                END",
                                new
                                {
                                    ServiceId = serviceId,
                                    account.UserId,
                                    ReferenceNumber = account.ReferenceNumber.Trim(),
                                    CustomerName = string.IsNullOrWhiteSpace(account.CustomerName)
                                        ? null
                                        : account.CustomerName.Trim()
                                },
                                trans);
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public int CreateServiceAccount(ServiceAccountDto account)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QuerySingle<int>(@"
                    IF EXISTS (
                        SELECT 1
                        FROM service_accounts
                        WHERE service_id = @ServiceId
                          AND reference_number = @ReferenceNumber
                    )
                    BEGIN
                        UPDATE service_accounts
                        SET user_id = @UserId,
                            customer_name = @CustomerName,
                            is_active = 1
                        WHERE service_id = @ServiceId
                          AND reference_number = @ReferenceNumber;

                        SELECT id
                        FROM service_accounts
                        WHERE service_id = @ServiceId
                          AND reference_number = @ReferenceNumber;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO service_accounts (service_id, user_id, reference_number, customer_name, is_active)
                        VALUES (@ServiceId, @UserId, @ReferenceNumber, @CustomerName, 1);
                        SELECT CAST(SCOPE_IDENTITY() as int);
                    END",
                    new
                    {
                        account.ServiceId,
                        account.UserId,
                        ReferenceNumber = account.ReferenceNumber.Trim(),
                        CustomerName = string.IsNullOrWhiteSpace(account.CustomerName)
                            ? null
                            : account.CustomerName.Trim()
                    });
            }
        }

        public void DeactivateServiceAccount(int serviceAccountId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("UPDATE service_accounts SET is_active = 0 WHERE id = @Id", new { Id = serviceAccountId });
            }
        }
    }
}
