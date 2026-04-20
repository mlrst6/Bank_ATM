using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;

namespace Bank_ATM.User
{
    internal sealed class UserSettingsForm : Form
    {
        private readonly BankingService _bankingService = new BankingService();

        private Label _titleLabel;
        private Label _subtitleLabel;
        private Label _fullNameValue;
        private Label _usernameValue;
        private Label _phoneValue;
        private Label _accountValue;
        private Label _balanceValue;
        private Label _accountStatusValue;
        private Label _cardNumberValue;
        private Label _cardExpiryValue;
        private Label _cardStatusValue;
        private Label _sessionTimeoutValue;
        private TextBox _currentPinTextBox;
        private TextBox _newPinTextBox;
        private TextBox _confirmPinTextBox;
        private Label _pinStatusLabel;
        private Button _changePinButton;
        private Button _closeButton;

        public UserSettingsForm()
        {
            InitializeComponent();
            LoadSessionDetails();
        }

        private void InitializeComponent()
        {
            Text = LanguageManager.GetString("UserSettings");
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(760, 610);
            BackColor = Color.FromArgb(12, 18, 32);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            _titleLabel = new Label
            {
                Text = LanguageManager.GetString("UserSettings"),
                Location = new Point(30, 24),
                Size = new Size(700, 36),
                Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold),
                ForeColor = Color.White
            };

            _subtitleLabel = new Label
            {
                Text = LanguageManager.GetString("UserSettingsSubtitle"),
                Location = new Point(32, 66),
                Size = new Size(680, 42),
                ForeColor = Color.FromArgb(170, 184, 204)
            };

            var profilePanel = CreatePanel(30, 124, 330, 302);
            var cardPanel = CreatePanel(390, 124, 330, 302);
            var pinPanel = CreatePanel(30, 448, 690, 118);

            AddSectionTitle(profilePanel, LanguageManager.GetString("ProfileAndAccount"), 18);
            _fullNameValue = AddInfoRow(profilePanel, LanguageManager.GetString("FullName"), 58);
            _usernameValue = AddInfoRow(profilePanel, LanguageManager.GetString("Username"), 96);
            _phoneValue = AddInfoRow(profilePanel, LanguageManager.GetString("PhoneNumber"), 134);
            _accountValue = AddInfoRow(profilePanel, LanguageManager.GetString("AccountNumber"), 172);
            _balanceValue = AddInfoRow(profilePanel, LanguageManager.GetString("Balance"), 210);
            _accountStatusValue = AddInfoRow(profilePanel, LanguageManager.GetString("AccountStatus"), 248);

            AddSectionTitle(cardPanel, LanguageManager.GetString("ConnectedCard"), 18);
            _cardNumberValue = AddInfoRow(cardPanel, LanguageManager.GetString("CardNumber"), 58);
            _cardExpiryValue = AddInfoRow(cardPanel, LanguageManager.GetString("CardExpiryDate"), 96);
            _cardStatusValue = AddInfoRow(cardPanel, LanguageManager.GetString("CardStatus"), 134);
            _sessionTimeoutValue = AddInfoRow(cardPanel, LanguageManager.GetString("SessionTimeout"), 172);

            AddSectionTitle(pinPanel, LanguageManager.GetString("ChangePin"), 14);
            AddPinInput(pinPanel, LanguageManager.GetString("CurrentPin"), 18, 48, out _currentPinTextBox);
            AddPinInput(pinPanel, LanguageManager.GetString("NewPin"), 180, 48, out _newPinTextBox);
            AddPinInput(pinPanel, LanguageManager.GetString("ConfirmNewPin"), 342, 48, out _confirmPinTextBox);
            NumericInputDialog.Attach(_currentPinTextBox, LanguageManager.GetString("CurrentPin"));
            NumericInputDialog.Attach(_newPinTextBox, LanguageManager.GetString("NewPin"));
            NumericInputDialog.Attach(_confirmPinTextBox, LanguageManager.GetString("ConfirmNewPin"));

            _changePinButton = new Button
            {
                Text = LanguageManager.GetString("ChangePin"),
                Location = new Point(512, 46),
                Size = new Size(150, 34)
            };
            StyleButton(_changePinButton, Color.FromArgb(22, 163, 74));
            _changePinButton.Click += ChangePinButton_Click;
            pinPanel.Controls.Add(_changePinButton);

            _pinStatusLabel = new Label
            {
                Location = new Point(18, 86),
                Size = new Size(644, 24),
                ForeColor = Color.FromArgb(248, 113, 113)
            };
            pinPanel.Controls.Add(_pinStatusLabel);

            _closeButton = new Button
            {
                Text = LanguageManager.GetString("Back"),
                Location = new Point(570, 574),
                Size = new Size(150, 36)
            };
            StyleButton(_closeButton, Color.FromArgb(71, 85, 105));
            _closeButton.Click += (s, e) => Close();

