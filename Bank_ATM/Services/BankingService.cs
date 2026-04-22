using System.Threading.Tasks;
using System.Linq;
using System;
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

            decimal debitAmountUzs = decimal.Round(amount * currency.RateToUzs, 2);
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

            decimal creditAmountUzs = decimal.Round(amount * currency.RateToUzs, 2);

            bool success = normalizedCurrency == "UZS"
                ? await _accountRepository.DepositAsync(currentAccount.Id, amount)
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
                DebitedAmountUzs = creditAmountUzs,
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

            decimal creditAmountUzs = decimal.Round(cashAmount * currency.RateToUzs, 2);
            var savedNotes = await _accountRepository.DepositCurrencyWithDenominationsByCardAsync(
                currentCard.Id,
                currentAccount.Id,
                noteList,
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
                DebitedAmountUzs = creditAmountUzs,
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

            bool success = await _accountRepository.TransferByCardAsync(currentCard.Id, targetCard.Id, amount);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferFailed") };
            }

            var account = await RefreshCurrentSessionAsync();
            return new BankingResult { Success = true, Account = account, TargetCard = targetCard };
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

                bool success = await _accountRepository.PayServiceByCardAsync(
                    currentCard.Id,
                    currentAccount.Id,
                    amount,
                    description,
                    service.Id,
                    serviceAccount.Id,
                    sanitizedReference);
                if (!success)
                {
                    return new ServiceResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
                }

                await RefreshCurrentSessionAsync();
                AuditLogger.LogInfo($"Account service payment: {amount} UZS for {description}");
                return new ServiceResult { Success = true, Message = LanguageManager.GetString("ServicePaymentCompleted") };
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
                ? $"{service.ServiceName}: {sanitizedReference}; Cash notes: {FormatNotes(noteList)}"
                : $"{service.ServiceName}: {sanitizedReference} ({serviceAccount.CustomerName}); Cash notes: {FormatNotes(noteList)}";

            bool success = await _cashRepository.AddGuestCashPaymentAsync(
                service.Id,
                serviceAccount.Id,
                sanitizedReference,
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
                Message = LanguageManager.GetString("ServicePaymentCompleted")
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

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", notes.Select(note => $"{note.DenominationValue:N2} x {note.NoteCount}"));
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
