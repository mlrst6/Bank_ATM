using System;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Services;
using Bank_ATM.UI;

namespace Bank_ATM.Admin
{
    public partial class AdminActionsForm : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private readonly AuthenticationService _authenticationService = new AuthenticationService();
        private string _selectedAtmCashCurrencyCode = "UZS";

        public AdminActionsForm()
        {
            InitializeComponent();
        }

        private async void AdminActionsForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            lblAdminTitle.Text = LanguageManager.Format("AdminWelcomeTitle", SessionManager.Instance.CurrentUser.FullName);
            lblAdminSubtitle.Text = LanguageManager.GetString("AdminDashboardSubtitle");
             
            btnManageUsers.Text = LanguageManager.GetString("AdminUsers");
            btnManageUsers.Values.Text = btnManageUsers.Text;
            btnManageCards.Text = LanguageManager.GetString("AdminCards");
            btnManageCards.Values.Text = btnManageCards.Text;
            btnManageServices.Text = LanguageManager.GetString("AdminServices");
            btnManageServices.Values.Text = btnManageServices.Text;
            btnAuditLogs.Text = LanguageManager.GetString("AdminTransactions");
            btnAuditLogs.Values.Text = btnAuditLogs.Text;
            btnManageCurrencies.Text = LanguageManager.GetString("AdminCurrencies");
            btnManageCurrencies.Values.Text = btnManageCurrencies.Text;
            btnRefillAtm.Text = LanguageManager.GetString("RefillAtmCash");
            btnRefillAtm.Values.Text = btnRefillAtm.Text;
            btnLogout.Text = LanguageManager.GetString("Logout");
            btnLogout.Values.Text = btnLogout.Text;

            await RefreshStatsAsync();
        }

        private async void btnManageUsers_Click(object sender, EventArgs e)
        {
            var users = await _adminService.GetAllUsersAsync();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM USERS", users, "USERS");
            viewForm.ShowDialog();
        }

        private void btnManageCards_Click(object sender, EventArgs e)
        {
            var cards = _adminService.GetAllCards();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM CARDS", cards, "CARDS");
            viewForm.ShowDialog();
        }

        private void btnAuditLogs_Click(object sender, EventArgs e)
        {
            var transactions = _adminService.GetAllTransactions();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM TRANSACTIONS", transactions, "TRANSACTIONS");
            viewForm.ShowDialog();
        }

        private void btnManageServices_Click(object sender, EventArgs e)
        {
            var services = _adminService.GetAllServices();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM SERVICES", services, "SERVICES");
            viewForm.ShowDialog();
        }

        private async void btnManageCurrencies_Click(object sender, EventArgs e)
        {
            var currencies = _adminService.GetAllCurrencies();
            AdminDataViewForm viewForm = new AdminDataViewForm("SYSTEM CURRENCIES", currencies, "CURRENCIES");
            viewForm.ShowDialog();
            await RefreshStatsAsync();
        }

        private void pnlTransactions_Click(object sender, EventArgs e)
        {
            var cash = _adminService.GetAllCashDenominations();
            AdminDataViewForm viewForm = new AdminDataViewForm("ATM CASH DENOMINATIONS", cash, "CASH");
            viewForm.ShowDialog();
        }

        private async void btnRefillAtm_Click(object sender, EventArgs e)
        {
            var currencies = _adminService.GetAllCurrencies()
                .Where(currency => currency.IsActive)
                .OrderBy(currency => currency.Code)
                .ToArray();
            if (!currencies.Any())
            {
                MessageBox.Show(LanguageManager.GetString("SelectedCurrencyUnavailable"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialog = new CashNoteInputDialog(
                LanguageManager.GetString("RefillAtmCash"),
                LanguageManager.GetString("RefillCashNotesSubtitle"),
                currencies,
                code => _adminService.GetCashDenominations(code).ToArray(),
                true))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    _adminService.AddAtmCashNotes(dialog.CurrencyId, dialog.Notes);
                    await RefreshStatsAsync();
                    MessageBox.Show(
                        LanguageManager.Format("AtmCashNotesUpdated", dialog.TotalAmount, dialog.CurrencyCode),
                        LanguageManager.GetString("Success"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _authenticationService.Logout();
            FormNavigator.ShowExistingOrNew<MainForm>(this);
        }

        private async System.Threading.Tasks.Task RefreshStatsAsync()
        {
            var users = (await _adminService.GetAllUsersAsync()).ToList();
            var cards = _adminService.GetAllCards().ToList();
            var services = _adminService.GetAllServices().ToList();
            var transactions = _adminService.GetAllTransactions().Take(9999).ToList();
            var currencies = _adminService.GetAllCurrencies().ToList();
            var selectedCurrency = currencies.FirstOrDefault(currency => currency.Code == _selectedAtmCashCurrencyCode && currency.IsActive)
                ?? currencies.FirstOrDefault(currency => currency.Code == "UZS" && currency.IsActive)
                ?? currencies.FirstOrDefault(currency => currency.IsActive);

            lblUsersCount.Text = users.Count.ToString("N0");
            lblCardsCount.Text = cards.Count(c => !c.IsBlocked).ToString("N0");
            lblServicesCount.Text = services.Count(s => s.IsActive).ToString("N0");
            lblTransactionsCount.Text = selectedCurrency == null
                ? transactions.Count.ToString("N0")
                : selectedCurrency.CashAvailable.ToString("N0");
            lblTransactionsCaption.Text = selectedCurrency == null
                ? LanguageManager.GetString("AtmCashAvailable")
                : LanguageManager.GetString("AtmCashAvailable") + " (" + selectedCurrency.Code + ")";
        }
    }
}
