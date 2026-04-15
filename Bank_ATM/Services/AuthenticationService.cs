using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Bank_ATM.Core;
using Bank_ATM.Repositories;

namespace Bank_ATM.Services
{
    public class AuthenticationService
    {
        private const int MaxAdminAttempts = 5;
        private static readonly TimeSpan AdminLockoutDuration = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan FailedLoginDelay = TimeSpan.FromMilliseconds(700);
        private static readonly ConcurrentDictionary<string, AdminLoginAttemptState> AdminAttempts =
            new ConcurrentDictionary<string, AdminLoginAttemptState>(StringComparer.OrdinalIgnoreCase);

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
            string sanitizedCardNumber = SanitizeCardNumber(cardNumber);
            var cardAccess = ValidateCardForLogin(sanitizedCardNumber);
            if (!cardAccess.Success)
            {
                await Task.Delay(FailedLoginDelay);
                AuditLogger.LogWarning($"Failed card login pre-check for card {sanitizedCardNumber}: {cardAccess.Message}");
                return new AuthenticationResult { Success = false, Message = cardAccess.Message };
            }

            string message = string.Empty;
            bool isValid = await Task.Run(() => _cardRepository.ValidatePin(sanitizedCardNumber, pin, out message));
            if (!isValid)
            {
                await Task.Delay(FailedLoginDelay);
                AuditLogger.LogWarning($"Failed login attempt for card {sanitizedCardNumber}: {message}");
                return new AuthenticationResult { Success = false, Message = message };
            }

            var card = _cardRepository.GetCardByNumber(sanitizedCardNumber);
            if (card == null)
            {
                await Task.Delay(FailedLoginDelay);
                return new AuthenticationResult { Success = false, Message = LanguageManager.GetString("CardNotRecognized") };
            }

            var account = await _accountRepository.GetAccountByIdAsync(card.AccountId);
            if (account == null || !account.IsActive)
            {
                await Task.Delay(FailedLoginDelay);
                return new AuthenticationResult { Success = false, Message = "The linked account is not available." };
            }

            var user = await _accountRepository.GetUserByIdAsync(account.UserId);
            if (user == null)
            {
                await Task.Delay(FailedLoginDelay);
                return new AuthenticationResult { Success = false, Message = "The linked user profile was not found." };
            }

            SessionManager.Instance.Login(user, card, account);
            AuditLogger.LogInfo($"User {user.FullName} logged in with card {sanitizedCardNumber}");

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
            string normalizedUsername = NormalizeUsername(username);
            if (string.IsNullOrWhiteSpace(normalizedUsername) || string.IsNullOrWhiteSpace(password))
            {
                await Task.Delay(FailedLoginDelay);
                return new AuthenticationResult { Success = false, Message = "Invalid username or password." };
            }

            if (TryGetAdminLockoutMessage(normalizedUsername, out string lockoutMessage))
            {
                AuditLogger.LogWarning($"Blocked admin login attempt for username: {normalizedUsername}. Reason: {lockoutMessage}");
                return new AuthenticationResult { Success = false, Message = lockoutMessage };
            }

            var admin = await _accountRepository.GetAdminByUsernameAsync(normalizedUsername);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                await Task.Delay(FailedLoginDelay);
                string failureMessage = RecordAdminFailure(normalizedUsername);
                AuditLogger.LogWarning($"Failed Admin login attempt for username: {normalizedUsername}");
                return new AuthenticationResult { Success = false, Message = failureMessage };
            }

            if (!admin.IsActive)
            {
                await Task.Delay(FailedLoginDelay);
                AuditLogger.LogWarning($"Inactive admin login attempt for username: {normalizedUsername}");
                return new AuthenticationResult { Success = false, Message = "Invalid username or password." };
            }

            ResetAdminFailures(normalizedUsername);
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

        private static string SanitizeCardNumber(string rawCardNumber)
        {
            return (rawCardNumber ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
        }

        private static string NormalizeUsername(string username)
        {
            return (username ?? string.Empty).Trim();
        }

        private static bool TryGetAdminLockoutMessage(string username, out string message)
        {
            message = null;
            if (!AdminAttempts.TryGetValue(username, out AdminLoginAttemptState state))
            {
                return false;
            }

            lock (state)
            {
                if (!state.LockedUntil.HasValue || state.LockedUntil.Value <= DateTime.UtcNow)
                {
                    if (state.LockedUntil.HasValue)
                    {
                        state.LockedUntil = null;
                        state.FailureCount = 0;
                    }

                    return false;
                }

                TimeSpan remaining = state.LockedUntil.Value - DateTime.UtcNow;
                int remainingMinutes = (int)Math.Ceiling(remaining.TotalMinutes);
                if (remainingMinutes < 1)
                {
                    remainingMinutes = 1;
                }

                message = $"Too many failed attempts. Try again in {remainingMinutes} minute(s).";
                return true;
            }
        }

        private static string RecordAdminFailure(string username)
        {
            var state = AdminAttempts.GetOrAdd(username, _ => new AdminLoginAttemptState());
            lock (state)
            {
                if (state.LockedUntil.HasValue && state.LockedUntil.Value > DateTime.UtcNow)
                {
                    TimeSpan remaining = state.LockedUntil.Value - DateTime.UtcNow;
                    int remainingMinutes = (int)Math.Ceiling(remaining.TotalMinutes);
                    return $"Too many failed attempts. Try again in {Math.Max(1, remainingMinutes)} minute(s).";
                }

                state.FailureCount++;
                if (state.FailureCount >= MaxAdminAttempts)
                {
                    state.LockedUntil = DateTime.UtcNow.Add(AdminLockoutDuration);
                    return $"Too many failed attempts. Try again in {(int)AdminLockoutDuration.TotalMinutes} minute(s).";
                }

                return "Invalid username or password.";
            }
        }

        private static void ResetAdminFailures(string username)
        {
            AdminAttempts.TryRemove(username, out _);
        }

        private sealed class AdminLoginAttemptState
        {
            public int FailureCount { get; set; }
            public DateTime? LockedUntil { get; set; }
        }
    }
}
