using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.User
{
    public partial class UserTransactionHistoryForm : Form
    {
        private readonly BankingService _bankingService = new BankingService();
        private List<TransactionDto> _allTransactions = new List<TransactionDto>();
        private readonly int _currentCardId;

        public UserTransactionHistoryForm()
        {
            InitializeComponent();
            _currentCardId = SessionManager.Instance.CurrentCard?.Id ?? 0;
        }

        private void UserTransactionHistoryForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            var card = SessionManager.Instance.CurrentCard;
            lblTitle.Text = "My Transactions";
            if (card != null)
            {
                string masked = card.CardNumber.Length > 4
                    ? card.CardType + "  ****" + card.CardNumber.Substring(card.CardNumber.Length - 4)
                    : card.CardType;
                lblCardInfo.Text = masked;
            }
            else
            {
                lblCardInfo.Text = "Card unavailable";
            }

            cmbTypeFilter.Items.Clear();
            cmbTypeFilter.Items.Add("All Types");
            cmbTypeFilter.SelectedIndex = 0;

            LoadTransactions();
        }

        private void LoadTransactions()
        {
            _allTransactions = _bankingService.GetCurrentCardTransactions().ToList();

            string existingSelected = cmbTypeFilter.SelectedItem as string;
            var types = _allTransactions
                .Select(t => t.Type)
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct()
                .OrderBy(t => t)
                .ToArray();

            cmbTypeFilter.BeginUpdate();
            cmbTypeFilter.Items.Clear();
            cmbTypeFilter.Items.Add("All Types");
            cmbTypeFilter.Items.AddRange(types);
            cmbTypeFilter.EndUpdate();

            if (!string.IsNullOrWhiteSpace(existingSelected) && cmbTypeFilter.Items.Contains(existingSelected))
                cmbTypeFilter.SelectedItem = existingSelected;
            else
                cmbTypeFilter.SelectedIndex = 0;

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string selectedType = cmbTypeFilter.SelectedItem as string;
            IEnumerable<TransactionDto> filtered = _allTransactions;

            if (!string.IsNullOrWhiteSpace(selectedType) && selectedType != "All Types")
                filtered = filtered.Where(t => string.Equals(t.Type, selectedType, StringComparison.OrdinalIgnoreCase));

            var display = filtered.Select(t => new
            {
                Date = t.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Type = t.Type,
                Direction = GetDirection(t),
                Amount = t.Amount,
                Fee = t.FeeAmount,
                Net = t.NetAmount,
                Description = t.Description ?? string.Empty
            }).ToList();

            dataGridView.DataSource = display;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            lblResults.Text = $"{display.Count} transaction(s)";
        }

        private string GetDirection(TransactionDto t)
        {
            if (t.TargetCardId.HasValue && t.TargetCardId.Value == _currentCardId)
                return "IN";
            if (string.Equals(t.Type, "Deposit", StringComparison.OrdinalIgnoreCase))
                return "IN";
            return "OUT";
        }

        private void cmbTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
