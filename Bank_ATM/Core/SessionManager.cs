using System;
using Bank_ATM.Models;

namespace Bank_ATM.Core
{
    /// <summary>
    /// Holds the state of the currently logged-in user and their active card/account.
    /// This prevents us from having to pass data between every single Form constructor.
    /// </summary>
    public class SessionManager : IUserSession
    {
        // Singleton instance for legacy/easy access, but can be injected via Interface
        public static SessionManager Instance { get; } = new SessionManager();

        public UserRole CurrentRole { get; private set; } = UserRole.Guest;
        public UserDto CurrentUser { get; private set; }
        public CardDto CurrentCard { get; private set; }
        public AccountDto CurrentAccount { get; private set; }
        public static AtmDto CurrentAtm { get; private set; } 

        public bool IsLoggedIn => CurrentUser != null && CurrentCard != null;

        // Legacy static access for compatibility during transition
        public static bool IsLoggedInStatic => Instance.IsLoggedIn;
        public static UserRole CurrentRoleStatic => Instance.CurrentRole;
        public static UserDto CurrentUserStatic => Instance.CurrentUser;
        public static CardDto CurrentCardStatic => Instance.CurrentCard;
        public static AccountDto CurrentAccountStatic => Instance.CurrentAccount;

        public static event Action OnSessionChanged;

        public void Login(UserDto user, CardDto card, AccountDto account)
        {
            CurrentUser = user;
            CurrentCard = card;
            CurrentAccount = account;

            if (user.Role == "Admin") CurrentRole = UserRole.Admin;
            else CurrentRole = UserRole.User;

            TimeoutManager.Start(); 
            OnSessionChanged?.Invoke();
        }

        public void Logout()
        {
            CurrentUser = null;
            CurrentCard = null;
            CurrentAccount = null;
            CurrentRole = UserRole.Guest;
            TimeoutManager.Stop();
            OnSessionChanged?.Invoke();
        }

        // Static wrappers for legacy code
        public static void LoginStatic(UserDto user, CardDto card, AccountDto account) => Instance.Login(user, card, account);
        public static void LogoutStatic() => Instance.Logout();

        public static void SetCurrentAtm(AtmDto atm)
        {
            CurrentAtm = atm;
        }
    }
}
