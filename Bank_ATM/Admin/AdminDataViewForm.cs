using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminDataViewForm : BaseForm
    {
        private object _dataSource;
        private string _title;
        private string _mode; // "USERS", "CARDS", "SERVICES", "TRANSACTIONS"
        private readonly AdminService _adminService = new AdminService();
        private readonly BindingSource _bindingSource = new BindingSource();
        private List<object> _allItems = new List<object>();
        private List<object> _filteredItems = new List<object>();

        public AdminDataViewForm(string title, object dataSource, string mode)
        {
            InitializeComponent();
            _title = title;
            _dataSource = dataSource;
            _mode = mode;
        }

        private void AdminDataViewForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            lblTitle.Text = _title;
            btnAdd.Text = LanguageManager.GetString("Add");
            btnAdd.Values.Text = btnAdd.Text;
            btnEdit.Text = LanguageManager.GetString("Edit");
            btnEdit.Values.Text = btnEdit.Text;
            btnBack.Text = LanguageManager.GetString("Back");
            btnBack.Values.Text = btnBack.Text;
            btnDelete.Text = _mode == "USERS"
                ? LanguageManager.GetString("DeactivateUser")
                : LanguageManager.GetString("Delete");
            btnDelete.Values.Text = btnDelete.Text;
            ConfigureModeSpecificUi();
            RefreshGrid();

            // Disable CRUD for read-only operational data.
            bool isReadOnly = _mode == "TRANSACTIONS" || _mode == "CASH";
            btnAdd.Visible = !isReadOnly;
            btnEdit.Visible = !isReadOnly;
            btnDelete.Visible = !isReadOnly;
        }

        private void ConfigureModeSpecificUi()
        {
            bool isTransactionMode = _mode == "TRANSACTIONS";
            lblTransactionType.Visible = isTransactionMode;
            cmbTransactionType.Visible = isTransactionMode;
            lblDateFrom.Visible = isTransactionMode;
            dtpDateFrom.Visible = isTransactionMode;
            lblDateTo.Visible = isTransactionMode;
            dtpDateTo.Visible = isTransactionMode;
            lblAmountMin.Visible = isTransactionMode;
            txtAmountMin.Visible = isTransactionMode;
            lblAmountMax.Visible = isTransactionMode;
            txtAmountMax.Visible = isTransactionMode;
            btnClearFilters.Visible = isTransactionMode;
            btnExportCsv.Visible = isTransactionMode;
            lblTransactionSummary.Visible = isTransactionMode;

            if (!isTransactionMode)
            {
                return;
            }

            dtpDateFrom.Checked = false;
            dtpDateTo.Checked = false;
            cmbTransactionType.Items.Clear();
            cmbTransactionType.Items.Add("All Types");
            cmbTransactionType.SelectedIndex = 0;
        }

        private async void RefreshGrid()
        {
            if (_mode == "USERS")
                _allItems = (await _adminService.GetAllUsersAsync()).Cast<object>().ToList();
            else if (_mode == "CARDS")
                _allItems = _adminService.GetAllCards().Cast<object>().ToList();
            else if (_mode == "SERVICES")
                _allItems = _adminService.GetAllServices().Cast<object>().ToList();
            else if (_mode == "CURRENCIES")
                _allItems = _adminService.GetAllCurrencies().Cast<object>().ToList();
            else if (_mode == "FEES")
                _allItems = _adminService.GetAllFeeRules().Cast<object>().ToList();
            else if (_mode == "CASH")
                _allItems = _adminService.GetAllCashDenominations().Cast<object>().ToList();
            else
                _allItems = ((System.Collections.IEnumerable)_dataSource).Cast<object>().ToList();

            if (_mode == "TRANSACTIONS")
            {
                PopulateTransactionTypeFilter();
            }

            ApplyFilter();
        }

        private void PopulateTransactionTypeFilter()
        {
            string selected = cmbTransactionType.SelectedItem as string;
            var transactionTypes = _allItems
                .OfType<TransactionDto>()
                .Select(transaction => transaction.Type)
                .Where(type => !string.IsNullOrWhiteSpace(type))
                .Distinct()
                .OrderBy(type => type)
                .ToArray();

            cmbTransactionType.BeginUpdate();
            cmbTransactionType.Items.Clear();
            cmbTransactionType.Items.Add("All Types");
            cmbTransactionType.Items.AddRange(transactionTypes);
            cmbTransactionType.EndUpdate();

            if (!string.IsNullOrWhiteSpace(selected) && cmbTransactionType.Items.Contains(selected))
            {
                cmbTransactionType.SelectedItem = selected;
            }
            else
            {
                cmbTransactionType.SelectedIndex = 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_mode == "USERS")
            {
                using (var form = new AdminUserEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "CARDS")
            {
                using (var form = new AdminCardEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "SERVICES")
            {
                using (var form = new AdminServiceEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "CURRENCIES")
            {
                using (var form = new AdminCurrencyEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "FEES")
            {
                using (var form = new AdminFeeRuleEditForm())
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            if (_mode == "USERS")
            {
                var user = (UserDto)dataGridView.SelectedRows[0].DataBoundItem;
                using (var form = new AdminUserEditForm(user))
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "CARDS")
            {
                var card = (CardDto)dataGridView.SelectedRows[0].DataBoundItem;
                using (var form = new AdminCardEditForm(card))
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "SERVICES")
            {
                var service = (ServiceDto)dataGridView.SelectedRows[0].DataBoundItem;
                using (var form = new AdminServiceEditForm(service))
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "CURRENCIES")
            {
                var currency = (CurrencyDto)dataGridView.SelectedRows[0].DataBoundItem;
                using (var form = new AdminCurrencyEditForm(currency))
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
            else if (_mode == "FEES")
            {
                var rule = (FeeRuleDto)dataGridView.SelectedRows[0].DataBoundItem;
                using (var form = new AdminFeeRuleEditForm(rule))
                {
                    if (form.ShowDialog() == DialogResult.OK) RefreshGrid();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            string confirmMessage = _mode == "USERS"
                ? LanguageManager.GetString("ConfirmDeactivateUser")
                : LanguageManager.GetString("ConfirmDeleteItem");
            string confirmTitle = _mode == "USERS"
                ? LanguageManager.GetString("ConfirmDeactivation")
                : LanguageManager.GetString("ConfirmDelete");

            if (MessageBox.Show(confirmMessage, confirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                if (_mode == "USERS")
                {
                    var user = (UserDto)dataGridView.SelectedRows[0].DataBoundItem;
                    await _adminService.DeleteUserAsync(user.Id);
                }
                else if (_mode == "CARDS")
                {
                    var card = (CardDto)dataGridView.SelectedRows[0].DataBoundItem;
                    _adminService.DeleteCard(card.Id);
                }
                else if (_mode == "SERVICES")
                {
                    var service = (ServiceDto)dataGridView.SelectedRows[0].DataBoundItem;
                    _adminService.DeleteService(service.Id);
                }
                else if (_mode == "CURRENCIES")
                {
                    var currency = (CurrencyDto)dataGridView.SelectedRows[0].DataBoundItem;
                    _adminService.DeactivateCurrency(currency.Id);
                }
                else if (_mode == "FEES")
                {
                    var rule = (FeeRuleDto)dataGridView.SelectedRows[0].DataBoundItem;
                    _adminService.DeactivateFeeRule(rule.Id);
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageManager.Format("DeleteFailed", ex.Message), LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void TransactionFilterChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbTransactionType.SelectedIndex = 0;
            dtpDateFrom.Checked = false;
            dtpDateTo.Checked = false;
            txtAmountMin.Clear();
            txtAmountMax.Clear();
            ApplyFilter();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_mode != "TRANSACTIONS" || _filteredItems.Count == 0)
            {
                MessageBox.Show("There are no filtered transactions to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                dialog.FileName = "system-transactions-" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".csv";
                dialog.Title = "Export Transactions";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                var transactions = _filteredItems.OfType<TransactionDto>().ToList();
                var builder = new StringBuilder();
                builder.AppendLine("Id,TransactionDate,Type,Amount,AccountId,TargetAccountId,CardId,TargetCardId,ServiceId,ServiceAccountId,PaymentReference,Description");
                foreach (var transaction in transactions)
                {
                    builder.AppendLine(string.Join(",",
                        transaction.Id.ToString(CultureInfo.InvariantCulture),
                        EscapeCsv(transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)),
                        EscapeCsv(transaction.Type),
                        transaction.Amount.ToString("0.00", CultureInfo.InvariantCulture),
                        NullableToCsv(transaction.AccountId),
                        NullableToCsv(transaction.TargetAccountId),
                        NullableToCsv(transaction.CardId),
                        NullableToCsv(transaction.TargetCardId),
                        NullableToCsv(transaction.ServiceId),
                        NullableToCsv(transaction.ServiceAccountId),
                        EscapeCsv(transaction.PaymentReference),
                        EscapeCsv(transaction.Description)));
                }

                File.WriteAllText(dialog.FileName, builder.ToString(), Encoding.UTF8);
                MessageBox.Show("Transactions exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            ShowSelectedTransactionDetails();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (ShowSelectedTransactionDetails())
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ApplyFilter()
        {
            string query = (txtSearch.Text ?? string.Empty).Trim();
            IEnumerable<object> filtered = _allItems;

            if (!string.IsNullOrWhiteSpace(query))
            {
                string lowered = query.ToLowerInvariant();
                filtered = _allItems.Where(item => MatchesSearch(item, lowered));
            }

            if (_mode == "TRANSACTIONS")
            {
                filtered = filtered.Where(MatchesTransactionFilters);
            }

            var filteredList = filtered.ToList();
            _filteredItems = filteredList;
            _bindingSource.DataSource = filteredList;
            dataGridView.DataSource = _bindingSource;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ConfigureColumns();
            lblResults.Text = LanguageManager.Format("ResultsCount", filteredList.Count);
            UpdateTransactionSummary(filteredList);
        }

        private bool ShowSelectedTransactionDetails()
        {
            if (_mode != "TRANSACTIONS" || dataGridView.SelectedRows.Count == 0)
            {
                return false;
            }

            var transaction = dataGridView.SelectedRows[0].DataBoundItem as TransactionDto;
            if (transaction == null)
            {
                return false;
            }

            using (var form = new AdminTransactionDetailsForm(transaction))
            {
                form.ShowDialog(this);
            }

            return true;
        }

        private bool MatchesTransactionFilters(object item)
        {
            var transaction = item as TransactionDto;
            if (transaction == null)
            {
                return false;
            }

            string selectedType = cmbTransactionType.SelectedItem as string;
            if (!string.IsNullOrWhiteSpace(selectedType) &&
                selectedType != "All Types" &&
                !string.Equals(transaction.Type, selectedType, System.StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (dtpDateFrom.Checked && transaction.TransactionDate < dtpDateFrom.Value.Date)
            {
                return false;
            }

            if (dtpDateTo.Checked && transaction.TransactionDate >= dtpDateTo.Value.Date.AddDays(1))
            {
                return false;
            }

            decimal minAmount;
            if (!string.IsNullOrWhiteSpace(txtAmountMin.Text) &&
                TryParseDecimal(txtAmountMin.Text, out minAmount) &&
                transaction.Amount < minAmount)
            {
                return false;
            }

            decimal maxAmount;
            if (!string.IsNullOrWhiteSpace(txtAmountMax.Text) &&
                TryParseDecimal(txtAmountMax.Text, out maxAmount) &&
                transaction.Amount > maxAmount)
            {
                return false;
            }

            return true;
        }

        private void UpdateTransactionSummary(List<object> filteredList)
        {
            if (_mode != "TRANSACTIONS")
            {
                lblTransactionSummary.Text = string.Empty;
                return;
            }

            var transactions = filteredList.OfType<TransactionDto>().ToList();
            if (transactions.Count == 0)
            {
                lblTransactionSummary.Text = "No transactions match the current filters.";
                return;
            }

            decimal totalAmount = transactions.Sum(transaction => transaction.Amount);
            int withdrawals = transactions.Count(transaction => string.Equals(transaction.Type, "Withdraw", System.StringComparison.OrdinalIgnoreCase));
            int deposits = transactions.Count(transaction => string.Equals(transaction.Type, "Deposit", System.StringComparison.OrdinalIgnoreCase));
            int transfers = transactions.Count(transaction => string.Equals(transaction.Type, "Transfer", System.StringComparison.OrdinalIgnoreCase));
            int payments = transactions.Count(transaction => string.Equals(transaction.Type, "BillPayment", System.StringComparison.OrdinalIgnoreCase));
            int exchanges = transactions.Count(transaction => string.Equals(transaction.Type, "Exchange", System.StringComparison.OrdinalIgnoreCase));

            lblTransactionSummary.Text = string.Format(
                CultureInfo.InvariantCulture,
                "Filtered total: {0:N0} | Amount sum: {1:N2} | Withdraw: {2} | Deposit: {3} | Transfer: {4} | Bill pay: {5} | Exchange: {6}",
                transactions.Count,
                totalAmount,
                withdrawals,
                deposits,
                transfers,
                payments,
                exchanges);
        }

        private static bool TryParseDecimal(string raw, out decimal value)
        {
            return decimal.TryParse(raw, NumberStyles.Number, CultureInfo.CurrentCulture, out value) ||
                   decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private static string NullableToCsv(int? value)
        {
            return value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        private static string EscapeCsv(string value)
        {
            string normalized = value ?? string.Empty;
            if (normalized.Contains("\""))
            {
                normalized = normalized.Replace("\"", "\"\"");
            }

            if (normalized.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
            {
                normalized = "\"" + normalized + "\"";
            }

            return normalized;
        }

        private bool MatchesSearch(object item, string query)
        {
            if (item == null) return false;

            foreach (PropertyInfo property in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object value = property.GetValue(item, null);
                if (value != null && value.ToString().ToLowerInvariant().Contains(query))
                {
                    return true;
                }
            }

            return false;
        }

        private void ConfigureColumns()
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.HeaderText = MakeHeader(column.DataPropertyName ?? column.Name);
            }

            HideColumn("PinHash");
            HideColumn("PasswordHash");

            if (_mode == "USERS")
            {
                HideColumn("Id");
            }
            else if (_mode == "SERVICES")
            {
                HideColumn("Id");
            }
            else if (_mode == "CURRENCIES")
            {
                HideColumn("Id");
            }
            else if (_mode == "FEES")
            {
                HideColumn("Id");
            }
            else if (_mode == "CASH")
            {
                HideColumn("AtmId");
                HideColumn("CurrencyId");
            }
        }

        private void HideColumn(string name)
        {
            if (dataGridView.Columns.Contains(name))
            {
                dataGridView.Columns[name].Visible = false;
            }
        }

        private string MakeHeader(string propertyName)
        {
            switch (propertyName)
            {
                case "PrimaryAccountId": return "Account ID";
                case "PrimaryAccountNumber": return "Account Number";
                case "CardNumber": return "Card Number";
                case "CardType": return "Card Type";
                case "PhoneNumber": return "Phone";
                case "FullName": return "Full Name";
                case "CreatedAt": return "Created";
                case "ServiceName": return "Service Name";
                case "AccountHint": return "Payment Reference";
                case "ValidReferenceCount": return "Valid IDs";
                case "IsActive": return "Active";
                case "ServiceId": return "Service";
                case "ServiceAccountId": return "Service Account";
                case "PaymentReference": return "Payment Reference";
                case "CashbackPercent": return "Cashback %";
                case "CashbackAmount": return "Cashback";
                case "CurrencyName": return "Currency Name";
                case "RateToUzs": return "Rate to UZS";
                case "BuyRateToUzs": return "Buy rate to UZS";
                case "SellRateToUzs": return "Sell rate to UZS";
                case "FeeAmount": return "Fee";
                case "TotalDebited": return "Total debited";
                case "NetAmount": return "Net amount";
                case "ExchangeRate": return "Exchange rate";
                case "RateKind": return "Rate type";
                case "PercentFee": return "Percent %";
                case "FixedFee": return "Fixed fee";
                case "MinFee": return "Min fee";
                case "MaxFee": return "Max fee";
                case "TransactionType": return "Transaction Type";
                case "CashAvailable": return "ATM Cash";
                case "CurrencyCode": return "Currency";
                case "DenominationValue": return "Denomination";
                case "NoteCount": return "Notes";
                case "TotalValue": return "Total";
                case "UpdatedAt": return "Updated";
                case "ExpiryDate": return "Expires";
                case "FailedAttempts": return "Failed PIN Attempts";
                case "LockedUntil": return "Locked Until";
                case "TransactionDate": return "Transaction Date";
                case "TargetAccountId": return "Target Account";
                case "AccountId": return "Account";
                case "CardId": return "Card";
                case "TargetCardId": return "Target Card";
                default:
                    if (string.IsNullOrEmpty(propertyName)) return propertyName;
                    var chars = propertyName.Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString());
                    return string.Concat(chars);
            }
        }
    }
}
