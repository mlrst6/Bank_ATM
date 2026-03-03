using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bank_ATM
{
    public partial class ExchangeRateForm : Form
    {
        public ExchangeRateForm()
        {
            InitializeComponent();
            LanguageManager.Apply(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ExchangeRateForm exchangeRateForm = this;
            ExchangeFromUzsToUsdForm exchangeFromUzsToUsdForm = new ExchangeFromUzsToUsdForm();

            exchangeFromUzsToUsdForm.StartPosition = FormStartPosition.Manual;
            exchangeFromUzsToUsdForm.Location = this.Location;
            exchangeFromUzsToUsdForm.Show();
            this.Hide();
        }

        private void ExchangeRateForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ExchangeRateForm exchangeRateForm = this;
            GuestForm guestForm = new GuestForm();

            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;
            guestForm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
