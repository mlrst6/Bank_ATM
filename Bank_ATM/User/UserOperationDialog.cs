using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;

namespace Bank_ATM.User
{
    internal enum UserOperationType
    {
        Withdraw,
        Deposit,
        Transfer
    }

    internal sealed class UserOperationDialog : Form
    {
        private readonly UserOperationType _operationType;
        private readonly CurrencyDto[] _currencies;
        private bool _isConfirming;

        private Label _titleLabel;
        private Label _subtitleLabel;
        private Label _currencyLabel;
        private ComboBox _currencyComboBox;
        private Label _targetCardLabel;
        private TextBox _targetCardTextBox;
        private Label _amountLabel;
        private TextBox _amountTextBox;
        private Label _validationLabel;
        private Label _confirmationLabel;
        private Button _primaryButton;
        private Button _secondaryButton;
        private TableLayoutPanel _keypadPanel;
        private TextBox _activeInputTextBox;

        public decimal Amount { get; private set; }
        public string CurrencyCode { get; private set; }
        public string TargetCardNumber { get; private set; }

        public UserOperationDialog(UserOperationType operationType, CurrencyDto[] currencies)
        {
            _operationType = operationType;
            _currencies = currencies ?? new CurrencyDto[0];

            InitializeComponent();
            ApplyOperationText();
        }

        private void InitializeComponent()
        {
            Text = "ATM Operation";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(460, 620);
            BackColor = Color.FromArgb(12, 18, 32);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            _titleLabel = new Label
            {
                Location = new Point(28, 22),
                Size = new Size(404, 34),
                Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold),
                ForeColor = Color.White
            };

            _subtitleLabel = new Label
            {
                Location = new Point(30, 62),
                Size = new Size(400, 46),
                ForeColor = Color.FromArgb(170, 184, 204)
            };

            _currencyLabel = CreateLabel(LanguageManager.GetString("Currency"), 30, 122);
            _currencyComboBox = new ComboBox
            {
                Location = new Point(30, 148),
                Size = new Size(400, 34),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            _targetCardLabel = CreateLabel(LanguageManager.GetString("TargetCardNumber"), 30, 122);
            _targetCardTextBox = new TextBox
            {
                Location = new Point(30, 148),
                Size = new Size(400, 34),
                MaxLength = 16,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 13F, FontStyle.Regular),
                ReadOnly = true,
                ShortcutsEnabled = false
            };
            _targetCardTextBox.KeyPress += TargetCardTextBox_KeyPress;
            _targetCardTextBox.Enter += (s, e) => _activeInputTextBox = _targetCardTextBox;
            _targetCardTextBox.Click += (s, e) => _activeInputTextBox = _targetCardTextBox;

            _amountLabel = CreateLabel(LanguageManager.GetString("EnterAmount"), 30, 198);
            _amountTextBox = new TextBox
            {
                Location = new Point(30, 224),
                Size = new Size(400, 42),
                ReadOnly = true,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right
            };
            _amountTextBox.Enter += (s, e) => _activeInputTextBox = _amountTextBox;
            _amountTextBox.Click += (s, e) => _activeInputTextBox = _amountTextBox;

            _validationLabel = new Label
            {
                Location = new Point(30, 272),
                Size = new Size(400, 42),
                ForeColor = Color.FromArgb(248, 113, 113)
            };

            _confirmationLabel = new Label
            {
                Location = new Point(30, 122),
                Size = new Size(400, 192),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                Visible = false
            };

            _keypadPanel = new TableLayoutPanel
            {
                Location = new Point(30, 318),
                Size = new Size(400, 220),
                ColumnCount = 3,
                RowCount = 4
            };

            for (int i = 0; i < 3; i++)
            {
                _keypadPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }

