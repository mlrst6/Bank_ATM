using System;
using System.Drawing;
using System.Windows.Forms;
using Bank_ATM.Repositories;
using Bank_ATM.Core;
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
            await Task.Delay(1000); 

            var card = _cardRepo.GetCardByNumber(_cardNumber);
            bool isValid = _cardRepo.ValidatePin(_cardNumber, txtPin.Text);

            if (isValid && card != null)
            {
                AccountRepository accRepo = new AccountRepository();
                var account = accRepo.GetAccountById(card.AccountId);
                var user = accRepo.GetUserById(account.UserId);

                // This triggers SessionManager.OnSessionChanged
                SessionManager.Login(user, card, account);

                MessageBox.Show(LanguageManager.GetString("Success"), "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                MainForm mainForm = new MainForm();
                mainForm.StartPosition = FormStartPosition.Manual;
                mainForm.Location = this.Location;
                mainForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(LanguageManager.GetString("Error"), "Invalid PIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
