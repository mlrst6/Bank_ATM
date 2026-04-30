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
                        id,
                        account_id as AccountId,
                        target_account_id as TargetAccountId,
                        card_id as CardId,
                        target_card_id as TargetCardId,
                        type,
                        amount,
                        fee_amount as FeeAmount,
                        total_debited as TotalDebited,
                        net_amount as NetAmount,
                        exchange_rate as ExchangeRate,
                        rate_kind as RateKind,
                        description,
                        service_id as ServiceId,
                        service_account_id as ServiceAccountId,
                        payment_reference as PaymentReference,
                        cashback_amount as CashbackAmount,
                        transaction_date as TransactionDate
                    FROM transactions
                    ORDER BY transaction_date DESC";
                return db.Query<TransactionDto>(sql);
            }
        }

        public IEnumerable<TransactionDto> GetAccountTransactions(int accountId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sql = @"
                    SELECT
                        id,
                        account_id as AccountId,
                        target_account_id as TargetAccountId,
                        card_id as CardId,
                        target_card_id as TargetCardId,
                        type,
                        amount,
                        fee_amount as FeeAmount,
                        total_debited as TotalDebited,
                        net_amount as NetAmount,
                        exchange_rate as ExchangeRate,
                        rate_kind as RateKind,
                        description,
                        service_id as ServiceId,
                        service_account_id as ServiceAccountId,
                        payment_reference as PaymentReference,
                        cashback_amount as CashbackAmount,
                        transaction_date as TransactionDate
                    FROM transactions
                    WHERE account_id = @AccountId OR target_account_id = @AccountId
                    ORDER BY transaction_date DESC";
                return db.Query<TransactionDto>(sql, new { AccountId = accountId });
            }
        }
    }
}
