using System;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Admin
{
    public partial class AdminUserEditForm : BaseForm
    {
        private UserDto _user;
        private bool _isEdit;
        private AccountRepository _repo = new AccountRepository();

        public AdminUserEditForm(UserDto user = null)
        {
            InitializeComponent();
            _user = user ?? new UserDto { Role = "User" };
            _isEdit = user != null;
        }

        private void AdminUserEditForm_Load(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                txtFullName.Text = _user.FullName;
                txtUsername.Text = _user.Username;
                txtPhone.Text = _user.PhoneNumber;
                cmbRole.SelectedItem = _user.Role;
                lblPassNote.Text = "(Leave blank to keep current password)";
            }
            else
            {
                cmbRole.SelectedIndex = 0;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Full Name and Username are required.");
                return;
            }

            if (!_isEdit && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required for new users.");
                return;
            }

            _user.FullName = txtFullName.Text;
            _user.Username = txtUsername.Text;
            _user.PhoneNumber = txtPhone.Text;
            _user.Role = cmbRole.SelectedItem.ToString();

            try
            {
                if (_isEdit)
                    await _repo.UpdateUserAsync(_user, txtPassword.Text);
                else
                    await _repo.CreateUserAsync(_user, txtPassword.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving user: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
