using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class TransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public void AddTransaction(int? sourceAccountId, string type, decimal amount, int? destinationAccountId = null, string reference = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Note: The database schema needs to be updated to support these columns if they don't exist yet.
                // For now, we use existing columns or add logic to handle them.
                string sql = @"INSERT INTO transactions (account_id, type, amount, transaction_date) 
                               VALUES (@AccountId, @Type, @Amount, @Date)";
                
                db.Execute(sql, new { 
                    AccountId = sourceAccountId, 
                    Type = type, 
                    Amount = amount, 
                    Date = DateTime.Now 
                });
            }
        }

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, account_id as SourceAccountId, type, amount, transaction_date as TransactionDate FROM transactions ORDER BY transaction_date DESC";
                return db.Query<TransactionDto>(sql);
            }
        }
    }
}