            for (int i = 0; i < 4; i++)
            {
                _keypadPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            AddKeypadButton("1");
            AddKeypadButton("2");
            AddKeypadButton("3");
            AddKeypadButton("4");
            AddKeypadButton("5");
            AddKeypadButton("6");
            AddKeypadButton("7");
            AddKeypadButton("8");
            AddKeypadButton("9");
            AddKeypadButton(".");
            AddKeypadButton("0");
            AddKeypadButton("<");

            _secondaryButton = new Button
            {
                Location = new Point(30, 558),
                Size = new Size(190, 42),
                Text = LanguageManager.GetString("Cancel")
            };
            StyleButton(_secondaryButton, Color.FromArgb(71, 85, 105));
            _secondaryButton.Click += SecondaryButton_Click;

            _primaryButton = new Button
            {
                Location = new Point(240, 558),
                Size = new Size(190, 42),
                Text = LanguageManager.GetString("Confirm")
            };
            StyleButton(_primaryButton, Color.FromArgb(22, 163, 74));
            _primaryButton.Click += PrimaryButton_Click;

            Controls.Add(_titleLabel);
            Controls.Add(_subtitleLabel);
            Controls.Add(_currencyLabel);
            Controls.Add(_currencyComboBox);
            Controls.Add(_targetCardLabel);
            Controls.Add(_targetCardTextBox);
            Controls.Add(_amountLabel);
            Controls.Add(_amountTextBox);
            Controls.Add(_validationLabel);
            Controls.Add(_confirmationLabel);
            Controls.Add(_keypadPanel);
            Controls.Add(_secondaryButton);
            Controls.Add(_primaryButton);
            _activeInputTextBox = _operationType == UserOperationType.Transfer
                ? _targetCardTextBox
                : _amountTextBox;
        }

        private void ApplyOperationText()
        {
            bool isTransfer = _operationType == UserOperationType.Transfer;
            Text = GetOperationTitle();
            _titleLabel.Text = GetOperationTitle();
            _subtitleLabel.Text = isTransfer
                ? LanguageManager.GetString("TransferDialogSubtitle")
                : LanguageManager.GetString("CashDialogSubtitle");

            _currencyLabel.Visible = !isTransfer;
            _currencyComboBox.Visible = !isTransfer;
            _targetCardLabel.Visible = isTransfer;
            _targetCardTextBox.Visible = isTransfer;

            if (!isTransfer)
            {
                _currencyComboBox.DataSource = _currencies.OrderBy(c => c.Code).ToArray();
                _currencyComboBox.DisplayMember = "Code";
                _currencyComboBox.ValueMember = "Code";
                SelectCurrency("UZS");
            }

            _primaryButton.Text = LanguageManager.GetString("Continue");
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(400, 22),
                ForeColor = Color.FromArgb(170, 184, 204)
            };
        }

