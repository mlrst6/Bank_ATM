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

        public void AddTransaction(int? sourceAccountId, string type, decimal amount, int? destinationAccountId = null, string description = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO transactions (account_id, target_account_id, type, amount, description, transaction_date) 
                               VALUES (@AccountId, @TargetAccountId, @Type, @Amount, @Description, @Date)";
                
                db.Execute(sql, new { 
                    AccountId = sourceAccountId, 
                    TargetAccountId = destinationAccountId,
                    Type = type, 
                    Amount = amount, 
                    Description = description,
                    Date = DateTime.Now 
                });
            }
        }

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, account_id as AccountId, target_account_id as TargetAccountId, type, amount, description, transaction_date as TransactionDate FROM transactions ORDER BY transaction_date DESC";
                return db.Query<TransactionDto>(sql);
            }
        }

        public IEnumerable<TransactionDto> GetAccountTransactions(int accountId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT id, account_id as AccountId, target_account_id as TargetAccountId, type, amount, description, transaction_date as TransactionDate FROM transactions WHERE account_id = @AccountId OR target_account_id = @AccountId ORDER BY transaction_date DESC";
                return db.Query<TransactionDto>(sql, new { AccountId = accountId });
            }
        }
    }
}
