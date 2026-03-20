using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Repositories;

namespace Bank_ATM
{
    public partial class GuestActionsForm : Form
    {
        public GuestActionsForm()
        {
            InitializeComponent();
        }

        private void GuestActionsForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString("MainFormGuest");
            btnExchange.Text = LanguageManager.GetString("btnExchange");
            btnPayServices.Text = LanguageManager.GetString("PayServices");
            btnBack.Text = LanguageManager.GetString("Back");
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            ConverterForm converter = new ConverterForm();
            converter.StartPosition = FormStartPosition.Manual;
            converter.Location = this.Location;
            converter.Show();
            this.Close();
        }

        private void btnPayServices_Click(object sender, EventArgs e)
        {
            string service = Microsoft.VisualBasic.Interaction.InputBox("Enter Service Name (e.g. Beeline, UzMobile):", LanguageManager.GetString("PayServices"), "");
            if (string.IsNullOrWhiteSpace(service)) return;

            string account = Microsoft.VisualBasic.Interaction.InputBox("Enter Phone Number or Account ID:", service, "");
            if (string.IsNullOrWhiteSpace(account)) return;

            string amountStr = Microsoft.VisualBasic.Interaction.InputBox("Enter Amount to Pay:", "Payment", "0");
            if (decimal.TryParse(amountStr, out decimal amount) && amount > 0)
            {
                // Source is NULL for guest cash payments
                new TransactionRepository().AddTransaction(null, "BillPayment", amount, null, $"{service}: {account}");
                AuditLogger.LogInfo($"Guest payment: {amount} UZS for {service} ({account})");
                MessageBox.Show(LanguageManager.GetString("Success"), "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!string.IsNullOrEmpty(amountStr))
            {
                MessageBox.Show("Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
