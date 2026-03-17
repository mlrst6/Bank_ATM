using System;
using Bank_ATM.Models;

namespace Bank_ATM.Core
{
    /// <summary>
    /// Holds the state of the currently logged-in user and their active card/account.
    /// This prevents us from having to pass data between every single Form constructor.
    /// </summary>
    public static class SessionManager
    {
        public static UserRole CurrentRole { get; private set; } = UserRole.Guest;
        public static UserDto CurrentUser { get; private set; }
        public static CardDto CurrentCard { get; private set; }
        public static AccountDto CurrentAccount { get; private set; }
        public static AtmDto CurrentAtm { get; private set; } // The physical machine being used

        public static bool IsLoggedIn => CurrentUser != null && CurrentCard != null;

        // Event for UI to react to session changes
        public static event Action OnSessionChanged;

        public static void Login(UserDto user, CardDto card, AccountDto account)
        {
            CurrentUser = user;
            CurrentCard = card;
            CurrentAccount = account;

            // Map string role from DB to Enum
            if (user.Role == "Admin") CurrentRole = UserRole.Admin;
            else CurrentRole = UserRole.User;

            TimeoutManager.Start(); // Start the inactivity timer
            OnSessionChanged?.Invoke();
        }

        public static void Logout()
        {
            CurrentUser = null;
            CurrentCard = null;
            CurrentAccount = null;
            CurrentRole = UserRole.Guest;
            TimeoutManager.Stop();
            OnSessionChanged?.Invoke();
        }

        public static void SetCurrentAtm(AtmDto atm)
        {
            CurrentAtm = atm;
        }
    }
}
