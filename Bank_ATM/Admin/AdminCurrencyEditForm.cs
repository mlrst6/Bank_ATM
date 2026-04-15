using System;
using System.Globalization;
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

            txtCode.Text = _currency.Code;
            txtCurrencyName.Text = _currency.CurrencyName;
            txtRateToUzs.Text = _currency.RateToUzs.ToString("0.####", CultureInfo.InvariantCulture);
            txtCashAvailable.Text = _currency.CashAvailable.ToString("0.##", CultureInfo.InvariantCulture);
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

            if (code.Length != 3 ||
                string.IsNullOrWhiteSpace(txtCurrencyName.Text) ||
                !TryParseMoney(txtRateToUzs.Text, out rateToUzs) ||
                rateToUzs <= 0m ||
                !TryParseMoney(txtCashAvailable.Text, out cashAvailable) ||
                cashAvailable < 0m)
            {
                MessageBox.Show(LanguageManager.GetString("InvalidCurrencyInput"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currency.Code = code;
            _currency.CurrencyName = txtCurrencyName.Text.Trim();
            _currency.RateToUzs = code == "UZS" ? 1m : rateToUzs;
            _currency.CashAvailable = cashAvailable;
            _currency.IsActive = chkIsActive.Checked;

            try
            {
                _adminService.SaveCurrency(_currency);
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

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StyleTitle(lblTitle);
            AdminTheme.StyleLabel(lblCode, true);
            AdminTheme.StyleLabel(lblCurrencyName, true);
            AdminTheme.StyleLabel(lblRateToUzs, true);
            AdminTheme.StyleLabel(lblCashAvailable, true);
            AdminTheme.StyleTextBox(txtCode);
            AdminTheme.StyleTextBox(txtCurrencyName);
            AdminTheme.StyleTextBox(txtRateToUzs);
            AdminTheme.StyleTextBox(txtCashAvailable);
            AdminTheme.StyleCheckBox(chkIsActive);
            AdminTheme.StylePrimaryButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);
        }
    }
}
