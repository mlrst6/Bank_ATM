using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Repositories;

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
            var usd = new CurrencyRepository().GetCurrencyByCode("USD");
            ExchangeUzsPrice.Text = usd == null
                ? LanguageManager.GetString("SelectedCurrencyUnavailable")
                : LanguageManager.Format("ExchangeRateDisplay", usd.RateToUzs);
        }

        private void button1_Click(object sender, EventArgs e) => OpenConverterWithUsdAmount(1m);
        private void button2_Click(object sender, EventArgs e) => OpenConverterWithUsdAmount(5m);
        private void button3_Click(object sender, EventArgs e) => OpenConverterWithUsdAmount(10m);
        private void button5_Click(object sender, EventArgs e) => OpenConverterWithUsdAmount(50m);
        private void button6_Click(object sender, EventArgs e) => OpenConverterWithUsdAmount(100m);

        private void OtherAmount_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.GetString("EnterUsdAmount"),
                LanguageManager.GetString("Exchange"),
                "0");
            decimal usdAmount;
            if (!decimal.TryParse(input, out usdAmount) || usdAmount <= 0)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show(LanguageManager.GetString("InvalidAmount"), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            OpenConverterWithUsdAmount(usdAmount);
        }

        private void Back_Click(object sender, EventArgs e)
        {
            FormNavigator.GoBack(this, () => new GuestActionsForm());
        }

        private void converter_Click(object sender, EventArgs e)
        {
            FormNavigator.ShowNext(this, new ConverterForm(0m, "UZS", "USD"));
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void OpenConverterWithUsdAmount(decimal usdAmount)
        {
            FormNavigator.ShowNext(this, new ConverterForm(usdAmount, "USD", "UZS"));
        }
    }
}
