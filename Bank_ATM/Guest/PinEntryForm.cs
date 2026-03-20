using System;
using System.Drawing;
using System.Windows.Forms;
using Bank_ATM.Repositories;
using Bank_ATM.Core;
using Bank_ATM.Models;
using System.Threading.Tasks;

namespace Bank_ATM
{
    public partial class PinEntryForm : Form
    {
        private readonly string _cardNumber;
        private readonly CardRepository _cardRepo;

        public PinEntryForm(string cardNumber)
        {
            InitializeComponent();
            _cardNumber = cardNumber;
            _cardRepo = new CardRepository();
        }

        private void PinEntryForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            SetupKeypad();
        }

        private void SetupKeypad()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button btn && int.TryParse(btn.Text, out _))
                {
                    btn.Click += (s, e) => {
                        if (txtPin.Text.Length < 4)
                            txtPin.Text += btn.Text;
                    };
                }
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPin.Text.Length != 4) return;

            lblStatus.Text = LanguageManager.GetString("Processing");
            btnLogin.Enabled = false;
            
            string failMessage = "";
            bool isValid = await Task.Run(() => {
                return _cardRepo.ValidatePin(_cardNumber, txtPin.Text, out failMessage);
            });

            if (isValid)
            {
                var card = _cardRepo.GetCardByNumber(_cardNumber);
                AccountRepository accRepo = new AccountRepository();
                var account = await accRepo.GetAccountByIdAsync(card.AccountId);
                var user = await accRepo.GetUserByIdAsync(account.UserId);

                SessionManager.Instance.Login(user, card, account);
                AuditLogger.LogInfo($"User {user.FullName} logged in with card {_cardNumber}");

                MessageBox.Show(LanguageManager.GetString("Success"), "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (SessionManager.Instance.CurrentRole == UserRole.Admin)
                {
                    Admin.AdminActionsForm adminForm = new Admin.AdminActionsForm();
                    adminForm.StartPosition = FormStartPosition.Manual;
                    adminForm.Location = this.Location;
                    adminForm.Show();
                }
                else
                {
                    User.UserActionsForm userForm = new User.UserActionsForm();
                    userForm.StartPosition = FormStartPosition.Manual;
                    userForm.Location = this.Location;
                    userForm.Show();
                }
                this.Close();
            }
            else
            {
                AuditLogger.LogWarning($"Failed login attempt for card {_cardNumber}: {failMessage}");
                MessageBox.Show(failMessage, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                txtPin.Clear();
                lblStatus.Text = LanguageManager.GetString("lblStatus");
                btnLogin.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPin.Clear();
        }
    }
}