            Controls.Add(_titleLabel);
            Controls.Add(_subtitleLabel);
            Controls.Add(profilePanel);
            Controls.Add(cardPanel);
            Controls.Add(pinPanel);
            Controls.Add(_closeButton);
        }

        private void LoadSessionDetails()
        {
            UserDto user = SessionManager.Instance.CurrentUser;
            AccountDto account = SessionManager.Instance.CurrentAccount;
            CardDto card = SessionManager.Instance.CurrentCard;

            _fullNameValue.Text = ValueOrDash(user?.FullName);
            _usernameValue.Text = ValueOrDash(user?.Username);
            _phoneValue.Text = ValueOrDash(user?.PhoneNumber);
            _accountValue.Text = ValueOrDash(account?.AccountNumber);
            _balanceValue.Text = card == null
                ? "-"
                : LanguageManager.Format("CurrencyAmountUzs", card.Balance);
            _accountStatusValue.Text = account != null && account.IsActive
                ? LanguageManager.GetString("AccountActive")
                : LanguageManager.GetString("AccountInactive");

            _cardNumberValue.Text = card == null ? "-" : $"{card.CardType} {MaskCardNumber(card.CardNumber)}";
            _cardExpiryValue.Text = card == null ? "-" : card.ExpiryDate.ToString("yyyy-MM-dd");
            _cardStatusValue.Text = GetCardStatus(card);
            _sessionTimeoutValue.Text = LanguageManager.Format("SessionTimeoutValue", Config.SessionTimeoutSeconds);
        }

        private async void ChangePinButton_Click(object sender, EventArgs e)
        {
            _pinStatusLabel.ForeColor = Color.FromArgb(248, 113, 113);
            _pinStatusLabel.Text = string.Empty;
            SetPinInputsEnabled(false);

            try
            {
                ServiceResult result = await _bankingService.ChangeCurrentCardPinAsync(
                    _currentPinTextBox.Text,
                    _newPinTextBox.Text,
                    _confirmPinTextBox.Text);

                if (result.Success)
                {
                    _currentPinTextBox.Clear();
                    _newPinTextBox.Clear();
                    _confirmPinTextBox.Clear();
                    _pinStatusLabel.ForeColor = Color.FromArgb(74, 222, 128);
                }

                _pinStatusLabel.Text = result.Message;
            }
            finally
            {
                SetPinInputsEnabled(true);
            }
        }

        private void SetPinInputsEnabled(bool enabled)
        {
            _currentPinTextBox.Enabled = enabled;
            _newPinTextBox.Enabled = enabled;
            _confirmPinTextBox.Enabled = enabled;
            _changePinButton.Enabled = enabled;
            UseWaitCursor = !enabled;
        }

        private static Panel CreatePanel(int x, int y, int width, int height)
        {
            return new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.FromArgb(19, 32, 56),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private static void AddSectionTitle(Control parent, string text, int y)
        {
            parent.Controls.Add(new Label
            {
                Text = text,
                Location = new Point(18, y),
                Size = new Size(parent.Width - 36, 28),
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.White
            });
        }

        private static Label AddInfoRow(Control parent, string caption, int y)
        {
            parent.Controls.Add(new Label
            {
                Text = caption,
                Location = new Point(18, y),
                Size = new Size(130, 24),
                ForeColor = Color.FromArgb(148, 163, 184)
            });

            var valueLabel = new Label
            {
                Text = "-",
                Location = new Point(154, y),
                Size = new Size(parent.Width - 172, 24),
                ForeColor = Color.White,
                AutoEllipsis = true
            };
            parent.Controls.Add(valueLabel);
            return valueLabel;
        }

        private static void AddPinInput(Control parent, string caption, int x, int y, out TextBox textBox)
        {
            parent.Controls.Add(new Label
            {
                Text = caption,
                Location = new Point(x, y - 24),
                Size = new Size(140, 20),
                ForeColor = Color.FromArgb(148, 163, 184)
            });

            textBox = new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(130, 30),
                MaxLength = 4,
                PasswordChar = '*',
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                TextAlign = HorizontalAlignment.Center
            };
            textBox.KeyPress += PinTextBox_KeyPress;
            parent.Controls.Add(textBox);
        }

        private static void PinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static void StyleButton(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        private static string MaskCardNumber(string cardNumber)
        {
            string digits = new string((cardNumber ?? string.Empty).Where(char.IsDigit).ToArray());
            if (digits.Length != 16)
            {
                return "****";
            }

            return $"{digits.Substring(0, 4)} **** **** {digits.Substring(12, 4)}";
        }

        private static string GetCardStatus(CardDto card)
        {
            if (card == null)
            {
                return "-";
            }

            if (card.IsBlocked)
            {
                return LanguageManager.GetString("CardBlocked");
            }

            if (card.ExpiryDate.Date < DateTime.Today)
            {
                return LanguageManager.GetString("CardExpired");
            }

            return LanguageManager.GetString("CardActive");
        }

        private static string ValueOrDash(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value;
        }
    }
}
