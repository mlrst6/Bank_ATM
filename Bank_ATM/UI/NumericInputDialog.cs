using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bank_ATM.UI
{
    public sealed class NumericInputDialog : Form
    {
        private readonly bool _allowDecimal;
        private readonly bool _allowSeparators;
        private readonly int _maxLength;
        private readonly TextBox _displayTextBox;

        public string InputText { get; private set; }

        public NumericInputDialog(string title, string currentValue, bool allowDecimal = false, bool allowSeparators = false, int maxLength = 0)
        {
            _allowDecimal = allowDecimal;
            _allowSeparators = allowSeparators;
            _maxLength = maxLength;

            Text = title;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(340, allowSeparators ? 430 : 360);
            BackColor = Color.FromArgb(12, 18, 32);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            _displayTextBox = new TextBox
            {
                Location = new Point(20, 18),
                Size = new Size(300, 42),
                Text = currentValue ?? string.Empty,
                ReadOnly = true,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Right,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold)
            };
            Controls.Add(_displayTextBox);

            var keypad = new TableLayoutPanel
            {
                Location = new Point(20, 78),
                Size = new Size(300, allowSeparators ? 280 : 220),
                ColumnCount = 3,
                RowCount = allowSeparators ? 5 : 4
            };

            for (int i = 0; i < 3; i++)
            {
                keypad.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            for (int i = 0; i < keypad.RowCount; i++)
            {
                keypad.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / keypad.RowCount));
            }

            string[] buttons = allowSeparators
                ? new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "0", "<", ",", "CLR", "OK" }
                : new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", _allowDecimal ? "." : "<", "0", "OK" };

            foreach (string text in buttons)
            {
                keypad.Controls.Add(CreateKeyButton(text));
            }

            Controls.Add(keypad);
        }

        public static void Attach(TextBox textBox, string title, bool allowDecimal = false, bool allowSeparators = false)
        {
            if (textBox == null)
            {
                return;
            }

            bool opening = false;
            Action showDialog = () =>
            {
                if (opening || !textBox.Enabled)
                {
                    return;
                }

                opening = true;
                try
                {
                    ShowForTextBox(textBox, title, allowDecimal, allowSeparators);
                }
                finally
                {
                    opening = false;
                }
            };

            textBox.ReadOnly = true;
            textBox.ShortcutsEnabled = false;
            textBox.KeyPress += (s, e) => e.Handled = true;
            textBox.Click += (s, e) => showDialog();
            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.ShiftKey)
                {
                    return;
                }

                e.SuppressKeyPress = true;
                e.Handled = true;
                showDialog();
            };
        }

        public static bool ShowForTextBox(TextBox textBox, string title, bool allowDecimal = false, bool allowSeparators = false)
        {
            using (var dialog = new NumericInputDialog(title, textBox.Text, allowDecimal, allowSeparators, textBox.MaxLength))
            {
                if (dialog.ShowDialog(textBox.FindForm()) != DialogResult.OK)
                {
                    return false;
                }

                textBox.Text = dialog.InputText;
                textBox.SelectionStart = textBox.Text.Length;
                return true;
            }
        }

        private Button CreateKeyButton(string text)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                BackColor = GetButtonColor(text),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += KeyButton_Click;
            return button;
        }

        private static Color GetButtonColor(string text)
        {
            if (text == "CLR" || text == "<")
            {
                return Color.FromArgb(185, 28, 28);
            }

            if (text == "OK")
            {
                return Color.FromArgb(22, 163, 74);
            }

            return Color.FromArgb(30, 41, 59);
        }

        private void KeyButton_Click(object sender, EventArgs e)
        {
            string key = ((Button)sender).Text;
            if (key == "OK")
            {
                InputText = _displayTextBox.Text;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (key == "CLR")
            {
                _displayTextBox.Clear();
                return;
            }

            if (key == "<")
            {
                if (_displayTextBox.Text.Length > 0)
                {
                    _displayTextBox.Text = _displayTextBox.Text.Substring(0, _displayTextBox.Text.Length - 1);
                }
                return;
            }

            if ((key == "." && !_allowDecimal) || (key == "," && !_allowSeparators))
            {
                return;
            }

            if (key == "." && _displayTextBox.Text.Contains("."))
            {
                return;
            }

            if (_maxLength > 0 && _displayTextBox.Text.Length >= _maxLength)
            {
                return;
            }

            _displayTextBox.Text += key;
        }
    }
}
