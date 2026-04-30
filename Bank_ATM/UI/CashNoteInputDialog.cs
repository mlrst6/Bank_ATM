using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Models;

namespace Bank_ATM.UI
{
    public sealed class CashNoteInputDialog : Form
    {
        private readonly CurrencyDto[] _currencies;
        private readonly Func<string, CashDenominationDto[]> _denominationProvider;
        private readonly bool _showAvailableNotes;
        private readonly CashNoteDto[] _initialNotes;
        private readonly List<NoteInputRow> _rows = new List<NoteInputRow>();

        private Label _titleLabel;
        private Label _subtitleLabel;
        private ComboBox _currencyComboBox;
        private Panel _notesPanel;
        private Label _totalLabel;
        private Label _validationLabel;
        private Button _confirmButton;
        private Button _cancelButton;

        public string CurrencyCode { get; private set; }
        public int CurrencyId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public CashNoteDto[] Notes { get; private set; }

        public CashNoteInputDialog(
            string title,
            string subtitle,
            CurrencyDto[] currencies,
            Func<string, CashDenominationDto[]> denominationProvider,
            bool showAvailableNotes = false,
            CashNoteDto[] initialNotes = null)
        {
            _currencies = currencies ?? new CurrencyDto[0];
            _denominationProvider = denominationProvider ?? (_ => new CashDenominationDto[0]);
            _showAvailableNotes = showAvailableNotes;
            _initialNotes = initialNotes ?? new CashNoteDto[0];
            InitializeComponent(title, subtitle);
        }

