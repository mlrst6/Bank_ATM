using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

        public async Task<AccountDto> GetAccountByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<AccountDto>("SELECT id, user_id as UserId, account_number as AccountNumber, balance FROM accounts WHERE id = @Id", new { Id = id });
            }
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var account = await db.QueryFirstOrDefaultAsync<AccountDto>("SELECT balance FROM accounts WHERE id = @Id", new { Id = accountId }, trans);
                        if (account == null || account.Balance < amount) return false;

                        await db.ExecuteAsync("UPDATE accounts SET balance = balance - @Amount WHERE id = @Id", new { Amount = amount, Id = accountId }, trans);
                        await db.ExecuteAsync(@"INSERT INTO transactions (account_id, type, amount, transaction_date) 
                                               VALUES (@AccountId, 'Withdraw', @Amount, GETDATE())", 
                                               new { AccountId = accountId, Amount = amount }, trans);

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> DepositAsync(int accountId, decimal amount)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        await db.ExecuteAsync("UPDATE accounts SET balance = balance + @Amount WHERE id = @Id", new { Amount = amount, Id = accountId }, trans);
                        await db.ExecuteAsync(@"INSERT INTO transactions (account_id, type, amount, transaction_date) 
                                               VALUES (@AccountId, 'Deposit', @Amount, GETDATE())", 
                                               new { AccountId = accountId, Amount = amount }, trans);

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> TransferAsync(int sourceAccountId, int targetAccountId, decimal amount)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var source = await db.QueryFirstOrDefaultAsync<AccountDto>("SELECT balance FROM accounts WHERE id = @Id", new { Id = sourceAccountId }, trans);
                        if (source == null || source.Balance < amount) return false;

                        await db.ExecuteAsync("UPDATE accounts SET balance = balance - @Amount WHERE id = @Id", new { Amount = amount, Id = sourceAccountId }, trans);
                        await db.ExecuteAsync("UPDATE accounts SET balance = balance + @Amount WHERE id = @Id", new { Amount = amount, Id = targetAccountId }, trans);
                        
                        await db.ExecuteAsync(@"INSERT INTO transactions (account_id, target_account_id, type, amount, transaction_date) 
                                               VALUES (@SourceId, @TargetId, 'Transfer', @Amount, GETDATE())", 
                                               new { SourceId = sourceAccountId, TargetId = targetAccountId, Amount = amount }, trans);

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<UserDto>("SELECT id, full_name as FullName, role, username, password_hash as PasswordHash FROM users WHERE id = @UserId", new { UserId = userId });
            }
        }

        public async Task<UserDto> GetAdminByUsernameAsync(string username)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<UserDto>("SELECT id, full_name as FullName, role, username, password_hash as PasswordHash FROM users WHERE username = @Username AND role = 'Admin'", new { Username = username });
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<UserDto>("SELECT id, full_name as FullName, username, phone_number as PhoneNumber, role, created_at as CreatedAt FROM users");
            }
        }

        public async Task<int> CreateUserAsync(UserDto user, string password)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 11);
                string sql = @"INSERT INTO users (full_name, username, password_hash, phone_number, role) 
                               VALUES (@FullName, @Username, @PasswordHash, @PhoneNumber, @Role);
                               SELECT CAST(SCOPE_IDENTITY() as int)";
                
                return await db.QuerySingleAsync<int>(sql, new { 
                    user.FullName, 
                    user.Username, 
                    PasswordHash = hashedPassword, 
                    user.PhoneNumber, 
                    user.Role 
                });
            }
        }

        public async Task UpdateUserAsync(UserDto user, string newPassword = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE users SET full_name = @FullName, username = @Username, 
                               phone_number = @PhoneNumber, role = @Role";
                
                var parameters = new DynamicParameters(user);
                
                if (!string.IsNullOrEmpty(newPassword))
                {
                    sql += ", password_hash = @PasswordHash";
                    parameters.Add("PasswordHash", BCrypt.Net.BCrypt.HashPassword(newPassword, 11));
                }

                sql += " WHERE id = @Id";
                await db.ExecuteAsync(sql, parameters);
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                await db.ExecuteAsync("DELETE FROM users WHERE id = @Id", new { Id = userId });
            }
        }

        // Legacy support for non-async calls if needed, but async is preferred
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
    }
}
