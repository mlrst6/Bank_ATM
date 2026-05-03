using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;

namespace Bank_ATM
{
    public sealed partial class GuestCardTopUpForm : Form
    {
        private readonly BankingService _bankingService = new BankingService();
        private bool _cardVerified;
        private bool _isReviewing;
        private bool _completed;
        private bool _isLoading;
        private CardDto _verifiedCard;
        private string _verifiedCardNumber;
        private CashNoteDto[] _pendingNotes;
        private decimal _pendingCashAmount;
        private decimal _pendingFeeAmount;
        private decimal _pendingFeePercent;
        private decimal _pendingCreditAmount;
        private bool IsInDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;

        public GuestCardTopUpForm()
        {
            InitializeComponent();
        }

        private void GuestCardTopUpForm_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode) return;

            lblTitle.Text = LanguageManager.GetString("GuestCardTopUp");
            lblSubtitle.Text = LanguageManager.GetString("GuestCardTopUpSubtitle");
            lblCardNumber.Text = LanguageManager.GetString("CardNumberInput");
            lblCardHolderCaption.Text = LanguageManager.GetString("CardHolderLabel");
            lblCardStatusCaption.Text = LanguageManager.GetString("CardStatusLabel");
            lblCardHolderValue.Text = "-";
            lblCardStatusValue.Text = LanguageManager.GetString("WaitingForVerification");

            NumericInputDialog.Attach(txtCardNumber, LanguageManager.GetString("CardNumberInput"));

            ApplyState();
        }

        private async void btnDeposit_Click(object sender, EventArgs e)
        {
            if (_isLoading || _completed) return;

            if (_isReviewing)
            {
                await ExecuteDepositAsync();
                return;
            }

            if (!_cardVerified)
            {
                VerifyCard();
                return;
            }

            PrepareReview();
        }

        private void VerifyCard()
        {
            string raw = (txtCardNumber.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(raw))
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidTargetCardNumber"));
                return;
            }

            var lookup = _bankingService.LookupCardByNumber(raw);
            if (!lookup.Success)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), lookup.Message);
                return;
            }

            _verifiedCard = lookup.Card;
            _verifiedCardNumber = lookup.SanitizedCardNumber;
            _cardVerified = true;
            lblCardHolderValue.Text = MaskCardNumber(_verifiedCardNumber);
            lblCardStatusValue.Text = LanguageManager.GetString("CardStatusActive");

            ApplyState();
            ShowStatus(StatusBannerKind.Success, LanguageManager.GetString("Success"), LanguageManager.GetString("CardStatusActive"));
        }

        private void PrepareReview()
        {
            if (!_cardVerified || _verifiedCard == null) return;

            var uzsCurrencies = _bankingService.GetActiveCurrencies()
                .Where(c => c.Code == "UZS")
                .ToArray();
            if (!uzsCurrencies.Any())
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("SelectedCurrencyUnavailable"));
                return;
            }

            using (var dialog = new CashNoteInputDialog(
                LanguageManager.GetString("GuestCardTopUpCashTitle"),
                LanguageManager.GetString("GuestCardTopUpCashSubtitle"),
                uzsCurrencies,
                code => _bankingService.GetCashDenominations(code)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                _pendingNotes = dialog.Notes ?? new CashNoteDto[0];
                _pendingCashAmount = dialog.TotalAmount;
            }

            if (_pendingNotes == null || !_pendingNotes.Any() || _pendingCashAmount <= 0m)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidCashNotes"));
                return;
            }

            var fee = _bankingService.PreviewServicePaymentFee(_pendingCashAmount, false);
            _pendingFeeAmount = fee.FeeAmountUzs;
            _pendingFeePercent = fee.PercentFee;
            _pendingCreditAmount = Math.Max(0m, decimal.Round(_pendingCashAmount - _pendingFeeAmount, 2));

            if (_pendingCreditAmount <= 0m)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidAmount"));
                return;
            }

            _isReviewing = true;
            txtCardNumber.Enabled = false;
            ApplyState();
            ShowStatus(StatusBannerKind.Info, LanguageManager.GetString("Confirm"), BuildReviewMessage());
        }

        private async System.Threading.Tasks.Task ExecuteDepositAsync()
        {
            SetLoading(true);
            BankingResult result;
            try
            {
                result = await _bankingService.GuestDepositCashToCardAsync(_verifiedCardNumber, _pendingNotes);
            }
            catch (Exception ex)
            {
                SetLoading(false);
                AuditLogger.LogError($"Guest card top-up failed: {ex.Message}");
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("PaymentError"), LanguageManager.GetString("ExchangeError"));
                return;
            }
            SetLoading(false);

            if (!result.Success)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("PaymentError"), result.Message);
                return;
            }

            string receiptMessage = ReceiptWorkflow.OfferPdfReceipt(
                this,
                () => ReceiptService.GenerateGuestCardTopUpReceipt(result, _verifiedCardNumber));
            string successMsg = LanguageManager.GetString("CardTopUpCompleted");
            if (!string.IsNullOrWhiteSpace(receiptMessage))
                successMsg += Environment.NewLine + receiptMessage;

            _completed = true;
            ShowStatus(StatusBannerKind.Success, LanguageManager.GetString("Payment"), successMsg);
            ApplyState();
            btnCancel.Text = LanguageManager.GetString("Close");
            btnCancel.Values.Text = btnCancel.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_isLoading) return;

            if (_isReviewing && !_completed)
            {
                _isReviewing = false;
                _pendingNotes = null;
                _pendingCashAmount = 0m;
                txtCardNumber.Enabled = true;
                ApplyState();
                statusBanner.Clear();
                return;
            }

            DialogResult = _completed ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        private void SetLoading(bool loading)
        {
            _isLoading = loading;
            UseWaitCursor = loading;
            ApplyState();
        }

        private void ApplyState()
        {
            btnDeposit.Enabled = !_isLoading && !_completed;
            btnCancel.Enabled = !_isLoading;
            txtCardNumber.Enabled = !_isLoading && !_isReviewing && !_completed;

            if (_completed)
            {
                btnDeposit.Enabled = false;
                return;
            }

            if (_isReviewing)
            {
                btnDeposit.Text = LanguageManager.GetString("Confirm");
                btnDeposit.Values.Text = btnDeposit.Text;
                return;
            }

            if (_cardVerified)
            {
                btnDeposit.Text = LanguageManager.GetString("SelectCashNotes");
                btnDeposit.Values.Text = btnDeposit.Text;
                return;
            }

            btnDeposit.Text = LanguageManager.GetString("CardTopUpVerifyButton");
            btnDeposit.Values.Text = btnDeposit.Text;
        }

        private string BuildReviewMessage()
        {
            return LanguageManager.Format(
                "CardTopUpReviewMessage",
                MaskCardNumber(_verifiedCardNumber),
                _pendingCashAmount,
                _pendingFeeAmount,
                _pendingFeePercent,
                _pendingCreditAmount);
        }

        private void ShowStatus(StatusBannerKind kind, string title, string message)
        {
            statusBanner.ShowMessage(kind, title, message);
        }

        private static string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 4)
                return cardNumber;
            return $"**** **** **** {cardNumber.Substring(cardNumber.Length - 4)}";
        }
    }
}
