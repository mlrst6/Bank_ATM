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
using System.Runtime.CompilerServices;

namespace Bank_ATM
{
    public partial class ConverterForm : Form
    {
        public ConverterForm()
        {
            InitializeComponent();
            LanguageManager.Apply(this);
        }


        private void button1_Click(object sender, EventArgs e)  
        {
            textBox1.Text += "1";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();

        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();

        }
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();

        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
            long input = long.Parse(textBox1.Text);
            int i1 = listBox1.SelectedIndex;
            int i2 = listBox2.SelectedIndex;
            long result = 0;
            if (i1 == 0 && i2 == 0)
                result = input * 12000;

            else if ((i1 == 1 && i2 == 1))
                result = input / 12000;
            else
                result = input;
            textBox2.Text = result.ToString();

        }
        private void Back_Click(object sender, EventArgs e)
        {
            ConverterForm converterForm = this;
            ExchangeFromUzsToUsdForm exchangeFromUzsToUsdForm = new ExchangeFromUzsToUsdForm();

            exchangeFromUzsToUsdForm.StartPosition = FormStartPosition.Manual;
            exchangeFromUzsToUsdForm.Location = this.Location;
            exchangeFromUzsToUsdForm.Show();
            this.Hide();
        }

        private void ConverterForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(new string[]
            {
                "USD",
                "UZS"
            });
            listBox1.SelectedIndex = 0;



            listBox2.Items.AddRange(new string[] {
                "UZS",
                "USD"
            });
            listBox2.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCurrency = listBox1.SelectedItem.ToString();
            Console.WriteLine(selectedCurrency);
            if (textBox1.TextLength > 0)
                textBox1.Clear();
                textBox2.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedcurrency2 = listBox2.SelectedItem.ToString();
            Console.WriteLine(selectedcurrency2);
            if (textBox1.TextLength > 0)
                textBox1.Clear(); 
                textBox2.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }
    }
}
