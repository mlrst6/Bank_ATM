using System;
using System.Drawing;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Services;

namespace Bank_ATM.User
{
    public partial class UserPinEntryForm : Form
    {
        private readonly string _cardNumber;
        private readonly AuthenticationService _authenticationService;

        public UserPinEntryForm(string cardNumber)
        {
            InitializeComponent();
            _cardNumber = cardNumber;
            _authenticationService = new AuthenticationService();
        }

        private void UserPinEntryForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            btnBack.Text = LanguageManager.GetString("Back");
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
            
            var result = await _authenticationService.LoginWithCardAsync(_cardNumber, txtPin.Text);
            if (result.Success)
            {
                MessageBox.Show(LanguageManager.GetString("Success"), LanguageManager.GetString("LoginSuccessful"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormNavigator.CloseHiddenForm(this.Tag as Form);
                FormNavigator.ReplaceCurrent(this, new UserActionsForm());
            }
            else
            {
                MessageBox.Show(result.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                txtPin.Clear();
                lblStatus.Text = LanguageManager.GetString("lblStatus");
                btnLogin.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPin.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormNavigator.GoBack(this, () => new UserCardEntryForm("MainFormUser"));
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {

        }
    }
}
