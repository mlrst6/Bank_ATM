using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;

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
            _card = card ?? new CardDto { ExpiryDate = DateTime.Now.AddYears(5), CardType = CardTypes.Uzcard };
            _card.CardType = CardTypes.Normalize(_card.CardType);
            _isEdit = card != null;
        }

        private async void AdminCardEditForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
            label1.Text = "User:";
            lblCardType.Text = LanguageManager.GetString("CardType");
            lblDialogTitle.Text = _isEdit
                ? LanguageManager.GetString("EditCard")
                : LanguageManager.GetString("CreateCard");
            lblDialogSubtitle.Text = _isEdit
                ? LanguageManager.GetString("EditCardSubtitle")
                : LanguageManager.GetString("CreateCardSubtitle");
            if (_isEdit)
            {
                txtCardNumber.Text = _card.CardNumber;
                chkBlocked.Checked = _card.IsBlocked;
                dtpExpiry.Value = _card.ExpiryDate;
                lblPinNote.Text = LanguageManager.GetString("LeaveBlankToKeepPin");
            }

            cmbCardType.DataSource = CardTypes.All.ToArray();
            cmbCardType.SelectedItem = CardTypes.Normalize(_card.CardType);
            cmbCardType.SelectedIndexChanged += cmbCardType_SelectedIndexChanged;

            if (!_isEdit)
            {
                txtCardNumber.Text = _adminService.GenerateCardNumber(cmbCardType.SelectedItem.ToString());
            }
            else
            {
                cmbCardType.Enabled = false;
            }

            txtCardNumber.ReadOnly = true;
            NumericInputDialog.Attach(txtPin, LanguageManager.GetString("PIN"));

            var users = (await _adminService.GetAllUsersAsync())
                .Where(user => user.IsActive && user.Role == "User" && user.PrimaryAccountId.HasValue)
                .OrderBy(user => user.FullName)
                .ToList();

            cmbUsers.DataSource = users;
            cmbUsers.DisplayMember = "AccountDisplayName";
            cmbUsers.ValueMember = "PrimaryAccountId";

            if (_isEdit)
            {
                cmbUsers.SelectedItem = users.FirstOrDefault(user => user.PrimaryAccountId == _card.AccountId);
                cmbUsers.Enabled = false;
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

            var selectedUser = cmbUsers.SelectedItem as UserDto;
            if (!_isEdit && (selectedUser == null || !selectedUser.PrimaryAccountId.HasValue))
            {
                MessageBox.Show(LanguageManager.GetString("SelectUserWithAccountRequired"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _card.CardNumber = sanitizedCardNumber;
            _card.CardType = CardTypes.Normalize(cmbCardType.SelectedItem?.ToString());
            _card.IsBlocked = chkBlocked.Checked;
            _card.ExpiryDate = dtpExpiry.Value.Date;
            _card.AccountId = _isEdit ? _card.AccountId : selectedUser.PrimaryAccountId.Value;

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

        private void cmbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isEdit && cmbCardType.SelectedItem != null)
            {
                txtCardNumber.Text = _adminService.GenerateCardNumber(cmbCardType.SelectedItem.ToString());
            }
        }

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
            AdminTheme.StyleLabel(lblCardType, true);
            AdminTheme.StyleLabel(lblPinNote, true);
            AdminTheme.StyleComboBox(cmbUsers);
            AdminTheme.StyleComboBox(cmbCardType);
            cmbUsers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCardType.DropDownStyle = ComboBoxStyle.DropDownList;
            AdminTheme.StyleTextBox(txtCardNumber);
            txtCardNumber.BackColor = System.Drawing.Color.FromArgb(40, 52, 70);
            AdminTheme.StyleTextBox(txtPin);
            AdminTheme.StyleCheckBox(chkBlocked);
            AdminTheme.StyleSuccessButton(btnSave);
            AdminTheme.StyleSecondaryButton(btnCancel);
        }
    }
}
