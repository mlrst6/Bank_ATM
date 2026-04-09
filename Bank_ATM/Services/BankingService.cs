using System.Threading.Tasks;
using System.Linq;
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

        public async Task<BankingResult> WithdrawAsync(decimal amount)
        {
            if (SessionManager.Instance.CurrentAccount == null)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            bool success = await _accountRepository.WithdrawAsync(SessionManager.Instance.CurrentAccount.Id, amount);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
            }

            var account = await RefreshCurrentSessionAccountAsync();
            return new BankingResult { Success = true, Account = account };
        }

        public async Task<BankingResult> DepositAsync(decimal amount)
        {
            if (SessionManager.Instance.CurrentAccount == null)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            bool success = await _accountRepository.DepositAsync(SessionManager.Instance.CurrentAccount.Id, amount);
            if (!success)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("DepositFailed") };
            }

            var account = await RefreshCurrentSessionAccountAsync();
            return new BankingResult { Success = true, Account = account };
        }

        public async Task<BankingResult> TransferByCardAsync(string targetCardNumber, decimal amount)
        {
            if (SessionManager.Instance.CurrentAccount == null)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
            }

            string sanitizedCardNumber = targetCardNumber.Replace("-", "").Replace(" ", "");
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

            if (targetCard.AccountId == SessionManager.Instance.CurrentAccount.Id)
            {
                return new BankingResult { Success = false, Message = LanguageManager.GetString("TransferSameAccount") };
            }

            bool success = await _accountRepository.TransferAsync(SessionManager.Instance.CurrentAccount.Id, targetCard.AccountId, amount);
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
            if (string.IsNullOrWhiteSpace(accountReference) || amount <= 0)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("InvalidServicePaymentInput") };
            }

            var service = _servicesRepository.GetServiceById(serviceId);
            if (service == null || !service.IsActive)
            {
                return new ServiceResult { Success = false, Message = LanguageManager.GetString("SelectedServiceUnavailable") };
            }

            string description = $"{service.ServiceName}: {accountReference}";

            if (chargeCurrentAccount)
            {
                if (SessionManager.Instance.CurrentAccount == null)
                {
                    return new ServiceResult { Success = false, Message = LanguageManager.GetString("NoActiveAccountSession") };
                }

                bool success = await _accountRepository.PayServiceAsync(SessionManager.Instance.CurrentAccount.Id, amount, description);
                if (!success)
                {
                    return new ServiceResult { Success = false, Message = LanguageManager.GetString("InsufficientFunds") };
                }

                await RefreshCurrentSessionAccountAsync();
                AuditLogger.LogInfo($"Account service payment: {amount} UZS for {description}");
                return new ServiceResult { Success = true, Message = LanguageManager.GetString("ServicePaymentCompleted") };
            }

            _transactionRepository.AddTransaction(null, "BillPayment", amount, null, description);
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
