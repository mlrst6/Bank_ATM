using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Payments;
using Bank_ATM.Services;
using Bank_ATM.UI;

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
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            RefreshAccountSummary();

            btnWithdraw.Text = LanguageManager.GetString("Withdraw");
            btnWithdraw.Values.Text = btnWithdraw.Text;
            btnDeposit.Text = LanguageManager.GetString("Deposit");
            btnDeposit.Values.Text = btnDeposit.Text;
            btnTransfer.Text = LanguageManager.GetString("Transfer");
            btnTransfer.Values.Text = btnTransfer.Text;
            btnServices.Text = LanguageManager.GetString("PayServices");
            btnServices.Values.Text = btnServices.Text;
            btnBalance.Text = LanguageManager.GetString("ViewBalance");
            btnBalance.Values.Text = btnBalance.Text;
            btnSettings.Text = LanguageManager.GetString("UserSettings");
            btnSettings.Values.Text = btnSettings.Text;
            btnBack.Text = LanguageManager.GetString("Back");
            btnBack.Values.Text = btnBack.Text;
            btnLogout.Text = LanguageManager.GetString("Logout");
            btnLogout.Values.Text = btnLogout.Text;
        }

        private async void btnWithdraw_Click(object sender, EventArgs e)
        {
            var currencies = _bankingService.GetActiveCurrencies();
            if (currencies.Length == 0)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Error"), LanguageManager.GetString("SelectedCurrencyUnavailable"));
                return;
            }

            using (var dialog = new UserOperationDialog(UserOperationType.Withdraw, currencies))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                SetLoading(true);
                var result = await _bankingService.WithdrawAsync(dialog.Amount, dialog.CurrencyCode);
                SetLoading(false);

                if (result.Success)
                {
                    AuditLogger.LogTransaction("Withdraw", result.DebitedAmountUzs, SessionManager.Instance.CurrentAccount.AccountNumber);
                    RefreshAccountSummary();
                    string receiptMessage = OfferDigitalReceipt("Withdraw", result.DebitedAmountUzs, $"{dialog.Amount:N2} {dialog.CurrencyCode}");
                    ShowStatus(
                        StatusBannerKind.Success,
                        LanguageManager.GetString("Withdraw"),
                        BuildOperationSummary(LanguageManager.GetString("Success"), LanguageManager.GetString("CashDispensedBreakdown"), result.CashBreakdown, receiptMessage));
                }
                else
                {
                    ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Error"), result.Message);
                }
            }
        }

        private async void btnDeposit_Click(object sender, EventArgs e)
        {
            var currencies = _bankingService.GetActiveCurrencies();
            if (currencies.Length == 0)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Error"), LanguageManager.GetString("SelectedCurrencyUnavailable"));
                return;
            }

            using (var dialog = new CashNoteInputDialog(
                LanguageManager.GetString("Deposit"),
                LanguageManager.GetString("DepositCashNotesSubtitle"),
                currencies,
                code => _bankingService.GetCashDenominations(code)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                SetLoading(true);
                var result = await _bankingService.DepositCashNotesAsync(dialog.CurrencyCode, dialog.Notes);
                SetLoading(false);

                if (result.Success)
                {
                    RefreshAccountSummary();
                    ShowStatus(
                        StatusBannerKind.Success,
                        LanguageManager.GetString("Deposit"),
                        BuildOperationSummary(LanguageManager.GetString("Success"), LanguageManager.GetString("CashAcceptedBreakdown"), result.CashBreakdown, null));
                }
                else
                {
                    ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Error"), result.Message);
                }
            }
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            using (var dialog = new UserOperationDialog(UserOperationType.Transfer, new CurrencyDto[0]))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                SetLoading(true);
                var result = await _bankingService.TransferByCardAsync(dialog.TargetCardNumber, dialog.Amount);
                SetLoading(false);

                if (result.Success)
                {
                    AuditLogger.LogTransaction("Transfer", dialog.Amount, $"{SessionManager.Instance.CurrentAccount.AccountNumber} -> {result.TargetCard.CardNumber}");
                    RefreshAccountSummary();
                    string receiptMessage = OfferDigitalReceipt("Transfer", dialog.Amount, LanguageManager.Format("TransferReceiptDescription", result.TargetCard.CardNumber));
                    ShowStatus(
                        StatusBannerKind.Success,
                        LanguageManager.GetString("Transfer"),
                        BuildOperationSummary(LanguageManager.GetString("Success"), null, null, receiptMessage));
                }
                else
                {
                    ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Error"), result.Message);
                }
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            RefreshAccountSummary();
            ShowStatus(
                StatusBannerKind.Info,
                LanguageManager.GetString("BalanceInfo"),
                LanguageManager.Format("CurrentBalanceMessage", SessionManager.Instance.CurrentCard.Balance));
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            using (var form = new ServicePaymentForm(true))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshAccountSummary();
                    ShowStatus(StatusBannerKind.Success, LanguageManager.GetString("Payment"), LanguageManager.GetString("ServicePaymentCompleted"));
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new UserSettingsForm())
            {
                form.ShowDialog(this);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _authenticationService.Logout();
            FormNavigator.ShowExistingOrNew<MainForm>(this);
        }

        private void btnBack_Click(object sender, EventArgs e)
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
            var card = SessionManager.Instance.CurrentCard;

            if (user == null || account == null || card == null)
            {
                lblWelcome.Text = LanguageManager.GetString("AccountSessionUnavailable");
                lblAccountValue.Text = "-";
                lblBalanceValue.Text = LanguageManager.Format("CurrencyAmountUzs", 0m);
                lblStatusValue.Text = LanguageManager.GetString("Offline");
                return;
            }

            lblWelcome.Text = LanguageManager.Format("WelcomeBackUser", user.FullName);
            lblSubtitle.Text = LanguageManager.GetString("UserDashboardSubtitle");
            lblAccountValue.Text = $"{card.CardType} {MaskCardNumber(card.CardNumber)}";
            lblBalanceValue.Text = LanguageManager.Format("CurrencyAmountUzs", card.Balance);
            lblStatusValue.Text = account.IsActive
                ? LanguageManager.GetString("AccountActive")
                : LanguageManager.GetString("AccountInactive");
        }

        private string OfferDigitalReceipt(string transactionType, decimal amount, string description)
        {
            using (var dialog = new ReceiptChoiceDialog(LanguageManager.GetString("ReceiptChoiceSubtitle")))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK || dialog.Choice != ReceiptChoice.SavePdf)
                {
                    return null;
                }
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
                using (var savedDialog = new ReceiptSavedDialog(path))
                {
                    savedDialog.ShowDialog(this);
                }

                return LanguageManager.Format("ReceiptSavedTo", path);
            }

            return null;
        }

        private string BuildOperationSummary(string message, string cashTitle, CashNoteDto[] notes, string receiptMessage)
        {
            var parts = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrWhiteSpace(message))
            {
                parts.Add(message);
            }

            if (notes != null && notes.Length > 0)
            {
                parts.Add(cashTitle + ": " + string.Join("; ", notes.Select(note =>
                    $"{note.DenominationValue:N0} {note.CurrencyCode} x {note.NoteCount}")));
            }

            if (!string.IsNullOrWhiteSpace(receiptMessage))
            {
                parts.Add(receiptMessage);
            }

            return string.Join(Environment.NewLine, parts);
        }

        private void ShowStatus(StatusBannerKind kind, string title, string message)
        {
            statusBanner.ShowMessage(kind, title, message);
        }

        private static string MaskCardNumber(string cardNumber)
        {
            string digits = (cardNumber ?? string.Empty).Trim();
            if (digits.Length <= 4)
            {
                return digits;
            }

            return "**** " + digits.Substring(digits.Length - 4);
        }

    }
}
