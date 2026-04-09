using System;
using System.IO;
using Serilog;

namespace Bank_ATM.Core
{
    public static class AuditLogger
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized) return;

            Directory.CreateDirectory(Config.LogDirectory);
            string logPath = Path.Combine(Config.LogDirectory, "audit-.txt");
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            _initialized = true;
            Log.Information("Audit Logger initialized.");
        }

        public static void LogInfo(string message) => Log.Information(message);
        public static void LogWarning(string message) => Log.Warning(message);
        public static void LogError(string message, Exception ex = null) => Log.Error(ex, message);

        public static void LogTransaction(string type, decimal amount, string account)
        {
            Log.Information("Transaction: {Type} of {Amount:N2} UZS for Account {Account}", type, amount, account);
        }

        public static void Shutdown()
        {
            Log.CloseAndFlush();
        }
    }
}
