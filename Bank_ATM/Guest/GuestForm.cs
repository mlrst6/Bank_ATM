using System;
using System.Drawing;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Services;

namespace Bank_ATM
{
    public partial class GuestForm : BaseForm
    {
        private string _customTitleKey = "MainFormUser";
        private bool _isCardPlaceholderActive;
        private readonly AuthenticationService _authenticationService = new AuthenticationService();

        public GuestForm()
        {
            InitializeComponent();
        }

        public GuestForm(string titleKey) : this()
        {
            _customTitleKey = titleKey;
        }

        private void GuestForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString(_customTitleKey);
            
            btnExchange.Visible = false; // Hide Guest-only features here, they are in GuestActionsForm
            btnInsertCard.Text = LanguageManager.GetString("btnInsertCard");
            btnBack.Text = LanguageManager.GetString("btnBack");
            InitializeCardPlaceholder();
            txtCardNumber.Enter -= txtCardNumber_Enter;
            txtCardNumber.Enter += txtCardNumber_Enter;
            txtCardNumber.Leave -= txtCardNumber_Leave;
            txtCardNumber.Leave += txtCardNumber_Leave;
            txtCardNumber.KeyPress -= txtCardNumber_KeyPress;
            txtCardNumber.KeyPress += txtCardNumber_KeyPress;
        }

        private void btnInsertCard_Click(object sender, EventArgs e)
        {
            var access = _authenticationService.ValidateCardForLogin(txtCardNumber.Text);
            if (!access.Success)
            {
                MessageBoxIcon icon = access.Message == LanguageManager.GetString("InvalidCardNumberMessage")
                    ? MessageBoxIcon.Warning
                    : MessageBoxIcon.Error;
                MessageBox.Show(access.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, icon);
                return;
            }

            NavigateTo(new PinEntryForm(access.SanitizedCardNumber));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        private void InitializeCardPlaceholder()
        {
            txtCardNumber.Text = LanguageManager.GetString("txtCardNumber");
            txtCardNumber.ForeColor = Color.Silver;
            _isCardPlaceholderActive = true;
        }

        private void txtCardNumber_Enter(object sender, EventArgs e)
        {
            if (_isCardPlaceholderActive)
            {
                txtCardNumber.Clear();
                txtCardNumber.ForeColor = Color.White;
                _isCardPlaceholderActive = false;
            }
        }

        private void txtCardNumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                InitializeCardPlaceholder();
            }
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
