using System;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.Core;
using Bank_ATM.UI;

namespace Bank_ATM.Payments
{
    public partial class ServicePaymentForm : Form
    {
        private readonly BankingService _bankingService = new BankingService();
        private readonly bool _chargeCurrentAccount;
        private readonly ServiceDto _service;
        private bool _completed;
        private bool _isReviewing;
        private bool _referenceVerified;
        private ServiceAccountDto _verifiedServiceAccount;
        private string _pendingReference;
        private decimal _pendingAmount;
        private decimal _completedFeePercent;
        private decimal _completedFee;
        private decimal _completedTotalDebited;
        private decimal _completedNetAmount;
        private decimal _completedCashback;
        private CashNoteDto[] _pendingNotes;

        public ServicePaymentForm(ServiceDto service, bool chargeCurrentAccount)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            InitializeComponent();
            _service = service;
            _chargeCurrentAccount = chargeCurrentAccount;
        }

        private void ServicePaymentForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            lblTitle.Text = _chargeCurrentAccount
                ? LanguageManager.GetString("PayFromAccount")
                : LanguageManager.GetString("GuestServicePayment");
            lblSubtitle.Text = _chargeCurrentAccount
                ? LanguageManager.GetString("PayFromAccountSubtitle")
                : LanguageManager.GetString("GuestServicePaymentSubtitle");
            btnPay.Text = LanguageManager.GetString("Pay");
            btnPay.Values.Text = btnPay.Text;
            btnCancel.Text = LanguageManager.GetString("Cancel");
            btnCancel.Values.Text = btnCancel.Text;
            lblCustomerNameValue.Text = "-";
            lblReferenceStatusValue.Text = LanguageManager.GetString("WaitingForVerification");
            lblCategory.Text = LanguageManager.GetString("Category");
            lblCashback.Text = LanguageManager.GetString("Cashback");
            lblReference.Text = LanguageManager.GetString("AccountNumber");
            lblAmount.Text = LanguageManager.GetString("Amount") + " UZS";

            lblServiceIcon.Text = string.IsNullOrWhiteSpace(_service.IconEmoji) ? "•" : _service.IconEmoji;
            lblServiceName.Text = _service.ServiceName;
            lblCategoryValue.Text = string.IsNullOrWhiteSpace(_service.Category) ? "-" : _service.Category;
            lblCashbackValue.Text = _service.CashbackPercent.ToString("0.##", CultureInfo.CurrentCulture) + " %";
            lblReferenceHint.Text = string.IsNullOrWhiteSpace(_service.AccountHint)
                ? string.Empty
                : LanguageManager.Format("RequiredFieldHint", _service.AccountHint);

            NumericInputDialog.Attach(txtReference, LanguageManager.GetString("AccountNumber"));
            if (!_chargeCurrentAccount)
            {
                txtAmount.ReadOnly = true;
                txtAmount.Text = LanguageManager.GetString("CashAmountCalculatedFromNotes");
            }
            else
            {
                NumericInputDialog.Attach(txtAmount, LanguageManager.GetString("Amount"));
            }

