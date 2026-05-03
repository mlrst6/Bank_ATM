using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Services
{
    public class BankingService
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();
        private readonly CardRepository _cardRepository = new CardRepository();
        private readonly TransactionRepository _transactionRepository = new TransactionRepository();
        private readonly ServicesRepository _servicesRepository = new ServicesRepository();
        private readonly AtmRepository _atmRepository = new AtmRepository();
        private readonly CurrencyRepository _currencyRepository = new CurrencyRepository();
        private readonly CashRepository _cashRepository = new CashRepository();
        private readonly FeeCalculationService _feeCalculationService = new FeeCalculationService();

        public async Task<BankingResult> WithdrawAsync(decimal amount)
        {
            return await WithdrawAsync(amount, "UZS");
        }

        public async Task<BankingResult> WithdrawAsync(decimal amount, string currencyCode)
        {
            var currentAccount = SessionManager.Instance.CurrentAccount;
            var currentCard = SessionManager.Instance.CurrentCard;
            if (currentAccount == null || !currentAccount.IsActive || currentCard == null || currentCard.IsBlocked)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            if (!IsValidMoneyAmount(amount))
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("DepositFailed") };
            }

            string normalizedCurrency = string.IsNullOrWhiteSpace(currencyCode)
                ? "UZS"
                : currencyCode.Trim().ToUpperInvariant();

            var currency = _currencyRepository.GetCurrencyByCode(normalizedCurrency);
            if (currency == null || !currency.IsActive)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            decimal cashValueUzs = decimal.Round(amount * GetCashOutRate(currency), 2);
            var fee = _feeCalculationService.Calculate(currentCard.CardType, "Withdraw", cashValueUzs);
            decimal debitAmountUzs = fee.TotalDebitUzs;
            if (currentCard.Balance < debitAmountUzs)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
            }

            if (currency.CashAvailable < amount)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("AtmInsufficientCash") };
            }

            var breakdown = await _accountRepository.WithdrawCurrencyWithDenominationsByCardAsync(
                currentCard.Id,
                currentAccount.Id,
                amount,
                debitAmountUzs,
                fee.FeeAmountUzs,
                currency.Id,
                normalizedCurrency);
            if (breakdown == null || !breakdown.Any())
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("AtmCannotDispenseRequestedAmount") };
            }

            var account = await RefreshCurrentSessionAsync();
            return new BankingResult
            {
                Success = true,
                Account = account,
                DebitedAmountUzs = debitAmountUzs,
                FeePercent = fee.PercentFee,
                FeeAmountUzs = fee.FeeAmountUzs,
                NetAmountUzs = cashValueUzs,
                CashCurrencyCode = normalizedCurrency,
                CashAmount = amount,
                CashBreakdown = breakdown.ToArray()
            };
        }

        public CurrencyDto[] GetActiveCurrencies()
        {
            return _currencyRepository.GetActiveCurrencies().ToArray();
        }

        public CashDenominationDto[] GetCashDenominations(string currencyCode)
        {
            return _cashRepository.GetDenominations(currencyCode).ToArray();
        }

        public FeeCalculationResult PreviewWithdrawFee(decimal amount, string currencyCode)
        {
            var currentCard = SessionManager.Instance.CurrentCard;
            string normalizedCurrency = string.IsNullOrWhiteSpace(currencyCode)
                ? "UZS"
                : currencyCode.Trim().ToUpperInvariant();
            var currency = _currencyRepository.GetCurrencyByCode(normalizedCurrency);
            decimal cashValueUzs = currency == null ? 0m : decimal.Round(amount * GetCashOutRate(currency), 2);
            return _feeCalculationService.Calculate(currentCard?.CardType, "Withdraw", cashValueUzs);
        }

        public FeeCalculationResult PreviewDepositFee(decimal cashAmount, string currencyCode)
        {
            var currentCard = SessionManager.Instance.CurrentCard;
            string normalizedCurrency = string.IsNullOrWhiteSpace(currencyCode)
                ? "UZS"
                : currencyCode.Trim().ToUpperInvariant();
            var currency = _currencyRepository.GetCurrencyByCode(normalizedCurrency);
            decimal cashValueUzs = currency == null ? 0m : decimal.Round(cashAmount * GetCashInRate(currency), 2);
            return _feeCalculationService.Calculate(currentCard?.CardType, "Deposit", cashValueUzs);
        }

        public FeeCalculationResult PreviewTransferFee(decimal amount)
        {
            var currentCard = SessionManager.Instance.CurrentCard;
            return _feeCalculationService.Calculate(currentCard?.CardType, "Transfer", amount);
        }

        public FeeCalculationResult PreviewServicePaymentFee(decimal amount, bool chargeCurrentAccount)
        {
            if (chargeCurrentAccount)
            {
                return BuildNoFeeResult("BillPayment", amount);
            }

            var currentCard = SessionManager.Instance.CurrentCard;
            return _feeCalculationService.Calculate(null, "BillPayment", amount);
        }

        public async Task<BankingResult> DepositAsync(decimal amount)
        {
            return await DepositAsync(amount, "UZS");
        }

        public async Task<BankingResult> DepositAsync(decimal amount, string currencyCode)
        {
            var currentAccount = SessionManager.Instance.CurrentAccount;
            if (currentAccount == null || !currentAccount.IsActive)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            if (!IsValidMoneyAmount(amount))
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("DepositFailed") };
            }

            string normalizedCurrency = string.IsNullOrWhiteSpace(currencyCode)
                ? "UZS"
                : currencyCode.Trim().ToUpperInvariant();

            var currency = _currencyRepository.GetCurrencyByCode(normalizedCurrency);
            if (currency == null || !currency.IsActive)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            decimal grossAmountUzs = decimal.Round(amount * GetCashInRate(currency), 2);
            var fee = _feeCalculationService.Calculate(SessionManager.Instance.CurrentCard?.CardType, "Deposit", grossAmountUzs);
            decimal creditAmountUzs = decimal.Round(grossAmountUzs - fee.FeeAmountUzs, 2);
            if (creditAmountUzs < 0m)
            {
                creditAmountUzs = 0m;
            }

            bool success = normalizedCurrency == "UZS"
                ? await _accountRepository.DepositAsync(currentAccount.Id, creditAmountUzs)
                : await _accountRepository.DepositCurrencyAsync(currentAccount.Id, amount, creditAmountUzs, currency.Id, normalizedCurrency);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("DepositFailed") };
            }

            var account = await RefreshCurrentSessionAsync();
            return new BankingResult
            {
                Success = true,
                Account = account,
                DebitedAmountUzs = grossAmountUzs,
                FeePercent = fee.PercentFee,
                FeeAmountUzs = fee.FeeAmountUzs,
                NetAmountUzs = creditAmountUzs,
                CashCurrencyCode = normalizedCurrency,
                CashAmount = amount
            };
        }

        public async Task<BankingResult> DepositCashNotesAsync(string currencyCode, CashNoteDto[] notes)
        {
            var currentAccount = SessionManager.Instance.CurrentAccount;
            var currentCard = SessionManager.Instance.CurrentCard;
            if (currentAccount == null || !currentAccount.IsActive || currentCard == null || currentCard.IsBlocked)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            string normalizedCurrency = string.IsNullOrWhiteSpace(currencyCode)
                ? "UZS"
                : currencyCode.Trim().ToUpperInvariant();

            var currency = _currencyRepository.GetCurrencyByCode(normalizedCurrency);
            if (currency == null || !currency.IsActive)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            var noteList = CashRepository.NormalizeNotes(notes).ToArray();
            decimal cashAmount = noteList.Sum(note => note.TotalValue);
            if (!IsValidMoneyAmount(cashAmount))
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("InvalidCashNotes") };
            }

            decimal grossAmountUzs = decimal.Round(cashAmount * GetCashInRate(currency), 2);
            var fee = _feeCalculationService.Calculate(currentCard.CardType, "Deposit", grossAmountUzs);
            decimal creditAmountUzs = decimal.Round(grossAmountUzs - fee.FeeAmountUzs, 2);
            if (creditAmountUzs < 0m)
            {
                creditAmountUzs = 0m;
            }

            var savedNotes = await _accountRepository.DepositCurrencyWithDenominationsByCardAsync(
                currentCard.Id,
                currentAccount.Id,
                noteList,
                grossAmountUzs,
                fee.FeeAmountUzs,
                creditAmountUzs,
                currency.Id,
                normalizedCurrency);
            if (savedNotes == null)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("DepositFailed") };
            }

            var account = await RefreshCurrentSessionAsync();
            return new BankingResult
            {
                Success = true,
                Account = account,
                DebitedAmountUzs = grossAmountUzs,
                FeePercent = fee.PercentFee,
                FeeAmountUzs = fee.FeeAmountUzs,
                NetAmountUzs = creditAmountUzs,
                CashCurrencyCode = normalizedCurrency,
                CashAmount = cashAmount,
                CashBreakdown = savedNotes.ToArray()
            };
        }

        public async Task<BankingResult> TransferByCardAsync(string targetCardNumber, decimal amount)
        {
            var currentAccount = SessionManager.Instance.CurrentAccount;
            var currentCard = SessionManager.Instance.CurrentCard;
            if (currentAccount == null || !currentAccount.IsActive || currentCard == null || currentCard.IsBlocked)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            if (!IsValidMoneyAmount(amount))
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferFailed") };
            }

            string sanitizedCardNumber = SanitizeCardNumber(targetCardNumber);
            if (sanitizedCardNumber.Length != 16 || !long.TryParse(sanitizedCardNumber, out _))
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("InvalidTargetCardNumber") };
            }

            var targetCard = _cardRepository.GetCardByNumber(sanitizedCardNumber);
            if (targetCard == null)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TargetCardNotFound") };
            }

            if (targetCard.ExpiryDate.Date < System.DateTime.Today || targetCard.IsBlocked)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TargetCardUnavailable") };
            }

            if (targetCard.Id == currentCard.Id)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferSameAccount") };
            }

            var fee = _feeCalculationService.Calculate(currentCard.CardType, "Transfer", amount);
            if (currentCard.Balance < fee.TotalDebitUzs)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
            }

            bool success = await _accountRepository.TransferByCardAsync(currentCard.Id, targetCard.Id, amount, fee.FeeAmountUzs);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferFailed") };
            }

            var account = await RefreshCurrentSessionAsync();
            return new BankingResult
            {
                Success = true,
                Account = account,
                TargetCard = targetCard,
                DebitedAmountUzs = fee.TotalDebitUzs,
                FeePercent = fee.PercentFee,
                FeeAmountUzs = fee.FeeAmountUzs,
                NetAmountUzs = amount
            };
        }

        public ServiceDto[] GetAvailableServices()
        {
            return _servicesRepository.GetActiveServices().ToArray();
        }

        public ServiceLookupResult VerifyServiceAccount(int serviceId, string accountReference)
        {
            string sanitizedReference = (accountReference ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(sanitizedReference))
            {
                return new ServiceLookupResult
                {
                    Success = false,
                    Message = LanguageManager.GetString("InvalidServicePaymentInput")
                };
            }

            var service = _servicesRepository.GetServiceById(serviceId);
            if (service == null || !service.IsActive)
            {
                return new ServiceLookupResult
                {
                    Success = false,
                    Message = LanguageManager.GetString("SelectedServiceUnavailable")
                };
            }

            var serviceAccount = _servicesRepository.GetActiveServiceAccount(service.Id, sanitizedReference);
            if (serviceAccount == null)
            {
                return new ServiceLookupResult
                {
                    Success = false,
                    Message = LanguageManager.Format("ServiceReferenceNotFound", sanitizedReference, service.ServiceName),
                    Service = service
                };
            }

            return new ServiceLookupResult
            {
                Success = true,
                Message = $"{service.ServiceName}: {serviceAccount.CustomerName}",
                Service = service,
                ServiceAccount = serviceAccount
            };
        }

        public async Task<ServiceResult> PayServiceAsync(int serviceId, string accountReference, decimal amount, bool chargeCurrentAccount)
        {
            string sanitizedReference = (accountReference ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(sanitizedReference) || !IsValidMoneyAmount(amount))
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("InvalidServicePaymentInput") };
            }

            var service = _servicesRepository.GetServiceById(serviceId);
            if (service == null || !service.IsActive)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("SelectedServiceUnavailable") };
            }

            var serviceAccount = _servicesRepository.GetActiveServiceAccount(service.Id, sanitizedReference);
            if (serviceAccount == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = LanguageManager.Format("ServiceReferenceNotFound", sanitizedReference, service.ServiceName)
                };
            }

            string description = string.IsNullOrWhiteSpace(serviceAccount.CustomerName)
                ? $"{service.ServiceName}: {sanitizedReference}"
                : $"{service.ServiceName}: {sanitizedReference} ({serviceAccount.CustomerName})";

            if (chargeCurrentAccount)
            {
                var currentAccount = SessionManager.Instance.CurrentAccount;
                var currentCard = SessionManager.Instance.CurrentCard;
                if (currentAccount == null || !currentAccount.IsActive || currentCard == null || currentCard.IsBlocked)
                {
                    return new ServiceResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
                }

                decimal cashbackAmount = CalculateCashback(amount, service.CashbackPercent);
                var fee = BuildNoFeeResult("BillPayment", amount);
                string paymentDescription = cashbackAmount > 0m
                    ? $"{description}; Cashback: {cashbackAmount:N2} UZS ({service.CashbackPercent:N4}%)"
                    : description;
                bool success = await _accountRepository.PayServiceByCardAsync(
                    currentCard.Id,
                    currentAccount.Id,
                    amount,
                    fee.FeeAmountUzs,
                    cashbackAmount,
                    paymentDescription,
                    service.Id,
                    serviceAccount.Id,
                    sanitizedReference);
                if (!success)
                {
                    return new ServiceResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
                }

                await RefreshCurrentSessionAsync();
                AuditLogger.LogInfo($"Account service payment: {amount} UZS for {description}");
                return new ServiceResult
                {
                    Success = true,
                    Message = LanguageManager.GetString("ServicePaymentCompleted"),
                    FeePercent = fee.PercentFee,
                    FeeAmountUzs = fee.FeeAmountUzs,
                    TotalDebitedUzs = fee.TotalDebitUzs,
                    NetAmountUzs = amount,
                    CashbackAmountUzs = cashbackAmount
                };
            }

            _transactionRepository.AddTransaction(
                null,
                "BillPayment",
                amount,
                null,
                description,
                service.Id,
                serviceAccount.Id,
                sanitizedReference);
            AuditLogger.LogInfo($"Guest service payment: {amount} UZS for {description}");
            return new ServiceResult { Success = true, Message = LanguageManager.GetString("ServicePaymentCompleted") };
        }

        public async Task<ServiceResult> PayServiceWithCashNotesAsync(int serviceId, string accountReference, CashNoteDto[] notes)
        {
            string sanitizedReference = (accountReference ?? string.Empty).Trim();
            var noteList = CashRepository.NormalizeNotes(notes).ToArray();
            decimal amount = noteList.Sum(note => note.TotalValue);
            if (string.IsNullOrWhiteSpace(sanitizedReference) || !IsValidMoneyAmount(amount))
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("InvalidServicePaymentInput") };
            }

            var uzs = _currencyRepository.GetCurrencyByCode("UZS");
            if (uzs == null || !uzs.IsActive)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("SelectedCurrencyUnavailable") };
            }

            var fee = _feeCalculationService.Calculate(null, "BillPayment", amount);
            decimal netServiceAmount = decimal.Round(amount - fee.FeeAmountUzs, 2);
            if (netServiceAmount <= 0m)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("InvalidServicePaymentInput") };
            }

            var service = _servicesRepository.GetServiceById(serviceId);
            if (service == null || !service.IsActive)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("SelectedServiceUnavailable") };
            }

            var serviceAccount = _servicesRepository.GetActiveServiceAccount(service.Id, sanitizedReference);
            if (serviceAccount == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = LanguageManager.Format("ServiceReferenceNotFound", sanitizedReference, service.ServiceName)
                };
            }

            string description = string.IsNullOrWhiteSpace(serviceAccount.CustomerName)
                ? $"{service.ServiceName}: {sanitizedReference}; Cash paid: {amount:N2} UZS; Fee: {fee.FeeAmountUzs:N2} UZS; Net service amount: {netServiceAmount:N2} UZS; Cash notes: {FormatNotes(noteList)}"
                : $"{service.ServiceName}: {sanitizedReference} ({serviceAccount.CustomerName}); Cash paid: {amount:N2} UZS; Fee: {fee.FeeAmountUzs:N2} UZS; Net service amount: {netServiceAmount:N2} UZS; Cash notes: {FormatNotes(noteList)}";

            bool success = await _cashRepository.AddGuestCashPaymentAsync(
                service.Id,
                serviceAccount.Id,
                sanitizedReference,
                netServiceAmount,
                fee.FeeAmountUzs,
                amount,
                description,
                uzs.Id,
                noteList);

            if (!success)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("InvalidCashNotes") };
            }

            AuditLogger.LogInfo($"Guest cash service payment: {amount} UZS for {description}");
            return new ServiceResult
            {
                Success = true,
                Message = LanguageManager.GetString("ServicePaymentCompleted"),
                FeePercent = fee.PercentFee,
                FeeAmountUzs = fee.FeeAmountUzs,
                TotalDebitedUzs = amount,
                NetAmountUzs = netServiceAmount
            };
        }

        public async Task<ServiceResult> ChangeCurrentCardPinAsync(string currentPin, string newPin, string confirmPin)
        {
            var currentCard = SessionManager.Instance.CurrentCard;
            if (currentCard == null || string.IsNullOrWhiteSpace(currentCard.CardNumber))
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("NoActiveCardSession") };
            }

            string trimmedCurrentPin = (currentPin ?? string.Empty).Trim();
            string trimmedNewPin = (newPin ?? string.Empty).Trim();
            string trimmedConfirmPin = (confirmPin ?? string.Empty).Trim();

            if (!IsFourDigitPin(trimmedCurrentPin) || !IsFourDigitPin(trimmedNewPin) || !IsFourDigitPin(trimmedConfirmPin))
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("PinMustBeFourDigits") };
            }

            if (trimmedNewPin != trimmedConfirmPin)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("PinConfirmationMismatch") };
            }

            if (trimmedCurrentPin == trimmedNewPin)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("NewPinMustBeDifferent") };
            }

            string message = string.Empty;
            bool currentPinValid = await Task.Run(() =>
                _cardRepository.ValidatePin(currentCard.CardNumber, trimmedCurrentPin, out message));
            if (!currentPinValid)
            {
                AuditLogger.LogWarning($"Failed PIN change verification for card {currentCard.CardNumber}: {message}");
                return new ServiceResult { Success = false, Message = message };
            }

            string hashedPin = BCrypt.Net.BCrypt.HashPassword(trimmedNewPin, 11);
            await Task.Run(() => _cardRepository.UpdatePin(currentCard.CardNumber, hashedPin));
            AuditLogger.LogInfo($"PIN changed for card {currentCard.CardNumber}");

            return new ServiceResult { Success = true, Message = LanguageManager.GetString("PinChangedSuccessfully") };
        }

        public string GenerateReceiptForCurrentSession(TransactionDto transaction)
        {
            if (transaction == null ||
                SessionManager.Instance.CurrentUser == null ||
                SessionManager.Instance.CurrentAccount == null)
            {
                return null;
            }

            decimal remainingBalance = SessionManager.Instance.CurrentCard == null
                ? SessionManager.Instance.CurrentAccount.Balance
                : SessionManager.Instance.CurrentCard.Balance;

            return ReceiptService.GenerateReceipt(
                transaction,
                SessionManager.Instance.CurrentUser.FullName,
                remainingBalance);
        }

        private static string SanitizeCardNumber(string rawCardNumber)
        {
            return (rawCardNumber ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
        }

        private static bool IsValidMoneyAmount(decimal amount)
        {
            return amount > 0m && decimal.Round(amount, 2) == amount;
        }

        private static bool IsFourDigitPin(string pin)
        {
            return pin != null && pin.Length == 4 && pin.All(char.IsDigit);
        }

        private static decimal GetCashInRate(CurrencyDto currency)
        {
            if (currency == null)
            {
                return 0m;
            }

            return currency.BuyRateToUzs > 0m ? currency.BuyRateToUzs : currency.RateToUzs;
        }

        private static decimal CalculateCashback(decimal amount, decimal cashbackPercent)
        {
            if (amount <= 0m || cashbackPercent <= 0m)
            {
                return 0m;
            }

            return decimal.Round(amount * cashbackPercent / 100m, 2);
        }

        private static FeeCalculationResult BuildNoFeeResult(string transactionType, decimal amount)
        {
            decimal roundedAmount = amount <= 0m ? 0m : decimal.Round(amount, 2);
            return new FeeCalculationResult
            {
                TransactionType = transactionType,
                BaseAmountUzs = roundedAmount,
                FeeAmountUzs = 0m,
                TotalDebitUzs = roundedAmount,
                PercentFee = 0m,
                FixedFee = 0m
            };
        }

        private static decimal GetCashOutRate(CurrencyDto currency)
        {
            if (currency == null)
            {
                return 0m;
            }

            return currency.SellRateToUzs > 0m ? currency.SellRateToUzs : currency.RateToUzs;
        }

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", notes.Select(note => $"{note.DenominationValue:N2} x {note.NoteCount}"));
        }

        public IEnumerable<TransactionDto> GetCurrentUserTransactions()
        {
            var account = SessionManager.Instance.CurrentAccount;
            if (account == null)
            {
                return Enumerable.Empty<TransactionDto>();
            }

            return _transactionRepository.GetAccountTransactions(account.Id);
        }

        public IEnumerable<TransactionDto> GetCurrentCardTransactions()
        {
            var card = SessionManager.Instance.CurrentCard;
            if (card == null)
            {
                return Enumerable.Empty<TransactionDto>();
            }

            return _transactionRepository.GetCardTransactions(card.Id);
        }

        private async Task<AccountDto> RefreshCurrentSessionAsync()
        {
            var refreshed = await _accountRepository.GetAccountByIdAsync(SessionManager.Instance.CurrentAccount.Id);
            if (refreshed != null)
            {
                SessionManager.Instance.CurrentAccount.Balance = refreshed.Balance;
                SessionManager.Instance.CurrentAccount.IsActive = refreshed.IsActive;
                SessionManager.Instance.CurrentAccount.AccountNumber = refreshed.AccountNumber;
            }

            if (SessionManager.Instance.CurrentCard != null)
            {
                var refreshedCard = _cardRepository.GetCardByNumber(SessionManager.Instance.CurrentCard.CardNumber);
                if (refreshedCard != null)
                {
                    SessionManager.Instance.CurrentCard.Balance = refreshedCard.Balance;
                    SessionManager.Instance.CurrentCard.IsBlocked = refreshedCard.IsBlocked;
                    SessionManager.Instance.CurrentCard.ExpiryDate = refreshedCard.ExpiryDate;
                    SessionManager.Instance.CurrentCard.CardType = refreshedCard.CardType;
                }
            }

            return refreshed;
        }
    }
}
