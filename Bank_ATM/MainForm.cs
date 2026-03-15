using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Repositories;
using Bank_ATM.Models;

namespace Bank_ATM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SessionManager.OnSessionChanged += SetupUI;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupUI();
        }

        private void SetupUI()
        {
            // Clear existing subscriptions to prevent double-execution
            MainFormGuest.Click -= MainFormGuest_Click;
            MainFormGuest.Click -= Withdraw_Click;
            MainFormGuest.Click -= AdminUsers_Click;

            MainFormUser.Click -= MainFormUser_Click;
            MainFormUser.Click -= Deposit_Click;
            MainFormUser.Click -= AdminCards_Click;

            MainFormAdmin.Click -= MainFormAdmin_Click;
            MainFormAdmin.Click -= Transfer_Click;
            MainFormAdmin.Click -= AdminTransactions_Click;

            Back.Click -= Back_Click;
            Back.Click -= Logout_Click;

            // 1. Authenticated Card-User Flow
            if (SessionManager.CurrentRole == UserRole.CardUser)
            {
                MainFormInfo.Text = $"{LanguageManager.GetString("label1")} - {SessionManager.CurrentUser.FullName}";
                MainFormAdmin.Visible = true;

                MainFormGuest.Text = LanguageManager.GetString("Withdraw");
                MainFormGuest.Click += Withdraw_Click;

                MainFormUser.Text = LanguageManager.GetString("Deposit");
                MainFormUser.Click += Deposit_Click;

                MainFormAdmin.Text = LanguageManager.GetString("Transfer");
                MainFormAdmin.Click += Transfer_Click;

                Back.Text = LanguageManager.GetString("Logout");
                Back.Click += Logout_Click;
            }
            // 2. Authenticated Admin Flow
            else if (SessionManager.CurrentRole == UserRole.Admin)
            {
                MainFormInfo.Text = "ADMIN: " + SessionManager.CurrentUser.FullName;
                MainFormAdmin.Visible = true;

                MainFormGuest.Text = LanguageManager.GetString("AdminUsers");
                MainFormGuest.Click += AdminUsers_Click;

                MainFormUser.Text = "VIEW CARDS";
                MainFormUser.Click += AdminCards_Click;

                MainFormAdmin.Text = LanguageManager.GetString("AdminTransactions");
                MainFormAdmin.Click += AdminTransactions_Click;

                Back.Text = LanguageManager.GetString("Logout");
                Back.Click += Logout_Click;
            }
            // 3. Selection / Landing Flow (Guest / Selection)
            else
            {
                LanguageManager.Apply(this);
                MainFormInfo.Text = LanguageManager.GetString("MainFormInfo");
                MainFormAdmin.Visible = true;

                MainFormGuest.Text = LanguageManager.GetString("MainFormGuest");
                MainFormGuest.Click += MainFormGuest_Click;

                MainFormUser.Text = LanguageManager.GetString("MainFormUser");
                MainFormUser.Click += MainFormUser_Click;

                MainFormAdmin.Text = LanguageManager.GetString("MainFormAdmin");
                MainFormAdmin.Click += MainFormAdmin_Click;

                Back.Text = LanguageManager.GetString("Back");
                Back.Click += Back_Click;
            }
        }

        #region Role Selection Handlers
        private void MainFormGuest_Click(object sender, EventArgs e)
        {
            // Direct transition to Guest features without login
            GuestActionsForm guestActions = new GuestActionsForm();
            guestActions.StartPosition = FormStartPosition.Manual;
            guestActions.Location = this.Location;
            guestActions.Show();
            this.Hide();
        }

        private void MainFormUser_Click(object sender, EventArgs e)
        {
            // Transition to Card Insertion
            GuestForm guestForm = new GuestForm();
            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;
            guestForm.Show();
            this.Hide();
        }

        private void MainFormAdmin_Click(object sender, EventArgs e)
        {
            // Transition to Admin Login (Uses the same Card Insertion screen)
            GuestForm guestForm = new GuestForm();
            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;
            guestForm.Show();
            this.Hide();
        }
        #endregion

        #region Card-User Features
        private void Withdraw_Click(object sender, EventArgs e)
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

        private void Deposit_Click(object sender, EventArgs e)
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

        private void Transfer_Click(object sender, EventArgs e)
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
        #endregion

        #region Admin Features
        private void AdminUsers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Displaying all users in the system...", "Admin Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AdminCards_Click(object sender, EventArgs e)
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

        private void AdminTransactions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Viewing master transaction logs...", "System Audit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void Logout_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            LanguageForm1 langForm = new LanguageForm1();
            langForm.Location = this.Location;
            langForm.Show();
            this.Close();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            LanguageForm1 langForm = new LanguageForm1();
            langForm.Location = this.Location;
            langForm.Show();
            this.Close();
        }
    }
}
