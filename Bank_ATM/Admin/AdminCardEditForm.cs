using System;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminCardEditForm : BaseForm
    {
        private CardDto _card;
        private bool _isEdit;
        private readonly AdminService _adminService = new AdminService();

        public AdminCardEditForm(CardDto card = null)
        {
            InitializeComponent();
            _card = card ?? new CardDto { ExpiryDate = DateTime.Now.AddYears(5) };
            _isEdit = card != null;
        }

        private void AdminCardEditForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            lblDialogTitle.Text = _isEdit
                ? LanguageManager.GetString("EditCard")
                : LanguageManager.GetString("CreateCard");
            lblDialogSubtitle.Text = _isEdit
                ? LanguageManager.GetString("EditCardSubtitle")
                : LanguageManager.GetString("CreateCardSubtitle");
            if (_isEdit)
            {
                txtAccountId.Text = _card.AccountId.ToString();
                txtCardNumber.Text = _card.CardNumber;
                chkBlocked.Checked = _card.IsBlocked;
                dtpExpiry.Value = _card.ExpiryDate;
                lblPinNote.Text = LanguageManager.GetString("LeaveBlankToKeepPin");
                txtAccountId.ReadOnly = true; // Usually don't change linked account
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sanitizedCardNumber = txtCardNumber.Text.Replace(" ", "").Replace("-", "");

            if (string.IsNullOrWhiteSpace(sanitizedCardNumber) || sanitizedCardNumber.Length != 16 || !long.TryParse(sanitizedCardNumber, out _))
            {
                MessageBox.Show(LanguageManager.GetString("ValidCardNumberRequired"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_isEdit && (string.IsNullOrWhiteSpace(txtPin.Text) || txtPin.Text.Length != 4 || !int.TryParse(txtPin.Text, out _)))
            {
                MessageBox.Show(LanguageManager.GetString("ValidPinRequiredForNewCard"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtPin.Text) && (txtPin.Text.Length != 4 || !int.TryParse(txtPin.Text, out _)))
            {
                MessageBox.Show(LanguageManager.GetString("PinMustBeFourDigits"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAccountId.Text, out int accId) || accId <= 0)
            {
                MessageBox.Show(LanguageManager.GetString("ValidAccountIdRequired"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _card.CardNumber = sanitizedCardNumber;
            _card.IsBlocked = chkBlocked.Checked;
            _card.ExpiryDate = dtpExpiry.Value.Date;
            _card.AccountId = accId;

            try
            {
                _adminService.SaveCard(_card, txtPin.Text, _isEdit);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Format("SaveCardFailed", ex.Message), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => this.Close();

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StylePanel(pnlHeader);
            AdminTheme.StyleTitle(lblDialogTitle);
            AdminTheme.StyleLabel(lblDialogSubtitle, true);
            AdminTheme.StyleLabel(label1, true);
            AdminTheme.StyleLabel(label2, true);
            AdminTheme.StyleLabel(label3, true);
            AdminTheme.StyleLabel(label4, true);
            AdminTheme.StyleLabel(lblPinNote, true);
            AdminTheme.StyleTextBox(txtAccountId);
            AdminTheme.StyleTextBox(txtCardNumber);
            AdminTheme.StyleTextBox(txtPin);
            AdminTheme.StyleCheckBox(chkBlocked);
            AdminTheme.StyleSuccessButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);
        }
    }
}
