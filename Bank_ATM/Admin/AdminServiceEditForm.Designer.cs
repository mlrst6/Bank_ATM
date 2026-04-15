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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblAccountHint = new System.Windows.Forms.Label();
            this.txtAccountHint = new System.Windows.Forms.TextBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblValidReferences = new System.Windows.Forms.Label();
            this.pnlServiceAccounts = new System.Windows.Forms.Panel();
            this.dgvServiceAccounts = new System.Windows.Forms.DataGridView();
            this.btnAddServiceAccount = new System.Windows.Forms.Button();
            this.btnDeactivateServiceAccount = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlServiceAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServiceAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(94, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create Service";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(30, 58);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(255, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Set up a service users can pay from the ATM";
            // 
            // lblServiceName
            // 
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Location = new System.Drawing.Point(30, 112);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(91, 17);
            this.lblServiceName.TabIndex = 2;
            this.lblServiceName.Text = "Service name";
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(33, 132);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(422, 22);
            this.txtServiceName.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(30, 176);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(65, 17);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category";
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(33, 196);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(422, 24);
            this.cmbCategory.TabIndex = 5;
            // 
            // lblAccountHint
            // 
            this.lblAccountHint.AutoSize = true;
            this.lblAccountHint.Location = new System.Drawing.Point(30, 240);
            this.lblAccountHint.Name = "lblAccountHint";
            this.lblAccountHint.Size = new System.Drawing.Size(111, 17);
            this.lblAccountHint.TabIndex = 6;
            this.lblAccountHint.Text = "Payment reference";
            // 
            // txtAccountHint
            // 
            this.txtAccountHint.Location = new System.Drawing.Point(33, 260);
            this.txtAccountHint.Name = "txtAccountHint";
            this.txtAccountHint.Size = new System.Drawing.Size(422, 22);
            this.txtAccountHint.TabIndex = 7;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(33, 288);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(280, 17);
            this.lblHint.TabIndex = 8;
            this.lblHint.Text = "Example: phone number, subscriber ID, meter.";
            // 
            // lblValidReferences
            // 
            this.lblValidReferences.AutoSize = true;
            this.lblValidReferences.Location = new System.Drawing.Point(30, 322);
            this.lblValidReferences.Name = "lblValidReferences";
            this.lblValidReferences.Size = new System.Drawing.Size(138, 17);
            this.lblValidReferences.TabIndex = 9;
            this.lblValidReferences.Text = "User service accounts";
            // 
            // pnlServiceAccounts
            // 
            this.pnlServiceAccounts.Controls.Add(this.dgvServiceAccounts);
            this.pnlServiceAccounts.Location = new System.Drawing.Point(33, 342);
            this.pnlServiceAccounts.Name = "pnlServiceAccounts";
            this.pnlServiceAccounts.Size = new System.Drawing.Size(422, 126);
            this.pnlServiceAccounts.TabIndex = 10;
            // 
            // dgvServiceAccounts
            // 
            this.dgvServiceAccounts.AllowUserToAddRows = false;
            this.dgvServiceAccounts.AllowUserToDeleteRows = false;
            this.dgvServiceAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServiceAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServiceAccounts.Location = new System.Drawing.Point(0, 0);
            this.dgvServiceAccounts.MultiSelect = false;
            this.dgvServiceAccounts.Name = "dgvServiceAccounts";
            this.dgvServiceAccounts.RowHeadersWidth = 51;
            this.dgvServiceAccounts.RowTemplate.Height = 24;
            this.dgvServiceAccounts.Size = new System.Drawing.Size(422, 126);
            this.dgvServiceAccounts.TabIndex = 0;
            // 
            // btnAddServiceAccount
            // 
            this.btnAddServiceAccount.Location = new System.Drawing.Point(33, 478);
            this.btnAddServiceAccount.Name = "btnAddServiceAccount";
            this.btnAddServiceAccount.Size = new System.Drawing.Size(206, 36);
            this.btnAddServiceAccount.TabIndex = 11;
            this.btnAddServiceAccount.Text = "Add User Account";
            this.btnAddServiceAccount.UseVisualStyleBackColor = true;
            this.btnAddServiceAccount.Click += new System.EventHandler(this.btnAddServiceAccount_Click);
            // 
            // btnDeactivateServiceAccount
            // 
            this.btnDeactivateServiceAccount.Location = new System.Drawing.Point(249, 478);
            this.btnDeactivateServiceAccount.Name = "btnDeactivateServiceAccount";
            this.btnDeactivateServiceAccount.Size = new System.Drawing.Size(206, 36);
            this.btnDeactivateServiceAccount.TabIndex = 12;
            this.btnDeactivateServiceAccount.Text = "Deactivate Account";
            this.btnDeactivateServiceAccount.UseVisualStyleBackColor = true;
            this.btnDeactivateServiceAccount.Click += new System.EventHandler(this.btnDeactivateServiceAccount_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(33, 534);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(100, 21);
            this.chkIsActive.TabIndex = 13;
            this.chkIsActive.Text = "Active service";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(189, 582);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 42);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 582);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 42);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdminServiceEditForm
            // 
            this.ClientSize = new System.Drawing.Size(492, 650);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnDeactivateServiceAccount);
            this.Controls.Add(this.btnAddServiceAccount);
            this.Controls.Add(this.pnlServiceAccounts);
            this.Controls.Add(this.lblValidReferences);
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
        private System.Windows.Forms.Label lblValidReferences;
        private System.Windows.Forms.Panel pnlServiceAccounts;
        private System.Windows.Forms.DataGridView dgvServiceAccounts;
        private System.Windows.Forms.Button btnAddServiceAccount;
        private System.Windows.Forms.Button btnDeactivateServiceAccount;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
