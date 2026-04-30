using System.Drawing;

namespace Bank_ATM.Admin
{
    partial class AdminServiceEditForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblAccountHint = new System.Windows.Forms.Label();
            this.txtAccountHint = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblCashbackPercent = new System.Windows.Forms.Label();
            this.txtCashbackPercent = new System.Windows.Forms.TextBox();
            this.lblValidReferences = new System.Windows.Forms.Label();
            this.pnlServiceAccounts = new System.Windows.Forms.Panel();
            this.dgvServiceAccounts = new System.Windows.Forms.DataGridView();
            this.btnAddServiceAccount = new Krypton.Toolkit.KryptonButton();
            this.btnDeactivateServiceAccount = new Krypton.Toolkit.KryptonButton();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.pnlServiceAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(28, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(219, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create Service";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(32, 72);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(311, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Set up a service users can pay from the ATM";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblServiceName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblServiceName.Location = new System.Drawing.Point(30, 112);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(104, 23);
            this.lblServiceName.TabIndex = 2;
            this.lblServiceName.Text = "Service name";
            // 
            // txtServiceName
            // 
            this.txtServiceName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtServiceName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtServiceName.ForeColor = System.Drawing.Color.White;
            this.txtServiceName.Location = new System.Drawing.Point(33, 138);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(422, 32);
            this.txtServiceName.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCategory.Location = new System.Drawing.Point(30, 182);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(73, 23);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cmbCategory.ForeColor = System.Drawing.Color.White;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(33, 208);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(422, 31);
            this.cmbCategory.TabIndex = 5;
            // 
            // lblAccountHint
            // 
            this.lblAccountHint.AutoSize = true;
            this.lblAccountHint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAccountHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblAccountHint.Location = new System.Drawing.Point(30, 251);
            this.lblAccountHint.Name = "lblAccountHint";
            this.lblAccountHint.Size = new System.Drawing.Size(139, 23);
            this.lblAccountHint.TabIndex = 6;
            this.lblAccountHint.Text = "Payment reference";
            // 
            // txtAccountHint
            // 
            this.txtAccountHint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAccountHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountHint.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAccountHint.ForeColor = System.Drawing.Color.White;
            this.txtAccountHint.Location = new System.Drawing.Point(33, 277);
            this.txtAccountHint.Name = "txtAccountHint";
            this.txtAccountHint.Size = new System.Drawing.Size(422, 32);
            this.txtAccountHint.TabIndex = 7;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHint.Location = new System.Drawing.Point(33, 315);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(270, 20);
            this.lblHint.TabIndex = 8;
            this.lblHint.Text = "Example: phone number, subscriber ID, meter.";
            // 
            // lblCashbackPercent
            // 
            this.lblCashbackPercent.AutoSize = true;
            this.lblCashbackPercent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCashbackPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCashbackPercent.Location = new System.Drawing.Point(30, 344);
            this.lblCashbackPercent.Name = "lblCashbackPercent";
            this.lblCashbackPercent.Size = new System.Drawing.Size(153, 23);
            this.lblCashbackPercent.TabIndex = 9;
            this.lblCashbackPercent.Text = "Cashback percent";
            // 
            // txtCashbackPercent
            // 
            this.txtCashbackPercent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCashbackPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashbackPercent.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCashbackPercent.ForeColor = System.Drawing.Color.White;
            this.txtCashbackPercent.Location = new System.Drawing.Point(33, 370);
            this.txtCashbackPercent.Name = "txtCashbackPercent";
            this.txtCashbackPercent.Size = new System.Drawing.Size(422, 32);
            this.txtCashbackPercent.TabIndex = 10;
            // 
            // lblValidReferences
            // 
            this.lblValidReferences.AutoSize = true;
            this.lblValidReferences.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValidReferences.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblValidReferences.Location = new System.Drawing.Point(30, 416);
            this.lblValidReferences.Name = "lblValidReferences";
            this.lblValidReferences.Size = new System.Drawing.Size(161, 23);
            this.lblValidReferences.TabIndex = 11;
            this.lblValidReferences.Text = "User service accounts";
            // 
            // pnlServiceAccounts
            // 
            this.pnlServiceAccounts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlServiceAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlServiceAccounts.Controls.Add(this.dgvServiceAccounts);
            this.pnlServiceAccounts.Location = new System.Drawing.Point(33, 444);
            this.pnlServiceAccounts.Name = "pnlServiceAccounts";
            this.pnlServiceAccounts.Size = new System.Drawing.Size(422, 126);
            this.pnlServiceAccounts.TabIndex = 12;
            // 
            // dgvServiceAccounts
            // 
            this.dgvServiceAccounts.AllowUserToAddRows = false;
            this.dgvServiceAccounts.AllowUserToDeleteRows = false;
            this.dgvServiceAccounts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.dgvServiceAccounts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServiceAccounts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvServiceAccounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvServiceAccounts.ColumnHeadersHeight = 38;
            this.dgvServiceAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvServiceAccounts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvServiceAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServiceAccounts.EnableHeadersVisualStyles = false;
            this.dgvServiceAccounts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.dgvServiceAccounts.Location = new System.Drawing.Point(0, 0);
            this.dgvServiceAccounts.MultiSelect = false;
            this.dgvServiceAccounts.Name = "dgvServiceAccounts";
            this.dgvServiceAccounts.RowHeadersVisible = false;
            this.dgvServiceAccounts.RowHeadersWidth = 51;
            this.dgvServiceAccounts.RowTemplate.Height = 28;
            this.dgvServiceAccounts.Size = new System.Drawing.Size(420, 124);
            this.dgvServiceAccounts.TabIndex = 0;
            // 
            // btnAddServiceAccount
            // 
            this.btnAddServiceAccount.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnAddServiceAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddServiceAccount.Location = new System.Drawing.Point(33, 580);
            this.btnAddServiceAccount.Name = "btnAddServiceAccount";
            this.btnAddServiceAccount.Size = new System.Drawing.Size(206, 38);
            this.btnAddServiceAccount.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddServiceAccount.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAddServiceAccount.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnAddServiceAccount.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnAddServiceAccount.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnAddServiceAccount.StateCommon.Border.Rounding = 10F;
            this.btnAddServiceAccount.StateCommon.Border.Width = 2;
            this.btnAddServiceAccount.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnAddServiceAccount.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddServiceAccount.TabIndex = 13;
            this.btnAddServiceAccount.Values.Text = "Add User Account";
            this.btnAddServiceAccount.Click += new System.EventHandler(this.btnAddServiceAccount_Click);
            // 
            // btnDeactivateServiceAccount
            // 
            this.btnDeactivateServiceAccount.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnDeactivateServiceAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeactivateServiceAccount.Location = new System.Drawing.Point(249, 580);
            this.btnDeactivateServiceAccount.Name = "btnDeactivateServiceAccount";
            this.btnDeactivateServiceAccount.Size = new System.Drawing.Size(206, 38);
            this.btnDeactivateServiceAccount.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnDeactivateServiceAccount.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnDeactivateServiceAccount.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnDeactivateServiceAccount.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnDeactivateServiceAccount.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnDeactivateServiceAccount.StateCommon.Border.Rounding = 10F;
            this.btnDeactivateServiceAccount.StateCommon.Border.Width = 2;
            this.btnDeactivateServiceAccount.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnDeactivateServiceAccount.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeactivateServiceAccount.TabIndex = 14;
            this.btnDeactivateServiceAccount.Values.Text = "Deactivate Account";
            this.btnDeactivateServiceAccount.Click += new System.EventHandler(this.btnDeactivateServiceAccount_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsActive.ForeColor = System.Drawing.Color.White;
            this.chkIsActive.Location = new System.Drawing.Point(33, 630);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(123, 27);
            this.chkIsActive.TabIndex = 15;
            this.chkIsActive.Text = "Active service";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(174, 664);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 44);
            this.btnSave.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSave.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSave.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSave.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSave.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnSave.StateCommon.Border.Rounding = 10F;
            this.btnSave.StateCommon.Border.Width = 2;
            this.btnSave.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.TabIndex = 16;
            this.btnSave.Values.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(327, 664);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 44);
            this.btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancel.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancel.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnCancel.StateCommon.Border.Rounding = 10F;
            this.btnCancel.StateCommon.Border.Width = 2;
            this.btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Values.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdminServiceEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(500, 730);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnDeactivateServiceAccount);
            this.Controls.Add(this.btnAddServiceAccount);
            this.Controls.Add(this.pnlServiceAccounts);
            this.Controls.Add(this.lblValidReferences);
            this.Controls.Add(this.txtCashbackPercent);
            this.Controls.Add(this.lblCashbackPercent);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.txtAccountHint);
            this.Controls.Add(this.lblAccountHint);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.txtServiceName);
            this.Controls.Add(this.lblServiceName);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminServiceEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Service";
            this.Load += new System.EventHandler(this.AdminServiceEditForm_Load);
            this.pnlServiceAccounts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceAccounts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblAccountHint;
        private System.Windows.Forms.TextBox txtAccountHint;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblCashbackPercent;
        private System.Windows.Forms.TextBox txtCashbackPercent;
        private System.Windows.Forms.Label lblValidReferences;
        private System.Windows.Forms.Panel pnlServiceAccounts;
        private System.Windows.Forms.DataGridView dgvServiceAccounts;
        private Krypton.Toolkit.KryptonButton btnAddServiceAccount;
        private Krypton.Toolkit.KryptonButton btnDeactivateServiceAccount;
        private System.Windows.Forms.CheckBox chkIsActive;
        private Krypton.Toolkit.KryptonButton btnSave;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
