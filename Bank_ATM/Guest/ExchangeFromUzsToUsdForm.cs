using System;
using System.Windows.Forms;

namespace Bank_ATM
{
    public partial class ExchangeFromUzsToUsdForm : Form
    {
        public ExchangeFromUzsToUsdForm()
        {
            InitializeComponent();
        }

        private void ExchangeFromUzsToUsdForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
        }

        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void button5_Click(object sender, EventArgs e) { }
        private void button6_Click(object sender, EventArgs e) { }
        private void OtherAmount_Click(object sender, EventArgs e) { }

        private void Back_Click(object sender, EventArgs e)
        {
            ExchangeRateForm exchangeRateForm = new ExchangeRateForm();
            exchangeRateForm.StartPosition = FormStartPosition.Manual;
            exchangeRateForm.Location = this.Location;
            exchangeRateForm.Show();
            this.Close();
        }

        private void converter_Click(object sender, EventArgs e)
        {
            ConverterForm converterForm = new ConverterForm();
            converterForm.StartPosition = FormStartPosition.Manual;
            converterForm.Location = this.Location;
            converterForm.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}
