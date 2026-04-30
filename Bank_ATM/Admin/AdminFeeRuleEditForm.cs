using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;

namespace Bank_ATM.Admin
{
    public partial class AdminFeeRuleEditForm : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private readonly FeeRuleDto _rule;
        private readonly bool _isEdit;

        public AdminFeeRuleEditForm()
        {
            InitializeComponent();
            _rule = new FeeRuleDto { TransactionType = "Transfer", IsActive = true };
        }

        public AdminFeeRuleEditForm(FeeRuleDto rule)
        {
            InitializeComponent();
            _rule = rule;
            _isEdit = true;
        }

        private void AdminFeeRuleEditForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _isEdit ? "Edit Fee Rule" : "Create Fee Rule";
            btnSave.Text = LanguageManager.GetString("Save");
            btnSave.Values.Text = btnSave.Text;
            btnCancel.Text = LanguageManager.GetString("Cancel");
            btnCancel.Values.Text = btnCancel.Text;

            cmbCardType.Items.Add("All card types");
            cmbCardType.Items.AddRange(CardTypes.All.Cast<object>().ToArray());
            cmbTransactionType.Items.AddRange(new object[]
            {
                "Withdraw",
                "Deposit",
                "Transfer",
                "BillPayment",
                "Exchange"
            });

            NumericInputDialog.Attach(txtPercentFee, lblPercentFee.Text, true);
            NumericInputDialog.Attach(txtFixedFee, lblFixedFee.Text, true);
            NumericInputDialog.Attach(txtMinFee, lblMinFee.Text, true);
            NumericInputDialog.Attach(txtMaxFee, lblMaxFee.Text, true);

            cmbCardType.SelectedItem = string.IsNullOrWhiteSpace(_rule.CardType)
                ? "All card types"
                : CardTypes.Normalize(_rule.CardType);
            cmbTransactionType.SelectedItem = string.IsNullOrWhiteSpace(_rule.TransactionType)
                ? "Transfer"
                : _rule.TransactionType;
            txtPercentFee.Text = _rule.PercentFee.ToString("0.####", CultureInfo.InvariantCulture);
            txtFixedFee.Text = _rule.FixedFee.ToString("0.##", CultureInfo.InvariantCulture);
            txtMinFee.Text = _rule.MinFee.ToString("0.##", CultureInfo.InvariantCulture);
            txtMaxFee.Text = _rule.MaxFee.HasValue ? _rule.MaxFee.Value.ToString("0.##", CultureInfo.InvariantCulture) : string.Empty;
            chkIsActive.Checked = _rule.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal percentFee;
            decimal fixedFee;
            decimal minFee;
            decimal maxFee = 0m;

            bool hasMaxFee = !string.IsNullOrWhiteSpace(txtMaxFee.Text);
            if (cmbTransactionType.SelectedItem == null ||
                !TryParseMoney(txtPercentFee.Text, out percentFee) ||
                !TryParseMoney(txtFixedFee.Text, out fixedFee) ||
                !TryParseMoney(txtMinFee.Text, out minFee) ||
                (hasMaxFee && !TryParseMoney(txtMaxFee.Text, out maxFee)) ||
                percentFee < 0m ||
                fixedFee < 0m ||
                minFee < 0m ||
                (hasMaxFee && maxFee < minFee))
            {
                MessageBox.Show("Enter valid non-negative fee values. Max fee must be blank or greater than min fee.", LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedCardType = cmbCardType.SelectedItem as string;
            _rule.CardType = selectedCardType == "All card types" ? null : selectedCardType;
            _rule.TransactionType = cmbTransactionType.SelectedItem.ToString();
            _rule.PercentFee = percentFee;
            _rule.FixedFee = fixedFee;
            _rule.MinFee = minFee;
            _rule.MaxFee = hasMaxFee ? (decimal?)maxFee : null;
            _rule.IsActive = chkIsActive.Checked;

            try
            {
                _adminService.SaveFeeRule(_rule);
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
    }
}
