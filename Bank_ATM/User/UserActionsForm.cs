using System;
using System.Drawing;
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
        private StatusBanner _statusBanner;
        private bool _statusLayoutApplied;

        public UserActionsForm()
        {
            InitializeComponent();
        }

        private void UserActionsForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(784, 715);
            EnsureStatusBanner();
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            ApplyTheme();
            RefreshAccountSummary();

            btnWithdraw.Text = LanguageManager.GetString("Withdraw");
            btnDeposit.Text = LanguageManager.GetString("Deposit");
            btnTransfer.Text = LanguageManager.GetString("Transfer");
            btnServices.Text = LanguageManager.GetString("PayServices");
            btnBalance.Text = LanguageManager.GetString("ViewBalance");
            btnSettings.Text = LanguageManager.GetString("UserSettings");
            btnBack.Text = LanguageManager.GetString("Back");
            btnLogout.Text = LanguageManager.GetString("Logout");
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
            EnsureStatusBanner();
            _statusBanner.ShowMessage(kind, title, message);
        }

        private void EnsureStatusBanner()
        {
            if (_statusBanner == null)
            {
                _statusBanner = new StatusBanner
                {
                    Location = new Point(28, 332),
                    Size = new Size(728, 82),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                Controls.Add(_statusBanner);
            }

            if (_statusLayoutApplied)
            {
                return;
            }

            ShiftControl(btnWithdraw, 94);
            ShiftControl(btnDeposit, 94);
            ShiftControl(btnTransfer, 94);
            ShiftControl(btnServices, 94);
            ShiftControl(btnBalance, 94);
            ShiftControl(btnSettings, 94);
            ShiftControl(btnBack, 94);
            ShiftControl(btnLogout, 94);
            _statusLayoutApplied = true;
        }

        private static void ShiftControl(Control control, int deltaY)
        {
            control.Location = new Point(control.Location.X, control.Location.Y + deltaY);
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
            StyleActionButton(btnSettings, Color.FromArgb(79, 70, 229));
            StyleActionButton(btnBack, Color.FromArgb(71, 85, 105));
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
