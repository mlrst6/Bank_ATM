using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bank_ATM
{
    public sealed class GuestExchangeForm : Form
    {
        private readonly GuestExchangeService _exchangeService;
        private CurrencyDto[] _currencies = new CurrencyDto[0];
        private CashNoteDto[] _insertedNotes = new CashNoteDto[0];
        private GuestExchangeResult _preview;

        private Label _titleLabel;
        private Label _subtitleLabel;
        private ComboBox _fromCurrencyComboBox;
        private ComboBox _toCurrencyComboBox;
        private Label _rateLabel;
        private Label _insertedLabel;
        private Label _receivedLabel;
        private Label _breakdownLabel;
        private Label _statusLabel;
        private Button _selectCashButton;
        private Button _previewButton;
        private Button _confirmButton;
        private Button _backButton;
        private bool IsInDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;

        public GuestExchangeForm()
        {
            InitializeComponent();
            if (IsInDesignMode)
                return;
            _exchangeService = new GuestExchangeService();
        }

        private void InitializeComponent()
        {
            Text = LanguageManager.GetString("Exchange");
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            ClientSize = new Size(900, 650);
            BackColor = Color.FromArgb(25, 25, 35);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(35, 35, 50)
            };

            _titleLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = LanguageManager.GetString("Exchange"),
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };
            header.Controls.Add(_titleLabel);

            _subtitleLabel = new Label
            {
                Location = new Point(90, 102),
                Size = new Size(720, 42),
                Text = LanguageManager.GetString("GuestExchangeSubtitle"),
                ForeColor = Color.FromArgb(170, 184, 204)
            };

            var fromLabel = CreateCaption(LanguageManager.GetString("ExchangeFromCurrency"), 90, 166);
            _fromCurrencyComboBox = CreateCurrencyComboBox(90, 196);
            _fromCurrencyComboBox.SelectedIndexChanged += CurrencySelectionChanged;

            var toLabel = CreateCaption(LanguageManager.GetString("ExchangeToCurrency"), 470, 166);
            _toCurrencyComboBox = CreateCurrencyComboBox(470, 196);
            _toCurrencyComboBox.SelectedIndexChanged += CurrencySelectionChanged;

            _rateLabel = CreateValueLabel(90, 252, 720, LanguageManager.GetString("ExchangeRateUnavailable"));
            _insertedLabel = CreateValueLabel(90, 292, 720, LanguageManager.GetString("NoCashInserted"));
            _receivedLabel = CreateValueLabel(90, 332, 720, "-");
            _breakdownLabel = CreateValueLabel(90, 372, 720, "-");
            _breakdownLabel.Size = new Size(720, 64);

            _statusLabel = new Label
            {
                Location = new Point(90, 446),
                Size = new Size(720, 34),
                ForeColor = Color.FromArgb(248, 113, 113)
            };

            _selectCashButton = CreateButton(LanguageManager.GetString("SelectCashNotes"), 90, 500, 210, Color.FromArgb(100, 88, 255));
            _selectCashButton.Click += SelectCashButton_Click;

            _previewButton = CreateButton(LanguageManager.GetString("PreviewExchange"), 320, 500, 210, Color.FromArgb(8, 145, 178));
            _previewButton.Click += PreviewButton_Click;

            _confirmButton = CreateButton(LanguageManager.GetString("ConfirmExchange"), 550, 500, 260, Color.FromArgb(22, 163, 74));
            _confirmButton.Enabled = false;
            _confirmButton.Click += ConfirmButton_Click;

            _backButton = CreateButton(LanguageManager.GetString("Back"), 320, 568, 260, Color.FromArgb(45, 45, 65));
            _backButton.ForeColor = Color.IndianRed;
            _backButton.Click += (s, e) => FormNavigator.GoBack(this, () => new GuestActionsForm());

            Controls.Add(header);
            Controls.Add(_subtitleLabel);
            Controls.Add(fromLabel);
            Controls.Add(_fromCurrencyComboBox);
            Controls.Add(toLabel);
            Controls.Add(_toCurrencyComboBox);
            Controls.Add(_rateLabel);
            Controls.Add(_insertedLabel);
            Controls.Add(_receivedLabel);
            Controls.Add(_breakdownLabel);
            Controls.Add(_statusLabel);
            Controls.Add(_selectCashButton);
            Controls.Add(_previewButton);
            Controls.Add(_confirmButton);
            Controls.Add(_backButton);

            Load += GuestExchangeForm_Load;
        }

        private void GuestExchangeForm_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode)
                return;

            AppWindow.ApplyMainScreen(this);
            _currencies = _exchangeService.GetActiveCurrencies();
            _fromCurrencyComboBox.DataSource = _currencies.ToArray();
            _fromCurrencyComboBox.DisplayMember = "Code";
            _fromCurrencyComboBox.ValueMember = "Code";

            _toCurrencyComboBox.DataSource = _currencies.ToArray();
            _toCurrencyComboBox.DisplayMember = "Code";
            _toCurrencyComboBox.ValueMember = "Code";

            SelectCurrency(_fromCurrencyComboBox, "UZS");
            SelectDifferentTargetCurrency();
            UpdateRateLabel();
        }

        private void CurrencySelectionChanged(object sender, EventArgs e)
        {
            _insertedNotes = new CashNoteDto[0];
            _preview = null;
            _confirmButton.Enabled = false;
            _statusLabel.Text = string.Empty;
            _insertedLabel.Text = LanguageManager.GetString("NoCashInserted");
            _receivedLabel.Text = "-";
            _breakdownLabel.Text = "-";
            UpdateRateLabel();
        }

        private void SelectCashButton_Click(object sender, EventArgs e)
        {
            var from = _fromCurrencyComboBox.SelectedItem as CurrencyDto;
            if (from == null)
            {
                _statusLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                return;
            }

            using (var dialog = new CashNoteInputDialog(
                LanguageManager.GetString("GuestExchangeCashTitle"),
                LanguageManager.GetString("GuestExchangeCashSubtitle"),
                new[] { from },
                code => _exchangeService.GetCashDenominations(code)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                _insertedNotes = dialog.Notes;
                _insertedLabel.Text = LanguageManager.Format("ExchangeInsertedAmount", dialog.TotalAmount, dialog.CurrencyCode);
                _preview = null;
                _confirmButton.Enabled = false;
                PreviewExchange();
            }
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            PreviewExchange();
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            var from = _fromCurrencyComboBox.SelectedItem as CurrencyDto;
            var to = _toCurrencyComboBox.SelectedItem as CurrencyDto;
            if (from == null || to == null)
            {
                _statusLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                return;
            }

            if (_preview == null || !_preview.Success)
            {
                PreviewExchange();
                if (_preview == null || !_preview.Success)
                {
                    return;
                }
            }

            if (MessageBox.Show(
                    _preview.Message,
                    LanguageManager.GetString("ConfirmExchange"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            SetLoading(true);
            var result = await _exchangeService.ExecuteExchangeAsync(from.Code, to.Code, _insertedNotes);
            SetLoading(false);

            if (!result.Success)
            {
                _statusLabel.ForeColor = Color.FromArgb(248, 113, 113);
                _statusLabel.Text = result.Message;
                _confirmButton.Enabled = false;
                return;
            }

            _statusLabel.ForeColor = Color.FromArgb(74, 222, 128);
            _statusLabel.Text = string.IsNullOrWhiteSpace(result.ReceiptPath)
                ? result.Message
                : LanguageManager.Format("ExchangeCompletedWithReceipt", result.ReceiptPath);
            _insertedNotes = new CashNoteDto[0];
            _preview = null;
            _confirmButton.Enabled = false;
            UpdateRateLabel();
        }

        private void PreviewExchange()
        {
            var from = _fromCurrencyComboBox.SelectedItem as CurrencyDto;
            var to = _toCurrencyComboBox.SelectedItem as CurrencyDto;
            if (from == null || to == null)
            {
                _statusLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                return;
            }

            _preview = _exchangeService.PreviewExchange(from.Code, to.Code, _insertedNotes);
            _confirmButton.Enabled = _preview.Success;
            _statusLabel.ForeColor = _preview.Success ? Color.FromArgb(74, 222, 128) : Color.FromArgb(248, 113, 113);
            _statusLabel.Text = _preview.Success ? LanguageManager.GetString("ExchangeReadyToConfirm") : _preview.Message;

            if (_preview.Success)
            {
                _receivedLabel.Text = LanguageManager.Format("ExchangeReceiveAmount", _preview.TargetAmount, _preview.ToCurrencyCode);
                _breakdownLabel.Text = LanguageManager.Format("ExchangeDispenseBreakdown", FormatNotes(_preview.DispensedNotes));
            }
            else
            {
                _receivedLabel.Text = "-";
                _breakdownLabel.Text = "-";
            }
        }

        private void UpdateRateLabel()
        {
            var from = _fromCurrencyComboBox.SelectedItem as CurrencyDto;
            var to = _toCurrencyComboBox.SelectedItem as CurrencyDto;
            if (from == null || to == null || to.RateToUzs <= 0m)
            {
                _rateLabel.Text = LanguageManager.GetString("ExchangeRateUnavailable");
                return;
            }

            decimal rate = decimal.Round(from.RateToUzs / to.RateToUzs, 6);
            _rateLabel.Text = LanguageManager.Format("ExchangeRatePair", 1m, from.Code, rate, to.Code);
        }

        private void SetLoading(bool loading)
        {
            UseWaitCursor = loading;
            _selectCashButton.Enabled = !loading;
            _previewButton.Enabled = !loading;
            _confirmButton.Enabled = !loading && _preview != null && _preview.Success;
            _backButton.Enabled = !loading;
            _fromCurrencyComboBox.Enabled = !loading;
            _toCurrencyComboBox.Enabled = !loading;
        }

        private void SelectDifferentTargetCurrency()
        {
            var from = _fromCurrencyComboBox.SelectedItem as CurrencyDto;
            if (from == null || _toCurrencyComboBox.Items.Count == 0)
            {
                return;
            }

            foreach (var item in _toCurrencyComboBox.Items)
            {
                var currency = item as CurrencyDto;
                if (currency != null && currency.Code != from.Code)
                {
                    _toCurrencyComboBox.SelectedItem = item;
                    return;
                }
            }
        }

        private static void SelectCurrency(ComboBox comboBox, string currencyCode)
        {
            foreach (var item in comboBox.Items)
            {
                var currency = item as CurrencyDto;
                if (currency != null && currency.Code == currencyCode)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private static Label CreateCaption(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(320, 24),
                ForeColor = Color.FromArgb(170, 184, 204)
            };
        }

        private static Label CreateValueLabel(int x, int y, int width, string text)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Regular)
            };
        }

        private static ComboBox CreateCurrencyComboBox(int x, int y)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(340, 34),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(45, 45, 65),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
        }

        private static Button CreateButton(string text, int x, int y, int width, Color backColor)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 48),
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", (notes ?? new CashNoteDto[0])
                .Where(note => note != null)
                .Select(note => $"{note.DenominationValue:N2} {note.CurrencyCode} x {note.NoteCount}"));
        }
    }
}
