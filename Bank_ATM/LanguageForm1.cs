using System;
using System.Windows.Forms;

namespace Bank_ATM
{
    public partial class LanguageForm1 : Form
    {
        public LanguageForm1()
        {
            InitializeComponent();
        }

        private void LanguageForm1_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
        }

        private void SetLanguageAndOpenMainForm(string lang)
        {
            LanguageManager.CurrentLang = lang;
            LanguageManager.Load();

            MainForm mainForm = new MainForm();
            mainForm.Location = this.Location;
            mainForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetLanguageAndOpenMainForm("uz");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetLanguageAndOpenMainForm("eng");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetLanguageAndOpenMainForm("ru");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
