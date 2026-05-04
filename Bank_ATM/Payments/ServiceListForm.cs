using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Bank_ATM.Core;
using Bank_ATM.Models;
using Bank_ATM.Services;
using Krypton.Toolkit;

namespace Bank_ATM.Payments
{
    public partial class ServiceListForm : Form
    {
        private const int TilesPerPage = 15;
        private static readonly Size TileSize = new Size(195, 145);

        private readonly BankingService _bankingService = new BankingService();
        private readonly ServiceCategoryDto _category;
        private readonly bool _chargeCurrentAccount;
        private ServiceDto[] _services = new ServiceDto[0];
        private int _currentPage;

        public ServiceListForm(ServiceCategoryDto category, bool chargeCurrentAccount)
        {
            InitializeComponent();
            _category = category;
            _chargeCurrentAccount = chargeCurrentAccount;
        }

        private void ServiceListForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            string emoji = string.IsNullOrWhiteSpace(_category?.IconEmoji) ? string.Empty : _category.IconEmoji + "  ";
            lblTitle.Text = emoji + (_category?.Name ?? string.Empty).ToUpperInvariant();
            lblSubtitle.Text = LanguageManager.GetString("ChooseService");
            btnPrev.Values.Text = LanguageManager.GetString("Prev");
            btnNext.Values.Text = LanguageManager.GetString("Next");
            btnBack.Values.Text = LanguageManager.GetString("Back");

            LoadServices();
        }

        private void LoadServices()
        {
            try
            {
                _services = _category == null
                    ? new ServiceDto[0]
                    : _bankingService.GetServicesByCategory(_category.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                _services = new ServiceDto[0];
            }

            _currentPage = 0;
            RenderPage();
        }

        private void RenderPage()
        {
            flowServices.SuspendLayout();
            flowServices.Controls.Clear();

            int totalPages = Math.Max(1, (int)Math.Ceiling(_services.Length / (double)TilesPerPage));
            if (_currentPage >= totalPages) _currentPage = totalPages - 1;
            if (_currentPage < 0) _currentPage = 0;

            var pageItems = _services
                .Skip(_currentPage * TilesPerPage)
                .Take(TilesPerPage)
                .ToArray();

            foreach (var service in pageItems)
            {
                flowServices.Controls.Add(BuildServiceTile(service));
            }

            flowServices.ResumeLayout();

            lblPageInfo.Text = LanguageManager.Format("PageOfTotal", _currentPage + 1, totalPages);
            btnPrev.Enabled = _currentPage > 0;
            btnNext.Enabled = _currentPage < totalPages - 1;
            bool needsPagination = totalPages > 1;
            btnPrev.Visible = needsPagination;
            btnNext.Visible = needsPagination;
            lblPageInfo.Visible = needsPagination;
        }

        private KryptonButton BuildServiceTile(ServiceDto service)
        {
            var tile = new KryptonButton
            {
                Size = TileSize,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = service,
                ButtonStyle = ButtonStyle.Custom1,
                TabStop = false
            };
            tile.Values.Text = string.IsNullOrWhiteSpace(service.IconEmoji) ? "•" : service.IconEmoji;
            tile.Values.ExtraText = service.ServiceName ?? string.Empty;

            tile.StateCommon.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.StateCommon.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.StateCommon.Back.ColorAngle = 90F;
            tile.StateCommon.Border.Color1 = Color.FromArgb(34, 197, 94);
            tile.StateCommon.Border.Color2 = Color.FromArgb(22, 163, 74);
            tile.StateCommon.Border.Rounding = 12F;
            tile.StateCommon.Border.Width = 2;
            tile.StateCommon.Content.ShortText.Color1 = Color.White;
            tile.StateCommon.Content.ShortText.Font = new Font("Segoe UI Emoji", 26F, FontStyle.Regular, GraphicsUnit.Point);
            tile.StateCommon.Content.LongText.Color1 = Color.FromArgb(226, 232, 240);
            tile.StateCommon.Content.LongText.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            tile.StateNormal.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.StateNormal.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.OverrideDefault.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.OverrideDefault.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.OverrideDefault.Border.Color1 = Color.FromArgb(34, 197, 94);
            tile.OverrideDefault.Border.Color2 = Color.FromArgb(22, 163, 74);
            tile.OverrideDefault.Border.Rounding = 12F;
            tile.OverrideDefault.Border.Width = 2;
            tile.OverrideDefault.Content.ShortText.Color1 = Color.White;
            tile.OverrideDefault.Content.LongText.Color1 = Color.FromArgb(226, 232, 240);
            tile.OverrideFocus.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.OverrideFocus.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.OverrideFocus.Border.Color1 = Color.FromArgb(34, 197, 94);
            tile.OverrideFocus.Border.Color2 = Color.FromArgb(22, 163, 74);
            tile.OverrideFocus.Border.Rounding = 12F;
            tile.OverrideFocus.Border.Width = 2;
            tile.OverrideFocus.Content.ShortText.Color1 = Color.White;
            tile.OverrideFocus.Content.LongText.Color1 = Color.FromArgb(226, 232, 240);
            tile.StateTracking.Back.Color1 = Color.FromArgb(22, 163, 74);
            tile.StateTracking.Back.Color2 = Color.FromArgb(21, 128, 61);
            tile.StatePressed.Back.Color1 = Color.FromArgb(21, 128, 61);
            tile.StatePressed.Back.Color2 = Color.FromArgb(20, 83, 45);

            tile.Click += ServiceTile_Click;
            return tile;
        }

        private void ServiceTile_Click(object sender, EventArgs e)
        {
            var tile = sender as KryptonButton;
            var service = tile?.Tag as ServiceDto;
            if (service == null)
            {
                return;
            }

            using (var paymentForm = new ServicePaymentForm(service, _chargeCurrentAccount))
            {
                paymentForm.ShowDialog(this);
            }

            ActiveControl = btnBack;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                RenderPage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling(_services.Length / (double)TilesPerPage);
            if (_currentPage < totalPages - 1)
            {
                _currentPage++;
                RenderPage();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
