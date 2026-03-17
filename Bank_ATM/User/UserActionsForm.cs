using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Repositories;
using Bank_ATM.Models;

namespace Bank_ATM.User
{
    public partial class UserActionsForm : Form
    {
        public UserActionsForm()
        {
            InitializeComponent();
        }

        private void UserActionsForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            lblWelcome.Text = $"{LanguageManager.GetString("label1")} - {SessionManager.CurrentUser.FullName}";
            
            btnWithdraw.Text = LanguageManager.GetString("Withdraw");
            btnDeposit.Text = LanguageManager.GetString("Deposit");
            btnTransfer.Text = LanguageManager.GetString("Transfer");
            btnBalance.Text = LanguageManager.GetString("Balance");
            btnLogout.Text = LanguageManager.GetString("Logout");
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            string amountStr = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.GetString("EnterAmount"), LanguageManager.GetString("Withdraw"), "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                if (SessionManager.CurrentAccount.Balance >= amount)
                {
                    SessionManager.CurrentAccount.Balance -= amount;
                    new AccountRepository().UpdateBalance(SessionManager.CurrentAccount.Id, SessionManager.CurrentAccount.Balance);
                    new TransactionRepository().AddTransaction(SessionManager.CurrentAccount.Id, "Withdraw", amount);
                    MessageBox.Show(LanguageManager.GetString("Success"), "Withdrawal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(LanguageManager.GetString("InsufficientFunds"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            string amountStr = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.GetString("EnterAmount"), LanguageManager.GetString("Deposit"), "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                SessionManager.CurrentAccount.Balance += amount;
                new AccountRepository().UpdateBalance(SessionManager.CurrentAccount.Id, SessionManager.CurrentAccount.Balance);
                new TransactionRepository().AddTransaction(SessionManager.CurrentAccount.Id, "Deposit", amount);
                MessageBox.Show(LanguageManager.GetString("Success"), "Deposit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            string targetCardNum = Microsoft.VisualBasic.Interaction.InputBox("Enter Target Card Number:", LanguageManager.GetString("Transfer"), "");
            if (string.IsNullOrEmpty(targetCardNum)) return;

            string amountStr = Microsoft.VisualBasic.Interaction.InputBox("Enter Amount:", LanguageManager.GetString("Transfer"), "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                if (SessionManager.CurrentAccount.Balance < amount)
                {
                    MessageBox.Show(LanguageManager.GetString("InsufficientFunds"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cardRepo = new CardRepository();
                var targetCard = cardRepo.GetCardByNumber(targetCardNum);
                if (targetCard == null)
                {
                    MessageBox.Show("Target card not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Atomic transfer
                SessionManager.CurrentAccount.Balance -= amount;
                new AccountRepository().UpdateBalance(SessionManager.CurrentAccount.Id, SessionManager.CurrentAccount.Balance);
                new AccountRepository().UpdateBalance(targetCard.AccountId, amount, true);
                new TransactionRepository().AddTransaction(SessionManager.CurrentAccount.Id, "Transfer", amount, targetCard.AccountId);

                MessageBox.Show(LanguageManager.GetString("Success"), "Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Current Balance: {SessionManager.CurrentAccount.Balance:N2} UZS", "Balance Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