        private void InitializeComponent(string title, string subtitle)
        {
            Text = title;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(560, 620);
            BackColor = Color.FromArgb(12, 18, 32);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            _titleLabel = new Label
            {
                Text = title,
                Location = new Point(28, 22),
                Size = new Size(504, 34),
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = Color.White
            };

            _subtitleLabel = new Label
            {
                Text = subtitle,
                Location = new Point(30, 62),
                Size = new Size(500, 44),
                ForeColor = Color.FromArgb(170, 184, 204)
            };

            _currencyComboBox = new ComboBox
            {
                Location = new Point(30, 118),
                Size = new Size(500, 34),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _currencyComboBox.DisplayMember = "Code";
            _currencyComboBox.SelectedIndexChanged += (s, e) => LoadDenominations();

            if (_currencies.Length > 0)
            {
                _currencyComboBox.Items.AddRange(_currencies.Cast<object>().ToArray());
            }

            _notesPanel = new Panel
            {
                Location = new Point(30, 168),
                Size = new Size(500, 320),
                AutoScroll = true,
                BackColor = Color.FromArgb(17, 24, 39)
            };

            _totalLabel = new Label
            {
                Location = new Point(30, 500),
                Size = new Size(500, 28),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };

            _validationLabel = new Label
            {
                Location = new Point(30, 532),
                Size = new Size(500, 24),
                ForeColor = Color.FromArgb(248, 113, 113)
            };

            _cancelButton = new Button
            {
                Text = LanguageManager.GetString("Cancel"),
                Location = new Point(30, 565),
                Size = new Size(235, 42)
            };
            StyleButton(_cancelButton, Color.FromArgb(71, 85, 105));
            _cancelButton.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            _confirmButton = new Button
            {
                Text = LanguageManager.GetString("Confirm"),
                Location = new Point(295, 565),
                Size = new Size(235, 42)
            };
            StyleButton(_confirmButton, Color.FromArgb(22, 163, 74));
            _confirmButton.Click += ConfirmButton_Click;

            Controls.Add(_titleLabel);
            Controls.Add(_subtitleLabel);
            Controls.Add(_currencyComboBox);
            Controls.Add(_notesPanel);
            Controls.Add(_totalLabel);
            Controls.Add(_validationLabel);
            Controls.Add(_cancelButton);
            Controls.Add(_confirmButton);

            if (_currencyComboBox.Items.Count > 0)
            {
                int initialCurrencyIndex = GetInitialCurrencyIndex();
                _currencyComboBox.SelectedIndex = initialCurrencyIndex >= 0 ? initialCurrencyIndex : 0;
                LoadDenominations();
            }
            else
            {
                _validationLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                _confirmButton.Enabled = false;
            }
        }

        private void LoadDenominations()
        {
            _notesPanel.Controls.Clear();
            _rows.Clear();

            var currency = GetSelectedCurrency();
            if (currency == null)
            {
                _validationLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                _confirmButton.Enabled = false;
                UpdateTotal();
                return;
            }

            var denominations = _denominationProvider(currency.Code)
                .OrderByDescending(d => d.DenominationValue)
                .ToArray();

            if (denominations.Length == 0)
            {
                _validationLabel.Text = LanguageManager.GetString("NoCashDenominationsConfigured");
                _confirmButton.Enabled = false;
                UpdateTotal();
                return;
            }

            _validationLabel.Text = string.Empty;
            _confirmButton.Enabled = true;

            int y = 12;
            foreach (var denomination in denominations)
            {
                int initialCount = GetInitialCount(currency.Code, denomination.DenominationValue);
                var valueLabel = new Label
                {
                    Text = denomination.DenominationValue.ToString("N0", CultureInfo.CurrentCulture) + " " + currency.Code,
                    Location = new Point(16, y + 8),
                    Size = new Size(_showAvailableNotes ? 180 : 275, 24),
                    ForeColor = Color.White
                };

                var availableLabel = new Label
                {
                    Text = LanguageManager.Format("AvailableNotes", denomination.NoteCount),
                    Location = new Point(205, y + 8),
                    Size = new Size(110, 24),
                    ForeColor = Color.FromArgb(170, 184, 204)
                };

                 var countTextBox = new TextBox
                 {
                     Text = initialCount.ToString(CultureInfo.InvariantCulture),
                     Location = new Point(330, y + 4),
                     Size = new Size(72, 30),
                     MaxLength = 5,
                     BackColor = Color.FromArgb(30, 41, 59),
                     ForeColor = Color.White,
                     BorderStyle = BorderStyle.FixedSingle,
                     ReadOnly = true,
                     TextAlign = HorizontalAlignment.Right,
                     Tag = denomination
                 };
                 countTextBox.TextChanged += (s, e) => UpdateTotal();
                 countTextBox.KeyDown += CountTextBox_KeyDown;

                 var incrementButton = new Button
                 {
                     Text = "+",
                     Location = new Point(414, y + 3),
                     Size = new Size(46, 32),
                     Tag = countTextBox
                 };
                 StyleButton(incrementButton, Color.FromArgb(37, 99, 235));
                 incrementButton.Click += (s, e) => IncrementCount(countTextBox);

                 _notesPanel.Controls.Add(valueLabel);
                if (_showAvailableNotes)
                {
                    _notesPanel.Controls.Add(availableLabel);
                }
                _notesPanel.Controls.Add(countTextBox);
                _notesPanel.Controls.Add(incrementButton);
                _rows.Add(new NoteInputRow(denomination, countTextBox));
                y += 42;
            }

            UpdateTotal();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            _validationLabel.Text = string.Empty;
            var currency = GetSelectedCurrency();
            if (currency == null)
            {
                _validationLabel.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                return;
            }

            var notes = ReadNotes(currency).ToArray();
            decimal total = notes.Sum(note => note.TotalValue);
            if (!notes.Any() || total <= 0m)
            {
                _validationLabel.Text = LanguageManager.GetString("InvalidCashNotes");
                return;
            }

            CurrencyCode = currency.Code;
            CurrencyId = currency.Id;
            TotalAmount = total;
            Notes = notes;
            DialogResult = DialogResult.OK;
            Close();
        }

        private IEnumerable<CashNoteDto> ReadNotes(CurrencyDto currency)
        {
            foreach (var row in _rows)
            {
                int count;
                if (!int.TryParse(row.CountTextBox.Text, out count) || count <= 0)
                {
                    continue;
                }

                yield return new CashNoteDto
                {
                    CurrencyId = currency.Id,
                    CurrencyCode = currency.Code,
                    DenominationValue = row.Denomination.DenominationValue,
                    NoteCount = count
                };
            }
        }

        private void UpdateTotal()
        {
            var currency = GetSelectedCurrency();
            decimal total = currency == null ? 0m : ReadNotes(currency).Sum(note => note.TotalValue);
            string code = currency == null ? string.Empty : currency.Code;
            _totalLabel.Text = LanguageManager.Format("CashNotesTotal", total, code);
        }

        private int GetInitialCurrencyIndex()
        {
            string initialCode = _initialNotes
                .Select(note => note == null ? null : note.CurrencyCode)
                .FirstOrDefault(code => !string.IsNullOrWhiteSpace(code));
            if (string.IsNullOrWhiteSpace(initialCode))
            {
                return -1;
            }

            for (int i = 0; i < _currencyComboBox.Items.Count; i++)
            {
                var currency = _currencyComboBox.Items[i] as CurrencyDto;
                if (currency != null && string.Equals(currency.Code, initialCode, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetInitialCount(string currencyCode, decimal denominationValue)
        {
            return _initialNotes
                .Where(note => note != null &&
                               string.Equals(note.CurrencyCode, currencyCode, StringComparison.OrdinalIgnoreCase) &&
                               decimal.Round(note.DenominationValue, 2) == decimal.Round(denominationValue, 2))
                .Sum(note => note.NoteCount);
        }

        private static void IncrementCount(TextBox textBox)
        {
            int current;
            if (!int.TryParse(textBox.Text, out current))
            {
                current = 0;
            }

            if (current >= 99999)
            {
                return;
            }

            textBox.Text = (current + 1).ToString(CultureInfo.InvariantCulture);
        }

        private CurrencyDto GetSelectedCurrency()
        {
            var selected = _currencyComboBox.SelectedItem as CurrencyDto;
            if (selected != null)
            {
                return selected;
            }

            return _currencyComboBox.Items.OfType<CurrencyDto>().FirstOrDefault();
        }

        private void CountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                IncrementCount((TextBox)sender);
                e.Handled = true;
                e.SuppressKeyPress = true;
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

        private sealed class NoteInputRow
        {
            public NoteInputRow(CashDenominationDto denomination, TextBox countTextBox)
            {
                Denomination = denomination;
                CountTextBox = countTextBox;
            }

            public CashDenominationDto Denomination { get; }
            public TextBox CountTextBox { get; }
        }
    }
}
