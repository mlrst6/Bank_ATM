using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;

namespace Bank_ATM.Core
{
    public static class DatabaseMigrator
    {
        private static string _connectionString = Config.ConnectionString;

        public static string DescribeConnectionTarget()
        {
            return Config.DescribeConnectionTarget();
        }

        public static void Migrate()
        {
            Migrate(Config.SeedDefaultAdminOnStartup);
        }

        public static void Migrate(bool seedDefaultAdmin)
        {
            try
            {
                CreateDatabaseIfNotExists();
                EnsureMigrationTableExists();

                var migrationsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations");
                if (!Directory.Exists(migrationsDir))
                {
                    migrationsDir = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Bank_ATM", "Migrations");
                }

                if (!Directory.Exists(migrationsDir))
                {
                    throw new DirectoryNotFoundException($"Migrations directory was not found. Expected path: {migrationsDir}");
                }

                var migrationFiles = Directory.GetFiles(migrationsDir, "*.sql").OrderBy(f => f).ToList();
                var appliedMigrations = GetAppliedMigrations();

                using (var connection = OpenConnection(_connectionString))
                {
                    foreach (var file in migrationFiles)
                    {
                        var fileName = Path.GetFileName(file);
                        if (!appliedMigrations.Contains(fileName))
                        {
                            Console.WriteLine($"Applying migration: {fileName}");
                            var script = File.ReadAllText(file);
                            
                            using (var transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    connection.Execute(script, null, transaction);
                                    connection.Execute("INSERT INTO schema_migrations (version) VALUES (@Version)", new { Version = fileName }, transaction);
                                    transaction.Commit();
                                    AuditLogger.LogInfo($"Migration applied successfully: {fileName}");
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    AuditLogger.LogError($"Migration failed: {fileName}", ex);
                                    throw;
                                }
                            }
                        }
                    }
                }

                if (seedDefaultAdmin)
                {
                    EnsureDefaultAdmin();
                }
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Fatal error during database migration", ex);
                throw;
            }
        }

        public static void VerifySchemaReady()
        {
            try
            {
                using (var connection = OpenConnection(_connectionString))
                {
                    int requiredTables = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.tables
                        WHERE name IN ('users', 'accounts', 'cards', 'transactions', 'services', 'service_accounts', 'atms', 'currencies', 'atm_currency_cash', 'atm_cash_denominations')");

                    if (requiredTables < 10)
                    {
                        throw new InvalidOperationException(
                            "Database schema is incomplete. Run the migration bootstrap once or deploy the SQL scripts first.");
                    }

                    int requiredColumns = connection.ExecuteScalar<int>(@"
                        SELECT
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.users')
                               AND name IN ('is_active'))
                            +
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.transactions')
                               AND name IN ('service_id', 'service_account_id', 'payment_reference'))");

                    if (requiredColumns < 4)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the latest migrations before starting the application.");
                    }

                    int serviceAccountUserColumns = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.columns
                        WHERE object_id = OBJECT_ID('dbo.service_accounts')
                          AND name IN ('user_id')");

                    if (serviceAccountUserColumns < 1)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the latest service account migrations before starting the application.");
                    }

                    int atmCashColumns = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.columns
                        WHERE object_id = OBJECT_ID('dbo.atms')
                          AND name IN ('current_balance')");

