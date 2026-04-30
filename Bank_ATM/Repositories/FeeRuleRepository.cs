using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Bank_ATM.Models;

namespace Bank_ATM.Repositories
{
    public class FeeRuleRepository
    {
        private readonly string _connectionString;

        public FeeRuleRepository()
        {
            _connectionString = Config.ConnectionString;
        }

        public FeeRuleDto GetActiveRule(string cardType, string transactionType)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QueryFirstOrDefault<FeeRuleDto>(@"
                    SELECT TOP 1
                        id,
                        card_type as CardType,
                        transaction_type as TransactionType,
                        percent_fee as PercentFee,
                        fixed_fee as FixedFee,
                        min_fee as MinFee,
                        max_fee as MaxFee,
                        is_active as IsActive
                    FROM fee_rules
                    WHERE is_active = 1
                      AND transaction_type = @TransactionType
                      AND (
                            card_type = @CardType
                            OR card_type IS NULL
                      )
                    ORDER BY CASE WHEN card_type = @CardType THEN 0 ELSE 1 END, id",
                    new
                    {
                        CardType = string.IsNullOrWhiteSpace(cardType) ? null : cardType.Trim().ToUpperInvariant(),
                        TransactionType = transactionType
                    });
            }
        }

        public IEnumerable<FeeRuleDto> GetAllRules()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<FeeRuleDto>(@"
                    SELECT
                        id,
                        card_type as CardType,
                        transaction_type as TransactionType,
                        percent_fee as PercentFee,
                        fixed_fee as FixedFee,
                        min_fee as MinFee,
                        max_fee as MaxFee,
                        is_active as IsActive
                    FROM fee_rules
                    ORDER BY transaction_type, card_type").ToList();
            }
        }

        public int SaveRule(FeeRuleDto rule)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string cardType = string.IsNullOrWhiteSpace(rule.CardType)
                    ? null
                    : CardTypes.Normalize(rule.CardType);
                string transactionType = (rule.TransactionType ?? string.Empty).Trim();

                if (rule.Id > 0)
                {
                    db.Execute(@"
                        UPDATE fee_rules
                        SET card_type = @CardType,
                            transaction_type = @TransactionType,
                            percent_fee = @PercentFee,
                            fixed_fee = @FixedFee,
                            min_fee = @MinFee,
                            max_fee = @MaxFee,
                            is_active = @IsActive
                        WHERE id = @Id",
                        new
                        {
                            rule.Id,
                            CardType = cardType,
                            TransactionType = transactionType,
                            rule.PercentFee,
                            rule.FixedFee,
                            rule.MinFee,
                            rule.MaxFee,
                            rule.IsActive
                        });
                    return rule.Id;
                }

                return db.QuerySingle<int>(@"
                    INSERT INTO fee_rules (
                        card_type,
                        transaction_type,
                        percent_fee,
                        fixed_fee,
                        min_fee,
                        max_fee,
                        is_active)
                    VALUES (
                        @CardType,
                        @TransactionType,
                        @PercentFee,
                        @FixedFee,
                        @MinFee,
                        @MaxFee,
                        @IsActive);
                    SELECT CAST(SCOPE_IDENTITY() as int);",
                    new
                    {
                        CardType = cardType,
                        TransactionType = transactionType,
                        rule.PercentFee,
                        rule.FixedFee,
                        rule.MinFee,
                        rule.MaxFee,
                        rule.IsActive
                    });
            }
        }

        public void DeactivateRule(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute("UPDATE fee_rules SET is_active = 0 WHERE id = @Id", new { Id = id });
            }
        }
    }
}
