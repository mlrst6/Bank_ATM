using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class CardRepository
    {
        private readonly string _connectionString;

        public CardRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public CardDto GetCardByNumber(string cardNumber)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, account_id as AccountId, card_number as CardNumber, pin_hash as PinHash, is_blocked as IsBlocked, expiry_date as ExpiryDate, failed_attempts as FailedAttempts, locked_until as LockedUntil FROM cards WHERE card_number = @CardNumber";
                return db.QueryFirstOrDefault<CardDto>(sql, new { CardNumber = cardNumber });
            }
        }

        public void IncrementFailedAttempts(string cardNumber)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE cards SET failed_attempts = failed_attempts + 1, 
                               locked_until = CASE WHEN failed_attempts + 1 >= 3 THEN DATEADD(minute, 15, GETDATE()) ELSE NULL END
                               WHERE card_number = @CardNumber";
                db.Execute(sql, new { CardNumber = cardNumber });
            }
        }

        public void ResetFailedAttempts(string cardNumber)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE cards SET failed_attempts = 0, locked_until = NULL WHERE card_number = @CardNumber";
                db.Execute(sql, new { CardNumber = cardNumber });
            }
        }

        public void BlockCard(string cardNumber)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE cards SET is_blocked = 1 WHERE card_number = @CardNumber";
                db.Execute(sql, new { CardNumber = cardNumber });
            }
        }

        public void UpdatePin(string cardNumber, string newHashedPin)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE cards SET pin_hash = @PinHash, failed_attempts = 0, locked_until = NULL WHERE card_number = @CardNumber";
                db.Execute(sql, new { PinHash = newHashedPin, CardNumber = cardNumber });
            }
        }

        public IEnumerable<CardDto> GetAllCards()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, account_id as AccountId, card_number as CardNumber, pin_hash as PinHash, is_blocked as IsBlocked, expiry_date as ExpiryDate, failed_attempts as FailedAttempts, locked_until as LockedUntil, created_at as CreatedAt FROM cards";
                return db.Query<CardDto>(sql);
            }
        }

        public void CreateCard(CardDto card, string pin)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string hashedPin = BCrypt.Net.BCrypt.HashPassword(pin, 11);
                string sql = @"INSERT INTO cards (account_id, card_number, pin_hash, expiry_date) 
                               VALUES (@AccountId, @CardNumber, @PinHash, @ExpiryDate)";
                db.Execute(sql, new { card.AccountId, card.CardNumber, PinHash = hashedPin, card.ExpiryDate });
            }
        }

        public void UpdateCard(CardDto card, string newPin = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE cards SET card_number = @CardNumber, is_blocked = @IsBlocked, expiry_date = @ExpiryDate";
                var parameters = new DynamicParameters(card);

                if (!string.IsNullOrEmpty(newPin))
                {
                    sql += ", pin_hash = @PinHash";
                    parameters.Add("PinHash", BCrypt.Net.BCrypt.HashPassword(newPin, 11));
                }

                sql += " WHERE id = @Id";
                db.Execute(sql, parameters);
            }
        }

        public void DeleteCard(int cardId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("DELETE FROM cards WHERE id = @Id", new { Id = cardId });
            }
        }

        public bool ValidatePin(string cardNumber, string inputPin, out string message)
        {
            message = "";
            var card = GetCardByNumber(cardNumber);
            if (card == null)
            {
                message = "Card not found.";
                return false;
            }

            if (card.IsBlocked)
            {
                message = "This card is permanently blocked.";
                return false;
            }

            if (card.LockedUntil.HasValue && card.LockedUntil.Value > DateTime.Now)
            {
                var remaining = card.LockedUntil.Value - DateTime.Now;
                message = $"Card is locked. Try again in {remaining.Minutes}m {remaining.Seconds}s.";
                return false;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(inputPin, card.PinHash);
            
            if (isValid)
            {
                ResetFailedAttempts(cardNumber);
            }
            else
            {
                IncrementFailedAttempts(cardNumber);
                var updatedCard = GetCardByNumber(cardNumber);
                int attemptsLeft = 3 - updatedCard.FailedAttempts;
                if (attemptsLeft > 0)
                    message = $"Invalid PIN. {attemptsLeft} attempts remaining.";
                else
                    message = "Too many failed attempts. Card locked for 15 minutes.";
            }

            return isValid;
        }
    }
}
