using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
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

        public AdminDataViewForm(string title, object dataSource, string mode)
        {
            InitializeComponent();
            _title = title;
            _dataSource = dataSource;
            _mode = mode;
        }

        private void AdminDataViewForm_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            lblTitle.Text = _title;
            btnAdd.Text = LanguageManager.GetString("Add");
            btnEdit.Text = LanguageManager.GetString("Edit");
            btnBack.Text = LanguageManager.GetString("Back");
            btnDelete.Text = _mode == "USERS"
                ? LanguageManager.GetString("DeactivateUser")
                : LanguageManager.GetString("Delete");
            RefreshGrid();

            // Disable CRUD for Transactions
            bool isReadOnly = _mode == "TRANSACTIONS";
            btnAdd.Visible = !isReadOnly;
            btnEdit.Visible = !isReadOnly;
            btnDelete.Visible = !isReadOnly;
        }

        private async void RefreshGrid()
        {
            if (_mode == "USERS")
                _allItems = (await _adminService.GetAllUsersAsync()).Cast<object>().ToList();
            else if (_mode == "CARDS")
                _allItems = _adminService.GetAllCards().Cast<object>().ToList();
            else if (_mode == "SERVICES")
                _allItems = _adminService.GetAllServices().Cast<object>().ToList();
            else
                _allItems = ((System.Collections.IEnumerable)_dataSource).Cast<object>().ToList();

            ApplyFilter();
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

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StyleTitle(lblTitle);
            AdminTheme.StyleLabel(lblSubtitle, true);
            AdminTheme.StyleLabel(lblSearch, true);
            AdminTheme.StyleLabel(lblResults, true);
            AdminTheme.StyleTextBox(txtSearch);
            AdminTheme.StyleGrid(dataGridView);
            AdminTheme.StylePrimaryButton(btnAdd);
            AdminTheme.StyleSecondaryButton(btnEdit);
            AdminTheme.StyleDangerButton(btnDelete);
            AdminTheme.StyleSecondaryButton(btnBack);
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

            var filteredList = filtered.ToList();
            _bindingSource.DataSource = filteredList;
            dataGridView.DataSource = _bindingSource;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ConfigureColumns();
            lblResults.Text = LanguageManager.Format("ResultsCount", filteredList.Count);
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
                case "PhoneNumber": return "Phone";
                case "FullName": return "Full Name";
                case "CreatedAt": return "Created";
                case "ServiceName": return "Service Name";
                case "AccountHint": return "Payment Reference";
                case "IsActive": return "Active";
                case "ExpiryDate": return "Expires";
                case "FailedAttempts": return "Failed PIN Attempts";
                case "LockedUntil": return "Locked Until";
                case "TransactionDate": return "Transaction Date";
                case "TargetAccountId": return "Target Account";
                case "AccountId": return "Account";
                default:
                    if (string.IsNullOrEmpty(propertyName)) return propertyName;
                    var chars = propertyName.Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString());
                    return string.Concat(chars);
            }
        }
    }
}
