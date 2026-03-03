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
    public partial class LanguageForm1 : Form
    {
        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
        }
        public void RefreshLanguage()
        {
            LanguageManager.Apply(this);
        }
        string connStr = @"Server=.;Database=ATM;Trusted_Connection=True;";
        public LanguageForm1()
        {
            InitializeComponent();
        }

        private void SetLanguageAndOpenMainForm(string lang)
        {
            LanguageManager.CurrentLang = lang;
            LanguageManager.Load();

            foreach (Form f in Application.OpenForms)
            {
                if (f is MainForm)
                {
                    f.Close();
                    break;
                }
            }

            var main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LanguageManager.CurrentLang = "uz";
            LanguageManager.Load();

            LanguageForm1 languageForm = this;

            MainForm mainForm = new MainForm();

            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;

            mainForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LanguageManager.CurrentLang = "eng";
            LanguageManager.Load();

            LanguageForm1 languageForm = this;

            MainForm mainForm = new MainForm();

            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;

            mainForm.Show();
            this.Hide(); ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LanguageManager.CurrentLang = "ru";
            LanguageManager.Load();

            LanguageForm1 languageForm = this;

            MainForm mainForm = new MainForm();

            mainForm.StartPosition = FormStartPosition.Manual;
            mainForm.Location = this.Location;

            mainForm.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LanguageForm1_Load(object sender, EventArgs e)
        {

        }
    }
}
