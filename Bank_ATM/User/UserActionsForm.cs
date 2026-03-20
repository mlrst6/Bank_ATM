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
            lblWelcome.Text = $"{LanguageManager.GetString("label1")} - {SessionManager.Instance.CurrentUser.FullName}";
            
            btnWithdraw.Text = LanguageManager.GetString("Withdraw");
            btnDeposit.Text = LanguageManager.GetString("Deposit");
            btnTransfer.Text = LanguageManager.GetString("Transfer");
            btnBalance.Text = LanguageManager.GetString("Balance");
            btnLogout.Text = LanguageManager.GetString("Logout");
        }

        private async void btnWithdraw_Click(object sender, EventArgs e)
        {
            string amountStr = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.GetString("EnterAmount"), LanguageManager.GetString("Withdraw"), "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                SetLoading(true);
                bool success = await new AccountRepository().WithdrawAsync(SessionManager.Instance.CurrentAccount.Id, amount);
                SetLoading(false);

                if (success)
                {
                    SessionManager.Instance.CurrentAccount.Balance -= amount;
                    AuditLogger.LogTransaction("Withdraw", amount, SessionManager.Instance.CurrentAccount.AccountNumber);
                    
                    if (MessageBox.Show("Would you like a digital receipt?", "Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var transaction = new TransactionDto 
                        { 
                            Type = "Withdraw", 
                            Amount = amount, 
                            TransactionDate = DateTime.Now 
                        };
                        string path = ReceiptService.GenerateReceipt(transaction, SessionManager.Instance.CurrentUser.FullName, SessionManager.Instance.CurrentAccount.Balance);
                        if (path != null) MessageBox.Show($"Receipt saved to: {path}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetString("Success"), "Withdrawal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(LanguageManager.GetString("InsufficientFunds"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnDeposit_Click(object sender, EventArgs e)
        {
            string amountStr = Microsoft.VisualBasic.Interaction.InputBox(LanguageManager.GetString("EnterAmount"), LanguageManager.GetString("Deposit"), "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                SetLoading(true);
                bool success = await new AccountRepository().DepositAsync(SessionManager.Instance.CurrentAccount.Id, amount);
                SetLoading(false);

                if (success)
                {
                    SessionManager.Instance.CurrentAccount.Balance += amount;
                    MessageBox.Show(LanguageManager.GetString("Success"), "Deposit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            string targetCardNum = Microsoft.VisualBasic.Interaction.InputBox("Enter Target Card Number:", LanguageManager.GetString("Transfer"), "");
            if (string.IsNullOrEmpty(targetCardNum)) return;

            string amountStr = Microsoft.VisualBasic.Interaction.InputBox("Enter Amount:", LanguageManager.GetString("Transfer"), "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                SetLoading(true);
                var cardRepo = new CardRepository();
                var targetCard = cardRepo.GetCardByNumber(targetCardNum);
                
                if (targetCard == null)
                {
                    SetLoading(false);
                    MessageBox.Show("Target card not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool success = await new AccountRepository().TransferAsync(SessionManager.Instance.CurrentAccount.Id, targetCard.AccountId, amount);
                SetLoading(false);

                if (success)
                {
                    SessionManager.Instance.CurrentAccount.Balance -= amount;
                    AuditLogger.LogTransaction("Transfer", amount, $"{SessionManager.Instance.CurrentAccount.AccountNumber} -> {targetCard.CardNumber}");

                    if (MessageBox.Show("Would you like a digital receipt?", "Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var transaction = new TransactionDto 
                        { 
                            Type = "Transfer", 
                            Amount = amount, 
                            TransactionDate = DateTime.Now,
                            Description = $"To: {targetCard.CardNumber}"
                        };
                        string path = ReceiptService.GenerateReceipt(transaction, SessionManager.Instance.CurrentUser.FullName, SessionManager.Instance.CurrentAccount.Balance);
                        if (path != null) MessageBox.Show($"Receipt saved to: {path}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(LanguageManager.GetString("Success"), "Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(LanguageManager.GetString("InsufficientFunds"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Current Balance: {SessionManager.Instance.CurrentAccount.Balance:N2} UZS", "Balance Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void SetLoading(bool isLoading)
        {
            this.UseWaitCursor = isLoading;
            foreach (Control c in this.Controls) c.Enabled = !isLoading;
        }
    }
}
