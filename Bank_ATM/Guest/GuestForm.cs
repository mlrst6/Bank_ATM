using System;
using System.Windows.Forms;
using Bank_ATM.Core;

namespace Bank_ATM
{
    public partial class GuestForm : Form
    {
        public GuestForm()
        {
            InitializeComponent();
        }

        private void GuestForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            btnExchange.Visible = false; // Hide Guest-only features here, they are in GuestActionsForm
            btnInsertCard.Text = LanguageManager.GetString("btnInsertCard");
            btnBack.Text = LanguageManager.GetString("btnBack");
        }

        private void btnInsertCard_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text) || txtCardNumber.Text.Length < 16)
            {
                MessageBox.Show("Please enter a valid 16-digit card number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Repositories.CardRepository repo = new Repositories.CardRepository();
            var card = repo.GetCardByNumber(txtCardNumber.Text);

            if (card == null)
            {
                MessageBox.Show("Card not recognized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (card.IsBlocked)
            {
                MessageBox.Show("This card is blocked.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            PinEntryForm pinForm = new PinEntryForm(txtCardNumber.Text);
            pinForm.StartPosition = FormStartPosition.Manual;
            pinForm.Location = this.Location;
            pinForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;
            mainForm.Show();
            this.Close();
        }
    }
}
