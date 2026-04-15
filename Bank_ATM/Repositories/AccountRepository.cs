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
                return await db.QueryFirstOrDefaultAsync<AccountDto>(
                    "SELECT id, user_id as UserId, account_number as AccountNumber, balance, is_active as IsActive, created_at as CreatedAt FROM accounts WHERE id = @Id",
                    new { Id = id });
            }
        }

        public async Task<AccountDto> GetAccountByUserIdAsync(int userId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<AccountDto>(
                    "SELECT TOP 1 id, user_id as UserId, account_number as AccountNumber, balance, is_active as IsActive, created_at as CreatedAt FROM accounts WHERE user_id = @UserId ORDER BY id",
                    new { UserId = userId });
            }
        }

        public async Task<bool> AccountExistsAsync(int accountId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM accounts WHERE id = @Id AND is_active = 1",
                    new { Id = accountId }) > 0;
            }
        }

        public async Task<bool> WithdrawAsync(int accountId, decimal amount)
        {
            if (!IsValidMoneyAmount(amount)) return false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var account = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT balance, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = accountId },
                            trans);
                        if (account == null || !account.IsActive || account.Balance < amount)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int atmRows = await db.ExecuteAsync(@"
                            UPDATE atm_currency_cash
                            SET cash_balance = cash_balance - @Amount,
                                updated_at = GETDATE()
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')
                              AND cash_balance >= @Amount",
                            new { Amount = amount },
                            trans);

                        if (atmRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        await db.ExecuteAsync(@"
                            UPDATE atms
                            SET current_balance = (
                                    SELECT cash_balance
                                    FROM atm_currency_cash
                                    WHERE atm_id = atms.id
                                      AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')
                                ),
                                updated_at = GETDATE()
                            WHERE id = (SELECT TOP 1 id FROM atms ORDER BY id)",
                            transaction: trans);

                        int updatedRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance - @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = amount, Id = accountId },
                            trans);
                        if (updatedRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

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

        public async Task<bool> WithdrawCurrencyAsync(
            int accountId,
            decimal cashAmount,
            decimal debitAmountUzs,
            int currencyId,
            string currencyCode)
        {
            if (!IsValidMoneyAmount(cashAmount) || !IsValidMoneyAmount(debitAmountUzs)) return false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var account = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT balance, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = accountId },
                            trans);
                        if (account == null || !account.IsActive || account.Balance < debitAmountUzs)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int cashRows = await db.ExecuteAsync(@"
                            UPDATE atm_currency_cash
                            SET cash_balance = cash_balance - @CashAmount,
                                updated_at = GETDATE()
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = @CurrencyId
                              AND cash_balance >= @CashAmount",
                            new { CashAmount = cashAmount, CurrencyId = currencyId },
                            trans);

                        if (cashRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int accountRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance - @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = debitAmountUzs, Id = accountId },
                            trans);

                        if (accountRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        await db.ExecuteAsync(@"
                            INSERT INTO transactions (account_id, type, amount, description, transaction_date)
                            VALUES (@AccountId, 'Withdraw', @Amount, @Description, GETDATE())",
                            new
                            {
                                AccountId = accountId,
                                Amount = debitAmountUzs,
                                Description = $"Cash withdrawal: {cashAmount:N2} {currencyCode}"
                            },
                            trans);

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
            if (!IsValidMoneyAmount(amount)) return false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var account = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT id, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = accountId },
                            trans);
                        if (account == null || !account.IsActive)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int updatedRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance + @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = amount, Id = accountId },
                            trans);
                        if (updatedRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        await db.ExecuteAsync(@"
                            UPDATE atm_currency_cash
                            SET cash_balance = cash_balance + @Amount,
                                updated_at = GETDATE()
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')",
                            new { Amount = amount },
                            trans);

                        await db.ExecuteAsync(@"
                            UPDATE atms
                            SET current_balance = (
                                    SELECT cash_balance
                                    FROM atm_currency_cash
                                    WHERE atm_id = atms.id
                                      AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')
                                ),
                                updated_at = GETDATE()
                            WHERE id = (SELECT TOP 1 id FROM atms ORDER BY id)",
                            transaction: trans);

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

        public async Task<bool> DepositCurrencyAsync(
            int accountId,
            decimal cashAmount,
            decimal creditAmountUzs,
            int currencyId,
            string currencyCode)
        {
            if (!IsValidMoneyAmount(cashAmount) || !IsValidMoneyAmount(creditAmountUzs)) return false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var account = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT id, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = accountId },
                            trans);
                        if (account == null || !account.IsActive)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int accountRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance + @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = creditAmountUzs, Id = accountId },
                            trans);

                        if (accountRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int cashRows = await db.ExecuteAsync(@"
                            UPDATE atm_currency_cash
                            SET cash_balance = cash_balance + @CashAmount,
                                updated_at = GETDATE()
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = @CurrencyId",
                            new { CashAmount = cashAmount, CurrencyId = currencyId },
                            trans);

                        if (cashRows != 1)
                        {
                            await db.ExecuteAsync(@"
                                INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
                                VALUES ((SELECT TOP 1 id FROM atms ORDER BY id), @CurrencyId, @CashAmount)",
                                new { CashAmount = cashAmount, CurrencyId = currencyId },
                                trans);
                        }

                        if (currencyCode == "UZS")
                        {
                            await db.ExecuteAsync(@"
                                UPDATE atms
                                SET current_balance = (
                                        SELECT cash_balance
                                        FROM atm_currency_cash
                                        WHERE atm_id = atms.id
                                          AND currency_id = @CurrencyId
                                    ),
                                    updated_at = GETDATE()
                                WHERE id = (SELECT TOP 1 id FROM atms ORDER BY id)",
                                new { CurrencyId = currencyId },
                                trans);
                        }

                        await db.ExecuteAsync(@"
                            INSERT INTO transactions (account_id, type, amount, description, transaction_date)
                            VALUES (@AccountId, 'Deposit', @Amount, @Description, GETDATE())",
                            new
                            {
                                AccountId = accountId,
                                Amount = creditAmountUzs,
                                Description = $"Cash deposit: {cashAmount:N2} {currencyCode}"
                            },
                            trans);

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
            if (!IsValidMoneyAmount(amount) || sourceAccountId == targetAccountId) return false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var source = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT balance, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = sourceAccountId },
                            trans);
                        if (source == null || !source.IsActive || source.Balance < amount)
                        {
                            trans.Rollback();
                            return false;
                        }

                        var target = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT id, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = targetAccountId },
                            trans);
                        if (target == null || !target.IsActive)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int debitRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance - @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = amount, Id = sourceAccountId },
                            trans);
                        if (debitRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int creditRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance + @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = amount, Id = targetAccountId },
                            trans);
                        if (creditRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }
                        
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
                return await db.QueryFirstOrDefaultAsync<UserDto>(@"
                    SELECT
                        id,
                        full_name as FullName,
                        role,
                        username,
                        password_hash as PasswordHash,
                        is_active as IsActive
                    FROM users
                    WHERE id = @UserId AND is_active = 1", new { UserId = userId });
            }
        }

        public async Task<UserDto> GetAdminByUsernameAsync(string username)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<UserDto>(@"
                    SELECT
                        id,
                        full_name as FullName,
                        role,
                        username,
                        password_hash as PasswordHash,
                        is_active as IsActive
                    FROM users
                    WHERE username = @Username AND role = 'Admin' AND is_active = 1", new { Username = username });
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<UserDto>(@"
                    SELECT
                        u.id,
                        u.full_name as FullName,
                        u.username,
                        u.phone_number as PhoneNumber,
                        u.role,
                        u.is_active as IsActive,
                        u.created_at as CreatedAt,
                        a.id as PrimaryAccountId,
                        a.account_number as PrimaryAccountNumber
                    FROM users u
                    OUTER APPLY (
                        SELECT TOP 1 id, account_number
                        FROM accounts
                        WHERE user_id = u.id AND is_active = 1
                        ORDER BY id
                    ) a
                    ORDER BY u.id");
            }
        }

        public async Task<bool> PayServiceAsync(
            int accountId,
            decimal amount,
            string description,
            int serviceId,
            int serviceAccountId,
            string paymentReference)
        {
            if (!IsValidMoneyAmount(amount)) return false;

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var account = await db.QueryFirstOrDefaultAsync<AccountDto>(
                            "SELECT balance, is_active as IsActive FROM accounts WITH (UPDLOCK, ROWLOCK) WHERE id = @Id",
                            new { Id = accountId },
                            trans);
                        if (account == null || !account.IsActive || account.Balance < amount)
                        {
                            trans.Rollback();
                            return false;
                        }

                        int updatedRows = await db.ExecuteAsync(
                            "UPDATE accounts SET balance = balance - @Amount WHERE id = @Id AND is_active = 1",
                            new { Amount = amount, Id = accountId },
                            trans);
                        if (updatedRows != 1)
                        {
                            trans.Rollback();
                            return false;
                        }

                        await db.ExecuteAsync(@"
                            INSERT INTO transactions (
                                account_id,
                                type,
                                amount,
                                description,
                                service_id,
                                service_account_id,
                                payment_reference,
                                transaction_date)
                            VALUES (
                                @AccountId,
                                'BillPayment',
                                @Amount,
                                @Description,
                                @ServiceId,
                                @ServiceAccountId,
                                @PaymentReference,
                                GETDATE())",
                            new
                            {
                                AccountId = accountId,
                                Amount = amount,
                                Description = description,
                                ServiceId = serviceId,
                                ServiceAccountId = serviceAccountId,
                                PaymentReference = paymentReference
                            },
                            trans);

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

        public async Task<int> CreateUserWithProvisioningAsync(UserDto user, string password, string initialPin)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, 11);
                        int userId = await db.QuerySingleAsync<int>(@"
                            INSERT INTO users (full_name, username, password_hash, phone_number, role)
                            VALUES (@FullName, @Username, @PasswordHash, @PhoneNumber, @Role);
                            SELECT CAST(SCOPE_IDENTITY() as int)",
                            new
                            {
                                user.FullName,
                                user.Username,
                                PasswordHash = hashedPassword,
                                user.PhoneNumber,
                                user.Role
                            },
                            trans);

                        if (user.Role == "User")
                        {
                            string accountNumber = await GenerateUniqueAccountNumberAsync(db, trans);
                            int accountId = await db.QuerySingleAsync<int>(@"
                                INSERT INTO accounts (user_id, account_number, balance, is_active)
                                VALUES (@UserId, @AccountNumber, 0, 1);
                                SELECT CAST(SCOPE_IDENTITY() as int)",
                                new { UserId = userId, AccountNumber = accountNumber },
                                trans);

                            if (!string.IsNullOrWhiteSpace(initialPin))
                            {
                                string cardNumber = await GenerateUniqueCardNumberAsync(db, trans);
                                string pinHash = BCrypt.Net.BCrypt.HashPassword(initialPin, 11);
                                await db.ExecuteAsync(@"
                                    INSERT INTO cards (account_id, card_number, pin_hash, expiry_date, is_blocked, failed_attempts)
                                    VALUES (@AccountId, @CardNumber, @PinHash, @ExpiryDate, 0, 0)",
                                    new
                                    {
                                        AccountId = accountId,
                                        CardNumber = cardNumber,
                                        PinHash = pinHash,
                                        ExpiryDate = DateTime.Today.AddYears(5)
                                    },
                                    trans);
                            }
                        }

                        trans.Commit();
                        return userId;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task UpdateUserAsync(UserDto user, string newPassword = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE users SET full_name = @FullName, username = @Username, 
                               phone_number = @PhoneNumber, role = @Role, is_active = @IsActive";
                
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
            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var user = await db.QueryFirstOrDefaultAsync<UserDto>(@"
                            SELECT
                                id,
                                role,
                                is_active as IsActive
                            FROM users
                            WHERE id = @Id",
                            new { Id = userId },
                            trans);

                        if (user == null)
                        {
                            return;
                        }

                        if (!user.IsActive)
                        {
                            throw new InvalidOperationException("This user is already inactive.");
                        }

                        if (string.Equals(user.Role, "Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            int activeAdmins = await db.ExecuteScalarAsync<int>(
                                "SELECT COUNT(*) FROM users WHERE role = 'Admin' AND is_active = 1",
                                transaction: trans);

                            if (activeAdmins <= 1)
                            {
                                throw new InvalidOperationException("You cannot deactivate the last active administrator.");
                            }
                        }

                        await db.ExecuteAsync(
                            "UPDATE users SET is_active = 0 WHERE id = @Id",
                            new { Id = userId },
                            trans);

                        await db.ExecuteAsync(
                            "UPDATE accounts SET is_active = 0 WHERE user_id = @Id",
                            new { Id = userId },
                            trans);

                        await db.ExecuteAsync(@"
                            UPDATE cards
                            SET is_blocked = 1
                            WHERE account_id IN (
                                SELECT id FROM accounts WHERE user_id = @Id
                            )",
                            new { Id = userId },
                            trans);

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        private async Task<string> GenerateUniqueAccountNumberAsync(SqlConnection db, IDbTransaction trans)
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                string candidate = $"ACC{DateTime.UtcNow:yyMMddHHmmss}{attempt}";
                int exists = await db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM accounts WHERE account_number = @AccountNumber",
                    new { AccountNumber = candidate },
                    trans);
                if (exists == 0) return candidate;
            }

            throw new InvalidOperationException("Could not generate a unique account number.");
        }

        private async Task<string> GenerateUniqueCardNumberAsync(SqlConnection db, IDbTransaction trans)
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                string candidate = $"{DateTime.UtcNow:yyyyMMddHHmmss}{attempt % 10}{(attempt + 7) % 10}";
                int exists = await db.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM cards WHERE card_number = @CardNumber",
                    new { CardNumber = candidate },
                    trans);
                if (exists == 0) return candidate;
            }

            throw new InvalidOperationException("Could not generate a unique card number.");
        }

        private static bool IsValidMoneyAmount(decimal amount)
        {
            return amount > 0m && decimal.Round(amount, 2) == amount;
        }
    }
}
