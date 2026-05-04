using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminServiceEditForm : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private readonly ServiceDto _service;
        private readonly bool _isEdit;
        private ServiceCategoryDto[] _categories = new ServiceCategoryDto[0];

        public AdminServiceEditForm()
        {
            InitializeComponent();
            _service = new ServiceDto { IsActive = true };
        }

        public AdminServiceEditForm(ServiceDto service)
        {
            InitializeComponent();
            _service = service;
            _isEdit = true;
        }

        private void AdminServiceEditForm_Load(object sender, EventArgs e)
        {
            btnSave.Text = LanguageManager.GetString("Save");
            btnSave.Values.Text = btnSave.Text;
            btnCancel.Text = LanguageManager.GetString("Cancel");
            btnCancel.Values.Text = btnCancel.Text;
            lblValidReferences.Text = LanguageManager.GetString("ValidPaymentReferences");
            btnAddServiceAccount.Values.Text = btnAddServiceAccount.Text;
            btnDeactivateServiceAccount.Values.Text = btnDeactivateServiceAccount.Text;
            lblCashbackPercent.Text = "Cashback percent";
            LoadCategories();

            lblTitle.Text = _isEdit
                ? LanguageManager.GetString("EditService")
                : LanguageManager.GetString("CreateService");
            lblSubtitle.Text = _isEdit
                ? LanguageManager.GetString("EditServiceSubtitle")
                : LanguageManager.GetString("CreateServiceSubtitle");

            if (_isEdit)
            {
                txtServiceName.Text = _service.ServiceName;
                txtAccountHint.Text = _service.AccountHint;
                txtCashbackPercent.Text = _service.CashbackPercent.ToString("0.####", CultureInfo.CurrentCulture);
                chkIsActive.Checked = _service.IsActive;
                SelectCategory(_service.Category);
                RefreshServiceAccounts();
            }
            else
            {
                chkIsActive.Checked = true;
                txtCashbackPercent.Text = "0";
                if (cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }
            }

            pnlServiceAccounts.Visible = _isEdit;
            btnAddServiceAccount.Visible = _isEdit;
            btnDeactivateServiceAccount.Visible = _isEdit;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text) ||
                cmbCategory.SelectedItem == null ||
                string.IsNullOrWhiteSpace(txtAccountHint.Text))
            {
                MessageBox.Show(LanguageManager.GetString("AllServiceFieldsRequired"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal cashbackPercent;
            if (!TryParsePercent(txtCashbackPercent.Text, out cashbackPercent))
            {
                MessageBox.Show("Cashback percent must be between 0 and 100.", LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCashbackPercent.Focus();
                return;
            }

            var selectedCategory = cmbCategory.SelectedItem as ServiceCategoryDto;
            _service.ServiceName = txtServiceName.Text.Trim();
            _service.Category = selectedCategory != null ? selectedCategory.Name : cmbCategory.SelectedItem.ToString();
            _service.CategoryId = selectedCategory?.Id;
            if (string.IsNullOrWhiteSpace(_service.IconEmoji) && selectedCategory != null)
            {
                _service.IconEmoji = selectedCategory.IconEmoji;
            }
            _service.AccountHint = txtAccountHint.Text.Trim();
            _service.CashbackPercent = cashbackPercent;
            _service.IsActive = chkIsActive.Checked;

            try
            {
                _adminService.SaveService(_service, _isEdit);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Format("SaveServiceFailed", ex.Message), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadCategories()
        {
            _categories = _adminService.GetActiveServiceCategories().ToArray();
            cmbCategory.DisplayMember = "DisplayName";
            cmbCategory.ValueMember = "Id";
            cmbCategory.DataSource = _categories;
            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void SelectCategory(string category)
        {
            if (_service.CategoryId.HasValue)
            {
                var byId = _categories.FirstOrDefault(c => c.Id == _service.CategoryId.Value);
                if (byId != null)
                {
                    cmbCategory.SelectedItem = byId;
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                var byName = _categories.FirstOrDefault(c => string.Equals(c.Name, category, StringComparison.OrdinalIgnoreCase));
                if (byName != null)
                {
                    cmbCategory.SelectedItem = byName;
                    return;
                }
            }

            if (cmbCategory.Items.Count > 0)
            {
                cmbCategory.SelectedIndex = 0;
            }
        }

        private static bool TryParsePercent(string rawValue, out decimal value)
        {
            if (decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.CurrentCulture, out value) ||
                decimal.TryParse(rawValue, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                value = decimal.Round(value, 4);
                return value >= 0m && value <= 100m;
            }

            value = 0m;
            return false;
        }

        private void RefreshServiceAccounts()
        {
            if (!_isEdit || _service.Id <= 0)
            {
                return;
            }

            var accounts = _adminService.GetServiceAccounts(_service.Id).ToList();
            dgvServiceAccounts.DataSource = accounts;
            dgvServiceAccounts.ReadOnly = true;
            dgvServiceAccounts.AllowUserToAddRows = false;
            dgvServiceAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServiceAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            HideColumn("Id");
            HideColumn("ServiceId");
            HideColumn("UserId");
        }

        private void HideColumn(string columnName)
        {
            if (dgvServiceAccounts.Columns.Contains(columnName))
            {
                dgvServiceAccounts.Columns[columnName].Visible = false;
            }
        }

        private void btnAddServiceAccount_Click(object sender, EventArgs e)
        {
            using (var form = new AdminServiceAccountEditForm(_service))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshServiceAccounts();
                }
            }
        }

        private void btnDeactivateServiceAccount_Click(object sender, EventArgs e)
        {
            if (dgvServiceAccounts.SelectedRows.Count == 0)
            {
                return;
            }

            var account = dgvServiceAccounts.SelectedRows[0].DataBoundItem as ServiceAccountDto;
            if (account == null)
            {
                return;
            }

            _adminService.DeactivateServiceAccount(account.Id);
            RefreshServiceAccounts();
        }
    }
}
