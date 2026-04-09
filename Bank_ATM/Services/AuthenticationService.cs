using System.Threading.Tasks;
using Bank_ATM.Core;
using Bank_ATM.Repositories;

namespace Bank_ATM.Services
{
    public class AuthenticationService
    {
        private readonly CardRepository _cardRepository = new CardRepository();
        private readonly AccountRepository _accountRepository = new AccountRepository();

        public CardAccessResult ValidateCardForLogin(string rawCardNumber)
        {
            string sanitizedCardNumber = (rawCardNumber ?? string.Empty).Replace("-", "").Replace(" ", "");
            if (sanitizedCardNumber.Length != 16 || !long.TryParse(sanitizedCardNumber, out _))
            {
                return new CardAccessResult
                {
                    Success = false,
                    Message = LanguageManager.GetString("InvalidCardNumberMessage")
                };
            }

            var card = _cardRepository.GetCardByNumber(sanitizedCardNumber);
            if (card == null)
            {
                return new CardAccessResult
                {
                    Success = false,
                    Message = LanguageManager.GetString("CardNotRecognized")
                };
            }

            if (card.IsBlocked)
            {
                return new CardAccessResult
                {
                    Success = false,
                    Message = LanguageManager.GetString("CardBlocked")
                };
            }

            if (card.ExpiryDate.Date < System.DateTime.Today)
            {
                return new CardAccessResult
                {
                    Success = false,
                    Message = LanguageManager.GetString("CardExpired")
                };
            }

            return new CardAccessResult
            {
                Success = true,
                SanitizedCardNumber = sanitizedCardNumber,
                Card = card
            };
        }

        public async Task<AuthenticationResult> LoginWithCardAsync(string cardNumber, string pin)
        {
            string message = string.Empty;
            bool isValid = await Task.Run(() => _cardRepository.ValidatePin(cardNumber, pin, out message));
            if (!isValid)
            {
                AuditLogger.LogWarning($"Failed login attempt for card {cardNumber}: {message}");
                return new AuthenticationResult { Success = false, Message = message };
            }

            var card = _cardRepository.GetCardByNumber(cardNumber);
            if (card == null)
            {
                return new AuthenticationResult { Success = false, Message = "Card not found." };
            }

            var account = await _accountRepository.GetAccountByIdAsync(card.AccountId);
            if (account == null || !account.IsActive)
            {
                return new AuthenticationResult { Success = false, Message = "The linked account is not available." };
            }

            var user = await _accountRepository.GetUserByIdAsync(account.UserId);
            if (user == null)
            {
                return new AuthenticationResult { Success = false, Message = "The linked user profile was not found." };
            }

            SessionManager.Instance.Login(user, card, account);
            AuditLogger.LogInfo($"User {user.FullName} logged in with card {cardNumber}");

            return new AuthenticationResult
            {
                Success = true,
                User = user,
                Card = card,
                Account = account
            };
        }

        public async Task<AuthenticationResult> LoginAdminAsync(string username, string password)
        {
            var admin = await _accountRepository.GetAdminByUsernameAsync(username);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                AuditLogger.LogWarning($"Failed Admin login attempt for username: {username}");
                return new AuthenticationResult { Success = false, Message = "Invalid username or password." };
            }

            SessionManager.Instance.Login(admin, null, null);
            AuditLogger.LogInfo($"Admin {admin.FullName} logged in via credentials.");

            return new AuthenticationResult
            {
                Success = true,
                User = admin
            };
        }

        public void Logout()
        {
            SessionManager.Instance.Logout();
        }
    }
}
