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

        public async Task<BankingResult> WithdrawAsync(decimal amount)
        {
            return await WithdrawAsync(amount, "UZS");
        }

        public async Task<BankingResult> WithdrawAsync(decimal amount, string currencyCode)
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

            decimal debitAmountUzs = decimal.Round(amount * currency.RateToUzs, 2);
            if (currency.CashAvailable < amount)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("AtmInsufficientCash") };
            }

            bool success = normalizedCurrency == "UZS"
                ? await _accountRepository.WithdrawAsync(currentAccount.Id, amount)
                : await _accountRepository.WithdrawCurrencyAsync(currentAccount.Id, amount, debitAmountUzs, currency.Id, normalizedCurrency);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
            }

            var account = await RefreshCurrentSessionAccountAsync();
            return new BankingResult
            {
                Success = true,
                Account = account,
                DebitedAmountUzs = debitAmountUzs,
                CashCurrencyCode = normalizedCurrency,
                CashAmount = amount
            };
        }

        public CurrencyDto[] GetActiveCurrencies()
        {
            return _currencyRepository.GetActiveCurrencies().ToArray();
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

            var account = await RefreshCurrentSessionAccountAsync();
            return new BankingResult
            {
                Success = true,
                Account = account,
                DebitedAmountUzs = creditAmountUzs,
                CashCurrencyCode = normalizedCurrency,
                CashAmount = amount
            };
        }

        public async Task<BankingResult> TransferByCardAsync(string targetCardNumber, decimal amount)
        {
            var currentAccount = SessionManager.Instance.CurrentAccount;
            if (currentAccount == null || !currentAccount.IsActive)
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

            if (targetCard.AccountId == currentAccount.Id)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferSameAccount") };
            }

            bool success = await _accountRepository.TransferAsync(currentAccount.Id, targetCard.AccountId, amount);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferFailed") };
            }

            var account = await RefreshCurrentSessionAccountAsync();
            return new BankingResult { Success = true, Account = account, TargetCard = targetCard };
        }

        public ServiceDto[] GetAvailableServices()
        {
            return _servicesRepository.GetActiveServices().ToArray();
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
                if (currentAccount == null || !currentAccount.IsActive)
                {
                    return new ServiceResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
                }

                bool success = await _accountRepository.PayServiceAsync(
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

                await RefreshCurrentSessionAccountAsync();
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

        public string GenerateReceiptForCurrentSession(TransactionDto transaction)
        {
            if (transaction == null ||
                SessionManager.Instance.CurrentUser == null ||
                SessionManager.Instance.CurrentAccount == null)
            {
                return null;
            }

            return ReceiptService.GenerateReceipt(
                transaction,
                SessionManager.Instance.CurrentUser.FullName,
                SessionManager.Instance.CurrentAccount.Balance);
        }

        private static string SanitizeCardNumber(string rawCardNumber)
        {
            return (rawCardNumber ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
        }

        private static bool IsValidMoneyAmount(decimal amount)
        {
            return amount > 0m && decimal.Round(amount, 2) == amount;
        }

        private async Task<AccountDto> RefreshCurrentSessionAccountAsync()
        {
            var refreshed = await _accountRepository.GetAccountByIdAsync(SessionManager.Instance.CurrentAccount.Id);
            if (refreshed != null)
            {
                SessionManager.Instance.CurrentAccount.Balance = refreshed.Balance;
                SessionManager.Instance.CurrentAccount.IsActive = refreshed.IsActive;
                SessionManager.Instance.CurrentAccount.AccountNumber = refreshed.AccountNumber;
            }

            return refreshed;
        }
    }
}
