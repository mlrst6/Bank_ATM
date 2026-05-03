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
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString("MainFormGuest");
            btnExchange.Text = LanguageManager.GetString("btnExchange");
            btnExchange.Values.Text = btnExchange.Text;
            btnPayServices.Text = LanguageManager.GetString("PayServices");
            btnPayServices.Values.Text = btnPayServices.Text;
            btnCardTopUp.Text = LanguageManager.GetString("GuestCardTopUp");
            btnCardTopUp.Values.Text = btnCardTopUp.Text;
            btnBack.Text = LanguageManager.GetString("Back");
            btnBack.Values.Text = btnBack.Text;
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            FormNavigator.ShowNext(this, new GuestExchangeForm());
        }

        private void btnPayServices_Click(object sender, EventArgs e)
        {
            using (var form = new ServicePaymentForm(false))
            {
                form.ShowDialog(this);
            }
        }

        private void btnCardTopUp_Click(object sender, EventArgs e)
        {
            using (var form = new GuestCardTopUpForm())
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