        private void AddKeypadButton(string text)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                BackColor = text == "<" ? Color.FromArgb(71, 85, 105) : Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += KeypadButton_Click;
            _keypadPanel.Controls.Add(button);
        }

        private void KeypadButton_Click(object sender, EventArgs e)
        {
            _validationLabel.Text = string.Empty;
            string key = ((Button)sender).Text;

            if (_operationType == UserOperationType.Transfer && _activeInputTextBox == _targetCardTextBox)
            {
                if (key == "<")
                {
                    if (_targetCardTextBox.Text.Length > 0)
                    {
                        _targetCardTextBox.Text = _targetCardTextBox.Text.Substring(0, _targetCardTextBox.Text.Length - 1);
                    }
                    return;
                }

                if (key != "." && _targetCardTextBox.Text.Length < 16)
                {
                    _targetCardTextBox.Text += key;
                }

                return;
            }

            if (key == "<")
            {
                if (_amountTextBox.Text.Length > 0)
                {
                    _amountTextBox.Text = _amountTextBox.Text.Substring(0, _amountTextBox.Text.Length - 1);
                }
                return;
            }

            if (key == "." && _amountTextBox.Text.Contains("."))
            {
                return;
            }

            if (key == "." && _amountTextBox.Text.Length == 0)
            {
                _amountTextBox.Text = "0";
            }

            _amountTextBox.Text += key;
        }

        private void PrimaryButton_Click(object sender, EventArgs e)
        {
            if (_isConfirming)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (!ValidateInput())
            {
                return;
            }

            ShowConfirmation();
        }

        private void SecondaryButton_Click(object sender, EventArgs e)
        {
            if (_isConfirming)
            {
                ShowInput();
                return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInput()
        {
            if (_operationType == UserOperationType.Transfer)
            {
                TargetCardNumber = (_targetCardTextBox.Text ?? string.Empty).Trim();
                if (TargetCardNumber.Length != 16 || !long.TryParse(TargetCardNumber, out _))
                {
                    _validationLabel.Text = LanguageManager.GetString("InvalidTargetCardNumber");
                    _targetCardTextBox.Focus();
                    return false;
                }
            }
            else
            {
                var selectedCurrency = _currencyComboBox.SelectedItem as CurrencyDto;
                if (selectedCurrency == null)
                {
                    _validationLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                    return false;
                }

                CurrencyCode = selectedCurrency.Code;
            }

            if (!TryParseAmount(_amountTextBox.Text, out decimal amount))
            {
                _validationLabel.Text = LanguageManager.GetString("InvalidPaymentAmount");
                return false;
            }

            Amount = amount;
            return true;
        }

        private void ShowConfirmation()
        {
            _isConfirming = true;
            _currencyLabel.Visible = false;
            _currencyComboBox.Visible = false;
            _targetCardLabel.Visible = false;
            _targetCardTextBox.Visible = false;
            _amountLabel.Visible = false;
            _amountTextBox.Visible = false;
            _validationLabel.Visible = false;
            _keypadPanel.Visible = false;
            _confirmationLabel.Visible = true;

            _confirmationLabel.Text = BuildConfirmationText();
            _primaryButton.Text = LanguageManager.GetString("Confirm");
            _secondaryButton.Text = LanguageManager.GetString("Back");
        }

        private void ShowInput()
        {
            _isConfirming = false;
            bool isTransfer = _operationType == UserOperationType.Transfer;
            _currencyLabel.Visible = !isTransfer;
            _currencyComboBox.Visible = !isTransfer;
            _targetCardLabel.Visible = isTransfer;
            _targetCardTextBox.Visible = isTransfer;
            _amountLabel.Visible = true;
            _amountTextBox.Visible = true;
            _validationLabel.Visible = true;
            _keypadPanel.Visible = true;
            _confirmationLabel.Visible = false;

            _primaryButton.Text = LanguageManager.GetString("Continue");
            _secondaryButton.Text = LanguageManager.GetString("Cancel");
        }

        private string BuildConfirmationText()
        {
            string amountLine = $"{LanguageManager.GetString("Amount")}: {Amount:N2} UZS";
            if (_operationType == UserOperationType.Transfer)
            {
                return $"{GetOperationTitle()}{Environment.NewLine}{Environment.NewLine}" +
                       $"{LanguageManager.GetString("TargetCardNumber")}: {TargetCardNumber}{Environment.NewLine}" +
                       amountLine;
            }

            return $"{GetOperationTitle()}{Environment.NewLine}{Environment.NewLine}" +
                   $"{LanguageManager.GetString("Currency")}: {CurrencyCode}{Environment.NewLine}" +
                   $"{LanguageManager.GetString("CashAmount")}: {Amount:N2} {CurrencyCode}";
        }

        private string GetOperationTitle()
        {
            switch (_operationType)
            {
                case UserOperationType.Withdraw:
                    return LanguageManager.GetString("Withdraw");
                case UserOperationType.Deposit:
                    return LanguageManager.GetString("Deposit");
                case UserOperationType.Transfer:
                    return LanguageManager.GetString("Transfer");
                default:
                    return "Operation";
            }
        }

        private void SelectCurrency(string currencyCode)
        {
            foreach (var item in _currencyComboBox.Items)
            {
                var currency = item as CurrencyDto;
                if (currency != null && currency.Code == currencyCode)
                {
                    _currencyComboBox.SelectedItem = item;
                    return;
                }
            }

            if (_currencyComboBox.Items.Count > 0)
            {
                _currencyComboBox.SelectedIndex = 0;
            }
        }

        private static bool TryParseAmount(string input, out decimal amount)
        {
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out amount) ||
                decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
            {
                return amount > 0m && decimal.Round(amount, 2) == amount;
            }

            amount = 0m;
            return false;
        }

        private void TargetCardTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private static void StyleButton(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}
