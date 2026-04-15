using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminCurrencyEditForm : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private readonly CurrencyDto _currency;
        private readonly bool _isEdit;

        public AdminCurrencyEditForm()
        {
            InitializeComponent();
            _currency = new CurrencyDto { IsActive = true, RateToUzs = 1m };
        }

        public AdminCurrencyEditForm(CurrencyDto currency)
        {
            InitializeComponent();
            _currency = currency;
            _isEdit = true;
        }

        private void AdminCurrencyEditForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            lblTitle.Text = _isEdit ? "Edit Currency" : "Create Currency";
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            lblDenominations.Text = LanguageManager.GetString("Denominations");

            txtCode.Text = _currency.Code;
            txtCurrencyName.Text = _currency.CurrencyName;
            txtRateToUzs.Text = _currency.RateToUzs.ToString("0.####", CultureInfo.InvariantCulture);
            txtCashAvailable.Text = _currency.CashAvailable.ToString("0.##", CultureInfo.InvariantCulture);
            txtDenominations.Text = GetDenominationText();
            chkIsActive.Checked = _currency.IsActive;

            if (_currency.Code == "UZS")
            {
                txtCode.ReadOnly = true;
                txtRateToUzs.ReadOnly = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal rateToUzs;
            decimal cashAvailable;
            string code = (txtCode.Text ?? string.Empty).Trim().ToUpperInvariant();
            decimal[] denominations;

            if (code.Length != 3 ||
                string.IsNullOrWhiteSpace(txtCurrencyName.Text) ||
                !TryParseMoney(txtRateToUzs.Text, out rateToUzs) ||
                rateToUzs <= 0m ||
                !TryParseMoney(txtCashAvailable.Text, out cashAvailable) ||
                cashAvailable < 0m ||
                !TryParseDenominations(txtDenominations.Text, out denominations))
            {
                MessageBox.Show(LanguageManager.GetString("InvalidCurrencyInput") + Environment.NewLine + LanguageManager.GetString("DenominationsHint"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currency.Code = code;
            _currency.CurrencyName = txtCurrencyName.Text.Trim();
            _currency.RateToUzs = code == "UZS" ? 1m : rateToUzs;
            _currency.CashAvailable = cashAvailable;
            _currency.IsActive = chkIsActive.Checked;

            try
            {
                _adminService.SaveCurrency(_currency, denominations);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static bool TryParseMoney(string raw, out decimal value)
        {
            return decimal.TryParse(raw, NumberStyles.Number, CultureInfo.CurrentCulture, out value) ||
                   decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private string GetDenominationText()
        {
            if (!string.IsNullOrWhiteSpace(_currency.Code))
            {
                var denominations = _adminService.GetCashDenominations(_currency.Code)
                    .Select(note => note.DenominationValue.ToString("0.##", CultureInfo.InvariantCulture))
                    .ToArray();
                if (denominations.Length > 0)
                {
                    return string.Join(", ", denominations);
                }
            }

            return "1, 5, 10, 20, 50, 100";
        }

        private static bool TryParseDenominations(string raw, out decimal[] denominations)
        {
            denominations = new decimal[0];
            if (string.IsNullOrWhiteSpace(raw))
            {
                return false;
            }

            var values = raw
                .Split(new[] { ',', ';', '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part =>
                {
                    decimal value;
                    return TryParseMoney(part.Trim(), out value) ? decimal.Round(value, 2) : -1m;
                })
                .Where(value => value > 0m)
                .Distinct()
                .OrderBy(value => value)
                .ToArray();

            denominations = values;
            return values.Length > 0;
        }

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StyleTitle(lblTitle);
            AdminTheme.StyleLabel(lblCode, true);
            AdminTheme.StyleLabel(lblCurrencyName, true);
            AdminTheme.StyleLabel(lblRateToUzs, true);
            AdminTheme.StyleLabel(lblCashAvailable, true);
            AdminTheme.StyleLabel(lblDenominations, true);
            AdminTheme.StyleTextBox(txtCode);
            AdminTheme.StyleTextBox(txtCurrencyName);
            AdminTheme.StyleTextBox(txtRateToUzs);
            AdminTheme.StyleTextBox(txtCashAvailable);
            AdminTheme.StyleTextBox(txtDenominations);
            AdminTheme.StyleCheckBox(chkIsActive);
            AdminTheme.StylePrimaryButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);
        }
    }
}
