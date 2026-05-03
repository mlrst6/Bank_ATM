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

        public void AddTransaction(
            int? sourceAccountId,
            string type,
            decimal amount,
            int? destinationAccountId = null,
            string description = null,
            int? serviceId = null,
            int? serviceAccountId = null,
            string paymentReference = null,
            decimal feeAmount = 0m,
            decimal totalDebited = 0m,
            decimal netAmount = 0m,
            decimal? exchangeRate = null,
            string rateKind = null,
            decimal cashbackAmount = 0m)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
                    INSERT INTO transactions (
                        account_id,
                        target_account_id,
                        type,
                        amount,
                        fee_amount,
                        total_debited,
                        net_amount,
                        exchange_rate,
                        rate_kind,
                        description,
                        service_id,
                        service_account_id,
                        payment_reference,
                        cashback_amount,
                        transaction_date)
                    VALUES (
                        @AccountId,
                        @TargetAccountId,
                        @Type,
                        @Amount,
                        @FeeAmount,
                        @TotalDebited,
                        @NetAmount,
                        @ExchangeRate,
                        @RateKind,
                        @Description,
                        @ServiceId,
                        @ServiceAccountId,
                        @PaymentReference,
                        @CashbackAmount,
                        @Date)";
                 
                db.Execute(sql, new { 
                    AccountId = sourceAccountId, 
                    TargetAccountId = destinationAccountId,
                    Type = type, 
                    Amount = amount, 
                    FeeAmount = feeAmount,
                    TotalDebited = totalDebited == 0m ? amount + feeAmount : totalDebited,
                    NetAmount = netAmount == 0m ? amount : netAmount,
                    ExchangeRate = exchangeRate,
                    RateKind = rateKind,
                    Description = description,
                    ServiceId = serviceId,
                    ServiceAccountId = serviceAccountId,
                    PaymentReference = paymentReference,
                    CashbackAmount = cashbackAmount,
                    Date = DateTime.Now 
                });
            }
        }

        public IEnumerable<TransactionDto> GetAllTransactions()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT
                        t.id,
                        t.account_id as AccountId,
                        t.target_account_id as TargetAccountId,
                        t.card_id as CardId,
                        t.target_card_id as TargetCardId,
                        sc.card_number as SourceCardNumber,
                        tc.card_number as TargetCardNumber,
                        t.type,
                        t.amount,
                        t.fee_amount as FeeAmount,
                        t.total_debited as TotalDebited,
                        t.net_amount as NetAmount,
                        t.exchange_rate as ExchangeRate,
                        t.rate_kind as RateKind,
                        t.description,
                        t.service_id as ServiceId,
                        t.service_account_id as ServiceAccountId,
                        t.payment_reference as PaymentReference,
                        t.cashback_amount as CashbackAmount,
                        t.transaction_date as TransactionDate
                    FROM transactions t
                    LEFT JOIN cards sc ON sc.id = t.card_id
                    LEFT JOIN cards tc ON tc.id = t.target_card_id
                    ORDER BY t.transaction_date DESC";
                return db.Query<TransactionDto>(sql);
            }
        }

        public IEnumerable<TransactionDto> GetAccountTransactions(int accountId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT
                        t.id,
                        t.account_id as AccountId,
                        t.target_account_id as TargetAccountId,
                        t.card_id as CardId,
                        t.target_card_id as TargetCardId,
                        sc.card_number as SourceCardNumber,
                        tc.card_number as TargetCardNumber,
                        t.type,
                        t.amount,
                        t.fee_amount as FeeAmount,
                        t.total_debited as TotalDebited,
                        t.net_amount as NetAmount,
                        t.exchange_rate as ExchangeRate,
                        t.rate_kind as RateKind,
                        t.description,
                        t.service_id as ServiceId,
                        t.service_account_id as ServiceAccountId,
                        t.payment_reference as PaymentReference,
                        t.cashback_amount as CashbackAmount,
                        t.transaction_date as TransactionDate
                    FROM transactions t
                    LEFT JOIN cards sc ON sc.id = t.card_id
                    LEFT JOIN cards tc ON tc.id = t.target_card_id
                    WHERE t.account_id = @AccountId OR t.target_account_id = @AccountId
                    ORDER BY t.transaction_date DESC";
                return db.Query<TransactionDto>(sql, new { AccountId = accountId });
            }
        }

        public IEnumerable<TransactionDto> GetCardTransactions(int cardId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT
                        t.id,
                        t.account_id as AccountId,
                        t.target_account_id as TargetAccountId,
                        t.card_id as CardId,
                        t.target_card_id as TargetCardId,
                        sc.card_number as SourceCardNumber,
                        tc.card_number as TargetCardNumber,
                        t.type,
                        t.amount,
                        t.fee_amount as FeeAmount,
                        t.total_debited as TotalDebited,
                        t.net_amount as NetAmount,
                        t.exchange_rate as ExchangeRate,
                        t.rate_kind as RateKind,
                        t.description,
                        t.service_id as ServiceId,
                        t.service_account_id as ServiceAccountId,
                        t.payment_reference as PaymentReference,
                        t.cashback_amount as CashbackAmount,
                        t.transaction_date as TransactionDate
                    FROM transactions t
                    LEFT JOIN cards sc ON sc.id = t.card_id
                    LEFT JOIN cards tc ON tc.id = t.target_card_id
                    WHERE t.card_id = @CardId OR t.target_card_id = @CardId
                    ORDER BY t.transaction_date DESC";
                return db.Query<TransactionDto>(sql, new { CardId = cardId });
            }
        }
    }
}
