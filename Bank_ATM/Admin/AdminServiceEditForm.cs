using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminServiceEditForm : Form
    {
        private static readonly string[] ServiceCategories = { "Mobile", "Internet", "Utilities" };
        private readonly AdminService _adminService = new AdminService();
        private readonly ServiceDto _service;
        private readonly bool _isEdit;

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
            ApplyTheme();
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            lblValidReferences.Text = LanguageManager.GetString("ValidPaymentReferences");
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(ServiceCategories);

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
                chkIsActive.Checked = _service.IsActive;
                SelectCategory(_service.Category);
                RefreshServiceAccounts();
            }
            else
            {
                chkIsActive.Checked = true;
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

            _service.ServiceName = txtServiceName.Text.Trim();
            _service.Category = cmbCategory.SelectedItem.ToString();
            _service.AccountHint = txtAccountHint.Text.Trim();
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

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StyleTitle(lblTitle);
            AdminTheme.StyleLabel(lblSubtitle, true);
            AdminTheme.StyleLabel(lblServiceName, true);
            AdminTheme.StyleLabel(lblCategory, true);
            AdminTheme.StyleLabel(lblAccountHint, true);
            AdminTheme.StyleLabel(lblHint, true);
            AdminTheme.StyleLabel(lblValidReferences, true);
            AdminTheme.StyleTextBox(txtServiceName);
            cmbCategory.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            cmbCategory.ForeColor = System.Drawing.Color.White;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            AdminTheme.StyleTextBox(txtAccountHint);
            AdminTheme.StyleGrid(dgvServiceAccounts);
            AdminTheme.StylePrimaryButton(btnAddServiceAccount);
            AdminTheme.StyleDangerButton(btnDeactivateServiceAccount);
            AdminTheme.StylePrimaryButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);

            chkIsActive.ForeColor = ForeColor;
            chkIsActive.BackColor = BackColor;
        }

        private void SelectCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
                return;
            }

            int index = cmbCategory.FindStringExact(category);
            if (index >= 0)
            {
                cmbCategory.SelectedIndex = index;
                return;
            }

            cmbCategory.Items.Add(category);
            cmbCategory.SelectedItem = category;
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
