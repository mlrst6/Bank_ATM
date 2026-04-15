using System;
using System.Drawing;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminLoginForm : BaseForm
    {
        private readonly AuthenticationService _authenticationService;

        public AdminLoginForm()
        {
            InitializeComponent();
            _authenticationService = new AuthenticationService();
        }

        private void AdminLoginForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString("MainFormAdmin");
            btnLogin.Text = LanguageManager.GetString("btnLogin");
            btnBack.Text = LanguageManager.GetString("btnBack");
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(LanguageManager.GetString("AdminCredentialsRequired"), LanguageManager.GetString("LoginError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetLoading(true);
            try
            {
                var result = await _authenticationService.LoginAdminAsync(username, password);
                if (result.Success)
                {
                    FormNavigator.ReplaceCurrent(this, new AdminActionsForm());
                    return;
                }

                MessageBox.Show(result.Message, LanguageManager.GetString("LoginFailed"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        private void SetLoading(bool isLoading)
        {
            this.UseWaitCursor = isLoading;
            btnLogin.Enabled = !isLoading;
            btnBack.Enabled = !isLoading;
        }

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StyleTitle(lblTitle);
            AdminTheme.StyleLabel(lblUser, true);
            AdminTheme.StyleLabel(lblPass, true);
            AdminTheme.StyleTextBox(txtUsername);
            AdminTheme.StyleTextBox(txtPassword);
            AdminTheme.StyleSuccessButton(btnLogin);
            AdminTheme.StyleSecondaryButton(btnBack);
        }
    }
}
