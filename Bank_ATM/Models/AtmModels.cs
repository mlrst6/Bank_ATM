using System;

namespace Bank_ATM.Models
{
    public enum UserRole 
    { 
        Guest, 
        CardUser, 
        Admin 
    }

    public enum TransactionType 
    { 
        Withdraw, 
        Deposit, 
        Transfer, 
        BillPayment, 
        CurrencyExchange 
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // Stored as string in DB, mapped to Enum in logic
    }

    public class CardDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string PinHash { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class AccountDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }

    public class TransactionDto
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        
        // Nullable for Guest transactions
        public int? SourceAccountId { get; set; }
        public int? DestinationAccountId { get; set; }
        
        public string ReferenceNumber { get; set; } // Phone or Utility ID
        public string CurrencyPair { get; set; }    // e.g. USD/UZS
    }

    public class AtmDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Status { get; set; }
        public decimal CurrentCash { get; set; }
    }
}
