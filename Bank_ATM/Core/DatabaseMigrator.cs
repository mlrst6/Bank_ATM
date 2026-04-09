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

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
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
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    int requiredTables = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.tables
                        WHERE name IN ('users', 'accounts', 'cards', 'transactions', 'services')");

                    if (requiredTables < 5)
                    {
                        throw new InvalidOperationException(
                            "Database schema is incomplete. Run the migration bootstrap once or deploy the SQL scripts first.");
                    }

                    int requiredColumns = connection.ExecuteScalar<int>(@"
                        SELECT COUNT(*)
                        FROM sys.columns
                        WHERE object_id = OBJECT_ID('dbo.users')
                          AND name IN ('is_active')");

                    if (requiredColumns < 1)
                    {
                        throw new InvalidOperationException(
                            "Database schema is outdated. Apply the latest migrations before starting the application.");
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

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
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
            using (var connection = new SqlConnection(_connectionString))
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
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<string>("SELECT version FROM schema_migrations").ToList();
            }
        }

        private static void EnsureDefaultAdmin()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
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
    }
}
