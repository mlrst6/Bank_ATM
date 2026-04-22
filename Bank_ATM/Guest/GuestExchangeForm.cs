using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Bank_ATM
{
    public sealed partial class GuestExchangeForm : Form
    {
        private readonly GuestExchangeService _exchangeService;
        private CurrencyDto[] _currencies = new CurrencyDto[0];
        private CashNoteDto[] _insertedNotes = new CashNoteDto[0];
        private GuestExchangeResult _preview;
        private decimal _plannedSourceAmount;
        private bool IsInDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode;

        public GuestExchangeForm()
        {
            InitializeComponent();
            if (IsInDesignMode)
                return;
            _exchangeService = new GuestExchangeService();
        }

        private void GuestExchangeForm_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode)
                return;

            AppWindow.ApplyMainScreen(this);
            lblTitle.Text = LanguageManager.GetString("Exchange");
            lblSubtitle.Text = LanguageManager.GetString("GuestExchangeSubtitle");
            lblFromCaption.Text = LanguageManager.GetString("ExchangeFromCurrency");
            lblToCaption.Text = LanguageManager.GetString("ExchangeToCurrency");
            lblAmountCaption.Text = LanguageManager.GetString("ExchangeAmountCaption");
            lblRateCaption.Text = LanguageManager.GetString("CurrentRateCaption");
            lblInsertedCaption.Text = LanguageManager.GetString("InsertedCashCaption");
            lblReceivedCaption.Text = LanguageManager.GetString("GuestReceivesCaption");
            lblBreakdownCaption.Text = LanguageManager.GetString("DispenseBreakdownCaption");
            btnSelectCash.Text = LanguageManager.GetString("SelectCashNotes");
            btnSelectCash.Values.Text = btnSelectCash.Text;
            btnPreview.Text = LanguageManager.GetString("PreviewExchange");
            btnPreview.Values.Text = btnPreview.Text;
            btnConfirm.Text = LanguageManager.GetString("ConfirmExchange");
            btnConfirm.Values.Text = btnConfirm.Text;
            btnBack.Text = LanguageManager.GetString("Back");
            btnBack.Values.Text = btnBack.Text;
            NumericInputDialog.Attach(txtAmount, LanguageManager.GetString("ExchangeAmountCaption"), true);
            txtAmount.TextChanged += (s, args) => ResetPlannedExchange();

            _currencies = _exchangeService.GetActiveCurrencies();
            cmbFromCurrency.DataSource = _currencies.ToArray();
            cmbFromCurrency.DisplayMember = "Code";
            cmbFromCurrency.ValueMember = "Code";

            cmbToCurrency.DataSource = _currencies.ToArray();
            cmbToCurrency.DisplayMember = "Code";
            cmbToCurrency.ValueMember = "Code";

            SelectCurrency(cmbFromCurrency, "UZS");
            SelectDifferentTargetCurrency();
            UpdateRateLabel();
        }

        private void CurrencySelectionChanged(object sender, EventArgs e)
        {
            ResetPlannedExchange();
            UpdateRateLabel();
        }

        private void SelectCashButton_Click(object sender, EventArgs e)
        {
            var from = cmbFromCurrency.SelectedItem as CurrencyDto;
            if (from == null || _preview == null || !_preview.Success || _plannedSourceAmount <= 0m)
            {
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = from == null
                    ? LanguageManager.GetString("SelectedCurrencyUnavailable")
                    : LanguageManager.GetString("ExchangePlanFirst");
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

                if (decimal.Round(dialog.TotalAmount, 2) != decimal.Round(_plannedSourceAmount, 2))
                {
                    lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                    lblStatusValue.Text = LanguageManager.Format("ExchangeCashAmountMismatch", _plannedSourceAmount, from.Code);
                    btnConfirm.Enabled = false;
                    return;
                }

                _insertedNotes = dialog.Notes;
                lblInsertedValue.Text = LanguageManager.Format("ExchangeInsertedAmount", dialog.TotalAmount, dialog.CurrencyCode);
                btnConfirm.Enabled = true;
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(74, 222, 128);
                lblStatusValue.Text = LanguageManager.GetString("ExchangeReadyToConfirm");
            }
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            PreviewExchange();
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            var from = cmbFromCurrency.SelectedItem as CurrencyDto;
            var to = cmbToCurrency.SelectedItem as CurrencyDto;
            if (from == null || to == null)
            {
                lblStatusValue.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
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

            if (_insertedNotes == null || !_insertedNotes.Any())
            {
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = LanguageManager.Format("ExchangeInsertCashPrompt", _plannedSourceAmount, from.Code);
                btnConfirm.Enabled = false;
                return;
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
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = result.Message;
                btnConfirm.Enabled = false;
                return;
            }

            lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(74, 222, 128);
            string successMessage = string.IsNullOrWhiteSpace(result.ReceiptPath)
                ? result.Message
                : LanguageManager.Format("ExchangeCompletedWithReceipt", result.ReceiptPath);
            ResetPlannedExchange();
            txtAmount.Text = string.Empty;
            lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(74, 222, 128);
            lblStatusValue.Text = successMessage;
            UpdateRateLabel();
        }

        private void PreviewExchange()
        {
            var from = cmbFromCurrency.SelectedItem as CurrencyDto;
            var to = cmbToCurrency.SelectedItem as CurrencyDto;
            if (from == null || to == null)
            {
                lblStatusValue.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
                return;
            }

            decimal plannedAmount;
            if (!TryParseAmount(txtAmount.Text, out plannedAmount))
            {
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = LanguageManager.GetString("InvalidAmount");
                btnSelectCash.Enabled = false;
                btnConfirm.Enabled = false;
                return;
            }

            _plannedSourceAmount = plannedAmount;
            _preview = _exchangeService.PreviewExchange(from.Code, to.Code, plannedAmount);
            btnSelectCash.Enabled = _preview.Success;
            btnConfirm.Enabled = _preview.Success && _insertedNotes.Any();
            lblStatusValue.ForeColor = _preview.Success ? System.Drawing.Color.FromArgb(74, 222, 128) : System.Drawing.Color.FromArgb(248, 113, 113);
            lblStatusValue.Text = _preview.Success
                ? LanguageManager.Format("ExchangeInsertCashPrompt", _plannedSourceAmount, from.Code)
                : _preview.Message;

            if (_preview.Success)
            {
                lblReceivedValue.Text = LanguageManager.Format("ExchangeReceiveAmount", _preview.TargetAmount, _preview.ToCurrencyCode);
                lblBreakdownValue.Text = LanguageManager.Format("ExchangeDispenseBreakdown", FormatNotes(_preview.DispensedNotes));
                lblInsertedValue.Text = LanguageManager.Format("ExchangeExpectedInsertAmount", _plannedSourceAmount, from.Code);
            }
            else
            {
                lblInsertedValue.Text = LanguageManager.GetString("NoCashInserted");
                lblReceivedValue.Text = "-";
                lblBreakdownValue.Text = "-";
            }
        }

        private void UpdateRateLabel()
        {
            var from = cmbFromCurrency.SelectedItem as CurrencyDto;
            var to = cmbToCurrency.SelectedItem as CurrencyDto;
            if (from == null || to == null || to.RateToUzs <= 0m)
            {
                lblRateValue.Text = LanguageManager.GetString("ExchangeRateUnavailable");
                return;
            }

            decimal rate = decimal.Round(from.RateToUzs / to.RateToUzs, 6);
            lblRateValue.Text = LanguageManager.Format("ExchangeRatePair", 1m, from.Code, rate, to.Code);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormNavigator.GoBack(this, () => new GuestActionsForm());
        }

        private void SetLoading(bool loading)
        {
            UseWaitCursor = loading;
            btnSelectCash.Enabled = !loading && _preview != null && _preview.Success;
            btnPreview.Enabled = !loading;
            btnConfirm.Enabled = !loading && _preview != null && _preview.Success && _insertedNotes.Any();
            btnBack.Enabled = !loading;
            cmbFromCurrency.Enabled = !loading;
            cmbToCurrency.Enabled = !loading;
            txtAmount.Enabled = !loading;
        }

        private void SelectDifferentTargetCurrency()
        {
            var from = cmbFromCurrency.SelectedItem as CurrencyDto;
            if (from == null || cmbToCurrency.Items.Count == 0)
            {
                return;
            }

            foreach (var item in cmbToCurrency.Items)
            {
                var currency = item as CurrencyDto;
                if (currency != null && currency.Code != from.Code)
                {
                    cmbToCurrency.SelectedItem = item;
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

        private void ResetPlannedExchange()
        {
            _insertedNotes = new CashNoteDto[0];
            _preview = null;
            _plannedSourceAmount = 0m;
            btnSelectCash.Enabled = false;
            btnConfirm.Enabled = false;
            lblStatusValue.Text = string.Empty;
            lblInsertedValue.Text = LanguageManager.GetString("NoCashInserted");
            lblReceivedValue.Text = "-";
            lblBreakdownValue.Text = "-";
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

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", (notes ?? new CashNoteDto[0])
                .Where(note => note != null)
                .Select(note => $"{note.DenominationValue:N2} {note.CurrencyCode} x {note.NoteCount}"));
        }
    }
}
