using System;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;

namespace Bank_ATM.Admin
{
    public partial class AdminUserEditForm : BaseForm
    {
        private UserDto _user;
        private bool _isEdit;
        private readonly AdminService _adminService = new AdminService();

        public AdminUserEditForm(UserDto user = null)
        {
            InitializeComponent();
            _user = user ?? new UserDto { Role = "User" };
            _isEdit = user != null;
        }

        private void AdminUserEditForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            NumericInputDialog.Attach(txtInitialPin, LanguageManager.GetString("InitialPin"));
            cmbInitialCardType.DataSource = CardTypes.All;
            cmbInitialCardType.SelectedItem = CardTypes.Uzcard;
            lblInitialCardType.Text = LanguageManager.GetString("CardType");
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            lblDialogTitle.Text = _isEdit
                ? LanguageManager.GetString("EditUser")
                : LanguageManager.GetString("CreateUser");
            lblDialogSubtitle.Text = _isEdit
                ? LanguageManager.GetString("EditUserSubtitle")
                : LanguageManager.GetString("CreateUserSubtitle");
            if (_isEdit)
            {
                txtFullName.Text = _user.FullName;
                txtUsername.Text = _user.Username;
                txtPhone.Text = _user.PhoneNumber;
                cmbRole.SelectedItem = _user.Role;
                lblPassNote.Text = LanguageManager.GetString("LeaveBlankToKeepPassword");
                txtInitialPin.Visible = false;
                lblInitialPin.Visible = false;
                cmbInitialCardType.Visible = false;
                lblInitialCardType.Visible = false;
                lblPinNote.Visible = false;
            }
            else
            {
                cmbRole.SelectedIndex = 0;
            }

            UpdateProvisioningHints();
            cmbRole.SelectedIndexChanged -= cmbRole_SelectedIndexChanged;
            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(LanguageManager.GetString("FullNameUsernameRequired"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isEdit && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(LanguageManager.GetString("PasswordRequiredForNewUser"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isEdit && cmbRole.SelectedItem != null && cmbRole.SelectedItem.ToString() == "User")
            {
                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show(LanguageManager.GetString("PhoneRequiredForCustomer"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtInitialPin.Text) || txtInitialPin.Text.Length != 4 || !int.TryParse(txtInitialPin.Text, out _))
                {
                    MessageBox.Show(LanguageManager.GetString("InitialPinRequired"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            _user.FullName = txtFullName.Text;
            _user.Username = txtUsername.Text;
            _user.PhoneNumber = txtPhone.Text;
            _user.Role = cmbRole.SelectedItem.ToString();

            try
            {
                string initialCardType = cmbInitialCardType.SelectedItem == null
                    ? CardTypes.Uzcard
                    : cmbInitialCardType.SelectedItem.ToString();
                await _adminService.SaveUserAsync(_user, txtPassword.Text, txtInitialPin.Text, _isEdit, initialCardType);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Format("SaveUserFailed", ex.Message), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProvisioningHints();
        }

        private void UpdateProvisioningHints()
        {
            bool isUser = !_isEdit && cmbRole.SelectedItem != null && cmbRole.SelectedItem.ToString() == "User";
            txtInitialPin.Visible = isUser;
            lblInitialPin.Visible = isUser;
            cmbInitialCardType.Visible = isUser;
            lblInitialCardType.Visible = isUser;
            lblPinNote.Visible = isUser;
            lblPinNote.Text = isUser ? LanguageManager.GetString("CreatesUserAccountCard") : string.Empty;
        }

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StylePanel(pnlHeader);
            AdminTheme.StyleTitle(lblDialogTitle);
            AdminTheme.StyleLabel(lblDialogSubtitle, true);
            AdminTheme.StyleLabel(label1, true);
            AdminTheme.StyleLabel(label2, true);
            AdminTheme.StyleLabel(label3, true);
            AdminTheme.StyleLabel(label4, true);
            AdminTheme.StyleLabel(label5, true);
            AdminTheme.StyleLabel(lblPassNote, true);
            AdminTheme.StyleLabel(lblInitialPin, true);
            AdminTheme.StyleLabel(lblInitialCardType, true);
            AdminTheme.StyleLabel(lblPinNote, true);
            AdminTheme.StyleTextBox(txtFullName);
            AdminTheme.StyleTextBox(txtUsername);
            AdminTheme.StyleTextBox(txtPassword);
            AdminTheme.StyleTextBox(txtPhone);
            AdminTheme.StyleTextBox(txtInitialPin);
            AdminTheme.StyleComboBox(cmbRole);
            AdminTheme.StyleComboBox(cmbInitialCardType);
            AdminTheme.StyleSuccessButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);
        }
    }
}
