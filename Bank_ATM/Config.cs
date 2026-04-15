using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Data.SqlClient;

namespace Bank_ATM
{
    public static class Config
    {
        private const bool DefaultBootstrapDatabaseOnStartup = false;

        public static string ConnectionString
        {
            get
            {
                string fromEnvironment = Environment.GetEnvironmentVariable("BANK_ATM_CONNECTION_STRING");
                if (!string.IsNullOrWhiteSpace(fromEnvironment))
                {
                    return fromEnvironment;
                }

                var namedConnection = ConfigurationManager.ConnectionStrings["ATM"];
                if (namedConnection != null && !string.IsNullOrWhiteSpace(namedConnection.ConnectionString))
                {
                    return namedConnection.ConnectionString;
                }

                string legacyConnection = ConfigurationManager.AppSettings["ConnectionString"];
                if (!string.IsNullOrWhiteSpace(legacyConnection))
                {
                    return legacyConnection;
                }

                throw new ConfigurationErrorsException(
                    "No SQL Server connection string is configured. Use connectionStrings/ATM or BANK_ATM_CONNECTION_STRING.");
            }
        }

        public static string ConnectionStringSource
        {
            get
            {
                string fromEnvironment = Environment.GetEnvironmentVariable("BANK_ATM_CONNECTION_STRING");
                if (!string.IsNullOrWhiteSpace(fromEnvironment))
                {
                    return "BANK_ATM_CONNECTION_STRING environment variable";
                }

                var namedConnection = ConfigurationManager.ConnectionStrings["ATM"];
                if (namedConnection != null && !string.IsNullOrWhiteSpace(namedConnection.ConnectionString))
                {
                    return "App.config connectionStrings/ATM";
                }

                string legacyConnection = ConfigurationManager.AppSettings["ConnectionString"];
                if (!string.IsNullOrWhiteSpace(legacyConnection))
                {
                    return "App.config appSettings/ConnectionString";
                }

                return "Not configured";
            }
        }

        public static string DescribeConnectionTarget()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(ConnectionString);
                string authenticationMode = builder.IntegratedSecurity
                    ? "Windows Authentication"
                    : "SQL Authentication";

                return $"Server: {builder.DataSource}{Environment.NewLine}" +
                       $"Database: {builder.InitialCatalog}{Environment.NewLine}" +
                       $"Authentication: {authenticationMode}{Environment.NewLine}" +
                       $"Source: {ConnectionStringSource}";
            }
            catch (Exception ex)
            {
                return $"Connection string could not be parsed. Source: {ConnectionStringSource}{Environment.NewLine}{ex.Message}";
            }
        }

        public static decimal UzsToUsdRate =>
            GetDecimal("UzsToUsdRate", "BANK_ATM_UZS_TO_USD_RATE", 12000m);

        public static int SessionTimeoutSeconds =>
            Math.Max(15, GetInt("SessionTimeoutSeconds", "BANK_ATM_SESSION_TIMEOUT_SECONDS", 30));

        public static bool BootstrapDatabaseOnStartup =>
            GetBool("BootstrapDatabaseOnStartup", "BANK_ATM_BOOTSTRAP_DATABASE", DefaultBootstrapDatabaseOnStartup);

        public static bool SeedDefaultAdminOnStartup =>
            GetBool("SeedDefaultAdminOnStartup", "BANK_ATM_SEED_DEFAULT_ADMIN", false);

        public static string LogDirectory
        {
            get
            {
                string configured = ReadSetting("LogDirectory", "BANK_ATM_LOG_DIRECTORY");
                if (!string.IsNullOrWhiteSpace(configured))
                {
                    return configured;
                }

                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Bank_ATM",
                    "Logs");
            }
        }

        private static string ReadSetting(string appSettingKey, string environmentVariableName)
        {
            string fromEnvironment = Environment.GetEnvironmentVariable(environmentVariableName);
            if (!string.IsNullOrWhiteSpace(fromEnvironment))
            {
                return fromEnvironment.Trim();
            }

            return ConfigurationManager.AppSettings[appSettingKey];
        }

        private static decimal GetDecimal(string appSettingKey, string environmentVariableName, decimal fallback)
        {
            decimal parsedRate;
            return decimal.TryParse(
                ReadSetting(appSettingKey, environmentVariableName),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out parsedRate)
                ? parsedRate
                : fallback;
        }

        private static int GetInt(string appSettingKey, string environmentVariableName, int fallback)
        {
            int parsed;
            return int.TryParse(
                ReadSetting(appSettingKey, environmentVariableName),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out parsed)
                ? parsed
                : fallback;
        }

        private static bool GetBool(string appSettingKey, string environmentVariableName, bool fallback)
        {
            string rawValue = ReadSetting(appSettingKey, environmentVariableName);
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return fallback;
            }

            switch (rawValue.Trim().ToLowerInvariant())
            {
                case "1":
                case "true":
                case "yes":
                case "on":
                    return true;
                case "0":
                case "false":
                case "no":
                case "off":
                    return false;
                default:
                    return fallback;
            }
        }
    }
}
