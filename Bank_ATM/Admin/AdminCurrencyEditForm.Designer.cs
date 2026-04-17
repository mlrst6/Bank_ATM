namespace Bank_ATM.Admin
{
    partial class AdminCurrencyEditForm
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
            this.lblCode = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCurrencyName = new System.Windows.Forms.Label();
            this.txtCurrencyName = new System.Windows.Forms.TextBox();
            this.lblRateToUzs = new System.Windows.Forms.Label();
            this.txtRateToUzs = new System.Windows.Forms.TextBox();
            this.lblCashAvailable = new System.Windows.Forms.Label();
            this.txtCashAvailable = new System.Windows.Forms.TextBox();
            this.lblDenominations = new System.Windows.Forms.Label();
            this.txtDenominations = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(103, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create Currency";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(30, 84);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(40, 16);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(33, 104);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(422, 22);
            this.txtCode.TabIndex = 2;
            // 
            // lblCurrencyName
            // 
            this.lblCurrencyName.AutoSize = true;
            this.lblCurrencyName.Location = new System.Drawing.Point(30, 146);
            this.lblCurrencyName.Name = "lblCurrencyName";
            this.lblCurrencyName.Size = new System.Drawing.Size(97, 16);
            this.lblCurrencyName.TabIndex = 3;
            this.lblCurrencyName.Text = "Currency name";
            // 
            // txtCurrencyName
            // 
            this.txtCurrencyName.Location = new System.Drawing.Point(33, 166);
            this.txtCurrencyName.Name = "txtCurrencyName";
            this.txtCurrencyName.Size = new System.Drawing.Size(422, 22);
            this.txtCurrencyName.TabIndex = 4;
            // 
            // lblRateToUzs
            // 
            this.lblRateToUzs.AutoSize = true;
            this.lblRateToUzs.Location = new System.Drawing.Point(30, 208);
            this.lblRateToUzs.Name = "lblRateToUzs";
            this.lblRateToUzs.Size = new System.Drawing.Size(80, 16);
            this.lblRateToUzs.TabIndex = 5;
            this.lblRateToUzs.Text = "Rate to UZS";
            // 
            // txtRateToUzs
            // 
            this.txtRateToUzs.Location = new System.Drawing.Point(33, 228);
            this.txtRateToUzs.Name = "txtRateToUzs";
            this.txtRateToUzs.Size = new System.Drawing.Size(422, 22);
            this.txtRateToUzs.TabIndex = 6;
            // 
            // lblCashAvailable
            // 
            this.lblCashAvailable.AutoSize = true;
            this.lblCashAvailable.Location = new System.Drawing.Point(30, 270);
            this.lblCashAvailable.Name = "lblCashAvailable";
            this.lblCashAvailable.Size = new System.Drawing.Size(127, 16);
            this.lblCashAvailable.TabIndex = 7;
            this.lblCashAvailable.Text = "ATM cash available";
            // 
            // txtCashAvailable
            // 
            this.txtCashAvailable.Location = new System.Drawing.Point(33, 290);
            this.txtCashAvailable.Name = "txtCashAvailable";
            this.txtCashAvailable.Size = new System.Drawing.Size(422, 22);
            this.txtCashAvailable.TabIndex = 8;
            // 
            // lblDenominations
            // 
            this.lblDenominations.AutoSize = true;
            this.lblDenominations.Location = new System.Drawing.Point(30, 332);
            this.lblDenominations.Name = "lblDenominations";
            this.lblDenominations.Size = new System.Drawing.Size(97, 16);
            this.lblDenominations.TabIndex = 9;
            this.lblDenominations.Text = "Denominations";
            // 
            // txtDenominations
            // 
            this.txtDenominations.Location = new System.Drawing.Point(33, 352);
            this.txtDenominations.Multiline = true;
            this.txtDenominations.Name = "txtDenominations";
            this.txtDenominations.Size = new System.Drawing.Size(422, 62);
            this.txtDenominations.TabIndex = 10;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(33, 432);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(66, 20);
            this.chkIsActive.TabIndex = 11;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(189, 490);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 42);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 490);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 42);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdminCurrencyEditForm
            // 
            this.ClientSize = new System.Drawing.Size(882, 603);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.txtDenominations);
            this.Controls.Add(this.lblDenominations);
            this.Controls.Add(this.txtCashAvailable);
            this.Controls.Add(this.lblCashAvailable);
            this.Controls.Add(this.txtRateToUzs);
            this.Controls.Add(this.lblRateToUzs);
            this.Controls.Add(this.txtCurrencyName);
            this.Controls.Add(this.lblCurrencyName);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminCurrencyEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Currency";
            this.Load += new System.EventHandler(this.AdminCurrencyEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCurrencyName;
        private System.Windows.Forms.TextBox txtCurrencyName;
        private System.Windows.Forms.Label lblRateToUzs;
        private System.Windows.Forms.TextBox txtRateToUzs;
        private System.Windows.Forms.Label lblCashAvailable;
        private System.Windows.Forms.TextBox txtCashAvailable;
        private System.Windows.Forms.Label lblDenominations;
        private System.Windows.Forms.TextBox txtDenominations;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
