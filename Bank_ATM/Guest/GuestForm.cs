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
    public partial class GuestForm : Form
    {
        public GuestForm()
        {
            InitializeComponent();
            LanguageManager.Apply(this);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            GuestForm guestForm = this;

            MainForm mainForm = new MainForm();

            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;

            mainForm.Show();
            this.Hide();
        }
        public void RefreshLanguage()
        {
            LanguageManager.Apply(this);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ExchangeRate_Click(object sender, EventArgs e)
        {
            GuestForm guestForm = this;
            ExchangeRateForm exchangeRateForm = new ExchangeRateForm();

            exchangeRateForm.StartPosition = FormStartPosition.Manual;
            exchangeRateForm.Location = this.Location;
            exchangeRateForm.Show();
            this.Hide();
        }
    }
}
