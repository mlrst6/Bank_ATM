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
    public partial class ServiceCategoryForm : Form
    {
        private const int TilesPerPage = 15;
        private static readonly Size TileSize = new Size(195, 145);

        private readonly BankingService _bankingService = new BankingService();
        private readonly bool _chargeCurrentAccount;
        private ServiceCategoryDto[] _categories = new ServiceCategoryDto[0];
        private int _currentPage;

        public ServiceCategoryForm() : this(false)
        {
        }

        public ServiceCategoryForm(bool chargeCurrentAccount)
        {
            InitializeComponent();
            _chargeCurrentAccount = chargeCurrentAccount;
        }

        private void ServiceCategoryForm_Load(object sender, EventArgs e)
        {
            AppWindow.ApplyMainScreen(this);
            LanguageManager.Apply(this);
            lblTitle.Text = LanguageManager.GetString("PayServices");
            lblSubtitle.Text = _chargeCurrentAccount
                ? LanguageManager.GetString("PayFromAccountSubtitle")
                : LanguageManager.GetString("GuestServicePaymentSubtitle");
            btnPrev.Values.Text = LanguageManager.GetString("Prev");
            btnNext.Values.Text = LanguageManager.GetString("Next");
            btnBack.Values.Text = LanguageManager.GetString("Back");

            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                _categories = _bankingService.GetActiveServiceCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                _categories = new ServiceCategoryDto[0];
            }

            _currentPage = 0;
            RenderPage();
        }

        private void RenderPage()
        {
            flowCategories.SuspendLayout();
            flowCategories.Controls.Clear();

            int totalPages = Math.Max(1, (int)Math.Ceiling(_categories.Length / (double)TilesPerPage));
            if (_currentPage >= totalPages) _currentPage = totalPages - 1;
            if (_currentPage < 0) _currentPage = 0;

            var pageItems = _categories
                .Skip(_currentPage * TilesPerPage)
                .Take(TilesPerPage)
                .ToArray();

            foreach (var category in pageItems)
            {
                flowCategories.Controls.Add(BuildCategoryTile(category));
            }

            flowCategories.ResumeLayout();

            lblPageInfo.Text = LanguageManager.Format("PageOfTotal", _currentPage + 1, totalPages);
            btnPrev.Enabled = _currentPage > 0;
            btnNext.Enabled = _currentPage < totalPages - 1;
            bool needsPagination = totalPages > 1;
            btnPrev.Visible = needsPagination;
            btnNext.Visible = needsPagination;
            lblPageInfo.Visible = needsPagination;
        }

        private KryptonButton BuildCategoryTile(ServiceCategoryDto category)
        {
            var tile = new KryptonButton
            {
                Size = TileSize,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = category,
                ButtonStyle = ButtonStyle.Custom1,
                TabStop = false
            };
            tile.Values.Text = string.IsNullOrWhiteSpace(category.IconEmoji) ? "•" : category.IconEmoji;
            tile.Values.ExtraText = (category.Name ?? string.Empty).ToUpperInvariant();

            tile.StateCommon.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.StateCommon.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.StateCommon.Back.ColorAngle = 90F;
            tile.StateCommon.Border.Color1 = Color.FromArgb(59, 130, 246);
            tile.StateCommon.Border.Color2 = Color.FromArgb(37, 99, 235);
            tile.StateCommon.Border.Rounding = 12F;
            tile.StateCommon.Border.Width = 2;
            tile.StateCommon.Content.ShortText.Color1 = Color.White;
            tile.StateCommon.Content.ShortText.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Regular, GraphicsUnit.Point);
            tile.StateCommon.Content.LongText.Color1 = Color.FromArgb(226, 232, 240);
            tile.StateCommon.Content.LongText.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            tile.StateNormal.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.StateNormal.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.OverrideDefault.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.OverrideDefault.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.OverrideDefault.Border.Color1 = Color.FromArgb(59, 130, 246);
            tile.OverrideDefault.Border.Color2 = Color.FromArgb(37, 99, 235);
            tile.OverrideDefault.Border.Rounding = 12F;
            tile.OverrideDefault.Border.Width = 2;
            tile.OverrideDefault.Content.ShortText.Color1 = Color.White;
            tile.OverrideDefault.Content.LongText.Color1 = Color.FromArgb(226, 232, 240);
            tile.OverrideFocus.Back.Color1 = Color.FromArgb(30, 41, 59);
            tile.OverrideFocus.Back.Color2 = Color.FromArgb(15, 23, 42);
            tile.OverrideFocus.Border.Color1 = Color.FromArgb(59, 130, 246);
            tile.OverrideFocus.Border.Color2 = Color.FromArgb(37, 99, 235);
            tile.OverrideFocus.Border.Rounding = 12F;
            tile.OverrideFocus.Border.Width = 2;
            tile.OverrideFocus.Content.ShortText.Color1 = Color.White;
            tile.OverrideFocus.Content.LongText.Color1 = Color.FromArgb(226, 232, 240);
            tile.StateTracking.Back.Color1 = Color.FromArgb(48, 92, 180);
            tile.StateTracking.Back.Color2 = Color.FromArgb(28, 64, 140);
            tile.StatePressed.Back.Color1 = Color.FromArgb(28, 64, 140);
            tile.StatePressed.Back.Color2 = Color.FromArgb(15, 38, 90);

            tile.Click += CategoryTile_Click;
            return tile;
        }

        private void CategoryTile_Click(object sender, EventArgs e)
        {
            var tile = sender as KryptonButton;
            var category = tile?.Tag as ServiceCategoryDto;
            if (category == null)
            {
                return;
            }

            using (var listForm = new ServiceListForm(category, _chargeCurrentAccount))
            {
                listForm.ShowDialog(this);
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
            int totalPages = (int)Math.Ceiling(_categories.Length / (double)TilesPerPage);
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
