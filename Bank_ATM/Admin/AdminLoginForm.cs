using System;
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
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString("MainFormAdmin");
            btnLogin.Text = LanguageManager.GetString("btnLogin");
            btnLogin.Values.Text = btnLogin.Text;
            btnBack.Text = LanguageManager.GetString("btnBack");
            btnBack.Values.Text = btnBack.Text;
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
    }
}
