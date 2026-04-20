using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
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
                string sql = @"SELECT
                                id,
                                account_id as AccountId,
                                card_number as CardNumber,
                                card_type as CardType,
                                balance,
                                pin_hash as PinHash,
                                is_blocked as IsBlocked,
                                expiry_date as ExpiryDate,
                                failed_attempts as FailedAttempts,
                                locked_until as LockedUntil
                               FROM cards
                               WHERE card_number = @CardNumber";
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
                string sql = @"SELECT
                                id,
                                account_id as AccountId,
                                card_number as CardNumber,
                                card_type as CardType,
                                balance,
                                pin_hash as PinHash,
                                is_blocked as IsBlocked,
                                expiry_date as ExpiryDate,
                                failed_attempts as FailedAttempts,
                                locked_until as LockedUntil,
                                created_at as CreatedAt
                               FROM cards";
                return db.Query<CardDto>(sql);
            }
        }

        public string GenerateUniqueCardNumber(string cardType = null)
        {
            string prefix = CardTypes.GetPrefix(cardType);
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                for (int attempt = 0; attempt < 20; attempt++)
                {
                    string candidate = GenerateCardNumberCandidate(prefix);
                    int exists = db.ExecuteScalar<int>(
                        "SELECT COUNT(*) FROM cards WHERE card_number = @CardNumber",
                        new { CardNumber = candidate });
                    if (exists == 0)
                    {
                        return candidate;
                    }
                }
            }

            throw new InvalidOperationException("Could not generate a unique card number.");
        }

        public void CreateCard(CardDto card, string pin)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var accountExists = db.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM accounts WHERE id = @Id AND is_active = 1",
                    new { Id = card.AccountId }) > 0;
                if (!accountExists)
                {
                    throw new InvalidOperationException("The selected account does not exist or is inactive.");
                }

                if (string.IsNullOrWhiteSpace(card.CardNumber))
                {
                    card.CardNumber = GenerateUniqueCardNumber(card.CardType);
                }

                string cardType = CardTypes.Normalize(card.CardType);
                string hashedPin = BCrypt.Net.BCrypt.HashPassword(pin, 11);
                string sql = @"INSERT INTO cards (account_id, card_number, card_type, balance, pin_hash, expiry_date) 
                               VALUES (@AccountId, @CardNumber, @CardType, @Balance, @PinHash, @ExpiryDate)";
                db.Execute(sql, new { card.AccountId, card.CardNumber, CardType = cardType, card.Balance, PinHash = hashedPin, card.ExpiryDate });
            }
        }

        public void UpdateCard(CardDto card, string newPin = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                card.CardType = CardTypes.Normalize(card.CardType);
                string sql = "UPDATE cards SET card_number = @CardNumber, card_type = @CardType, balance = @Balance, is_blocked = @IsBlocked, expiry_date = @ExpiryDate";
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
                decimal balance = db.ExecuteScalar<decimal>("SELECT balance FROM cards WHERE id = @Id", new { Id = cardId });
                if (balance > 0m)
                {
                    throw new InvalidOperationException("Cannot delete a card that still has money on it.");
                }

                db.Execute("DELETE FROM cards WHERE id = @Id", new { Id = cardId });
            }
        }

        public bool ValidatePin(string cardNumber, string inputPin, out string message)
        {
            message = "";
            string sanitizedCardNumber = (cardNumber ?? string.Empty).Replace("-", "").Replace(" ", "").Trim();
            string sanitizedPin = (inputPin ?? string.Empty).Trim();

            if (sanitizedCardNumber.Length != 16 || !long.TryParse(sanitizedCardNumber, out _))
            {
                message = LanguageManager.GetString("InvalidCardNumberMessage");
                return false;
            }

            if (string.IsNullOrWhiteSpace(sanitizedPin))
            {
                message = "PIN is required.";
                return false;
            }

            var card = GetCardByNumber(sanitizedCardNumber);
            if (card == null)
            {
                message = LanguageManager.GetString("CardNotRecognized");
                return false;
            }

            if (card.IsBlocked)
            {
                message = LanguageManager.GetString("CardBlocked");
                return false;
            }

            if (card.ExpiryDate.Date < DateTime.Today)
            {
                message = LanguageManager.GetString("CardExpired");
                return false;
            }

            if (card.LockedUntil.HasValue && card.LockedUntil.Value > DateTime.Now)
            {
                var remaining = card.LockedUntil.Value - DateTime.Now;
                int remainingMinutes = (int)Math.Ceiling(remaining.TotalMinutes);
                message = $"Card is temporarily locked. Try again in {Math.Max(1, remainingMinutes)} minute(s).";
                return false;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(sanitizedPin, card.PinHash);
             
            if (isValid)
            {
                ResetFailedAttempts(sanitizedCardNumber);
            }
            else
            {
                IncrementFailedAttempts(sanitizedCardNumber);
                var updatedCard = GetCardByNumber(sanitizedCardNumber);
                int attemptsLeft = 3 - updatedCard.FailedAttempts;
                if (attemptsLeft > 0)
                    message = $"Invalid PIN. {attemptsLeft} attempts remaining.";
                else
                    message = "Too many failed attempts. Card locked for 15 minutes.";
            }

            return isValid;
        }

        private static string GenerateCardNumberCandidate(string prefix)
        {
            byte[] bytes = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            ulong number = BitConverter.ToUInt64(bytes, 0) % 1000000000000UL;
            return prefix + number.ToString("D12");
        }
    }
}
