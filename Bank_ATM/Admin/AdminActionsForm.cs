using System;
using System.Drawing;
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
            ApplyTheme();
            LanguageManager.Apply(this);
            lblAdminTitle.Text = LanguageManager.Format("AdminWelcomeTitle", SessionManager.Instance.CurrentUser.FullName);
            lblAdminSubtitle.Text = LanguageManager.GetString("AdminDashboardSubtitle");
            
            btnManageUsers.Text = LanguageManager.GetString("AdminUsers");
            btnManageCards.Text = LanguageManager.GetString("AdminCards");
            btnManageServices.Text = LanguageManager.GetString("AdminServices");
            btnAuditLogs.Text = LanguageManager.GetString("AdminTransactions");
            btnManageCurrencies.Text = LanguageManager.GetString("AdminCurrencies");
            btnRefillAtm.Text = LanguageManager.GetString("RefillAtmCash");
            btnLogout.Text = LanguageManager.GetString("Logout");

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

        private void ApplyTheme()
        {
            AdminTheme.ApplyForm(this);
            AdminTheme.StyleTitle(lblAdminTitle);
            AdminTheme.StyleLabel(lblAdminSubtitle, true);
            AdminTheme.StylePanel(pnlUsers);
            AdminTheme.StylePanel(pnlCards);
            AdminTheme.StylePanel(pnlServices);
            AdminTheme.StylePanel(pnlTransactions);
            pnlTransactions.Cursor = Cursors.Hand;
            lblTransactionsCount.Cursor = Cursors.Hand;
            lblTransactionsCaption.Cursor = Cursors.Hand;
            pnlTransactions.Click += pnlTransactions_Click;
            lblTransactionsCount.Click += pnlTransactions_Click;
            lblTransactionsCaption.Click += pnlTransactions_Click;
            AdminTheme.StyleStatValue(lblUsersCount);
            AdminTheme.StyleStatValue(lblCardsCount);
            AdminTheme.StyleStatValue(lblServicesCount);
            AdminTheme.StyleStatValue(lblTransactionsCount);
            AdminTheme.StyleStatCaption(lblUsersCaption);
            AdminTheme.StyleStatCaption(lblCardsCaption);
            AdminTheme.StyleStatCaption(lblServicesCaption);
            AdminTheme.StyleStatCaption(lblTransactionsCaption);
            AdminTheme.StylePrimaryButton(btnManageUsers);
            AdminTheme.StyleSuccessButton(btnManageCards);
            AdminTheme.StyleSuccessButton(btnManageServices);
            AdminTheme.StyleSecondaryButton(btnAuditLogs);
            AdminTheme.StyleSecondaryButton(btnManageCurrencies);
            AdminTheme.StylePrimaryButton(btnRefillAtm);
            AdminTheme.StyleDangerButton(btnLogout);

            btnManageUsers.TextAlign = ContentAlignment.MiddleLeft;
            btnManageCards.TextAlign = ContentAlignment.MiddleLeft;
            btnManageServices.TextAlign = ContentAlignment.MiddleLeft;
            btnAuditLogs.TextAlign = ContentAlignment.MiddleLeft;
            btnManageCurrencies.TextAlign = ContentAlignment.MiddleLeft;
            btnRefillAtm.TextAlign = ContentAlignment.MiddleLeft;
            btnManageUsers.Padding = new Padding(18, 0, 0, 0);
            btnManageCards.Padding = new Padding(18, 0, 0, 0);
            btnManageServices.Padding = new Padding(18, 0, 0, 0);
            btnAuditLogs.Padding = new Padding(18, 0, 0, 0);
            btnManageCurrencies.Padding = new Padding(18, 0, 0, 0);
            btnRefillAtm.Padding = new Padding(18, 0, 0, 0);
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
