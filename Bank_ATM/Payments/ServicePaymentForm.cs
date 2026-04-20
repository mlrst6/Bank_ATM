using System;
using System.Drawing;
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
        private StatusBanner _statusBanner;
        private bool _completed;
        private bool _isReviewing;
        private ServiceDto _pendingService;
        private string _pendingReference;
        private decimal _pendingAmount;
        private CashNoteDto[] _pendingNotes;

        public ServicePaymentForm(bool chargeCurrentAccount)
        {
            InitializeComponent();
            _chargeCurrentAccount = chargeCurrentAccount;
        }

        private void ServicePaymentForm_Load(object sender, EventArgs e)
        {
            EnsureStatusBanner();
            ApplyTheme();
            LanguageManager.Apply(this);
            lblTitle.Text = _chargeCurrentAccount
                ? LanguageManager.GetString("PayFromAccount")
                : LanguageManager.GetString("GuestServicePayment");
            lblSubtitle.Text = _chargeCurrentAccount
                ? LanguageManager.GetString("PayFromAccountSubtitle")
                : LanguageManager.GetString("GuestServicePaymentSubtitle");
            btnPay.Text = LanguageManager.GetString("Pay");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            NumericInputDialog.Attach(txtReference, LanguageManager.GetString("AccountNumber"));
            if (!_chargeCurrentAccount)
            {
                txtAmount.ReadOnly = true;
                txtAmount.Text = LanguageManager.GetString("CashAmountCalculatedFromNotes");
            }
            else
            {
                NumericInputDialog.Attach(txtAmount, LanguageManager.GetString("Amount"), true);
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

            cmbServices.SelectedIndexChanged += (s, args) => UpdateHint();
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

            PreparePaymentReview();
        }

        private void PreparePaymentReview()
        {
            if (cmbServices.SelectedItem == null)
            {
                ShowStatus(StatusBannerKind.Warning, LanguageManager.GetString("Validation"), LanguageManager.GetString("PleaseSelectService"));
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
            string reference = txtReference.Text.Trim();
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
            btnCancel.Text = LanguageManager.GetString("Back");

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

            string successMessage = string.IsNullOrWhiteSpace(result.ReceiptPath)
                ? result.Message
                : LanguageManager.Format("ServicePaymentCompletedWithReceipt", result.ReceiptPath);
            _completed = true;
            ShowStatus(StatusBannerKind.Success, LanguageManager.GetString("Payment"), successMessage);
            btnPay.Enabled = false;
            btnCancel.Text = LanguageManager.GetString("Close");
            cmbServices.Enabled = false;
            txtReference.Enabled = false;
            txtAmount.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_isReviewing && !_completed)
            {
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

        private void SetLoading(bool isLoading)
        {
            UseWaitCursor = isLoading;
            btnPay.Enabled = !isLoading && !_completed;
            btnCancel.Enabled = !isLoading;
            cmbServices.Enabled = !isLoading && !_completed && !_isReviewing;
            txtReference.Enabled = !isLoading && !_completed && !_isReviewing;
            txtAmount.Enabled = !isLoading && !_completed && !_isReviewing;
        }

        private void ApplyTheme()
        {
            BackColor = Color.FromArgb(14, 23, 38);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;

            pnlHeader.BackColor = Color.FromArgb(23, 37, 61);
            lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);

            txtReference.BackColor = Color.FromArgb(30, 41, 59);
            txtReference.ForeColor = Color.White;
            txtReference.BorderStyle = BorderStyle.FixedSingle;

            txtAmount.BackColor = Color.FromArgb(30, 41, 59);
            txtAmount.ForeColor = Color.White;
            txtAmount.BorderStyle = BorderStyle.FixedSingle;

            cmbServices.BackColor = Color.FromArgb(30, 41, 59);
            cmbServices.ForeColor = Color.White;
            cmbServices.FlatStyle = FlatStyle.Flat;

            btnPay.BackColor = Color.FromArgb(16, 185, 129);
            btnPay.ForeColor = Color.White;
            btnPay.FlatStyle = FlatStyle.Flat;
            btnPay.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = Color.FromArgb(51, 65, 85);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;

            lblService.ForeColor = Color.FromArgb(148, 163, 184);
            lblCategory.ForeColor = Color.FromArgb(148, 163, 184);
            lblCategoryValue.ForeColor = Color.White;
            lblReference.ForeColor = Color.FromArgb(148, 163, 184);
            lblAmount.ForeColor = Color.FromArgb(148, 163, 184);
            lblReferenceHint.ForeColor = Color.FromArgb(148, 163, 184);
        }

        private void ShowStatus(StatusBannerKind kind, string title, string message)
        {
            EnsureStatusBanner();
            _statusBanner.ShowMessage(kind, title, message);
        }

        private void EnsureStatusBanner()
        {
            if (_statusBanner != null)
            {
                return;
            }

            _statusBanner = new StatusBanner
            {
                Location = new Point(39, 438),
                Size = new Size(417, 82),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            Controls.Add(_statusBanner);
        }

        private void ClearReview()
        {
            _isReviewing = false;
            _pendingService = null;
            _pendingReference = null;
            _pendingAmount = 0m;
            _pendingNotes = null;
            btnPay.Text = LanguageManager.GetString("Pay");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            cmbServices.Enabled = true;
            txtReference.Enabled = true;
            txtAmount.Enabled = true;
            _statusBanner.Clear();
        }

        private string BuildReviewMessage()
        {
            string amountLine = LanguageManager.Format("ServiceReceiptAmount", _pendingAmount, "UZS");
            string message =
                LanguageManager.Format("ServiceReceiptService", _pendingService.ServiceName) + Environment.NewLine +
                LanguageManager.Format("ServiceReceiptReference", _pendingReference) + Environment.NewLine +
                amountLine;

            if (_pendingNotes != null && _pendingNotes.Length > 0)
            {
                message += Environment.NewLine + LanguageManager.GetString("CashAcceptedBreakdown") + ": " +
                    string.Join("; ", _pendingNotes.Select(note => $"{note.DenominationValue:N0} {note.CurrencyCode} x {note.NoteCount}"));
            }

            return message;
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
    }
}
