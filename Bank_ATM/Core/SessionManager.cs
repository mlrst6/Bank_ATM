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
        public static SessionManager Instance { get; } = new SessionManager();

        public UserRole CurrentRole { get; private set; } = UserRole.Guest;
        public UserDto CurrentUser { get; private set; }
        public CardDto CurrentCard { get; private set; }
        public AccountDto CurrentAccount { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsCardSession => CurrentUser != null && CurrentCard != null && CurrentAccount != null;

        public static event Action OnSessionChanged;

        public void Login(UserDto user, CardDto card, AccountDto account)
        {
            CurrentUser = user;
            CurrentCard = card;
            CurrentAccount = account;

            if (user == null) CurrentRole = UserRole.Guest;
            else if (user.Role == "Admin") CurrentRole = UserRole.Admin;
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
    }
}
