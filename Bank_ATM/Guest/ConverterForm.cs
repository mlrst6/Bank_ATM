using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM
{
    public partial class ConverterForm : Form
    {
        private readonly decimal _initialAmount;
        private readonly string _initialFromCurrency;
        private readonly string _initialToCurrency;
        private CurrencyDto[] _currencies = new CurrencyDto[0];

        public ConverterForm()
            : this(0m, "USD", "UZS")
        {
        }

        public ConverterForm(decimal initialAmount, string fromCurrency, string toCurrency)
        {
            InitializeComponent();
            _initialAmount = initialAmount;
            _initialFromCurrency = fromCurrency;
            _initialToCurrency = toCurrency;
        }

        private void ConverterForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            textBox1.ReadOnly = true;
            textBox1.ShortcutsEnabled = false;
            SetupNumericKeypad();
            _currencies = new CurrencyRepository().GetActiveCurrencies().ToArray();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(_currencies.Select(c => c.Code).Cast<object>().ToArray());

            listBox2.Items.Clear();
            listBox2.Items.AddRange(_currencies.Select(c => c.Code).Cast<object>().ToArray());

            listBox1.SelectedItem = _initialFromCurrency;
            if (listBox1.SelectedIndex < 0) listBox1.SelectedIndex = 0;

            listBox2.SelectedItem = _initialToCurrency;
            if (listBox2.SelectedIndex < 0) listBox2.SelectedIndex = 0;

            if (_initialAmount > 0)
            {
                textBox1.Text = _initialAmount.ToString("0.##");
                PerformConversion();
            }
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
            if (decimal.TryParse(textBox1.Text, out decimal input))
            {
                string fromCurrency = listBox1.SelectedItem as string;
                string toCurrency = listBox2.SelectedItem as string;
                decimal result;

                var from = _currencies.FirstOrDefault(c => c.Code == fromCurrency);
                var to = _currencies.FirstOrDefault(c => c.Code == toCurrency);

                if (from == null || to == null)
                    result = 0m;
                else if (fromCurrency == toCurrency)
                    result = input;
                else
                    result = (input * from.RateToUzs) / to.RateToUzs;

                textBox2.Text = result.ToString("N2");
            }
            else
            {
                textBox2.Text = "0";
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            FormNavigator.GoBack(this, () => new GuestActionsForm());
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PerformConversion();
        }
    }
}
