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
            btnTransactions.Text = LanguageManager.GetString("MyTransactions");
            btnTransactions.Values.Text = btnTransactions.Text;
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

                var feePreview = _bankingService.PreviewWithdrawFee(dialog.Amount, dialog.CurrencyCode);
                if (!ConfirmFeeReview(
                    LanguageManager.GetString("Withdraw"),
                    $"{dialog.Amount:N2} {dialog.CurrencyCode}",
                    feePreview,
                    "Cash value"))
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
                    string receiptMessage = OfferDigitalReceipt("Withdraw", result.DebitedAmountUzs, $"{dialog.Amount:N2} {dialog.CurrencyCode}; Fee: {result.FeeAmountUzs:N2} UZS");
                    ShowStatus(
                        StatusBannerKind.Success,
                        LanguageManager.GetString("Withdraw"),
                        BuildOperationSummary(BuildFeeSummary(LanguageManager.GetString("Success"), result), LanguageManager.GetString("CashDispensedBreakdown"), result.CashBreakdown, receiptMessage));
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

                var feePreview = _bankingService.PreviewDepositFee(dialog.TotalAmount, dialog.CurrencyCode);
                if (!ConfirmDepositFeeReview(dialog.TotalAmount, dialog.CurrencyCode, feePreview))
                {
                    return;
                }

                SetLoading(true);
                var result = await _bankingService.DepositCashNotesAsync(dialog.CurrencyCode, dialog.Notes);
                SetLoading(false);

                if (result.Success)
                {
                    RefreshAccountSummary();
                    string receiptMessage = OfferDigitalReceipt(
                        "Deposit",
                        result.NetAmountUzs,
                        $"{result.CashAmount:N2} {result.CashCurrencyCode}; Fee: {result.FeeAmountUzs:N2} UZS");
                    ShowStatus(
                        StatusBannerKind.Success,
                        LanguageManager.GetString("Deposit"),
                        BuildOperationSummary(BuildDepositFeeSummary(LanguageManager.GetString("Success"), result), LanguageManager.GetString("CashAcceptedBreakdown"), result.CashBreakdown, receiptMessage));
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

                var feePreview = _bankingService.PreviewTransferFee(dialog.Amount);
                if (!ConfirmFeeReview(
                    LanguageManager.GetString("Transfer"),
                    $"{dialog.Amount:N2} UZS",
                    feePreview,
                    "Recipient receives"))
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
                    string receiptMessage = OfferDigitalReceipt("Transfer", result.DebitedAmountUzs, LanguageManager.Format("TransferReceiptDescription", result.TargetCard.CardNumber) + $"; Fee: {result.FeeAmountUzs:N2} UZS");
                    ShowStatus(
                        StatusBannerKind.Success,
                        LanguageManager.GetString("Transfer"),
                        BuildOperationSummary(BuildFeeSummary(LanguageManager.GetString("Success"), result), null, null, receiptMessage));
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

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            using (var form = new UserTransactionHistoryForm())
            {
                form.ShowDialog(this);
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
            return ReceiptWorkflow.OfferPdfReceipt(
                this,
                () =>
                {
                    var transaction = new TransactionDto
                    {
                        Type = transactionType,
                        Amount = amount,
                        TransactionDate = DateTime.Now,
                        Description = description
                    };

                    return _bankingService.GenerateReceiptForCurrentSession(transaction);
                });
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

        private bool ConfirmFeeReview(string title, string displayAmount, FeeCalculationResult fee, string netLabel)
        {
            string message =
                $"Amount: {displayAmount}{Environment.NewLine}" +
                $"{netLabel}: {fee.BaseAmountUzs:N2} UZS{Environment.NewLine}" +
                $"Fee: {fee.PercentFee:N4}% ({fee.FeeAmountUzs:N2} UZS){Environment.NewLine}" +
                $"Total debited: {fee.TotalDebitUzs:N2} UZS";

            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private bool ConfirmDepositFeeReview(decimal cashAmount, string currencyCode, FeeCalculationResult fee)
        {
            decimal credited = fee.BaseAmountUzs - fee.FeeAmountUzs;
            if (credited < 0m)
            {
                credited = 0m;
            }

            string message =
                $"Cash inserted: {cashAmount:N2} {currencyCode}{Environment.NewLine}" +
                $"Cash value: {fee.BaseAmountUzs:N2} UZS{Environment.NewLine}" +
                $"Fee: {fee.PercentFee:N4}% ({fee.FeeAmountUzs:N2} UZS){Environment.NewLine}" +
                $"Credited: {credited:N2} UZS";

            return MessageBox.Show(message, LanguageManager.GetString("Deposit"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private static string BuildFeeSummary(string message, BankingResult result)
        {
            return message + Environment.NewLine +
                   $"Amount: {result.NetAmountUzs:N2} UZS" + Environment.NewLine +
                   $"Fee: {result.FeePercent:N4}% ({result.FeeAmountUzs:N2} UZS)" + Environment.NewLine +
                   $"Total debited: {result.DebitedAmountUzs:N2} UZS";
        }

        private static string BuildDepositFeeSummary(string message, BankingResult result)
        {
            return message + Environment.NewLine +
                   $"Cash value: {result.DebitedAmountUzs:N2} UZS" + Environment.NewLine +
                   $"Fee: {result.FeePercent:N4}% ({result.FeeAmountUzs:N2} UZS)" + Environment.NewLine +
                   $"Credited: {result.NetAmountUzs:N2} UZS";
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
