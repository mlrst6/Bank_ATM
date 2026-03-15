using System;
using System.Windows.Forms;

namespace Bank_ATM
{
    public partial class ExchangeRateForm : Form
    {
        public ExchangeRateForm()
        {
            InitializeComponent();
        }

        private void ExchangeRateForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
        }

        private void btnUzsToUsd_Click(object sender, EventArgs e)
        {
            ExchangeFromUzsToUsdForm nextForm = new ExchangeFromUzsToUsdForm();
            nextForm.StartPosition = FormStartPosition.Manual;
            nextForm.Location = this.Location;
            nextForm.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GuestForm guestForm = new GuestForm();
            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;
            guestForm.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e) { }
    }
}