                    if (atmCashColumns < 1)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the ATM cash balance migration before starting the application.");
                    }

                    int currencyColumns = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.columns
                        WHERE object_id = OBJECT_ID('dbo.currencies')
                          AND name IN ('code', 'rate_to_uzs', 'buy_rate_to_uzs', 'sell_rate_to_uzs', 'is_active')");

                    if (currencyColumns < 5)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the currencies migration before starting the application.");
                    }

                    int denominationColumns = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.columns
                        WHERE object_id = OBJECT_ID('dbo.atm_cash_denominations')
                          AND name IN ('atm_id', 'currency_id', 'denomination_value', 'note_count')");

                    if (denominationColumns < 4)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the ATM cash denominations migration before starting the application.");
                    }

                    int cardMoneyColumns = connection.ExecuteScalar<int>(@"
                        SELECT
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.cards')
                               AND name IN ('card_type', 'balance'))
                            +
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.transactions')
                               AND name IN ('card_id', 'target_card_id'))");

                    if (cardMoneyColumns < 4)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the card balance and card type migration before starting the application.");
                    }

                    int feeSchemaObjects = connection.ExecuteScalar<int>(@"
                        SELECT
                            (SELECT COUNT(*) FROM sys.tables WHERE name IN ('fee_rules'))
                            +
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.transactions')
                               AND name IN ('fee_amount', 'total_debited', 'net_amount', 'exchange_rate', 'rate_kind'))");

                    if (feeSchemaObjects < 6)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the fees and exchange spread migration before starting the application.");
                    }

                    int cashbackSchemaObjects = connection.ExecuteScalar<int>(@"
                        SELECT
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.services')
                               AND name IN ('cashback_percent'))
                            +
                            (SELECT COUNT(*)
                             FROM sys.columns
                             WHERE object_id = OBJECT_ID('dbo.transactions')
                               AND name IN ('cashback_amount'))");

                    if (cashbackSchemaObjects < 2)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the service cashback migration before starting the application.");
                    }

                    int longDescriptionColumns = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.columns
                        WHERE object_id = OBJECT_ID('dbo.transactions')
                          AND name = 'description'
                          AND max_length = -1");

                    if (longDescriptionColumns < 1)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the long transaction descriptions migration before starting the application.");
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Database schema verification failed", ex);
                throw;
            }
        }

        private static void CreateDatabaseIfNotExists()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            var targetDb = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            using (var connection = OpenConnection(builder.ConnectionString))
            {
                var dbExists = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM sys.databases WHERE name = @Name", new { Name = targetDb });
                if (dbExists == 0)
                {
                    connection.Execute($"CREATE DATABASE [{targetDb}]");
                    AuditLogger.LogInfo($"Database '{targetDb}' created.");
                }
            }
        }

        private static void EnsureMigrationTableExists()
        {
            using (var connection = OpenConnection(_connectionString))
            {
                connection.Execute(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'schema_migrations')
                    CREATE TABLE schema_migrations (
                        id INT IDENTITY(1,1) PRIMARY KEY,
                        version NVARCHAR(255) NOT NULL UNIQUE,
                        applied_at DATETIME DEFAULT GETDATE()
                    )");
            }
        }

        private static List<string> GetAppliedMigrations()
        {
            using (var connection = OpenConnection(_connectionString))
            {
                return connection.Query<string>("SELECT version FROM schema_migrations").ToList();
            }
        }

        private static void EnsureDefaultAdmin()
        {
            try
            {
                using (var connection = OpenConnection(_connectionString))
                {
                    var adminExists = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM users WHERE username = 'admin'");

                    if (adminExists == 0)
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123", 11);
                        connection.Execute(@"
                            INSERT INTO users (full_name, username, password_hash, role) 
                            VALUES ('System Admin', 'admin', @Password, 'Admin')", 
                            new { Password = hashedPassword });
                        AuditLogger.LogWarning("Default admin account 'admin' was created because seed bootstrap is enabled. Change its password immediately.");
                    }
                }
            }
            catch (Exception ex)
            {
                AuditLogger.LogWarning($"EnsureDefaultAdmin skipped: {ex.Message}");
            }
        }

        private static SqlConnection OpenConnection(string connectionString)
        {
            try
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex) when (IsServerUnavailable(ex))
            {
                throw new InvalidOperationException(
                    $"Could not connect to SQL Server instance '{GetDataSource(connectionString)}'.{Environment.NewLine}" +
                    $"Check that the SQL Server service or LocalDB instance exists and is running.{Environment.NewLine}" +
                    $"Update App.config or BANK_ATM_CONNECTION_STRING if this is the wrong server.",
                    ex);
            }
            catch (SqlException ex) when (ex.Number == 18456)
            {
                throw new InvalidOperationException(
                    $"SQL Server rejected the login for instance '{GetDataSource(connectionString)}'.{Environment.NewLine}" +
                    "Check the username/password or switch to Windows Authentication.",
                    ex);
            }
            catch (SqlException ex) when (ex.Number == 4060)
            {
                throw new InvalidOperationException(
                    $"Connected to SQL Server instance '{GetDataSource(connectionString)}', but database '{GetInitialCatalog(connectionString)}' is not accessible.{Environment.NewLine}" +
                    "Check that the database exists and that the login has permission to use it.",
                    ex);
            }
        }

        private static bool IsServerUnavailable(SqlException ex)
        {
            return ex.Number == 2 || ex.Number == 53 || ex.Number == -1;
        }

        private static string GetDataSource(string connectionString)
        {
            try
            {
                return new SqlConnectionStringBuilder(connectionString).DataSource;
            }
            catch
            {
                return "(unknown)";
            }
        }

        private static string GetInitialCatalog(string connectionString)
        {
            try
            {
                return new SqlConnectionStringBuilder(connectionString).InitialCatalog;
            }
            catch
            {
                return "(unknown)";
            }
        }
    }
}
