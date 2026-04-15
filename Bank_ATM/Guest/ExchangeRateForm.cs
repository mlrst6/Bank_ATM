using System;
using System.Windows.Forms;
using Bank_ATM.Core;

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
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
        }

        private void btnUzsToUsd_Click(object sender, EventArgs e)
        {
            FormNavigator.ShowNext(this, new ConverterForm(0m, "UZS", "USD"));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormNavigator.GoBack(this, () => new GuestActionsForm());
        }

        private void label2_Click(object sender, EventArgs e) { }
    }
}
