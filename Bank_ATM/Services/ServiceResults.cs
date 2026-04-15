using Bank_ATM.Models;

namespace Bank_ATM.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class AuthenticationResult : ServiceResult
    {
        public UserDto User { get; set; }
        public CardDto Card { get; set; }
        public AccountDto Account { get; set; }
    }

    public class BankingResult : ServiceResult
    {
        public AccountDto Account { get; set; }
        public CardDto TargetCard { get; set; }
        public decimal DebitedAmountUzs { get; set; }
        public string CashCurrencyCode { get; set; }
        public decimal CashAmount { get; set; }
        public CashNoteDto[] CashBreakdown { get; set; }
    }

    public class CardAccessResult : ServiceResult
    {
        public string SanitizedCardNumber { get; set; }
        public CardDto Card { get; set; }
    }
}
