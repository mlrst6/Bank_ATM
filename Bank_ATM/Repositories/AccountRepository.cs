using System.Data;
using System.Data.SqlClient;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class AccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public AccountDto GetAccountById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<AccountDto>("SELECT id, user_id as UserId, account_number as AccountNumber, balance FROM accounts WHERE id = @Id", new { Id = id });
            }
        }

        public void UpdateBalance(int accountId, decimal amount, bool isIncrement = false)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = isIncrement 
                    ? "UPDATE accounts SET balance = balance + @Amount WHERE id = @Id"
                    : "UPDATE accounts SET balance = @Amount WHERE id = @Id";
                    
                db.Execute(sql, new { Amount = amount, Id = accountId });
            }
        }

        public UserDto GetUserById(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<UserDto>("SELECT id, full_name as FullName, role FROM users WHERE id = @UserId", new { UserId = userId });
            }
        }
    }
}
