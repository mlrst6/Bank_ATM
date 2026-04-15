using System;
using System.Windows.Forms;
using Bank_ATM.Core;

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
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
        }

        private void SetLanguageAndOpenMainForm(string lang)
        {
            LanguageManager.CurrentLang = lang;
            LanguageManager.Load();

            FormNavigator.ShowNext(this, new MainForm());
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
