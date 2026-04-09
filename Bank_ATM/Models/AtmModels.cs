using System;

namespace Bank_ATM.Models
{
    public enum UserRole 
    { 
        Guest, 
        User, 
        Admin 
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
    }

    public class CardDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string PinHash { get; set; }
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
        public string Type { get; set; } // Withdraw, Deposit, Transfer, BillPayment, Exchange
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class ServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Category { get; set; }
        public string AccountHint { get; set; }
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
}

