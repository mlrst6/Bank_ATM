using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Payments;
using Bank_ATM.Services;

namespace Bank_ATM.User
{
    public partial class UserActionsForm : Form
    {
        private readonly BankingService _bankingService = new BankingService();
        private readonly AuthenticationService _authenticationService = new AuthenticationService();

        public UserActionsForm()
        {
            InitializeComponent();
        }

        private void UserActionsForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            ApplyTheme();
            RefreshAccountSummary();

            btnWithdraw.Text = LanguageManager.GetString("Withdraw");
            btnDeposit.Text = LanguageManager.GetString("Deposit");
            btnTransfer.Text = LanguageManager.GetString("Transfer");
            btnServices.Text = LanguageManager.GetString("PayServices");
            btnBalance.Text = LanguageManager.GetString("ViewBalance");
            btnLogout.Text = LanguageManager.GetString("Logout");
        }

        private async void btnWithdraw_Click(object sender, EventArgs e)
        {
            var currencies = _bankingService.GetActiveCurrencies();
            string currencyCode = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.Format("SelectWithdrawCurrency", string.Join(", ", currencies.Select(c => c.Code))),
                LanguageManager.GetString("Withdraw"),
                "UZS");
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                return;
            }

            currencyCode = currencyCode.Trim().ToUpperInvariant();
            if (!currencies.Any(c => c.Code == currencyCode))
            {
                MessageBox.Show(LanguageManager.GetString("SelectedCurrencyUnavailable"), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TryPromptAmount(LanguageManager.Format("WithdrawCurrencyAmount", currencyCode), out decimal amount))
            {
                SetLoading(true);
                var result = await _bankingService.WithdrawAsync(amount, currencyCode);
                SetLoading(false);

                if (result.Success)
                {
                    AuditLogger.LogTransaction("Withdraw", result.DebitedAmountUzs, SessionManager.Instance.CurrentAccount.AccountNumber);
                    RefreshAccountSummary();
                    bool showedReceiptMessage = OfferDigitalReceipt("Withdraw", result.DebitedAmountUzs, $"{amount:N2} {currencyCode}");
                    if (!showedReceiptMessage)
                    {
                        MessageBox.Show(LanguageManager.GetString("Success"), LanguageManager.GetString("Withdraw"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(result.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnDeposit_Click(object sender, EventArgs e)
        {
            var currencies = _bankingService.GetActiveCurrencies();
            string currencyCode = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.Format("SelectDepositCurrency", string.Join(", ", currencies.Select(c => c.Code))),
                LanguageManager.GetString("Deposit"),
                "UZS");
            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                return;
            }

            currencyCode = currencyCode.Trim().ToUpperInvariant();
            if (!currencies.Any(c => c.Code == currencyCode))
            {
                MessageBox.Show(LanguageManager.GetString("SelectedCurrencyUnavailable"), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (TryPromptAmount(LanguageManager.Format("DepositCurrencyAmount", currencyCode), out decimal amount))
            {
                SetLoading(true);
                var result = await _bankingService.DepositAsync(amount, currencyCode);
                SetLoading(false);

                if (result.Success)
                {
                    RefreshAccountSummary();
                    MessageBox.Show(LanguageManager.GetString("Success"), LanguageManager.GetString("Deposit"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            string targetCardNum = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.GetString("EnterTargetCardNumber"),
                LanguageManager.GetString("Transfer"),
                string.Empty);
            if (string.IsNullOrEmpty(targetCardNum)) return;

            string amountStr = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.GetString("EnterAmount"),
                LanguageManager.GetString("Transfer"),
                "0");
            if (TryParseAmount(amountStr, out decimal amount))
            {
                SetLoading(true);
                var result = await _bankingService.TransferByCardAsync(targetCardNum, amount);
                SetLoading(false);

                if (result.Success)
                {
                    AuditLogger.LogTransaction("Transfer", amount, $"{SessionManager.Instance.CurrentAccount.AccountNumber} -> {result.TargetCard.CardNumber}");
                    RefreshAccountSummary();
                    bool showedReceiptMessage = OfferDigitalReceipt("Transfer", amount, LanguageManager.Format("TransferReceiptDescription", result.TargetCard.CardNumber));
                    if (!showedReceiptMessage)
                    {
                        MessageBox.Show(LanguageManager.GetString("Success"), LanguageManager.GetString("Transfer"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(result.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (!string.IsNullOrWhiteSpace(amountStr))
            {
                MessageBox.Show(LanguageManager.GetString("InvalidPaymentAmount"), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            RefreshAccountSummary();
            MessageBox.Show(
                LanguageManager.Format("CurrentBalanceMessage", SessionManager.Instance.CurrentAccount.Balance),
                LanguageManager.GetString("BalanceInfo"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            using (var form = new ServicePaymentForm(true))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshAccountSummary();
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _authenticationService.Logout();
            FormNavigator.ShowExistingOrNew<MainForm>(this);
        }

        private void SetLoading(bool isLoading)
        {
            this.UseWaitCursor = isLoading;
            foreach (Control c in this.Controls) c.Enabled = !isLoading;
        }

        private void RefreshAccountSummary()
        {
            var user = SessionManager.Instance.CurrentUser;
            var account = SessionManager.Instance.CurrentAccount;

            if (user == null || account == null)
            {
                lblWelcome.Text = LanguageManager.GetString("AccountSessionUnavailable");
                lblAccountValue.Text = "-";
                lblBalanceValue.Text = LanguageManager.Format("CurrencyAmountUzs", 0m);
                lblStatusValue.Text = LanguageManager.GetString("Offline");
                return;
            }

            lblWelcome.Text = LanguageManager.Format("WelcomeBackUser", user.FullName);
            lblSubtitle.Text = LanguageManager.GetString("UserDashboardSubtitle");
            lblAccountValue.Text = account.AccountNumber;
            lblBalanceValue.Text = LanguageManager.Format("CurrencyAmountUzs", account.Balance);
            lblStatusValue.Text = account.IsActive
                ? LanguageManager.GetString("AccountActive")
                : LanguageManager.GetString("AccountInactive");
        }

        private bool OfferDigitalReceipt(string transactionType, decimal amount, string description)
        {
            if (MessageBox.Show(
                LanguageManager.GetString("DigitalReceiptPrompt"),
                LanguageManager.GetString("Receipt"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return false;
            }

            var transaction = new TransactionDto
            {
                Type = transactionType,
                Amount = amount,
                TransactionDate = DateTime.Now,
                Description = description
            };

            string path = _bankingService.GenerateReceiptForCurrentSession(transaction);
            if (!string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show(
                    LanguageManager.Format("ReceiptSavedTo", path),
                    LanguageManager.GetString("Success"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return true;
            }

            return false;
        }

        private bool TryPromptAmount(string title, out decimal amount)
        {
            string amountStr = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.GetString("EnterAmount"),
                title,
                "0");

            if (string.IsNullOrWhiteSpace(amountStr))
            {
                amount = 0m;
                return false;
            }

            if (!TryParseAmount(amountStr, out amount))
            {
                MessageBox.Show(LanguageManager.GetString("InvalidPaymentAmount"), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private static bool TryParseAmount(string input, out decimal amount)
        {
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out amount) ||
                decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
            {
                return amount > 0m && decimal.Round(amount, 2) == amount;
            }

            amount = 0m;
            return false;
        }

        private void ApplyTheme()
        {
            BackColor = Color.FromArgb(9, 14, 28);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);

            pnlHeader.BackColor = Color.FromArgb(15, 23, 42);
            lblWelcome.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);

            pnlAccountCard.BackColor = Color.FromArgb(19, 32, 56);
            pnlAccountCard.BorderStyle = BorderStyle.FixedSingle;
            lblCardTitle.ForeColor = Color.FromArgb(191, 219, 254);
            lblAccountCaption.ForeColor = Color.FromArgb(148, 163, 184);
            lblBalanceCaption.ForeColor = Color.FromArgb(148, 163, 184);
            lblStatusCaption.ForeColor = Color.FromArgb(148, 163, 184);
            lblAccountValue.ForeColor = Color.White;
            lblBalanceValue.ForeColor = Color.FromArgb(125, 211, 252);
            lblBalanceValue.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblStatusValue.ForeColor = Color.FromArgb(74, 222, 128);
            lblHelper.ForeColor = Color.FromArgb(148, 163, 184);

            StyleActionButton(btnWithdraw, Color.FromArgb(37, 99, 235));
            StyleActionButton(btnDeposit, Color.FromArgb(8, 145, 178));
            StyleActionButton(btnTransfer, Color.FromArgb(124, 58, 237));
            StyleActionButton(btnServices, Color.FromArgb(22, 163, 74));
            StyleActionButton(btnBalance, Color.FromArgb(234, 88, 12));
            StyleActionButton(btnLogout, Color.FromArgb(71, 85, 105));
        }

        private void StyleActionButton(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

    }
}
