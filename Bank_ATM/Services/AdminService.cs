using System.Collections.Generic;
using System.Threading.Tasks;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Services
{
    public class AdminService
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();
        private readonly CardRepository _cardRepository = new CardRepository();
        private readonly TransactionRepository _transactionRepository = new TransactionRepository();
        private readonly ServicesRepository _servicesRepository = new ServicesRepository();
        private readonly AtmRepository _atmRepository = new AtmRepository();
        private readonly CurrencyRepository _currencyRepository = new CurrencyRepository();
        private readonly CashRepository _cashRepository = new CashRepository();

        public Task<IEnumerable<UserDto>> GetAllUsersAsync() => _accountRepository.GetAllUsersAsync();
        public IEnumerable<CardDto> GetAllCards() => _cardRepository.GetAllCards();
        public string GenerateCardNumber() => _cardRepository.GenerateUniqueCardNumber();
        public IEnumerable<TransactionDto> GetAllTransactions() => _transactionRepository.GetAllTransactions();
        public IEnumerable<ServiceDto> GetAllServices() => _servicesRepository.GetAllServices();
        public IEnumerable<CurrencyDto> GetAllCurrencies() => _currencyRepository.GetAllCurrencies();
        public IEnumerable<CashDenominationDto> GetAllCashDenominations() => _cashRepository.GetAllDenominations();
        public IEnumerable<CashDenominationDto> GetCashDenominations(string currencyCode) => _cashRepository.GetDenominations(currencyCode);
        public int SaveCurrency(CurrencyDto currency) => _currencyRepository.SaveCurrency(currency);
        public int SaveCurrency(CurrencyDto currency, IEnumerable<decimal> denominationValues) => _currencyRepository.SaveCurrency(currency, denominationValues);
        public void DeactivateCurrency(int currencyId) => _currencyRepository.DeactivateCurrency(currencyId);
        public IEnumerable<ServiceAccountDto> GetServiceAccounts(int serviceId) => _servicesRepository.GetServiceAccounts(serviceId);
        public AtmDto GetDefaultAtm() => _atmRepository.GetDefaultAtm();
        public void AddAtmCash(decimal amount) => _atmRepository.AddCash(amount);
        public void AddAtmCashNotes(int currencyId, IEnumerable<CashNoteDto> notes) => _cashRepository.AddCashNotes(currencyId, notes);

        public async Task<ServiceResult> SaveUserAsync(UserDto user, string password, string initialPin, bool isEdit)
        {
            if (isEdit)
            {
                await _accountRepository.UpdateUserAsync(user, password);
            }
            else
            {
                await _accountRepository.CreateUserWithProvisioningAsync(user, password, initialPin);
            }

            return new ServiceResult { Success = true };
        }

        public ServiceResult SaveCard(CardDto card, string pin, bool isEdit)
        {
            if (isEdit)
            {
                _cardRepository.UpdateCard(card, pin);
            }
            else
            {
                _cardRepository.CreateCard(card, pin);
            }

            return new ServiceResult { Success = true };
        }

        public ServiceResult SaveService(ServiceDto service, bool isEdit)
        {
            if (isEdit)
            {
                _servicesRepository.UpdateService(service);
            }
            else
            {
                _servicesRepository.CreateService(service);
            }

            return new ServiceResult { Success = true };
        }

        public void ReplaceServiceAccounts(int serviceId, IEnumerable<ServiceAccountDto> accounts)
        {
            _servicesRepository.ReplaceServiceAccounts(serviceId, accounts);
        }

        public int CreateServiceAccount(ServiceAccountDto account) => _servicesRepository.CreateServiceAccount(account);
        public void DeactivateServiceAccount(int serviceAccountId) => _servicesRepository.DeactivateServiceAccount(serviceAccountId);

        public async Task DeleteUserAsync(int userId)
        {
            if (SessionManager.Instance.CurrentUser != null && SessionManager.Instance.CurrentUser.Id == userId)
            {
                throw new System.InvalidOperationException("You cannot deactivate the account you are currently using.");
            }

            await _accountRepository.DeleteUserAsync(userId);
        }

        public void DeleteCard(int cardId) => _cardRepository.DeleteCard(cardId);
        public void DeleteService(int serviceId) => _servicesRepository.DeleteService(serviceId);
    }
}
