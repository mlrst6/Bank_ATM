using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;

namespace Bank_ATM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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
            
            // Re-bind clicks if needed
            MainFormGuest.Click += MainFormGuest_Click;
            MainFormUser.Click += MainFormUser_Click;
            MainFormAdmin.Click += MainFormAdmin_Click;
            Back.Click += Back_Click;
        }

        private void MainFormGuest_Click(object sender, EventArgs e)
        {
            GuestActionsForm guestActions = new GuestActionsForm();
            guestActions.StartPosition = FormStartPosition.Manual;
            guestActions.Location = this.Location;
            guestActions.Show();
            this.Hide();
        }

        private void MainFormUser_Click(object sender, EventArgs e)
        {
            GuestForm guestForm = new GuestForm();
            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;
            guestForm.Show();
            this.Hide();
        }

        private void MainFormAdmin_Click(object sender, EventArgs e)
        {
            GuestForm guestForm = new GuestForm();
            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;
            guestForm.Show();
            this.Hide();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            LanguageForm1 langForm = new LanguageForm1();
            langForm.StartPosition = FormStartPosition.Manual;
            langForm.Location = this.Location;
            langForm.Show();
            this.Close();
        }
    }
}
