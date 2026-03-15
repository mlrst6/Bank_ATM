using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            // Add global activity listener for ATM security timeout
            Application.AddMessageFilter(new UserActivityFilter());
            
            // When a timeout happens, restart the application state
            TimeoutManager.OnTimeout += HandleTimeout;

            Application.Run(new LanguageForm1());
        }

        private static void HandleTimeout()
        {
            // Must be invoked on the UI thread
            if (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Invoke(new Action(() =>
                {
                    // If we aren't logged in, we don't care about timeout kicks
                    if (!SessionManager.IsLoggedIn) return;

                    MessageBox.Show("Session expired due to inactivity. Please remove your card.", "Security Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    SessionManager.Logout();

                    // Close all forms except the first one (LanguageForm)
                    for (int i = Application.OpenForms.Count - 1; i > 0; i--)
                    {
                        Application.OpenForms[i].Close();
                    }
                    
                    // Show the language form again
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
            // If the user clicks a mouse button or presses a key, reset the timer
            if (m.Msg == WM_KEYDOWN || m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN)
            {
                if (SessionManager.IsLoggedIn)
                {
                    TimeoutManager.ResetTimer();
                }
            }
            return false; // Do not block the message
        }
    }
}
