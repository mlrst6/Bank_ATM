using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;

namespace Bank_ATM
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            SetupUI();
        }

        private void SetupUI()
        {
            LanguageManager.Apply(this);
            MainFormInfo.Text = LanguageManager.GetString("MainFormInfo");
            
            MainFormGuest.Text = LanguageManager.GetString("MainFormGuest");
            MainFormUser.Text = LanguageManager.GetString("MainFormUser");
            MainFormAdmin.Text = LanguageManager.GetString("MainFormAdmin");
            Back.Text = LanguageManager.GetString("Back");
            
            // Events are now handled in InitializeComponent or safely here
            // Removing manual bindings that were causing duplicate calls
        }

        private void MainFormGuest_Click(object sender, EventArgs e)
        {
            NavigateTo(new GuestActionsForm());
        }

        private void MainFormUser_Click(object sender, EventArgs e)
        {
            NavigateTo(new GuestForm("MainFormUser"));
        }

        private void MainFormAdmin_Click(object sender, EventArgs e)
        {
            NavigateTo(new Admin.AdminLoginForm());
        }

        private void Back_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }
    }
}
