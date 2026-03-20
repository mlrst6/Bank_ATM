using Bank_ATM.Models;

namespace Bank_ATM.Core
{
    public interface IUserSession
    {
        UserRole CurrentRole { get; }
        UserDto CurrentUser { get; }
        CardDto CurrentCard { get; }
        AccountDto CurrentAccount { get; }
        bool IsLoggedIn { get; }
        
        void Login(UserDto user, CardDto card, AccountDto account);
        void Logout();
    }
}
