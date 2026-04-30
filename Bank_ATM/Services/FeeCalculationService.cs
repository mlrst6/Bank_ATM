using System;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Services
{
    public class FeeCalculationService
    {
        private readonly FeeRuleRepository _feeRuleRepository = new FeeRuleRepository();

        public FeeCalculationResult Calculate(string cardType, string transactionType, decimal baseAmountUzs)
        {
            decimal roundedBase = decimal.Round(baseAmountUzs, 2);
            if (roundedBase <= 0m)
            {
                return new FeeCalculationResult
                {
                    TransactionType = transactionType,
                    BaseAmountUzs = 0m,
                    FeeAmountUzs = 0m,
                    TotalDebitUzs = 0m,
                    PercentFee = 0m
                };
            }

            var rule = _feeRuleRepository.GetActiveRule(cardType, transactionType);
            if (rule == null)
            {
                return new FeeCalculationResult
                {
                    TransactionType = transactionType,
                    BaseAmountUzs = roundedBase,
                    FeeAmountUzs = 0m,
                    TotalDebitUzs = roundedBase,
                    PercentFee = 0m
                };
            }

            decimal fee = decimal.Round((roundedBase * rule.PercentFee / 100m) + rule.FixedFee, 2);
            if (fee < rule.MinFee)
            {
                fee = rule.MinFee;
            }

            if (rule.MaxFee.HasValue && fee > rule.MaxFee.Value)
            {
                fee = rule.MaxFee.Value;
            }

            return new FeeCalculationResult
            {
                TransactionType = transactionType,
                BaseAmountUzs = roundedBase,
                FeeAmountUzs = decimal.Round(fee, 2),
                TotalDebitUzs = decimal.Round(roundedBase + fee, 2),
                PercentFee = rule.PercentFee,
                FixedFee = rule.FixedFee,
                CardType = string.IsNullOrWhiteSpace(cardType) ? "DEFAULT" : cardType.Trim().ToUpperInvariant()
            };
        }
    }

    public class FeeCalculationResult
    {
        public string CardType { get; set; }
        public string TransactionType { get; set; }
        public decimal BaseAmountUzs { get; set; }
        public decimal FeeAmountUzs { get; set; }
        public decimal TotalDebitUzs { get; set; }
        public decimal PercentFee { get; set; }
        public decimal FixedFee { get; set; }

        public string BuildSummary()
        {
            return $"Amount: {BaseAmountUzs:N2} UZS{Environment.NewLine}" +
                   $"Fee: {FeeAmountUzs:N2} UZS{Environment.NewLine}" +
                   $"Total debited: {TotalDebitUzs:N2} UZS";
        }
    }
}
