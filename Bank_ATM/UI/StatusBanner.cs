using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bank_ATM.UI
{
    internal enum StatusBannerKind
    {
        Info,
        Success,
        Warning,
        Error
    }

    internal sealed class StatusBanner : Panel
    {
        private readonly Label _titleLabel;
        private readonly Label _messageLabel;
        private readonly Button _dismissButton;

        public StatusBanner()
        {
            Height = 82;
            Visible = false;
            BackColor = Color.FromArgb(30, 41, 59);
            Padding = new Padding(18, 10, 42, 10);

            _titleLabel = new Label
            {
                AutoSize = false,
                Location = new Point(18, 10),
                Size = new Size(610, 22),
                Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
                ForeColor = Color.White
            };

            _messageLabel = new Label
            {
                AutoSize = false,
                Location = new Point(18, 34),
                Size = new Size(650, 38),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(226, 232, 240)
            };

            _dismissButton = new Button
            {
                Text = "x",
                Size = new Size(28, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            _dismissButton.FlatAppearance.BorderSize = 0;
            _dismissButton.Click += (s, e) => Clear();

            Controls.Add(_titleLabel);
            Controls.Add(_messageLabel);
            Controls.Add(_dismissButton);
            Resize += (s, e) => LayoutChildren();
        }

        public void ShowMessage(StatusBannerKind kind, string title, string message)
        {
            BackColor = GetBackground(kind);
            _titleLabel.Text = title ?? string.Empty;
            _messageLabel.Text = message ?? string.Empty;
            Visible = true;
            BringToFront();
            LayoutChildren();
        }

        public void Clear()
        {
            Visible = false;
            _titleLabel.Text = string.Empty;
            _messageLabel.Text = string.Empty;
        }

        private void LayoutChildren()
        {
            _dismissButton.Location = new Point(Width - _dismissButton.Width - 10, 10);
            int contentWidth = Math.Max(120, Width - 72);
            _titleLabel.Width = contentWidth;
            _messageLabel.Width = contentWidth;
        }

        private static Color GetBackground(StatusBannerKind kind)
        {
            switch (kind)
            {
                case StatusBannerKind.Success:
                    return Color.FromArgb(20, 83, 45);
                case StatusBannerKind.Warning:
                    return Color.FromArgb(120, 53, 15);
                case StatusBannerKind.Error:
                    return Color.FromArgb(127, 29, 29);
                default:
                    return Color.FromArgb(30, 41, 59);
            }
        }
    }
}
