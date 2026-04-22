using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Bank_ATM.UI;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Bank_ATM
{
    public sealed partial class GuestExchangeForm : Form
    {
        private enum ExchangeStep
        {
            ChooseSourceCurrency,
            ChooseTargetCurrency,
            InsertCash,
            ReviewAndConfirm
        }

        private readonly GuestExchangeService _exchangeService;
        private CurrencyDto[] _currencies = new CurrencyDto[0];
        private CashNoteDto[] _insertedNotes = new CashNoteDto[0];
        private GuestExchangeResult _preview;
        private ExchangeStep _currentStep;
        private bool _isLoading;
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

            lblTitle.Text = LanguageManager.GetString("Exchange");
            lblSubtitle.Text = LanguageManager.GetString("GuestExchangeSubtitle");
            lblFromCaption.Text = LanguageManager.GetString("ExchangeFromCurrency");
            lblToCaption.Text = LanguageManager.GetString("ExchangeToCurrency");
            lblStepCaption.Text = LanguageManager.GetString("GuestExchangeStepCaption");
            lblRateCaption.Text = LanguageManager.GetString("CurrentRateCaption");
            lblInsertedCaption.Text = LanguageManager.GetString("InsertedCashCaption");
            lblReceivedCaption.Text = LanguageManager.GetString("GuestReceivesCaption");
            lblBreakdownCaption.Text = LanguageManager.GetString("DispenseBreakdownCaption");
            btnPreview.Text = LanguageManager.GetString("Continue");
            btnPreview.Values.Text = btnPreview.Text;
            btnSelectCash.Text = LanguageManager.GetString("SelectCashNotes");
            btnSelectCash.Values.Text = btnSelectCash.Text;
            btnConfirm.Text = LanguageManager.GetString("ConfirmExchange");
            btnConfirm.Values.Text = btnConfirm.Text;
            btnBack.Text = LanguageManager.GetString("Back");
            btnBack.Values.Text = btnBack.Text;

            _currencies = _exchangeService.GetActiveCurrencies();
            cmbFromCurrency.DataSource = _currencies.ToArray();
            cmbFromCurrency.DisplayMember = "Code";
            cmbFromCurrency.ValueMember = "Code";

            SelectCurrency(cmbFromCurrency, "UZS");
            RefreshTargetCurrencyOptions();
            ResetExchangeState();
            SetCurrentStep(ExchangeStep.ChooseSourceCurrency);
            UpdateRateLabel();
        }

        private void CurrencySelectionChanged(object sender, EventArgs e)
        {
            if (sender == cmbFromCurrency)
            {
                RefreshTargetCurrencyOptions();
            }

            if (_currentStep == ExchangeStep.InsertCash || _currentStep == ExchangeStep.ReviewAndConfirm)
            {
                ResetExchangeState();
                SetCurrentStep(ExchangeStep.ChooseTargetCurrency);
            }

            UpdateRateLabel();
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            CurrencyDto from;
            CurrencyDto to;
            if (!TryGetSelectedCurrencies(out from, out to))
            {
                ShowUnavailableCurrencyMessage();
                return;
            }

            if (_currentStep == ExchangeStep.ChooseSourceCurrency)
            {
                SetCurrentStep(ExchangeStep.ChooseTargetCurrency);
                return;
            }

            if (string.Equals(from.Code, to.Code, StringComparison.OrdinalIgnoreCase))
            {
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = LanguageManager.GetString("ExchangeSameCurrency");
                return;
            }

            ResetExchangeState();
            SetCurrentStep(ExchangeStep.InsertCash);
        }

        private void SelectCashButton_Click(object sender, EventArgs e)
        {
            CurrencyDto from;
            CurrencyDto to;
            if (!TryGetSelectedCurrencies(out from, out to))
            {
                ShowUnavailableCurrencyMessage();
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

                _insertedNotes = dialog.Notes ?? new CashNoteDto[0];
                if (!_insertedNotes.Any())
                {
                    lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                    lblStatusValue.Text = LanguageManager.GetString("InvalidCashNotes");
                    return;
                }

                lblInsertedValue.Text = LanguageManager.Format("ExchangeInsertedAmount", dialog.TotalAmount, dialog.CurrencyCode);
                PreviewInsertedCash();
            }
        }

        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            CurrencyDto from;
            CurrencyDto to;
            if (!TryGetSelectedCurrencies(out from, out to))
            {
                ShowUnavailableCurrencyMessage();
                return;
            }

            if (_insertedNotes == null || !_insertedNotes.Any())
            {
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = LanguageManager.GetString("GuestExchangeInsertCashPrompt");
                btnConfirm.Enabled = false;
                return;
            }

            if (_preview == null || !_preview.Success)
            {
                PreviewInsertedCash();
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
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = result.Message;
                btnConfirm.Enabled = false;
                return;
            }

            string receiptMessage = ReceiptWorkflow.OfferPdfReceipt(
                this,
                () => ReceiptService.GenerateGuestExchangeReceipt(result));
            string successMessage = string.IsNullOrWhiteSpace(receiptMessage)
                ? result.Message
                : result.Message + Environment.NewLine + receiptMessage;

            ResetExchangeState();
            SetCurrentStep(ExchangeStep.ChooseSourceCurrency);
            lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(74, 222, 128);
            lblStatusValue.Text = successMessage;
            UpdateRateLabel();
        }

        private void PreviewInsertedCash()
        {
            CurrencyDto from;
            CurrencyDto to;
            if (!TryGetSelectedCurrencies(out from, out to))
            {
                ShowUnavailableCurrencyMessage();
                return;
            }

            if (_insertedNotes == null || !_insertedNotes.Any())
            {
                lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
                lblStatusValue.Text = LanguageManager.GetString("GuestExchangeInsertCashPrompt");
                btnConfirm.Enabled = false;
                return;
            }

            _preview = _exchangeService.PreviewExchange(from.Code, to.Code, _insertedNotes);
            lblStatusValue.ForeColor = _preview.Success
                ? System.Drawing.Color.FromArgb(74, 222, 128)
                : System.Drawing.Color.FromArgb(248, 113, 113);
            lblStatusValue.Text = _preview.Message;

            if (_preview.Success)
            {
                lblInsertedValue.Text = LanguageManager.Format("ExchangeInsertedAmount", _preview.SourceAmount, _preview.FromCurrencyCode);
                lblReceivedValue.Text = LanguageManager.Format("ExchangeReceiveAmount", _preview.TargetAmount, _preview.ToCurrencyCode);
                lblBreakdownValue.Text = LanguageManager.Format("ExchangeDispenseBreakdown", FormatNotes(_preview.DispensedNotes));
                SetCurrentStep(ExchangeStep.ReviewAndConfirm);
            }
            else
            {
                lblReceivedValue.Text = "-";
                lblBreakdownValue.Text = "-";
                SetCurrentStep(ExchangeStep.InsertCash);
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
            if (_isLoading)
            {
                return;
            }

            if (_currentStep == ExchangeStep.ReviewAndConfirm)
            {
                ResetExchangeState();
                SetCurrentStep(ExchangeStep.InsertCash);
                return;
            }

            if (_currentStep == ExchangeStep.InsertCash)
            {
                ResetExchangeState();
                SetCurrentStep(ExchangeStep.ChooseTargetCurrency);
                return;
            }

            if (_currentStep == ExchangeStep.ChooseTargetCurrency)
            {
                ResetExchangeState();
                SetCurrentStep(ExchangeStep.ChooseSourceCurrency);
                return;
            }

            FormNavigator.GoBack(this, () => new GuestActionsForm());
        }

        private void SetLoading(bool loading)
        {
            _isLoading = loading;
            UseWaitCursor = loading;
            btnBack.Enabled = !loading;
            ApplyStepState();
        }

        private void SelectDifferentTargetCurrency()
        {
            if (cmbToCurrency.Items.Count == 0)
            {
                return;
            }

            cmbToCurrency.SelectedIndex = 0;
        }

        private void RefreshTargetCurrencyOptions()
        {
            var from = cmbFromCurrency.SelectedItem as CurrencyDto;
            string selectedToCode = (cmbToCurrency.SelectedItem as CurrencyDto)?.Code;
            var targetCurrencies = _currencies
                .Where(currency => from == null || !string.Equals(currency.Code, from.Code, StringComparison.OrdinalIgnoreCase))
                .ToArray();

            cmbToCurrency.DataSource = null;
            cmbToCurrency.Items.Clear();
            cmbToCurrency.DataSource = targetCurrencies;
            cmbToCurrency.DisplayMember = "Code";
            cmbToCurrency.ValueMember = "Code";

            if (targetCurrencies.Length == 0)
            {
                return;
            }

            foreach (var currency in targetCurrencies)
            {
                if (!string.IsNullOrWhiteSpace(selectedToCode) &&
                    string.Equals(currency.Code, selectedToCode, StringComparison.OrdinalIgnoreCase))
                {
                    cmbToCurrency.SelectedItem = currency;
                    return;
                }
            }

            SelectDifferentTargetCurrency();
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

        private void ResetExchangeState()
        {
            _insertedNotes = new CashNoteDto[0];
            _preview = null;
            lblInsertedValue.Text = LanguageManager.GetString("NoCashInserted");
            lblReceivedValue.Text = "-";
            lblBreakdownValue.Text = "-";
            ApplyStepState();
        }

        private void SetCurrentStep(ExchangeStep step)
        {
            _currentStep = step;
            ApplyStepState();
        }

        private void ApplyStepState()
        {
            bool hasInsertedCash = _insertedNotes != null && _insertedNotes.Any();
            bool hasPreview = _preview != null && _preview.Success;

            cmbFromCurrency.Enabled = !_isLoading && _currentStep == ExchangeStep.ChooseSourceCurrency;
            cmbToCurrency.Enabled = !_isLoading && _currentStep == ExchangeStep.ChooseTargetCurrency && cmbToCurrency.Items.Count > 0;

            btnPreview.Visible = _currentStep == ExchangeStep.ChooseSourceCurrency || _currentStep == ExchangeStep.ChooseTargetCurrency;
            btnPreview.Enabled = !_isLoading;
            btnSelectCash.Enabled = !_isLoading && (_currentStep == ExchangeStep.InsertCash || _currentStep == ExchangeStep.ReviewAndConfirm);
            btnSelectCash.Text = hasInsertedCash
                ? LanguageManager.GetString("GuestExchangeEditCash")
                : LanguageManager.GetString("SelectCashNotes");
            btnSelectCash.Values.Text = btnSelectCash.Text;
            btnConfirm.Enabled = !_isLoading && _currentStep == ExchangeStep.ReviewAndConfirm && hasPreview;

            if (_currentStep == ExchangeStep.ChooseSourceCurrency)
            {
                lblStepValue.Text = LanguageManager.GetString("GuestExchangeStepChooseSource");
                if (string.IsNullOrWhiteSpace(lblStatusValue.Text) || lblStatusValue.ForeColor != System.Drawing.Color.FromArgb(74, 222, 128))
                {
                    lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(125, 211, 252);
                    lblStatusValue.Text = LanguageManager.GetString("GuestExchangeChooseSourcePrompt");
                }

                return;
            }

            if (_currentStep == ExchangeStep.ChooseTargetCurrency)
            {
                lblStepValue.Text = LanguageManager.GetString("GuestExchangeStepChooseTarget");
                if (string.IsNullOrWhiteSpace(lblStatusValue.Text) || lblStatusValue.ForeColor != System.Drawing.Color.FromArgb(74, 222, 128))
                {
                    lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(125, 211, 252);
                    lblStatusValue.Text = LanguageManager.GetString("GuestExchangeChooseTargetPrompt");
                }

                return;
            }

            if (_currentStep == ExchangeStep.InsertCash)
            {
                lblStepValue.Text = LanguageManager.GetString("GuestExchangeStepInsertCash");
                if (string.IsNullOrWhiteSpace(lblStatusValue.Text) || lblStatusValue.ForeColor != System.Drawing.Color.FromArgb(74, 222, 128))
                {
                    lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(125, 211, 252);
                    lblStatusValue.Text = LanguageManager.GetString("GuestExchangeInsertCashPrompt");
                }

                return;
            }

            lblStepValue.Text = LanguageManager.GetString("GuestExchangeStepReview");
            if (hasPreview)
            {
                btnConfirm.Enabled = !_isLoading;
            }
        }

        private bool TryGetSelectedCurrencies(out CurrencyDto from, out CurrencyDto to)
        {
            from = cmbFromCurrency.SelectedItem as CurrencyDto;
            to = cmbToCurrency.SelectedItem as CurrencyDto;
            return from != null && to != null;
        }

        private void ShowUnavailableCurrencyMessage()
        {
            lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(248, 113, 113);
            lblStatusValue.Text = LanguageManager.GetString("SelectedCurrencyUnavailable");
        }

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", (notes ?? new CashNoteDto[0])
                .Where(note => note != null)
                .Select(note => $"{note.DenominationValue:N2} {note.CurrencyCode} x {note.NoteCount}"));
        }
    }
}
