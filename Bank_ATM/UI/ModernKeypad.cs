using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bank_ATM.UI
{
    public class ModernKeypad : UserControl
    {
        public event Action<string> KeyPressed;
        public event Action ClearPressed;

        public ModernKeypad()
        {
            this.Size = new Size(300, 400);
            InitializeKeypad();
        }

        private void InitializeKeypad()
        {
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.Dock = DockStyle.Fill;
            panel.ColumnCount = 3;
            panel.RowCount = 4;
            
            // Set percentages for columns and rows
            for (int i = 0; i < 3; i++) panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            for (int i = 0; i < 4; i++) panel.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));

            string[] buttons = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "CLR", "0", "OK" };

            foreach (string b in buttons)
            {
                RoundedButton btn = new RoundedButton();
                btn.Text = b;
                btn.Dock = DockStyle.Fill;
                btn.Margin = new Padding(5);
                
                if (b == "CLR")
                {
                    btn.BackColor = Color.FromArgb(220, 53, 69); // Red
                    btn.Click += (s, e) => ClearPressed?.Invoke();
                }
                else if (b == "OK")
                {
                    btn.BackColor = Color.FromArgb(40, 167, 69); // Green
                    // OK action usually handled by the parent form
                }
                else
                {
                    btn.Click += (s, e) => KeyPressed?.Invoke(b);
                }

                panel.Controls.Add(btn);
            }

            this.Controls.Add(panel);
        }
    }
}
