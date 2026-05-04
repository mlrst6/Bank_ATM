using System;
using System.Globalization;
using System.Windows.Forms;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Admin
{
    public partial class AdminCategoryEditForm : Form
    {
        private readonly AdminService _adminService = new AdminService();
        private readonly ServiceCategoryDto _category;
        private readonly bool _isEdit;

        public AdminCategoryEditForm()
        {
            InitializeComponent();
            _category = new ServiceCategoryDto { IsActive = true, SortOrder = 100 };
        }

        public AdminCategoryEditForm(ServiceCategoryDto category)
        {
            InitializeComponent();
            _category = category;
            _isEdit = true;
        }

        private void AdminCategoryEditForm_Load(object sender, EventArgs e)
        {
            btnSave.Text = LanguageManager.GetString("Save");
            btnSave.Values.Text = btnSave.Text;
            btnCancel.Text = LanguageManager.GetString("Cancel");
            btnCancel.Values.Text = btnCancel.Text;
            lblTitle.Text = _isEdit ? "Edit Category" : "Create Category";

            txtName.Text = _category.Name ?? string.Empty;
            txtIcon.Text = _category.IconEmoji ?? string.Empty;
            txtSortOrder.Text = _category.SortOrder.ToString(CultureInfo.InvariantCulture);
            chkIsActive.Checked = _category.IsActive;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = (txtName.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Category name is required.", LanguageManager.GetString("Validation"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            int sortOrder;
            if (!int.TryParse((txtSortOrder.Text ?? string.Empty).Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out sortOrder))
            {
                sortOrder = _category.SortOrder;
            }

            _category.Name = name;
            _category.IconEmoji = (txtIcon.Text ?? string.Empty).Trim();
            _category.SortOrder = sortOrder;
            _category.IsActive = chkIsActive.Checked;

            try
            {
                _adminService.SaveServiceCategory(_category, _isEdit);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LanguageManager.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
