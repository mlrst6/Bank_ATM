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
            try
            {
                CreateDatabaseIfNotExists();
                EnsureMigrationTableExists();
                
                // Ensure default admin is present
                EnsureDefaultAdmin();

                var migrationsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations");
                if (!Directory.Exists(migrationsDir))
                {
                    // Fallback for development/relative paths
                    migrationsDir = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Bank_ATM", "Migrations");
                }

                if (!Directory.Exists(migrationsDir)) return;

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
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Fatal error during database migration", ex);
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
                    
                    // Check if 'admin' user exists
                    var adminExists = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM users WHERE username = 'admin'");
                    
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword("admin123", 11);

                    if (adminExists == 0)
                    {
                        // Create admin
                        connection.Execute(@"
                            INSERT INTO users (full_name, username, password_hash, role) 
                            VALUES ('System Admin', 'admin', @Password, 'Admin')", 
                            new { Password = hashedPassword });
                        AuditLogger.LogInfo("Default admin 'admin' created with password 'admin123'.");
                    }
                    else
                    {
                        // Update existing admin password to 'admin123' to ensure it works
                        connection.Execute(@"
                            UPDATE users SET password_hash = @Password WHERE username = 'admin'", 
                            new { Password = hashedPassword });
                        AuditLogger.LogInfo("Default admin password updated to 'admin123'.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Table might not exist yet if it's the very first run before V1 script
                // We'll ignore this as V1 script will create it with a default hash anyway
                AuditLogger.LogWarning($"EnsureDefaultAdmin skipped: {ex.Message}");
            }
        }
    }
}
