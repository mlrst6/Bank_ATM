using Bank_ATM.Models;

namespace Bank_ATM.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ReceiptPath { get; set; }
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

    public class GuestExchangeResult : ServiceResult
    {
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public decimal SourceAmount { get; set; }
        public decimal SourceAmountUzs { get; set; }
        public decimal RequestedTargetAmount { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal Rate { get; set; }
        public bool IsApproximateAmount { get; set; }
        public decimal UnavailableAmount { get; set; }
        public CashNoteDto[] InsertedNotes { get; set; }
        public CashNoteDto[] DispensedNotes { get; set; }
    }

    public class ServiceLookupResult : ServiceResult
    {
        public ServiceDto Service { get; set; }
        public ServiceAccountDto ServiceAccount { get; set; }
    }

    public class CardAccessResult : ServiceResult
    {
        public string SanitizedCardNumber { get; set; }
        public CardDto Card { get; set; }
    }
}
