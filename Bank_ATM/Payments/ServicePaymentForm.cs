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

        public ServicePaymentForm(bool chargeCurrentAccount)
        {
            InitializeComponent();
            _chargeCurrentAccount = chargeCurrentAccount;
        }

        private void ServicePaymentForm_Load(object sender, EventArgs e)
        {
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
            if (!_chargeCurrentAccount)
            {
                txtAmount.ReadOnly = true;
                txtAmount.Text = LanguageManager.GetString("CashAmountCalculatedFromNotes");
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
            if (cmbServices.SelectedItem == null)
            {
                MessageBox.Show(LanguageManager.GetString("PleaseSelectService"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal amount = 0m;
            if (_chargeCurrentAccount && !TryParseAmount(txtAmount.Text, out amount))
            {
                MessageBox.Show(LanguageManager.GetString("InvalidPaymentAmount"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetLoading(true);
            var service = (ServiceDto)cmbServices.SelectedItem;
            ServiceResult result;
            if (_chargeCurrentAccount)
            {
                result = await _bankingService.PayServiceAsync(service.Id, txtReference.Text.Trim(), amount, true);
            }
            else
            {
                SetLoading(false);
                var uzsCurrency = _bankingService.GetActiveCurrencies()
                    .Where(currency => currency.Code == "UZS")
                    .ToArray();
                if (!uzsCurrency.Any())
                {
                    MessageBox.Show(LanguageManager.GetString("SelectedCurrencyUnavailable"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    SetLoading(true);
                    result = await _bankingService.PayServiceWithCashNotesAsync(service.Id, txtReference.Text.Trim(), notesDialog.Notes);
                }
            }
            SetLoading(false);

            if (!result.Success)
            {
                MessageBox.Show(result.Message, LanguageManager.GetString("PaymentError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(result.Message, LanguageManager.GetString("Payment"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
            btnPay.Enabled = !isLoading;
            btnCancel.Enabled = !isLoading;
            cmbServices.Enabled = !isLoading;
            txtReference.Enabled = !isLoading;
            txtAmount.Enabled = !isLoading;
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
