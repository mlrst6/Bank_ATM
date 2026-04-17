using System.Drawing;
using System.Windows.Forms;

namespace Bank_ATM.Core
{
    public static class AppWindow
    {
        public static void ApplyMainScreen(Form form)
        {
            if (form == null)
            {
                return;
            }

            if (form.StartPosition == FormStartPosition.CenterScreen)
            {
                Rectangle workingArea = Screen.FromControl(form).WorkingArea;
                form.Location = new Point(
                    workingArea.Left + (workingArea.Width - form.Width) / 2,
                    workingArea.Top + (workingArea.Height - form.Height) / 2);
            }
        }
    }
}
