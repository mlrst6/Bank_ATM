using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Bank_ATM.UI
{
    internal enum ReceiptChoice
    {
        None,
        SavePdf
    }

    internal sealed class ReceiptChoiceDialog : Form
    {
        private readonly string _subtitle;

        public ReceiptChoice Choice { get; private set; }

        public ReceiptChoiceDialog(string subtitle)
        {
            _subtitle = subtitle;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = LanguageManager.GetString("Receipt");
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(460, 310);
            BackColor = Color.FromArgb(12, 18, 32);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            var titleLabel = new Label
            {
                Text = LanguageManager.GetString("ReceiptChoiceTitle"),
                Location = new Point(28, 24),
                Size = new Size(404, 34),
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.White
            };

            var subtitleLabel = new Label
            {
                Text = string.IsNullOrWhiteSpace(_subtitle)
                    ? LanguageManager.GetString("ReceiptChoiceSubtitle")
                    : _subtitle,
                Location = new Point(30, 68),
                Size = new Size(400, 48),
                ForeColor = Color.FromArgb(170, 184, 204)
            };

            var saveButton = CreateButton(LanguageManager.GetString("SavePdfReceipt"), 30, 135, Color.FromArgb(22, 163, 74));
            saveButton.Click += (s, e) =>
            {
                Choice = ReceiptChoice.SavePdf;
                DialogResult = DialogResult.OK;
                Close();
            };

            var noReceiptButton = CreateButton(LanguageManager.GetString("NoReceipt"), 30, 190, Color.FromArgb(71, 85, 105));
            noReceiptButton.Click += (s, e) =>
            {
                Choice = ReceiptChoice.None;
                DialogResult = DialogResult.OK;
                Close();
            };

            var printButton = CreateButton(LanguageManager.GetString("PrintReceiptComingSoon"), 250, 190, Color.FromArgb(55, 65, 81));
            printButton.Enabled = false;

            var hintLabel = new Label
            {
                Text = LanguageManager.GetString("PrintReceiptHint"),
                Location = new Point(30, 250),
                Size = new Size(400, 28),
                ForeColor = Color.FromArgb(148, 163, 184)
            };

            Controls.Add(titleLabel);
            Controls.Add(subtitleLabel);
            Controls.Add(saveButton);
            Controls.Add(noReceiptButton);
            Controls.Add(printButton);
            Controls.Add(hintLabel);
        }

        private static Button CreateButton(string text, int x, int y, Color backColor)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(180, 42),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }
    }

    internal sealed class ReceiptSavedDialog : Form
    {
        private readonly string _receiptPath;

        public ReceiptSavedDialog(string receiptPath)
        {
            _receiptPath = receiptPath;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = LanguageManager.GetString("Receipt");
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(520, 265);
            BackColor = Color.FromArgb(12, 18, 32);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            var titleLabel = new Label
            {
                Text = LanguageManager.GetString("ReceiptSavedTitle"),
                Location = new Point(28, 24),
                Size = new Size(464, 34),
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.White
            };

            var pathTextBox = new TextBox
            {
                Text = _receiptPath ?? string.Empty,
                Location = new Point(30, 88),
                Size = new Size(460, 32),
                ReadOnly = true,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var hintLabel = new Label
            {
                Text = LanguageManager.GetString("ReceiptSavedHint"),
                Location = new Point(30, 132),
                Size = new Size(460, 42),
                ForeColor = Color.FromArgb(170, 184, 204)
            };

            var openFolderButton = CreateButton(LanguageManager.GetString("OpenReceiptFolder"), 170, 195, Color.FromArgb(14, 165, 233));
            openFolderButton.Click += (s, e) => OpenReceiptFolder();

            var closeButton = CreateButton(LanguageManager.GetString("Close"), 350, 195, Color.FromArgb(71, 85, 105));
            closeButton.Click += (s, e) => Close();

            Controls.Add(titleLabel);
            Controls.Add(pathTextBox);
            Controls.Add(hintLabel);
            Controls.Add(openFolderButton);
            Controls.Add(closeButton);
        }

        private void OpenReceiptFolder()
        {
            if (string.IsNullOrWhiteSpace(_receiptPath))
            {
                return;
            }

            string directory = Path.GetDirectoryName(_receiptPath);
            if (string.IsNullOrWhiteSpace(directory) || !Directory.Exists(directory))
            {
                return;
            }

            try
            {
                Process.Start("explorer.exe", "/select,\"" + _receiptPath + "\"");
            }
            catch
            {
                Process.Start("explorer.exe", "\"" + directory + "\"");
            }
        }

        private static Button CreateButton(string text, int x, int y, Color backColor)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(140, 42),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }
    }
}
