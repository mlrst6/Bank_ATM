using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Services;

namespace Bank_ATM.User
{
    public partial class UserCardEntryForm : BaseForm
    {
        private string _customTitleKey = "MainFormUser";
        private bool _isCardPlaceholderActive;
        private readonly AuthenticationService _authenticationService = new AuthenticationService();

        public UserCardEntryForm()
        {
            InitializeComponent();
        }

        public UserCardEntryForm(string titleKey) : this()
        {
            _customTitleKey = titleKey;
        }

        private void SetupKeypad()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button btn && int.TryParse(btn.Text, out _))
                {
                    btn.Click += (s, e) => {
                        if (_isCardPlaceholderActive)
                        {
                            txtCardNumber.Clear();
                            txtCardNumber.ForeColor = Color.White;
                            _isCardPlaceholderActive = false;
                        }

                        AppendCardDigit(btn.Text[0]);
                    };
                }
            }
        }

        private void UserCardEntryForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString(_customTitleKey);
            
            btnClear.Text = LanguageManager.GetString("btnClear");
            btnInsertCard.Text = LanguageManager.GetString("btnInsertCard");
            btnBack.Text = LanguageManager.GetString("btnBack");
            InitializeCardPlaceholder();
            txtCardNumber.ReadOnly = true;
            SetupKeypad();
            txtCardNumber.Enter -= txtCardNumber_Enter;
            txtCardNumber.Enter += txtCardNumber_Enter;
            txtCardNumber.Leave -= txtCardNumber_Leave;
            txtCardNumber.Leave += txtCardNumber_Leave;
            txtCardNumber.KeyPress -= txtCardNumber_KeyPress;
            txtCardNumber.KeyPress += txtCardNumber_KeyPress;
        }

        private void btnInsertCard_Click(object sender, EventArgs e)
        {
            var access = _authenticationService.ValidateCardForLogin(txtCardNumber.Text);
            if (!access.Success)
            {
                MessageBoxIcon icon = access.Message == LanguageManager.GetString("InvalidCardNumberMessage")
                    ? MessageBoxIcon.Warning
                    : MessageBoxIcon.Error;
                MessageBox.Show(access.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, icon);
                InitializeCardPlaceholder();
                return;
            }

            NavigateTo(new UserPinEntryForm(access.SanitizedCardNumber));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        private void InitializeCardPlaceholder()
        {
            txtCardNumber.Text = LanguageManager.GetString("txtCardNumber");
            txtCardNumber.ForeColor = Color.Silver;
            _isCardPlaceholderActive = true;
        }

        private void txtCardNumber_Enter(object sender, EventArgs e)
        {
            if (_isCardPlaceholderActive)
            {
                txtCardNumber.Clear();
                txtCardNumber.ForeColor = Color.White;
                _isCardPlaceholderActive = false;
            }
        }

        private void txtCardNumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                InitializeCardPlaceholder();
            }
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_isCardPlaceholderActive)
            {
                return;
            }

            string digits = GetCardDigits();
            if (digits.Length > 0)
            {
                txtCardNumber.Text = FormatCardNumber(digits.Substring(0, digits.Length - 1));
            }

            if (GetCardDigits().Length == 0)
            {
                InitializeCardPlaceholder();
            }
        }

        private void AppendCardDigit(char digit)
        {
            string digits = GetCardDigits();
            if (digits.Length >= 16)
            {
                return;
            }

            txtCardNumber.Text = FormatCardNumber(digits + digit);
        }

        private string GetCardDigits()
        {
            if (_isCardPlaceholderActive)
            {
                return string.Empty;
            }

            return (txtCardNumber.Text ?? string.Empty).Replace(" ", string.Empty).Trim();
        }

        private static string FormatCardNumber(string digits)
        {
            string sanitized = new string((digits ?? string.Empty).Where(char.IsDigit).Take(16).ToArray());
            if (sanitized.Length == 0)
            {
                return string.Empty;
            }

            var groups = Enumerable.Range(0, (sanitized.Length + 3) / 4)
                .Select(index => sanitized.Substring(index * 4, Math.Min(4, sanitized.Length - (index * 4))));
            return string.Join(" ", groups);
        }
    }
}
