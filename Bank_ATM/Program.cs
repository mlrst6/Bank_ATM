using System;
using System.Windows.Forms;
using Bank_ATM.Core;

namespace Bank_ATM
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                AuditLogger.Initialize();
                AuditLogger.LogInfo("Application Starting...");
                AuditLogger.LogInfo("Database target:" + Environment.NewLine + DatabaseMigrator.DescribeConnectionTarget());

                if (Config.BootstrapDatabaseOnStartup)
                {
                    AuditLogger.LogInfo("Database bootstrap on startup is enabled.");
                    DatabaseMigrator.Migrate();
                }
                else
                {
                    AuditLogger.LogInfo("Database bootstrap on startup is disabled. Verifying existing schema.");
                    DatabaseMigrator.VerifySchemaReady();
                }

                Application.AddMessageFilter(new UserActivityFilter());
                TimeoutManager.OnTimeout += HandleTimeout;

                Application.Run(new LanguageForm1());
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Application failed to start", ex);
                using (var form = new DatabaseSetupHelpForm(DatabaseMigrator.DescribeConnectionTarget(), ex))
                {
                    form.ShowDialog();
                }
            }
            finally
            {
                AuditLogger.LogInfo("Application shutting down.");
                AuditLogger.Shutdown();
            }
        }

        private static void HandleTimeout()
        {
            // Must be invoked on the UI thread
            if (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Invoke(new Action(() =>
                {
                    if (!SessionManager.Instance.IsLoggedIn) return;

                    string message = SessionManager.Instance.IsCardSession
                        ? "Session expired due to inactivity. Please remove your card."
                        : "Admin session expired due to inactivity. Please log in again.";

                    MessageBox.Show(message, "Security Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    SessionManager.Instance.Logout();

                    for (int i = Application.OpenForms.Count - 1; i > 0; i--)
                    {
                        Application.OpenForms[i].Close();
                    }
                    
                    if (Application.OpenForms.Count > 0)
                    {
                        Application.OpenForms[0].Show();
                    }
                }));
            }
        }
    }

    /// <summary>
    /// Listens to all Windows messages for mouse clicks and key presses to reset the session timer.
    /// </summary>
    public class UserActivityFilter : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN || m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN)
            {
                if (SessionManager.Instance.IsLoggedIn)
                {
                    TimeoutManager.ResetTimer();
                }
            }
            return false; // Do not block the message
        }
    }
}
