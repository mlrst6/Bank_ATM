using System.Drawing;

namespace Bank_ATM.Admin
{
    partial class AdminDataViewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.btnBack = new Krypton.Toolkit.KryptonButton();
            this.btnAdd = new Krypton.Toolkit.KryptonButton();
            this.btnEdit = new Krypton.Toolkit.KryptonButton();
            this.btnDelete = new Krypton.Toolkit.KryptonButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblResults = new System.Windows.Forms.Label();
            this.lblTransactionType = new System.Windows.Forms.Label();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblAmountMin = new System.Windows.Forms.Label();
            this.txtAmountMin = new System.Windows.Forms.TextBox();
            this.lblAmountMax = new System.Windows.Forms.Label();
            this.txtAmountMax = new System.Windows.Forms.TextBox();
            this.lblCardFilter = new System.Windows.Forms.Label();
            this.txtCardFilter = new System.Windows.Forms.TextBox();
            this.btnClearFilters = new Krypton.Toolkit.KryptonButton();
            this.btnExportCsv = new Krypton.Toolkit.KryptonButton();
            this.lblTransactionSummary = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeight = 42;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.dataGridView.Location = new System.Drawing.Point(24, 246);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.Size = new System.Drawing.Size(1012, 364);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(620, 46);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Data View";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(27, 66);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(278, 23);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Search, review, and manage records";
            // 
            // btnBack
            // 
            this.btnBack.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Location = new System.Drawing.Point(876, 632);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(160, 48);
            this.btnBack.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBack.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBack.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnBack.StateCommon.Border.Rounding = 10F;
            this.btnBack.StateCommon.Border.Width = 2;
            this.btnBack.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnBack.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnBack.TabIndex = 8;
            this.btnBack.Values.Text = "BACK";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Location = new System.Drawing.Point(24, 632);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 48);
            this.btnAdd.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnAdd.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnAdd.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnAdd.StateCommon.Border.Rounding = 10F;
            this.btnAdd.StateCommon.Border.Width = 2;
            this.btnAdd.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnAdd.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Values.Text = "ADD NEW";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.Location = new System.Drawing.Point(188, 632);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(150, 48);
            this.btnEdit.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnEdit.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnEdit.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnEdit.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnEdit.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnEdit.StateCommon.Border.Rounding = 10F;
            this.btnEdit.StateCommon.Border.Width = 2;
            this.btnEdit.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnEdit.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Values.Text = "EDIT";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Location = new System.Drawing.Point(352, 632);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(170, 48);
            this.btnDelete.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnDelete.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnDelete.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnDelete.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnDelete.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnDelete.StateCommon.Border.Rounding = 10F;
            this.btnDelete.StateCommon.Border.Width = 2;
            this.btnDelete.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnDelete.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Values.Text = "DELETE";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(726, 44);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(310, 32);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblSearch.Location = new System.Drawing.Point(723, 18);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(114, 23);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search records";
            // 
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblResults.Location = new System.Drawing.Point(27, 109);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(116, 23);
            this.lblResults.TabIndex = 9;
            this.lblResults.Text = "0 results loaded";
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.AutoSize = true;
            this.lblTransactionType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTransactionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblTransactionType.Location = new System.Drawing.Point(24, 142);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new System.Drawing.Size(41, 21);
            this.lblTransactionType.TabIndex = 10;
            this.lblTransactionType.Text = "Type";
            this.lblTransactionType.Visible = false;
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cmbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTransactionType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbTransactionType.ForeColor = System.Drawing.Color.White;
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Location = new System.Drawing.Point(24, 166);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(150, 29);
            this.cmbTransactionType.TabIndex = 11;
            this.cmbTransactionType.Visible = false;
            this.cmbTransactionType.SelectedIndexChanged += new System.EventHandler(this.TransactionFilterChanged);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDateFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblDateFrom.Location = new System.Drawing.Point(190, 142);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(82, 21);
            this.lblDateFrom.TabIndex = 12;
            this.lblDateFrom.Text = "Date from";
            this.lblDateFrom.Visible = false;
            //
            // dtpDateFrom
            //
            this.dtpDateFrom.CalendarForeColor = System.Drawing.Color.White;
            this.dtpDateFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dtpDateFrom.Checked = false;
            this.dtpDateFrom.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(190, 166);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.ShowCheckBox = true;
            this.dtpDateFrom.Size = new System.Drawing.Size(185, 29);
            this.dtpDateFrom.TabIndex = 13;
            this.dtpDateFrom.Visible = false;
            this.dtpDateFrom.ValueChanged += new System.EventHandler(this.TransactionFilterChanged);
            //
            // lblDateTo
            //
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblDateTo.Location = new System.Drawing.Point(391, 142);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(60, 21);
            this.lblDateTo.TabIndex = 14;
            this.lblDateTo.Text = "Date to";
            this.lblDateTo.Visible = false;
            //
            // dtpDateTo
            //
            this.dtpDateTo.CalendarForeColor = System.Drawing.Color.White;
            this.dtpDateTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dtpDateTo.Checked = false;
            this.dtpDateTo.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(391, 166);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.ShowCheckBox = true;
            this.dtpDateTo.Size = new System.Drawing.Size(185, 29);
            this.dtpDateTo.TabIndex = 15;
            this.dtpDateTo.Visible = false;
            this.dtpDateTo.ValueChanged += new System.EventHandler(this.TransactionFilterChanged);
            //
            // lblAmountMin
            //
            this.lblAmountMin.AutoSize = true;
            this.lblAmountMin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAmountMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblAmountMin.Location = new System.Drawing.Point(592, 142);
            this.lblAmountMin.Name = "lblAmountMin";
            this.lblAmountMin.Size = new System.Drawing.Size(93, 21);
            this.lblAmountMin.TabIndex = 16;
            this.lblAmountMin.Text = "Min amount";
            this.lblAmountMin.Visible = false;
            //
            // txtAmountMin
            //
            this.txtAmountMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAmountMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountMin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAmountMin.ForeColor = System.Drawing.Color.White;
            this.txtAmountMin.Location = new System.Drawing.Point(592, 166);
            this.txtAmountMin.Name = "txtAmountMin";
            this.txtAmountMin.Size = new System.Drawing.Size(110, 29);
            this.txtAmountMin.TabIndex = 17;
            this.txtAmountMin.Visible = false;
            this.txtAmountMin.TextChanged += new System.EventHandler(this.TransactionFilterChanged);
            //
            // lblAmountMax
            //
            this.lblAmountMax.AutoSize = true;
            this.lblAmountMax.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAmountMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblAmountMax.Location = new System.Drawing.Point(718, 142);
            this.lblAmountMax.Name = "lblAmountMax";
            this.lblAmountMax.Size = new System.Drawing.Size(97, 21);
            this.lblAmountMax.TabIndex = 18;
            this.lblAmountMax.Text = "Max amount";
            this.lblAmountMax.Visible = false;
            //
            // txtAmountMax
            //
            this.txtAmountMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAmountMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountMax.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAmountMax.ForeColor = System.Drawing.Color.White;
            this.txtAmountMax.Location = new System.Drawing.Point(718, 166);
            this.txtAmountMax.Name = "txtAmountMax";
            this.txtAmountMax.Size = new System.Drawing.Size(110, 29);
            this.txtAmountMax.TabIndex = 19;
            this.txtAmountMax.Visible = false;
            this.txtAmountMax.TextChanged += new System.EventHandler(this.TransactionFilterChanged);
            //
            // lblCardFilter
            //
            this.lblCardFilter.AutoSize = true;
            this.lblCardFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCardFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCardFilter.Location = new System.Drawing.Point(844, 142);
            this.lblCardFilter.Name = "lblCardFilter";
            this.lblCardFilter.Size = new System.Drawing.Size(97, 21);
            this.lblCardFilter.TabIndex = 23;
            this.lblCardFilter.Text = "Card Number";
            this.lblCardFilter.Visible = false;
            //
            // txtCardFilter
            //
            this.txtCardFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCardFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCardFilter.ForeColor = System.Drawing.Color.White;
            this.txtCardFilter.Location = new System.Drawing.Point(844, 166);
            this.txtCardFilter.Name = "txtCardFilter";
            this.txtCardFilter.Size = new System.Drawing.Size(148, 29);
            this.txtCardFilter.TabIndex = 24;
            this.txtCardFilter.Visible = false;
            this.txtCardFilter.TextChanged += new System.EventHandler(this.TransactionFilterChanged);
            //
            // btnClearFilters
            //
            this.btnClearFilters.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnClearFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearFilters.Location = new System.Drawing.Point(24, 203);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(116, 33);
            this.btnClearFilters.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClearFilters.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClearFilters.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClearFilters.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClearFilters.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnClearFilters.StateCommon.Border.Rounding = 8F;
            this.btnClearFilters.StateCommon.Border.Width = 2;
            this.btnClearFilters.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnClearFilters.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClearFilters.TabIndex = 20;
            this.btnClearFilters.Values.Text = "Clear Filters";
            this.btnClearFilters.Visible = false;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCsv.Location = new System.Drawing.Point(152, 203);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(116, 33);
            this.btnExportCsv.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnExportCsv.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnExportCsv.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnExportCsv.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnExportCsv.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnExportCsv.StateCommon.Border.Rounding = 8F;
            this.btnExportCsv.StateCommon.Border.Width = 2;
            this.btnExportCsv.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnExportCsv.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnExportCsv.TabIndex = 21;
            this.btnExportCsv.Values.Text = "Export CSV";
            this.btnExportCsv.Visible = false;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // lblTransactionSummary
            // 
            this.lblTransactionSummary.AutoSize = true;
            this.lblTransactionSummary.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTransactionSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.lblTransactionSummary.Location = new System.Drawing.Point(24, 616);
            this.lblTransactionSummary.Name = "lblTransactionSummary";
            this.lblTransactionSummary.Size = new System.Drawing.Size(0, 20);
            this.lblTransactionSummary.TabIndex = 22;
            this.lblTransactionSummary.Visible = false;
            // 
            // AdminDataViewForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1067, 702);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.lblTransactionSummary);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.txtCardFilter);
            this.Controls.Add(this.lblCardFilter);
            this.Controls.Add(this.txtAmountMax);
            this.Controls.Add(this.lblAmountMax);
            this.Controls.Add(this.txtAmountMin);
            this.Controls.Add(this.lblAmountMin);
            this.Controls.Add(this.dtpDateTo);
            this.Controls.Add(this.lblDateTo);
            this.Controls.Add(this.dtpDateFrom);
            this.Controls.Add(this.lblDateFrom);
            this.Controls.Add(this.cmbTransactionType);
            this.Controls.Add(this.lblTransactionType);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminDataViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AdminDataViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Krypton.Toolkit.KryptonButton btnBack;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonButton btnEdit;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.Label lblTransactionType;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lblAmountMin;
        private System.Windows.Forms.TextBox txtAmountMin;
        private System.Windows.Forms.Label lblAmountMax;
        private System.Windows.Forms.TextBox txtAmountMax;
        private System.Windows.Forms.Label lblCardFilter;
        private System.Windows.Forms.TextBox txtCardFilter;
        private Krypton.Toolkit.KryptonButton btnClearFilters;
        private Krypton.Toolkit.KryptonButton btnExportCsv;
        private System.Windows.Forms.Label lblTransactionSummary;
    }
}
