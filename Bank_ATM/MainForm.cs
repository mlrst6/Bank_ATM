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
    public partial class MainForm : Form
    {
        string connStr = @"Server=.;Database=ATM;Trusted_Connection=True;";
        public MainForm()
        {
            InitializeComponent();
            LanguageManager.Apply(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            MainForm mainForm = this;

            LanguageForm1 languageForm = new LanguageForm1();

            languageForm.StartPosition = FormStartPosition.Manual;
            languageForm.Location = this.Location;

            languageForm.Show();
            this.Hide();
        }

        private void MainFormGuest_Click(object sender, EventArgs e)
        {
            MainForm mainForm = this;

            GuestForm guestForm = new GuestForm();

            guestForm.StartPosition = FormStartPosition.Manual;
            guestForm.Location = this.Location;

            guestForm.Show();
            this.Hide();
        }

        private void MainFormInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
