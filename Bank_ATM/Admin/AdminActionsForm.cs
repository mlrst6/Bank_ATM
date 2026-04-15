using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Services;

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

        private async void pnlTransactions_Click(object sender, EventArgs e)
        {
            var currencies = _adminService.GetAllCurrencies()
                .Where(currency => currency.IsActive)
                .OrderBy(currency => currency.Code)
                .ToList();

            if (!currencies.Any())
            {
                MessageBox.Show(LanguageManager.GetString("SelectedCurrencyUnavailable"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string availableCodes = string.Join(", ", currencies.Select(currency => currency.Code));
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                LanguageManager.Format("SelectDashboardCurrency", availableCodes),
                LanguageManager.GetString("AtmCashAvailable"),
                _selectedAtmCashCurrencyCode);

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            string currencyCode = input.Trim().ToUpperInvariant();
            if (!currencies.Any(currency => currency.Code == currencyCode))
            {
                MessageBox.Show(LanguageManager.GetString("SelectedCurrencyUnavailable"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedAtmCashCurrencyCode = currencyCode;
            await RefreshStatsAsync();
        }

        private async void btnRefillAtm_Click(object sender, EventArgs e)
        {
            var atm = _adminService.GetDefaultAtm();
            string prompt = atm == null
                ? LanguageManager.GetString("EnterAtmRefillAmount")
                : LanguageManager.Format("AtmCashPrompt", atm.CurrentBalance);
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                prompt,
                LanguageManager.GetString("RefillAtmCash"),
                "1000000");

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            decimal amount;
            if (!decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out amount) &&
                !decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out amount))
            {
                MessageBox.Show(LanguageManager.GetString("InvalidPaymentAmount"), LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _adminService.AddAtmCash(amount);
                await RefreshStatsAsync();
                var updatedAtm = _adminService.GetDefaultAtm();
                MessageBox.Show(
                    LanguageManager.Format("AtmCashUpdated", updatedAtm.CurrentBalance),
                    LanguageManager.GetString("Success"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
