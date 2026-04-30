using System;

namespace Bank_ATM.Models
{
    public enum UserRole 
    { 
        Guest,
        User,
        Admin
    }

    public static class CardTypes
    {
        public const string Humo = "HUMO";
        public const string Uzcard = "UZCARD";
        public const string Visa = "VISA";
        public const string Mastercard = "MASTERCARD";

        public static readonly string[] All = { Humo, Uzcard, Visa, Mastercard };

        public static string Normalize(string cardType)
        {
            string normalized = (cardType ?? string.Empty).Trim().ToUpperInvariant();
            foreach (string supported in All)
            {
                if (normalized == supported)
                {
                    return supported;
                }
            }

            return Uzcard;
        }

        public static string GetPrefix(string cardType)
        {
            switch (Normalize(cardType))
            {
                case Humo:
                    return "9860";
                case Visa:
                case Mastercard:
                    return "4916";
                default:
                    return "8600";
            }
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? PrimaryAccountId { get; set; }
        public string PrimaryAccountNumber { get; set; }
        public string DisplayName => $"{FullName} ({Username})";
        public string AccountDisplayName => string.IsNullOrWhiteSpace(PrimaryAccountNumber)
            ? DisplayName
            : $"{FullName} ({Username}) - {PrimaryAccountNumber}";
    }

    public class CardDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string PinHash { get; set; }
        public decimal Balance { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int FailedAttempts { get; set; }
        public DateTime? LockedUntil { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AccountDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TransactionDto
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? TargetAccountId { get; set; }
        public int? CardId { get; set; }
        public int? TargetCardId { get; set; }
        public string Type { get; set; } // Withdraw, Deposit, Transfer, BillPayment, Exchange
        public decimal Amount { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal TotalDebited { get; set; }
        public decimal NetAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string RateKind { get; set; }
        public string Description { get; set; }
        public int? ServiceId { get; set; }
        public int? ServiceAccountId { get; set; }
        public string PaymentReference { get; set; }
        public decimal CashbackAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class ServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Category { get; set; }
        public string AccountHint { get; set; }
        public decimal CashbackPercent { get; set; }
        public bool IsActive { get; set; }
        public int ValidReferenceCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ServiceAccountDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int? UserId { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
        public string UserFullName { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AtmDto
    {
        public int Id { get; set; }
        public string AtmName { get; set; }
        public decimal CurrentBalance { get; set; }
        public string Location { get; set; }
    }

    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CurrencyName { get; set; }
        public decimal RateToUzs { get; set; }
        public decimal BuyRateToUzs { get; set; }
        public decimal SellRateToUzs { get; set; }
        public bool IsActive { get; set; }
        public decimal CashAvailable { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class FeeRuleDto
    {
        public int Id { get; set; }
        public string CardType { get; set; }
        public string TransactionType { get; set; }
        public decimal PercentFee { get; set; }
        public decimal FixedFee { get; set; }
        public decimal MinFee { get; set; }
        public decimal? MaxFee { get; set; }
        public bool IsActive { get; set; }
    }

    public class CashDenominationDto
    {
        public int AtmId { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal DenominationValue { get; set; }
        public int NoteCount { get; set; }
        public decimal TotalValue => DenominationValue * NoteCount;
        public DateTime UpdatedAt { get; set; }
    }

    public class CashNoteDto
    {
        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal DenominationValue { get; set; }
        public int NoteCount { get; set; }
        public decimal TotalValue => DenominationValue * NoteCount;
    }
}
