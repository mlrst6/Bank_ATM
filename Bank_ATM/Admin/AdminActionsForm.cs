using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Repositories;
using Bank_ATM.Models;

namespace Bank_ATM.Admin
{
    public partial class AdminActionsForm : Form
    {
        public AdminActionsForm()
        {
            InitializeComponent();
        }

        private void AdminActionsForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            lblAdminTitle.Text = "ADMIN: " + SessionManager.Instance.CurrentUser.FullName;
            
            btnManageUsers.Text = LanguageManager.GetString("AdminUsers");
            btnManageCards.Text = "VIEW CARDS";
            btnAuditLogs.Text = LanguageManager.GetString("AdminTransactions");
            btnLogout.Text = LanguageManager.GetString("Logout");
        }

        private async void btnManageUsers_Click(object sender, EventArgs e)
        {
            var users = await new AccountRepository().GetAllUsersAsync();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM USERS", users, "USERS");
            viewForm.ShowDialog();
        }

        private void btnManageCards_Click(object sender, EventArgs e)
        {
            var cards = new CardRepository().GetAllCards();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM CARDS", cards, "CARDS");
            viewForm.ShowDialog();
        }

        private void btnAuditLogs_Click(object sender, EventArgs e)
        {
            var transactions = new TransactionRepository().GetAllTransactions();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM TRANSACTIONS", transactions, "TRANSACTIONS");
            viewForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.Logout();
            MainForm mainForm = new MainForm();
            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;
            mainForm.Show();
            this.Close();
        }
    }
}
