using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class CashRepository
    {
        private readonly string _connectionString;

        public CashRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public IEnumerable<CashDenominationDto> GetDenominations(string currencyCode)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<CashDenominationDto>(@"
                    SELECT
                        d.atm_id as AtmId,
                        d.currency_id as CurrencyId,
                        c.code as CurrencyCode,
                        d.denomination_value as DenominationValue,
                        d.note_count as NoteCount,
                        d.updated_at as UpdatedAt
                    FROM atm_cash_denominations d
                    INNER JOIN currencies c ON c.id = d.currency_id
                    WHERE c.code = @Code
                    ORDER BY d.denomination_value DESC",
                    new { Code = (currencyCode ?? string.Empty).Trim().ToUpperInvariant() }).ToList();
            }
        }

        public IEnumerable<CashDenominationDto> GetAllDenominations()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<CashDenominationDto>(@"
                    SELECT
                        d.atm_id as AtmId,
                        d.currency_id as CurrencyId,
                        c.code as CurrencyCode,
                        d.denomination_value as DenominationValue,
                        d.note_count as NoteCount,
                        d.updated_at as UpdatedAt
                    FROM atm_cash_denominations d
                    INNER JOIN currencies c ON c.id = d.currency_id
                    ORDER BY c.code, d.denomination_value DESC").ToList();
            }
        }

        public void AddCashNotes(int currencyId, IEnumerable<CashNoteDto> notes)
        {
            var noteList = NormalizeNotes(notes).ToList();
            if (currencyId <= 0 || !noteList.Any())
            {
                throw new ArgumentException("At least one cash note count is required.");
            }

            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var note in noteList)
                        {
                            db.Execute(@"
                                IF EXISTS (
                                    SELECT 1 FROM atm_cash_denominations
                                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                      AND currency_id = @CurrencyId
                                      AND denomination_value = @DenominationValue
                                )
                                BEGIN
                                    UPDATE atm_cash_denominations
                                    SET note_count = note_count + @NoteCount,
                                        updated_at = GETDATE()
                                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                      AND currency_id = @CurrencyId
                                      AND denomination_value = @DenominationValue
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
                                    VALUES ((SELECT TOP 1 id FROM atms ORDER BY id), @CurrencyId, @DenominationValue, @NoteCount)
                                END",
                                new
                                {
                                    CurrencyId = currencyId,
                                    note.DenominationValue,
                                    note.NoteCount
                                },
                                trans);
                        }

                        SyncCashTotals(db, trans, currencyId);
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

        public async Task<bool> AddGuestCashPaymentAsync(
            int serviceId,
            int serviceAccountId,
            string paymentReference,
            decimal amount,
            string description,
            int currencyId,
            IEnumerable<CashNoteDto> notes)
        {
            var noteList = NormalizeNotes(notes).ToList();
            if (currencyId <= 0 || amount <= 0m || !noteList.Any())
            {
                return false;
            }

            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        foreach (var note in noteList)
                        {
                            await db.ExecuteAsync(@"
                                IF EXISTS (
                                    SELECT 1 FROM atm_cash_denominations
                                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                      AND currency_id = @CurrencyId
                                      AND denomination_value = @DenominationValue
                                )
                                BEGIN
                                    UPDATE atm_cash_denominations
                                    SET note_count = note_count + @NoteCount,
                                        updated_at = GETDATE()
                                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                      AND currency_id = @CurrencyId
                                      AND denomination_value = @DenominationValue
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
                                    VALUES ((SELECT TOP 1 id FROM atms ORDER BY id), @CurrencyId, @DenominationValue, @NoteCount)
                                END",
                                new
                                {
                                    CurrencyId = currencyId,
                                    note.DenominationValue,
                                    note.NoteCount
                                },
                                trans);
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
                                NULL,
                                'BillPayment',
                                @Amount,
                                @Description,
                                @ServiceId,
                                @ServiceAccountId,
                                @PaymentReference,
                                GETDATE())",
                            new
                            {
                                Amount = amount,
                                Description = description,
                                ServiceId = serviceId,
                                ServiceAccountId = serviceAccountId,
                                PaymentReference = paymentReference
                            },
                            trans);

                        await SyncCashTotalsAsync(db, trans, currencyId);
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

        public async Task<IList<CashNoteDto>> ExchangeGuestCashAsync(
            CurrencyDto fromCurrency,
            CurrencyDto toCurrency,
            IEnumerable<CashNoteDto> insertedNotes,
            decimal targetCashAmount,
            decimal amountUzs,
            string description)
        {
            var insertedNoteList = NormalizeNotes(insertedNotes).ToList();
            if (fromCurrency == null || toCurrency == null ||
                fromCurrency.Id == toCurrency.Id ||
                targetCashAmount <= 0m ||
                amountUzs <= 0m ||
                !insertedNoteList.Any())
            {
                return null;
            }

            using (var db = new SqlConnection(_connectionString))
            {
                await db.OpenAsync();
                using (var trans = db.BeginTransaction())
                {
                    try
                    {
                        var targetDenominations = (await db.QueryAsync<CashDenominationDto>(@"
                            SELECT
                                d.atm_id as AtmId,
                                d.currency_id as CurrencyId,
                                c.code as CurrencyCode,
                                d.denomination_value as DenominationValue,
                                d.note_count as NoteCount,
                                d.updated_at as UpdatedAt
                            FROM atm_cash_denominations d WITH (UPDLOCK, ROWLOCK)
                            INNER JOIN currencies c ON c.id = d.currency_id
                            WHERE d.atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND d.currency_id = @CurrencyId
                            ORDER BY d.denomination_value DESC",
                            new { CurrencyId = toCurrency.Id },
                            trans)).ToList();

                        var dispensedNotes = BuildDispenseBreakdown(targetCashAmount, targetDenominations);
                        if (dispensedNotes == null || dispensedNotes.Count == 0)
                        {
                            trans.Rollback();
                            return null;
                        }

                        foreach (var note in insertedNoteList)
                        {
                            await db.ExecuteAsync(@"
                                IF EXISTS (
                                    SELECT 1 FROM atm_cash_denominations
                                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                      AND currency_id = @CurrencyId
                                      AND denomination_value = @DenominationValue
                                )
                                BEGIN
                                    UPDATE atm_cash_denominations
                                    SET note_count = note_count + @NoteCount,
                                        updated_at = GETDATE()
                                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                      AND currency_id = @CurrencyId
                                      AND denomination_value = @DenominationValue
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO atm_cash_denominations (atm_id, currency_id, denomination_value, note_count)
                                    VALUES ((SELECT TOP 1 id FROM atms ORDER BY id), @CurrencyId, @DenominationValue, @NoteCount)
                                END",
                                new
                                {
                                    CurrencyId = fromCurrency.Id,
                                    note.DenominationValue,
                                    note.NoteCount
                                },
                                trans);
                        }

                        foreach (var note in dispensedNotes)
                        {
                            int rows = await db.ExecuteAsync(@"
                                UPDATE atm_cash_denominations
                                SET note_count = note_count - @NoteCount,
                                    updated_at = GETDATE()
                                WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                                  AND currency_id = @CurrencyId
                                  AND denomination_value = @DenominationValue
                                  AND note_count >= @NoteCount",
                                new
                                {
                                    CurrencyId = toCurrency.Id,
                                    note.DenominationValue,
                                    note.NoteCount
                                },
                                trans);

                            if (rows != 1)
                            {
                                trans.Rollback();
                                return null;
                            }
                        }

                        await db.ExecuteAsync(@"
                            INSERT INTO transactions (account_id, type, amount, description, transaction_date)
                            VALUES (NULL, 'Exchange', @Amount, @Description, GETDATE())",
                            new { Amount = amountUzs, Description = description },
                            trans);

                        await SyncCashTotalsAsync(db, trans, fromCurrency.Id);
                        await SyncCashTotalsAsync(db, trans, toCurrency.Id);
                        trans.Commit();
                        return dispensedNotes;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        internal static IEnumerable<CashNoteDto> NormalizeNotes(IEnumerable<CashNoteDto> notes)
        {
            return (notes ?? Enumerable.Empty<CashNoteDto>())
                .Where(note => note != null && note.NoteCount > 0 && note.DenominationValue > 0m)
                .GroupBy(note => note.DenominationValue)
                .Select(group => new CashNoteDto
                {
                    CurrencyId = group.First().CurrencyId,
                    CurrencyCode = group.First().CurrencyCode,
                    DenominationValue = group.Key,
                    NoteCount = group.Sum(note => note.NoteCount)
                })
                .OrderByDescending(note => note.DenominationValue);
        }

        internal static IList<CashNoteDto> BuildDispenseBreakdown(decimal amount, IEnumerable<CashDenominationDto> denominations)
        {
            decimal remaining = decimal.Round(amount, 2);
            var result = new List<CashNoteDto>();

            foreach (var denomination in (denominations ?? Enumerable.Empty<CashDenominationDto>())
                .OrderByDescending(d => d.DenominationValue))
            {
                if (denomination.NoteCount <= 0 || denomination.DenominationValue <= 0m)
                {
                    continue;
                }

                int needed = (int)Math.Floor(remaining / denomination.DenominationValue);
                int selected = Math.Min(needed, denomination.NoteCount);
                if (selected <= 0)
                {
                    continue;
                }

                result.Add(new CashNoteDto
                {
                    CurrencyId = denomination.CurrencyId,
                    CurrencyCode = denomination.CurrencyCode,
                    DenominationValue = denomination.DenominationValue,
                    NoteCount = selected
                });
                remaining -= denomination.DenominationValue * selected;
                remaining = decimal.Round(remaining, 2);
            }

            return remaining == 0m ? result : null;
        }

        internal static void SyncCashTotals(IDbConnection db, IDbTransaction trans, int currencyId)
        {
            db.Execute(@"
                IF EXISTS (
                    SELECT 1 FROM atm_currency_cash
                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                      AND currency_id = @CurrencyId
                )
                BEGIN
                    UPDATE atm_currency_cash
                    SET cash_balance = ISNULL((
                            SELECT SUM(denomination_value * note_count)
                            FROM atm_cash_denominations
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = @CurrencyId
                        ), 0),
                        updated_at = GETDATE()
                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                      AND currency_id = @CurrencyId
                END
                ELSE
                BEGIN
                    INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
                    VALUES (
                        (SELECT TOP 1 id FROM atms ORDER BY id),
                        @CurrencyId,
                        ISNULL((
                            SELECT SUM(denomination_value * note_count)
                            FROM atm_cash_denominations
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = @CurrencyId
                        ), 0)
                    )
                END

                UPDATE atms
                SET current_balance = (
                        SELECT cash_balance
                        FROM atm_currency_cash
                        WHERE atm_id = atms.id
                          AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')
                    ),
                    updated_at = GETDATE()
                WHERE id = (SELECT TOP 1 id FROM atms ORDER BY id)",
                new { CurrencyId = currencyId },
                trans);
        }

        internal static async Task SyncCashTotalsAsync(IDbConnection db, IDbTransaction trans, int currencyId)
        {
            await db.ExecuteAsync(@"
                IF EXISTS (
                    SELECT 1 FROM atm_currency_cash
                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                      AND currency_id = @CurrencyId
                )
                BEGIN
                    UPDATE atm_currency_cash
                    SET cash_balance = ISNULL((
                            SELECT SUM(denomination_value * note_count)
                            FROM atm_cash_denominations
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = @CurrencyId
                        ), 0),
                        updated_at = GETDATE()
                    WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                      AND currency_id = @CurrencyId
                END
                ELSE
                BEGIN
                    INSERT INTO atm_currency_cash (atm_id, currency_id, cash_balance)
                    VALUES (
                        (SELECT TOP 1 id FROM atms ORDER BY id),
                        @CurrencyId,
                        ISNULL((
                            SELECT SUM(denomination_value * note_count)
                            FROM atm_cash_denominations
                            WHERE atm_id = (SELECT TOP 1 id FROM atms ORDER BY id)
                              AND currency_id = @CurrencyId
                        ), 0)
                    )
                END

                UPDATE atms
                SET current_balance = (
                        SELECT cash_balance
                        FROM atm_currency_cash
                        WHERE atm_id = atms.id
                          AND currency_id = (SELECT id FROM currencies WHERE code = 'UZS')
                    ),
                    updated_at = GETDATE()
                WHERE id = (SELECT TOP 1 id FROM atms ORDER BY id)",
                new { CurrencyId = currencyId },
                trans);
        }
    }
}
