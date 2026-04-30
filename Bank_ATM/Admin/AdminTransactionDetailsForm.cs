using System;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;

namespace Bank_ATM.Admin
{
    public partial class AdminTransactionDetailsForm : BaseForm
    {
        private readonly TransactionDto _transaction;

        public AdminTransactionDetailsForm(TransactionDto transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            InitializeComponent();
        }

        private void AdminTransactionDetailsForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            btnClose.Text = LanguageManager.GetString("Back");
            btnClose.Values.Text = btnClose.Text;

            txtTransactionId.Text = _transaction.Id.ToString();
            txtTransactionDate.Text = _transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
            txtType.Text = _transaction.Type ?? string.Empty;
            txtAmount.Text = _transaction.Amount.ToString("N2");
            txtAccountId.Text = NullableToText(_transaction.AccountId);
            txtTargetAccountId.Text = NullableToText(_transaction.TargetAccountId);
            txtCardId.Text = NullableToText(_transaction.CardId);
            txtTargetCardId.Text = NullableToText(_transaction.TargetCardId);
            txtServiceId.Text = NullableToText(_transaction.ServiceId);
            txtServiceAccountId.Text = NullableToText(_transaction.ServiceAccountId);
            txtPaymentReference.Text = _transaction.PaymentReference ?? string.Empty;
            txtDescription.Text = _transaction.CashbackAmount > 0m
                ? (_transaction.Description ?? string.Empty) + Environment.NewLine + $"Cashback: {_transaction.CashbackAmount:N2} UZS"
                : _transaction.Description ?? string.Empty;
        }

        private static string NullableToText(int? value)
        {
            return value.HasValue ? value.Value.ToString() : "-";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
