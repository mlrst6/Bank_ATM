using System;
using System.Windows.Forms;

namespace Bank_ATM
{
    public partial class ConverterForm : Form
    {
        private const long UzsToUsdRate = 12000;

        public ConverterForm()
        {
            InitializeComponent();
        }

        private void ConverterForm_Load(object sender, EventArgs e)
        {
            LanguageManager.Apply(this);
            SetupNumericKeypad();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(new string[] { "USD", "UZS" });
            listBox1.SelectedIndex = 0;

            listBox2.Items.Clear();
            listBox2.Items.AddRange(new string[] { "UZS", "USD" });
            listBox2.SelectedIndex = 0;
        }

        private void SetupNumericKeypad()
        {
            button1.Click += NumberButton_Click;
            button2.Click += NumberButton_Click;
            button3.Click += NumberButton_Click;
            button4.Click += NumberButton_Click;
            button5.Click += NumberButton_Click;
            button6.Click += NumberButton_Click;
            button7.Click += NumberButton_Click;
            button8.Click += NumberButton_Click;
            button9.Click += NumberButton_Click;
            button11.Click += NumberButton_Click; 
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                textBox1.Text += btn.Text;
                PerformConversion();
            }
        }

        private void PerformConversion()
        {
            if (long.TryParse(textBox1.Text, out long input))
            {
                int fromIndex = listBox1.SelectedIndex;
                int toIndex = listBox2.SelectedIndex;
                long result = 0;

                if (fromIndex == 0 && toIndex == 0) // USD to UZS
                    result = input * UzsToUsdRate;
                else if (fromIndex == 1 && toIndex == 1) // UZS to USD
                    result = input / UzsToUsdRate;
                else
                    result = input;

                textBox2.Text = result.ToString("N0");
            }
            else
            {
                textBox2.Text = "0";
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            ExchangeFromUzsToUsdForm nextForm = new ExchangeFromUzsToUsdForm();
            nextForm.StartPosition = FormStartPosition.Manual;
            nextForm.Location = this.Location;
            nextForm.Show();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void button12_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
