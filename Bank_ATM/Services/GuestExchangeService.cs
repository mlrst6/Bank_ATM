using System;
using System.Linq;
using System.Threading.Tasks;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Services
{
    public class GuestExchangeService
    {
        private readonly CurrencyRepository _currencyRepository = new CurrencyRepository();
        private readonly CashRepository _cashRepository = new CashRepository();

        public CurrencyDto[] GetActiveCurrencies()
        {
            return _currencyRepository.GetActiveCurrencies()
                .OrderBy(currency => currency.Code)
                .ToArray();
        }

        public CashDenominationDto[] GetCashDenominations(string currencyCode)
        {
            return _cashRepository.GetDenominations(currencyCode).ToArray();
        }

        public GuestExchangeResult PreviewExchange(string fromCurrencyCode, string toCurrencyCode, CashNoteDto[] insertedNotes)
        {
            var validation = ValidateExchangeInput(fromCurrencyCode, toCurrencyCode, insertedNotes);
            if (!validation.Success)
            {
                return validation;
            }

            decimal requestedTargetAmount = validation.TargetAmount;
            var targetDenominations = _cashRepository.GetDenominations(validation.ToCurrencyCode).ToArray();
            decimal actualTargetAmount;
            var dispensedNotes = CashRepository.BuildClosestDispenseBreakdown(
                validation.TargetAmount,
                targetDenominations,
                out actualTargetAmount);
            if (dispensedNotes == null || dispensedNotes.Count == 0)
            {
                validation.Success = false;
                validation.Message = LanguageManager.GetString("AtmCannotDispenseRequestedAmount");
                return validation;
            }

            validation.Success = true;
            validation.RequestedTargetAmount = requestedTargetAmount;
            validation.TargetAmount = actualTargetAmount;
            validation.IsApproximateAmount = actualTargetAmount < requestedTargetAmount;
            validation.UnavailableAmount = decimal.Round(requestedTargetAmount - actualTargetAmount, 2);
            validation.Message = BuildExchangeSummary(validation, dispensedNotes.ToArray());
            validation.DispensedNotes = dispensedNotes.ToArray();
            return validation;
        }

        public GuestExchangeResult PreviewExchange(string fromCurrencyCode, string toCurrencyCode, decimal sourceAmount)
        {
            var validation = ValidateExchangeInput(fromCurrencyCode, toCurrencyCode, sourceAmount);
            if (!validation.Success)
            {
                return validation;
            }

            decimal requestedTargetAmount = validation.TargetAmount;
            var targetDenominations = _cashRepository.GetDenominations(validation.ToCurrencyCode).ToArray();
            decimal actualTargetAmount;
            var dispensedNotes = CashRepository.BuildClosestDispenseBreakdown(
                validation.TargetAmount,
                targetDenominations,
                out actualTargetAmount);
            if (dispensedNotes == null || dispensedNotes.Count == 0)
            {
                validation.Success = false;
                validation.Message = LanguageManager.GetString("AtmCannotDispenseRequestedAmount");
                return validation;
            }

            validation.Success = true;
            validation.RequestedTargetAmount = requestedTargetAmount;
            validation.TargetAmount = actualTargetAmount;
            validation.IsApproximateAmount = actualTargetAmount < requestedTargetAmount;
            validation.UnavailableAmount = decimal.Round(requestedTargetAmount - actualTargetAmount, 2);
            validation.Message = BuildExchangeSummary(validation, dispensedNotes.ToArray());
            validation.DispensedNotes = dispensedNotes.ToArray();
            return validation;
        }

        public async Task<GuestExchangeResult> ExecuteExchangeAsync(string fromCurrencyCode, string toCurrencyCode, CashNoteDto[] insertedNotes)
        {
            var preview = PreviewExchange(fromCurrencyCode, toCurrencyCode, insertedNotes);
            if (!preview.Success)
            {
                return preview;
            }

            var fromCurrency = _currencyRepository.GetCurrencyByCode(preview.FromCurrencyCode);
            var toCurrency = _currencyRepository.GetCurrencyByCode(preview.ToCurrencyCode);
            string description =
                $"Guest exchange: {preview.SourceAmount:N2} {preview.FromCurrencyCode} -> {preview.TargetAmount:N2} {preview.ToCurrencyCode}; " +
                $"Inserted: {FormatNotes(preview.InsertedNotes)}; Dispensed: {FormatNotes(preview.DispensedNotes)}";

            var actualDispensed = await _cashRepository.ExchangeGuestCashAsync(
                fromCurrency,
                toCurrency,
                preview.InsertedNotes,
                preview.TargetAmount,
                preview.SourceAmountUzs,
                description);

            if (actualDispensed == null || actualDispensed.Count == 0)
            {
                preview.Success = false;
                preview.Message = LanguageManager.GetString("AtmCannotDispenseRequestedAmount");
                return preview;
            }

            preview.DispensedNotes = actualDispensed.ToArray();
            preview.ReceiptPath = ReceiptService.GenerateGuestReceipt(
                LanguageManager.GetString("Exchange"),
                new[]
                {
                    LanguageManager.Format("ExchangeReceiptFrom", preview.SourceAmount, preview.FromCurrencyCode),
                    LanguageManager.Format("ExchangeReceiptTo", preview.TargetAmount, preview.ToCurrencyCode),
                    LanguageManager.Format("ExchangeReceiptRate", preview.Rate, preview.ToCurrencyCode, preview.FromCurrencyCode),
                    LanguageManager.GetString("CashAcceptedBreakdown") + ": " + FormatNotes(preview.InsertedNotes),
                    LanguageManager.GetString("CashDispensedBreakdown") + ": " + FormatNotes(preview.DispensedNotes)
                });
            preview.Success = true;
            preview.Message = LanguageManager.GetString("ExchangeCompleted");
            AuditLogger.LogInfo(description);
            return preview;
        }

        private GuestExchangeResult ValidateExchangeInput(string fromCurrencyCode, string toCurrencyCode, CashNoteDto[] insertedNotes)
        {
            string normalizedFrom = NormalizeCurrencyCode(fromCurrencyCode);
            string normalizedTo = NormalizeCurrencyCode(toCurrencyCode);
            if (string.IsNullOrWhiteSpace(normalizedFrom) || string.IsNullOrWhiteSpace(normalizedTo))
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            if (normalizedFrom == normalizedTo)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("ExchangeSameCurrency") };
            }

            var fromCurrency = _currencyRepository.GetCurrencyByCode(normalizedFrom);
            var toCurrency = _currencyRepository.GetCurrencyByCode(normalizedTo);
            if (fromCurrency == null || !fromCurrency.IsActive || toCurrency == null || !toCurrency.IsActive)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            var normalizedNotes = CashRepository.NormalizeNotes(insertedNotes).ToArray();
            decimal sourceAmount = normalizedNotes.Sum(note => note.TotalValue);
            if (normalizedNotes.Length == 0 || sourceAmount <= 0m)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("InvalidCashNotes") };
            }

            decimal sourceAmountUzs = decimal.Round(sourceAmount * fromCurrency.RateToUzs, 2);
            decimal targetAmount = decimal.Round(sourceAmountUzs / toCurrency.RateToUzs, 2);
            if (targetAmount <= 0m)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("InvalidAmount") };
            }

            return new GuestExchangeResult
            {
                Success = true,
                FromCurrencyCode = fromCurrency.Code,
                ToCurrencyCode = toCurrency.Code,
                SourceAmount = sourceAmount,
                SourceAmountUzs = sourceAmountUzs,
                TargetAmount = targetAmount,
                Rate = decimal.Round(fromCurrency.RateToUzs / toCurrency.RateToUzs, 6),
                InsertedNotes = normalizedNotes
            };
        }

        private GuestExchangeResult ValidateExchangeInput(string fromCurrencyCode, string toCurrencyCode, decimal sourceAmount)
        {
            string normalizedFrom = NormalizeCurrencyCode(fromCurrencyCode);
            string normalizedTo = NormalizeCurrencyCode(toCurrencyCode);
            if (string.IsNullOrWhiteSpace(normalizedFrom) || string.IsNullOrWhiteSpace(normalizedTo))
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            if (normalizedFrom == normalizedTo)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("ExchangeSameCurrency") };
            }

            var fromCurrency = _currencyRepository.GetCurrencyByCode(normalizedFrom);
            var toCurrency = _currencyRepository.GetCurrencyByCode(normalizedTo);
            if (fromCurrency == null || !fromCurrency.IsActive || toCurrency == null || !toCurrency.IsActive)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            decimal roundedSourceAmount = decimal.Round(sourceAmount, 2);
            if (roundedSourceAmount <= 0m)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("InvalidAmount") };
            }

            decimal sourceAmountUzs = decimal.Round(roundedSourceAmount * fromCurrency.RateToUzs, 2);
            decimal targetAmount = decimal.Round(sourceAmountUzs / toCurrency.RateToUzs, 2);
            if (targetAmount <= 0m)
            {
                return new GuestExchangeResult { Success = false, Message = LanguageManager.GetString("InvalidAmount") };
            }

            return new GuestExchangeResult
            {
                Success = true,
                FromCurrencyCode = fromCurrency.Code,
                ToCurrencyCode = toCurrency.Code,
                SourceAmount = roundedSourceAmount,
                SourceAmountUzs = sourceAmountUzs,
                TargetAmount = targetAmount,
                Rate = decimal.Round(fromCurrency.RateToUzs / toCurrency.RateToUzs, 6),
                InsertedNotes = new CashNoteDto[0]
            };
        }

        private static string NormalizeCurrencyCode(string currencyCode)
        {
            return (currencyCode ?? string.Empty).Trim().ToUpperInvariant();
        }

        private static string BuildExchangeSummary(GuestExchangeResult result, CashNoteDto[] dispensedNotes)
        {
            if (result.IsApproximateAmount)
            {
                return LanguageManager.Format(
                    "ExchangeApproximatePreviewSummary",
                    result.RequestedTargetAmount,
                    result.ToCurrencyCode,
                    result.TargetAmount,
                    FormatNotes(dispensedNotes));
            }

            return LanguageManager.Format(
                "ExchangePreviewSummary",
                result.SourceAmount,
                result.FromCurrencyCode,
                result.TargetAmount,
                result.ToCurrencyCode,
                FormatNotes(dispensedNotes));
        }

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", CashRepository.NormalizeNotes(notes)
                .Select(note => $"{note.DenominationValue:N2} {note.CurrencyCode} x {note.NoteCount}"));
        }
    }
}
