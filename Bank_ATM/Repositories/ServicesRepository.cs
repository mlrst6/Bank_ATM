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
                        created_at as CreatedAt
                    FROM services
                    WHERE id = @Id", new { Id = id });
            }
        }

        public int CreateService(ServiceDto service)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QuerySingle<int>(@"
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
                db.Execute("DELETE FROM services WHERE id = @Id", new { Id = id });
            }
        }
}
}
