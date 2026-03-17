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
            lblAdminTitle.Text = "ADMIN: " + SessionManager.CurrentUser.FullName;
            
            btnManageUsers.Text = LanguageManager.GetString("AdminUsers");
            btnManageCards.Text = "VIEW CARDS";
            btnAuditLogs.Text = LanguageManager.GetString("AdminTransactions");
            btnLogout.Text = LanguageManager.GetString("Logout");
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Displaying all users in the system (Not implemented in this view)...", "Admin Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnManageCards_Click(object sender, EventArgs e)
        {
            string cardNum = Microsoft.VisualBasic.Interaction.InputBox("Enter Card Number to Update PIN:", "Manage Cards", "");
            if (string.IsNullOrEmpty(cardNum)) return;

            string newPin = Microsoft.VisualBasic.Interaction.InputBox("Enter New 4-Digit PIN:", "Security Reset", "");
            if (newPin.Length == 4 && int.TryParse(newPin, out _))
            {
                string hashedPin = BCrypt.Net.BCrypt.HashPassword(newPin, 11);
                new CardRepository().UpdatePin(cardNum, hashedPin);
                MessageBox.Show("PIN successfully updated by Administrator.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAuditLogs_Click(object sender, EventArgs e)
        {
            var transactions = new TransactionRepository().GetAllTransactions();
            string log = "Recent Transactions:\n";
            int count = 0;
            foreach (var t in transactions)
            {
                log += $"[{t.TransactionDate}] {t.Type}: {t.Amount} UZS\n";
                if (++count > 10) break;
            }
            MessageBox.Show(log, "System Audit Logs", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            MainForm mainForm = new MainForm();
            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;
            mainForm.Show();
            this.Close();
        }
    }
}
