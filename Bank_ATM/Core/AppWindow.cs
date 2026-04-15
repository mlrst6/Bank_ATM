using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bank_ATM.Core
{
    public static class AppWindow
    {
        public static readonly Size MainScreenSize = new Size(900, 650);

        public static void ApplyMainScreen(Form form)
        {
            if (form == null)
            {
                return;
            }

            Rectangle contentBounds = GetContentBounds(form);
            form.ClientSize = MainScreenSize;

            if (!contentBounds.IsEmpty)
            {
                int offsetX = (form.ClientSize.Width - contentBounds.Width) / 2 - contentBounds.Left;
                int offsetY = (form.ClientSize.Height - contentBounds.Height) / 2 - contentBounds.Top;
                MoveTopLevelContent(form, offsetX, offsetY);
            }

            if (form.StartPosition == FormStartPosition.CenterScreen)
            {
                Rectangle workingArea = Screen.FromControl(form).WorkingArea;
                form.Location = new Point(
                    workingArea.Left + (workingArea.Width - form.Width) / 2,
                    workingArea.Top + (workingArea.Height - form.Height) / 2);
            }
        }

        private static Rectangle GetContentBounds(Form form)
        {
            var controls = form.Controls
                .Cast<Control>()
                .Where(control => control.Dock == DockStyle.None)
                .ToList();

            if (controls.Count == 0)
            {
                return Rectangle.Empty;
            }

            Rectangle bounds = controls[0].Bounds;
            for (int i = 1; i < controls.Count; i++)
            {
                bounds = Rectangle.Union(bounds, controls[i].Bounds);
            }

            return bounds;
        }

        private static void MoveTopLevelContent(Form form, int offsetX, int offsetY)
        {
            foreach (Control control in form.Controls)
            {
                if (control.Dock != DockStyle.None)
                {
                    continue;
                }

                control.Left += offsetX;
                control.Top += offsetY;
            }
        }
    }
}
