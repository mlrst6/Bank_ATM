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
        private ServiceDto[] _services = new ServiceDto[0];
        private bool _completed;
        private bool _isReviewing;
        private bool _referenceVerified;
        private ServiceDto _pendingService;
        private ServiceAccountDto _verifiedServiceAccount;
        private string _pendingReference;
        private decimal _pendingAmount;
        private decimal _completedFeePercent;
        private decimal _completedFee;
        private decimal _completedTotalDebited;
        private decimal _completedNetAmount;
        private decimal _completedCashback;
        private CashNoteDto[] _pendingNotes;

        public ServicePaymentForm() : this(false)
        {
        }

        public ServicePaymentForm(bool chargeCurrentAccount)
        {
            InitializeComponent();
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

            _services = _bankingService.GetAvailableServices();
            cmbServices.DataSource = _services;
            cmbServices.DisplayMember = "ServiceName";
            cmbServices.ValueMember = "Id";

            if (_services.Any())
            {
                cmbServices.SelectedIndex = 0;
                UpdateHint();
            }

            cmbServices.SelectedIndexChanged += (s, args) =>
            {
                UpdateHint();
                ResetVerificationState();
            };
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
            if (cmbServices.SelectedItem == null)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("PleaseSelectService"));
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReference.Text))
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("InvalidServicePaymentInput"));
                txtReference.Focus();
                return;
            }

            var service = (ServiceDto)cmbServices.SelectedItem;
            var lookup = _bankingService.VerifyServiceAccount(service.Id, txtReference.Text);
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
            if (!_referenceVerified || _verifiedServiceAccount == null || cmbServices.SelectedItem == null)
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

            var service = (ServiceDto)cmbServices.SelectedItem;
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

            _pendingService = service;
            _pendingReference = reference;
            _pendingAmount = amount;
            _pendingNotes = notes;
            _isReviewing = true;

            cmbServices.Enabled = false;
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
                ? await _bankingService.PayServiceAsync(_pendingService.Id, _pendingReference, _pendingAmount, true)
                : await _bankingService.PayServiceWithCashNotesAsync(_pendingService.Id, _pendingReference, _pendingNotes);

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
            cmbServices.Enabled = false;
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

        private void UpdateHint()
        {
            var selected = cmbServices.SelectedItem as ServiceDto;
            lblCategoryValue.Text = selected == null ? "-" : selected.Category;
            lblReferenceHint.Text = selected == null
                ? string.Empty
                : LanguageManager.Format("RequiredFieldHint", selected.AccountHint);
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
            cmbServices.Enabled = !isLoading && !_completed && !_isReviewing;
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
            _pendingService = null;
            _verifiedServiceAccount = null;
            _pendingReference = null;
            _pendingAmount = 0m;
            _pendingNotes = null;
            btnCancel.Text = LanguageManager.GetString("Cancel");
            btnCancel.Values.Text = btnCancel.Text;
            cmbServices.Enabled = true;
            txtReference.Enabled = true;
            txtAmount.Enabled = true;
            statusBanner.Clear();
            ResetVerificationState();
        }

        private string BuildReviewMessage()
        {
            string amountLine = LanguageManager.Format("ServiceReceiptAmount", _pendingAmount, "UZS");
            string message =
                LanguageManager.Format("ServiceReceiptService", _pendingService.ServiceName) + Environment.NewLine +
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
                    ? $"Total debited: {fee.TotalDebitUzs:N2} UZS{Environment.NewLine}Cashback: {CalculateCashback(_pendingAmount, _pendingService.CashbackPercent):N2} UZS"
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
                    _pendingService,
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
                return $"{_pendingService.ServiceName}: {_pendingReference}";
            }

            return $"{_pendingService.ServiceName}: {_pendingReference} ({customer})";
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