            txtReference.TextChanged += (s, args) => ResetVerificationState();
        }

        private async void btnPay_Click(object sender, EventArgs e)
        {
            if (_completed)
            {
                return;
            }

            if (_isReviewing)
            {
                await ExecuteReviewedPaymentAsync();
                return;
            }

            if (!_referenceVerified)
            {
                VerifyReference();
                return;
            }

            PreparePaymentReview();
        }

        private void VerifyReference()
        {
            if (string.IsNullOrWhiteSpace(txtReference.Text))
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidServicePaymentInput"));
                txtReference.Focus();
                return;
            }

            var lookup = _bankingService.VerifyServiceAccount(_service.Id, txtReference.Text);
            if (!lookup.Success)
            {
                ResetVerificationState();
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), lookup.Message);
                return;
            }

            _referenceVerified = true;
            _verifiedServiceAccount = lookup.ServiceAccount;
            lblCustomerNameValue.Text = string.IsNullOrWhiteSpace(lookup.ServiceAccount.CustomerName)
                ? lookup.ServiceAccount.ReferenceNumber
                : lookup.ServiceAccount.CustomerName;
            lblReferenceStatusValue.Text = LanguageManager.GetString("Verified");
            btnPay.Text = LanguageManager.GetString("Continue");
            btnPay.Values.Text = btnPay.Text;

            ShowStatus(
                StatusBannerKind.Success,
                LanguageManager.GetString("Success"),
                LanguageManager.Format(
                    "ServiceReferenceVerifiedSummary",
                    lookup.Service.ServiceName,
                    lblCustomerNameValue.Text,
                    lookup.ServiceAccount.ReferenceNumber));
        }

        private void PreparePaymentReview()
        {
            if (!_referenceVerified || _verifiedServiceAccount == null)
            {
                ShowStatus(
                    StatusBannerKind.Warning,
                    LanguageManager.GetString("Validation"),
                    LanguageManager.GetString("VerifyServiceReferenceBeforeContinuing"));
                return;
            }

            decimal amount = 0m;
            if (_chargeCurrentAccount && !TryParseAmount(txtAmount.Text, out amount))
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidPaymentAmount"));
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReference.Text))
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidServicePaymentInput"));
                txtReference.Focus();
                return;
            }

            string reference = _verifiedServiceAccount.ReferenceNumber;
            CashNoteDto[] notes = null;
            if (!_chargeCurrentAccount)
            {
                var uzsCurrency = _bankingService.GetActiveCurrencies()
                    .Where(currency => currency.Code == "UZS")
                    .ToArray();
                if (!uzsCurrency.Any())
                {
                    ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("SelectedCurrencyUnavailable"));
                    return;
                }

                using (var notesDialog = new CashNoteInputDialog(
                    LanguageManager.GetString("GuestServicePayment"),
                    LanguageManager.GetString("GuestCashPaymentSubtitle"),
                    uzsCurrency,
                    code => _bankingService.GetCashDenominations(code)))
                {
                    if (notesDialog.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }

                    notes = notesDialog.Notes;
                    amount = notesDialog.TotalAmount;
                    txtAmount.Text = amount.ToString("N2");
                }
            }

            _pendingReference = reference;
            _pendingAmount = amount;
            _pendingNotes = notes;
            _isReviewing = true;

            txtReference.Enabled = false;
            txtAmount.Enabled = false;
            btnPay.Text = LanguageManager.GetString("Confirm");
            btnPay.Values.Text = btnPay.Text;
            btnCancel.Text = _chargeCurrentAccount
                ? LanguageManager.GetString("Back")
                : LanguageManager.GetString("ReturnCash");
            btnCancel.Values.Text = btnCancel.Text;

            ShowStatus(StatusBannerKind.Info, LanguageManager.GetString("Confirm"), BuildReviewMessage());
        }

        private async System.Threading.Tasks.Task ExecuteReviewedPaymentAsync()
        {
            SetLoading(true);
            ServiceResult result = _chargeCurrentAccount
                ? await _bankingService.PayServiceAsync(_service.Id, _pendingReference, _pendingAmount, true)
                : await _bankingService.PayServiceWithCashNotesAsync(_service.Id, _pendingReference, _pendingNotes);

            SetLoading(false);

            if (!result.Success)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("PaymentError"), result.Message);
                return;
            }

            _completedFee = result.FeeAmountUzs;
            _completedFeePercent = result.FeePercent;
            _completedTotalDebited = result.TotalDebitedUzs;
            _completedNetAmount = result.NetAmountUzs;
            _completedCashback = result.CashbackAmountUzs;

            string receiptMessage = OfferReceiptForCompletedPayment();
            string successMessage = string.IsNullOrWhiteSpace(receiptMessage)
                ? result.Message
                : result.Message + Environment.NewLine + receiptMessage;
            _completed = true;
            ShowStatus(StatusBannerKind.Success, LanguageManager.GetString("Payment"), successMessage);
            btnPay.Enabled = false;
            btnCancel.Text = LanguageManager.GetString("Close");
            btnCancel.Values.Text = btnCancel.Text;
            txtReference.Enabled = false;
            txtAmount.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_isReviewing && !_completed)
            {
                if (!_chargeCurrentAccount)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                ClearReview();
                return;
            }

            DialogResult = _completed ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        private void ResetVerificationState()
        {
            if (_isReviewing || _completed)
            {
                return;
            }

            _referenceVerified = false;
            _verifiedServiceAccount = null;
            lblCustomerNameValue.Text = "-";
            lblReferenceStatusValue.Text = LanguageManager.GetString("WaitingForVerification");
            btnPay.Text = LanguageManager.GetString("Pay");
            btnPay.Values.Text = btnPay.Text;
        }

        private void SetLoading(bool isLoading)
        {
            UseWaitCursor = isLoading;
            btnPay.Enabled = !isLoading && !_completed;
            btnCancel.Enabled = !isLoading;
            txtReference.Enabled = !isLoading && !_completed && !_isReviewing;
            txtAmount.Enabled = !isLoading && !_completed && !_isReviewing;
        }

        private void ShowStatus(StatusBannerKind kind, string title, string message)
        {
            statusBanner.ShowMessage(kind, title, message);
        }

        private void ClearReview()
        {
            _isReviewing = false;
            _verifiedServiceAccount = null;
            _pendingReference = null;
            _pendingAmount = 0m;
            _pendingNotes = null;
            btnCancel.Text = LanguageManager.GetString("Cancel");
            btnCancel.Values.Text = btnCancel.Text;
            txtReference.Enabled = true;
            txtAmount.Enabled = true;
            statusBanner.Clear();
            ResetVerificationState();
        }

        private string BuildReviewMessage()
        {
            string amountLine = LanguageManager.Format("ServiceReceiptAmount", _pendingAmount, "UZS");
            string message =
                LanguageManager.Format("ServiceReceiptService", _service.ServiceName) + Environment.NewLine +
                LanguageManager.Format("ServiceReceiptReference", _pendingReference) + Environment.NewLine +
                LanguageManager.Format("ServiceReceiptCustomer", lblCustomerNameValue.Text) + Environment.NewLine +
                amountLine;

            var fee = _bankingService.PreviewServicePaymentFee(_pendingAmount, _chargeCurrentAccount);
            decimal netAmount = _chargeCurrentAccount
                ? _pendingAmount
                : Math.Max(0m, _pendingAmount - fee.FeeAmountUzs);
            message += Environment.NewLine +
                $"Fee: {fee.FeeAmountUzs:N2} UZS" + Environment.NewLine +
                $"Fee percent: {fee.PercentFee:N4}%" + Environment.NewLine +
                (_chargeCurrentAccount
                    ? $"Total debited: {fee.TotalDebitUzs:N2} UZS{Environment.NewLine}Cashback: {CalculateCashback(_pendingAmount, _service.CashbackPercent):N2} UZS"
                    : $"Net service amount: {netAmount:N2} UZS");

            if (_pendingNotes != null && _pendingNotes.Length > 0)
            {
                message += Environment.NewLine + LanguageManager.GetString("CashAcceptedBreakdown") + ": " +
                    string.Join("; ", _pendingNotes.Select(note => $"{note.DenominationValue:N0} {note.CurrencyCode} x {note.NoteCount}"));
            }

            return message;
        }

        private string OfferReceiptForCompletedPayment()
        {
            if (_chargeCurrentAccount)
            {
                return ReceiptWorkflow.OfferPdfReceipt(
                    this,
                    () =>
                    {
                        var transaction = new TransactionDto
                        {
                            Type = "BillPayment",
                            Amount = _completedTotalDebited > 0m ? _completedTotalDebited : _pendingAmount,
                            TransactionDate = DateTime.Now,
                            Description = BuildServicePaymentDescription() + $"; Fee: {_completedFeePercent:N4}% ({_completedFee:N2} UZS); Net amount: {_completedNetAmount:N2} UZS; Cashback: {_completedCashback:N2} UZS"
                        };

                        return _bankingService.GenerateReceiptForCurrentSession(transaction);
                    });
            }

            return ReceiptWorkflow.OfferPdfReceipt(
                this,
                () => ReceiptService.GenerateGuestServicePaymentReceipt(
                    _service,
                    _verifiedServiceAccount,
                    _pendingReference,
                    _pendingAmount,
                    _pendingNotes));
        }

        private string BuildServicePaymentDescription()
        {
            string customer = _verifiedServiceAccount == null
                ? string.Empty
                : (string.IsNullOrWhiteSpace(_verifiedServiceAccount.CustomerName)
                    ? _verifiedServiceAccount.ReferenceNumber
                    : _verifiedServiceAccount.CustomerName);

            if (string.IsNullOrWhiteSpace(customer))
            {
                return $"{_service.ServiceName}: {_pendingReference}";
            }

            return $"{_service.ServiceName}: {_pendingReference} ({customer})";
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

        private static decimal CalculateCashback(decimal amount, decimal cashbackPercent)
        {
            if (amount <= 0m || cashbackPercent <= 0m)
            {
                return 0m;
            }

            return decimal.Round(amount * cashbackPercent / 100m, 2);
        }
    }
}
