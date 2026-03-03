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
    public partial class ExchangeFromUzsToUsdForm : Form
    {
        public ExchangeFromUzsToUsdForm()
        {
            InitializeComponent();
            LanguageManager.Apply(this);
        }

        private void ExchangeFromUzsToUsdForm_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {

        }
        private void OtherAmount_Click(object sender, EventArgs e)
        {

        }
        private void Back_Click(object sender, EventArgs e)
        {
            ExchangeFromUzsToUsdForm exchangeFromUzsToUsdForm = this;
            ExchangeRateForm exchangeRateForm = new ExchangeRateForm();

            exchangeRateForm.StartPosition = FormStartPosition.Manual;
            exchangeRateForm.Location = this.Location;
            exchangeRateForm.Show();
            this.Hide();
        }
        private void converter_Click(object sender, EventArgs e)
        {
            ExchangeFromUzsToUsdForm exchangeFromUzsToUsdForm = this;
            ConverterForm converterForm = new ConverterForm();

            converterForm.StartPosition = FormStartPosition.Manual;
            converterForm.Location = this.Location;
            converterForm.Show();
            this.Hide();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
