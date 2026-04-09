using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Payments;

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
            FormNavigator.ShowNext(this, new ExchangeFromUzsToUsdForm());
        }

        private void btnPayServices_Click(object sender, EventArgs e)
        {
            using (var form = new ServicePaymentForm(false))
            {
                form.ShowDialog(this);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormNavigator.GoBack(this);
        }
    }
}
