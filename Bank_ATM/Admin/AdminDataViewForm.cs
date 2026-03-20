using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Repositories;

namespace Bank_ATM.Admin
{
    public partial class AdminDataViewForm : BaseForm
    {
        private object _dataSource;
        private string _title;
        private string _mode; // "USERS", "CARDS", "TRANSACTIONS"

        public AdminDataViewForm(string title, object dataSource, string mode)
        {
            InitializeComponent();
            _title = title;
            _dataSource = dataSource;
            _mode = mode;
        }

        private void AdminDataViewForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = _title;
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
                dataGridView.DataSource = await new AccountRepository().GetAllUsersAsync();
            else if (_mode == "CARDS")
                dataGridView.DataSource = new CardRepository().GetAllCards();
            else
                dataGridView.DataSource = _dataSource; // Transactions remain static for this view session

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            try
            {
                if (_mode == "USERS")
                {
                    var user = (UserDto)dataGridView.SelectedRows[0].DataBoundItem;
                    await new AccountRepository().DeleteUserAsync(user.Id);
                }
                else if (_mode == "CARDS")
                {
                    var card = (CardDto)dataGridView.SelectedRows[0].DataBoundItem;
                    new CardRepository().DeleteCard(card.Id);
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete failed: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
