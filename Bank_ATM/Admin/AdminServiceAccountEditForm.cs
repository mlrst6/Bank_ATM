using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;

namespace Bank_ATM.Admin
{
    public partial class AdminServiceAccountEditForm : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private readonly ServiceDto _service;

        public AdminServiceAccountEditForm(ServiceDto service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void AdminServiceAccountEditForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            lblTitle.Text = "Add user service account";
            lblSubtitle.Text = _service.ServiceName;
            lblReference.Text = _service.AccountHint;
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            NumericInputDialog.Attach(txtReference, _service.AccountHint);

            var users = (await _adminService.GetAllUsersAsync())
                .Where(user => user.IsActive && user.Role == "User")
                .OrderBy(user => user.FullName)
                .ToList();

            cmbUsers.DataSource = users;
            cmbUsers.DisplayMember = "DisplayName";
            cmbUsers.ValueMember = "Id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var user = cmbUsers.SelectedItem as UserDto;
            string referenceNumber = (txtReference.Text ?? string.Empty).Trim();

            if (user == null || string.IsNullOrWhiteSpace(referenceNumber))
            {
                MessageBox.Show(LanguageManager.GetString("InvalidServicePaymentInput"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _adminService.CreateServiceAccount(new ServiceAccountDto
            {
                ServiceId = _service.Id,
                UserId = user.Id,
                ReferenceNumber = referenceNumber,
                CustomerName = user.FullName,
                IsActive = true
            });

            DialogResult = DialogResult.OK;
            Close();
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
            AdminTheme.StyleLabel(lblUser, true);
            AdminTheme.StyleLabel(lblReference, true);
            cmbUsers.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            cmbUsers.ForeColor = System.Drawing.Color.White;
            cmbUsers.FlatStyle = FlatStyle.Flat;
            cmbUsers.DropDownStyle = ComboBoxStyle.DropDownList;
            AdminTheme.StyleTextBox(txtReference);
            AdminTheme.StylePrimaryButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);
        }
    }
}
