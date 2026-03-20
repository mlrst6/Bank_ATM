using System;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Admin
{
    public partial class AdminCardEditForm : BaseForm
    {
        private CardDto _card;
        private bool _isEdit;
        private CardRepository _repo = new CardRepository();

        public AdminCardEditForm(CardDto card = null)
        {
            InitializeComponent();
            _card = card ?? new CardDto { ExpiryDate = DateTime.Now.AddYears(5) };
            _isEdit = card != null;
        }

        private void AdminCardEditForm_Load(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                txtAccountId.Text = _card.AccountId.ToString();
                txtCardNumber.Text = _card.CardNumber;
                chkBlocked.Checked = _card.IsBlocked;
                dtpExpiry.Value = _card.ExpiryDate;
                lblPinNote.Text = "(Leave blank to keep current PIN)";
                txtAccountId.ReadOnly = true; // Usually don't change linked account
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text) || txtCardNumber.Text.Length != 16)
            {
                MessageBox.Show("Valid 16-digit Card Number is required.");
                return;
            }

            if (!_isEdit && string.IsNullOrWhiteSpace(txtPin.Text))
            {
                MessageBox.Show("PIN is required for new cards.");
                return;
            }

            _card.CardNumber = txtCardNumber.Text;
            _card.IsBlocked = chkBlocked.Checked;
            _card.ExpiryDate = dtpExpiry.Value;
            
            if (int.TryParse(txtAccountId.Text, out int accId))
                _card.AccountId = accId;

            try
            {
                if (_isEdit)
                    _repo.UpdateCard(_card, txtPin.Text);
                else
                    _repo.CreateCard(_card, txtPin.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving card: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();
    }
}
