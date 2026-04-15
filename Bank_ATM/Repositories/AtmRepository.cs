using System.Data;
using System.Data.SqlClient;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class AtmRepository
    {
        private readonly string _connectionString;

        public AtmRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public AtmDto GetDefaultAtm()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<AtmDto>(@"
                    SELECT TOP 1
                        a.id,
                        a.atm_name as AtmName,
                        ISNULL(uzs.cash_balance, a.current_balance) as CurrentBalance,
                        a.location as Location
                    FROM atms a
                    LEFT JOIN currencies c ON c.code = 'UZS'
                    LEFT JOIN atm_currency_cash uzs ON uzs.atm_id = a.id AND uzs.currency_id = c.id
                    ORDER BY a.id");
            }
        }

        public void AddCash(decimal amount)
        {
            if (amount <= 0m || decimal.Round(amount, 2) != amount)
            {
                throw new System.ArgumentException("Cash amount must be greater than zero.");
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        int rows = db.Execute(@"
                            UPDATE atm_currency_cash
                            SET cash_balance = cash_balance + @Amount,
                                updated_at = GETDATE()
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')",
                            new { Amount = amount },
                            trans);

                        if (rows == 0)
                        {
                            db.Execute(@"
                                INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
                                VALUES (
                                    (SELECT TOP 1 id FROM atms ORDER BY id),
                                    (SELECT id FROM currencies WHERE code = 'UZS'),
                                    @Amount
                                )",
                                new { Amount = amount },
                                trans);
                        }

                        db.Execute(@"
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
    }
}
