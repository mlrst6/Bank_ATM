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
    internal sealed partial class UserSettingsForm : Form
    {
        private readonly BankingService _bankingService = new BankingService();

        public UserSettingsForm()
        {
            InitializeComponent();
        }

        private void UserSettingsForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);

            lblTitle.Text = LanguageManager.GetString("UserSettings");
            lblSubtitle.Text = LanguageManager.GetString("UserSettingsSubtitle");
            lblProfileTitle.Text = LanguageManager.GetString("ProfileAndAccount");
            lblCardTitle.Text = LanguageManager.GetString("ConnectedCard");
            lblSecurityTitle.Text = LanguageManager.GetString("ChangePin");
            lblSecuritySubtitle.Text = LanguageManager.GetString("UserSettingsSubtitle");

            lblFullNameCaption.Text = LanguageManager.GetString("FullName");
            lblUsernameCaption.Text = LanguageManager.GetString("Username");
            lblPhoneCaption.Text = LanguageManager.GetString("PhoneNumber");
            lblAccountCaption.Text = LanguageManager.GetString("AccountNumber");
            lblBalanceCaption.Text = LanguageManager.GetString("Balance");
            lblAccountStatusCaption.Text = LanguageManager.GetString("AccountStatus");

            lblCardNumberCaption.Text = LanguageManager.GetString("CardNumber");
            lblCardTypeCaption.Text = LanguageManager.GetString("CardType");
            lblCardExpiryCaption.Text = LanguageManager.GetString("CardExpiryDate");
            lblCardStatusCaption.Text = LanguageManager.GetString("CardStatus");
            lblSessionTimeoutCaption.Text = LanguageManager.GetString("SessionTimeout");

            lblCurrentPinCaption.Text = LanguageManager.GetString("CurrentPin");
            lblNewPinCaption.Text = LanguageManager.GetString("NewPin");
            lblConfirmPinCaption.Text = LanguageManager.GetString("ConfirmNewPin");

            btnChangePin.Text = LanguageManager.GetString("ChangePin");
            btnChangePin.Values.Text = btnChangePin.Text;
            btnSubmitPin.Text = LanguageManager.GetString("ChangePin");
            btnSubmitPin.Values.Text = btnSubmitPin.Text;
            btnCancelPin.Text = LanguageManager.GetString("Cancel");
            btnCancelPin.Values.Text = btnCancelPin.Text;
            btnClose.Text = LanguageManager.GetString("Back");
            btnClose.Values.Text = btnClose.Text;

            NumericInputDialog.Attach(txtCurrentPin, LanguageManager.GetString("CurrentPin"));
            NumericInputDialog.Attach(txtNewPin, LanguageManager.GetString("NewPin"));
            NumericInputDialog.Attach(txtConfirmPin, LanguageManager.GetString("ConfirmNewPin"));

            TogglePinEditor(false);
            LoadSessionDetails();
        }

        private void LoadSessionDetails()
        {
            UserDto user = SessionManager.Instance.CurrentUser;
            AccountDto account = SessionManager.Instance.CurrentAccount;
            CardDto card = SessionManager.Instance.CurrentCard;

            lblFullNameValue.Text = ValueOrDash(user?.FullName);
            lblUsernameValue.Text = ValueOrDash(user?.Username);
            lblPhoneValue.Text = ValueOrDash(user?.PhoneNumber);
            lblAccountValue.Text = ValueOrDash(account?.AccountNumber);
            lblBalanceValue.Text = card == null
                ? "-"
                : LanguageManager.Format("CurrencyAmountUzs", card.Balance);
            lblAccountStatusValue.Text = account != null && account.IsActive
                ? LanguageManager.GetString("AccountActive")
                : LanguageManager.GetString("AccountInactive");

            lblCardNumberValue.Text = card == null ? "-" : MaskCardNumber(card.CardNumber);
            lblCardTypeValue.Text = ValueOrDash(card?.CardType);
            lblCardExpiryValue.Text = card == null ? "-" : card.ExpiryDate.ToString("yyyy-MM-dd");
            lblCardStatusValue.Text = GetCardStatus(card);
            lblSessionTimeoutValue.Text = LanguageManager.Format("SessionTimeoutValue", Config.SessionTimeoutSeconds);
        }

        private void btnChangePin_Click(object sender, EventArgs e)
        {
            TogglePinEditor(true);
            txtCurrentPin.Focus();
        }

        private void btnCancelPin_Click(object sender, EventArgs e)
        {
            TogglePinEditor(false);
        }

        private async void btnSubmitPin_Click(object sender, EventArgs e)
        {
            lblPinStatus.ForeColor = Color.FromArgb(248, 113, 113);
            lblPinStatus.Text = string.Empty;
            SetPinEditorEnabled(false);

            try
            {
                ServiceResult result = await _bankingService.ChangeCurrentCardPinAsync(
                    txtCurrentPin.Text,
                    txtNewPin.Text,
                    txtConfirmPin.Text);

                if (result.Success)
                {
                    lblPinStatus.ForeColor = Color.FromArgb(74, 222, 128);
                    lblPinStatus.Text = result.Message;
                    txtCurrentPin.Clear();
                    txtNewPin.Clear();
                    txtConfirmPin.Clear();
                    TogglePinEditor(false);
                    return;
                }

                lblPinStatus.Text = result.Message;
            }
            finally
            {
                SetPinEditorEnabled(true);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TogglePinEditor(bool visible)
        {
            pnlPinEditor.Visible = visible;
            btnChangePin.Visible = !visible;

            if (!visible)
            {
                txtCurrentPin.Clear();
                txtNewPin.Clear();
                txtConfirmPin.Clear();
                lblPinStatus.Text = string.Empty;
            }
        }

        private void SetPinEditorEnabled(bool enabled)
        {
            txtCurrentPin.Enabled = enabled;
            txtNewPin.Enabled = enabled;
            txtConfirmPin.Enabled = enabled;
            btnSubmitPin.Enabled = enabled;
            btnCancelPin.Enabled = enabled;
            btnChangePin.Enabled = enabled;
            UseWaitCursor = !enabled;
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

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}
