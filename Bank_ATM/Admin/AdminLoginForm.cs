using System;
using System.Drawing;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Repositories;
using Bank_ATM.Models;
using System.Threading.Tasks;

namespace Bank_ATM.Admin
{
    public partial class AdminLoginForm : BaseForm
    {
        private readonly AccountRepository _accRepo;

        public AdminLoginForm()
        {
            InitializeComponent();
            _accRepo = new AccountRepository();
        }

        private void AdminLoginForm_Load(object sender, EventArgs e)
        {
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
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetLoading(true);
            var admin = await _accRepo.GetAdminByUsernameAsync(username);

            if (admin != null && BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                // Admin login without a card
                SessionManager.Instance.Login(admin, null, null); 
                AuditLogger.LogInfo($"Admin {admin.FullName} logged in via credentials.");

                NavigateTo(new AdminActionsForm());
            }
            else
            {
                AuditLogger.LogWarning($"Failed Admin login attempt for username: {username}");
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
            }
            SetLoading(false);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigateBack(new MainForm());
        }

        private void SetLoading(bool isLoading)
        {
            this.UseWaitCursor = isLoading;
            btnLogin.Enabled = !isLoading;
            btnBack.Enabled = !isLoading;
        }
    }
}
