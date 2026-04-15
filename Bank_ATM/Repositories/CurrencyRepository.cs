using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class CurrencyRepository
    {
        private readonly string _connectionString;

        public CurrencyRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public IEnumerable<CurrencyDto> GetAllCurrencies()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<CurrencyDto>(@"
                    SELECT
                        c.id,
                        c.code as Code,
                        c.currency_name as CurrencyName,
                        c.rate_to_uzs as RateToUzs,
                        c.is_active as IsActive,
                        ISNULL(acc.cash_balance, 0) as CashAvailable,
                        c.updated_at as UpdatedAt
                    FROM currencies c
                    LEFT JOIN atm_currency_cash acc ON acc.currency_id = c.id
                        AND acc.atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                    ORDER BY c.code").ToList();
            }
        }

        public IEnumerable<CurrencyDto> GetActiveCurrencies()
        {
            return GetAllCurrencies().Where(c => c.IsActive).ToList();
        }

        public CurrencyDto GetCurrencyByCode(string code)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<CurrencyDto>(@"
                    SELECT
                        c.id,
                        c.code as Code,
                        c.currency_name as CurrencyName,
                        c.rate_to_uzs as RateToUzs,
                        c.is_active as IsActive,
                        ISNULL(acc.cash_balance, 0) as CashAvailable,
                        c.updated_at as UpdatedAt
                    FROM currencies c
                    LEFT JOIN atm_currency_cash acc ON acc.currency_id = c.id
                        AND acc.atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                    WHERE c.code = @Code",
                    new { Code = (code ?? string.Empty).Trim().ToUpperInvariant() });
            }
        }

        public int SaveCurrency(CurrencyDto currency)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        int id;
                        string code = currency.Code.Trim().ToUpperInvariant();

                        if (currency.Id > 0)
                        {
                            db.Execute(@"
                                UPDATE currencies
                                SET code = @Code,
                                    currency_name = @CurrencyName,
                                    rate_to_uzs = @RateToUzs,
                                    is_active = @IsActive,
                                    updated_at = GETDATE()
                                WHERE id = @Id",
                                new
                                {
                                    currency.Id,
                                    Code = code,
                                    currency.CurrencyName,
                                    currency.RateToUzs,
                                    currency.IsActive
                                },
                                trans);
                            id = currency.Id;
                        }
                        else
                        {
                            id = db.QuerySingle<int>(@"
                                INSERT INTO currencies (code, currency_name, rate_to_uzs, is_active)
                                VALUES (@Code, @CurrencyName, @RateToUzs, @IsActive);
                                SELECT CAST(SCOPE_IDENTITY() as int);",
                                new
                                {
                                    Code = code,
                                    currency.CurrencyName,
                                    currency.RateToUzs,
                                    currency.IsActive
                                },
                                trans);
                        }

                        db.Execute(@"
                            IF EXISTS (
                                SELECT 1 FROM atm_currency_cash
                                WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                  AND currency_id = @CurrencyId
                            )
                            BEGIN
                                UPDATE atm_currency_cash
                                SET cash_balance = @CashAvailable,
                                    updated_at = GETDATE()
                                WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                  AND currency_id = @CurrencyId
                            END
                            ELSE
                            BEGIN
                                INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
                                VALUES ((SELECT TOP 1 id FROM atms ORDER BY id), @CurrencyId, @CashAvailable)
                            END",
                            new { CurrencyId = id, currency.CashAvailable },
                            trans);

                        if (code == "UZS")
                        {
                            db.Execute(@"
                                UPDATE atms
                                SET current_balance = @CashAvailable,
                                    updated_at = GETDATE()
                                WHERE id = (SELECT TOP 1 id FROM atms ORDER BY id)",
                                new { currency.CashAvailable },
                                trans);
                        }

                        trans.Commit();
                        return id;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeactivateCurrency(int currencyId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("UPDATE currencies SET is_active = 0, updated_at = GETDATE() WHERE id = @Id", new { Id = currencyId });
            }
        }
    }
}
