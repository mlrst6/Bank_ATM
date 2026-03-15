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
                string sql = "SELECT id, account_id as AccountId, card_number as CardNumber, pin_hash as PinHash, is_blocked as IsBlocked, expiry_date as ExpiryDate FROM cards WHERE card_number = @CardNumber";
                return db.QueryFirstOrDefault<CardDto>(sql, new { CardNumber = cardNumber });
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
                string sql = "UPDATE cards SET pin_hash = @PinHash WHERE card_number = @CardNumber";
                db.Execute(sql, new { PinHash = newHashedPin, CardNumber = cardNumber });
            }
        }

        public bool ValidatePin(string cardNumber, string inputPin)
        {
            var card = GetCardByNumber(cardNumber);
            if (card == null || card.IsBlocked) return false;

            return BCrypt.Net.BCrypt.Verify(inputPin, card.PinHash);
        }
    }
}
