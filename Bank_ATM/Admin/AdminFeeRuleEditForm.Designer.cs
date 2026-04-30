namespace Bank_ATM.Admin
{
    partial class AdminFeeRuleEditForm
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
            this.lblCardType = new System.Windows.Forms.Label();
            this.cmbCardType = new System.Windows.Forms.ComboBox();
            this.lblTransactionType = new System.Windows.Forms.Label();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.lblPercentFee = new System.Windows.Forms.Label();
            this.txtPercentFee = new System.Windows.Forms.TextBox();
            this.lblFixedFee = new System.Windows.Forms.Label();
            this.txtFixedFee = new System.Windows.Forms.TextBox();
            this.lblMinFee = new System.Windows.Forms.Label();
            this.txtMinFee = new System.Windows.Forms.TextBox();
            this.lblMaxFee = new System.Windows.Forms.Label();
            this.txtMaxFee = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(28, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create Fee Rule";
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCardType.Location = new System.Drawing.Point(30, 92);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(82, 23);
            this.lblCardType.TabIndex = 1;
            this.lblCardType.Text = "Card type";
            // 
            // cmbCardType
            // 
            this.cmbCardType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cmbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCardType.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbCardType.ForeColor = System.Drawing.Color.White;
            this.cmbCardType.FormattingEnabled = true;
            this.cmbCardType.Location = new System.Drawing.Point(33, 116);
            this.cmbCardType.Name = "cmbCardType";
            this.cmbCardType.Size = new System.Drawing.Size(422, 33);
            this.cmbCardType.TabIndex = 2;
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.AutoSize = true;
            this.lblTransactionType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransactionType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblTransactionType.Location = new System.Drawing.Point(30, 160);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new System.Drawing.Size(131, 23);
            this.lblTransactionType.TabIndex = 3;
            this.lblTransactionType.Text = "Transaction type";
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cmbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTransactionType.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTransactionType.ForeColor = System.Drawing.Color.White;
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Location = new System.Drawing.Point(33, 184);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(422, 33);
            this.cmbTransactionType.TabIndex = 4;
            // 
            // lblPercentFee
            // 
            this.lblPercentFee.AutoSize = true;
            this.lblPercentFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPercentFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblPercentFee.Location = new System.Drawing.Point(30, 228);
            this.lblPercentFee.Name = "lblPercentFee";
            this.lblPercentFee.Size = new System.Drawing.Size(78, 23);
            this.lblPercentFee.TabIndex = 5;
            this.lblPercentFee.Text = "Percent %";
            // 
            // txtPercentFee
            // 
            this.txtPercentFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtPercentFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPercentFee.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPercentFee.ForeColor = System.Drawing.Color.White;
            this.txtPercentFee.Location = new System.Drawing.Point(33, 252);
            this.txtPercentFee.Name = "txtPercentFee";
            this.txtPercentFee.Size = new System.Drawing.Size(422, 32);
            this.txtPercentFee.TabIndex = 6;
            // 
            // lblFixedFee
            // 
            this.lblFixedFee.AutoSize = true;
            this.lblFixedFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFixedFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblFixedFee.Location = new System.Drawing.Point(30, 296);
            this.lblFixedFee.Name = "lblFixedFee";
            this.lblFixedFee.Size = new System.Drawing.Size(113, 23);
            this.lblFixedFee.TabIndex = 7;
            this.lblFixedFee.Text = "Fixed fee UZS";
            // 
            // txtFixedFee
            // 
            this.txtFixedFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtFixedFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFixedFee.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtFixedFee.ForeColor = System.Drawing.Color.White;
            this.txtFixedFee.Location = new System.Drawing.Point(33, 320);
            this.txtFixedFee.Name = "txtFixedFee";
            this.txtFixedFee.Size = new System.Drawing.Size(422, 32);
            this.txtFixedFee.TabIndex = 8;
            // 
            // lblMinFee
            // 
            this.lblMinFee.AutoSize = true;
            this.lblMinFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMinFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblMinFee.Location = new System.Drawing.Point(30, 364);
            this.lblMinFee.Name = "lblMinFee";
            this.lblMinFee.Size = new System.Drawing.Size(103, 23);
            this.lblMinFee.TabIndex = 9;
            this.lblMinFee.Text = "Min fee UZS";
            // 
            // txtMinFee
            // 
            this.txtMinFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtMinFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinFee.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMinFee.ForeColor = System.Drawing.Color.White;
            this.txtMinFee.Location = new System.Drawing.Point(33, 388);
            this.txtMinFee.Name = "txtMinFee";
            this.txtMinFee.Size = new System.Drawing.Size(422, 32);
            this.txtMinFee.TabIndex = 10;
            // 
            // lblMaxFee
            // 
            this.lblMaxFee.AutoSize = true;
            this.lblMaxFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaxFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblMaxFee.Location = new System.Drawing.Point(30, 432);
            this.lblMaxFee.Name = "lblMaxFee";
            this.lblMaxFee.Size = new System.Drawing.Size(160, 23);
            this.lblMaxFee.TabIndex = 11;
            this.lblMaxFee.Text = "Max fee UZS (blank)";
            // 
            // txtMaxFee
            // 
            this.txtMaxFee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtMaxFee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxFee.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMaxFee.ForeColor = System.Drawing.Color.White;
            this.txtMaxFee.Location = new System.Drawing.Point(33, 456);
            this.txtMaxFee.Name = "txtMaxFee";
            this.txtMaxFee.Size = new System.Drawing.Size(422, 32);
            this.txtMaxFee.TabIndex = 12;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsActive.ForeColor = System.Drawing.Color.White;
            this.chkIsActive.Location = new System.Drawing.Point(33, 506);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(76, 27);
            this.chkIsActive.TabIndex = 13;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(189, 552);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 44);
            this.btnSave.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnSave.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnSave.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnSave.StateCommon.Border.Rounding = 10F;
            this.btnSave.StateCommon.Border.Width = 2;
            this.btnSave.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSave.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.TabIndex = 14;
            this.btnSave.Values.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(327, 552);
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
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdminFeeRuleEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(500, 624);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.txtMaxFee);
            this.Controls.Add(this.lblMaxFee);
            this.Controls.Add(this.txtMinFee);
            this.Controls.Add(this.lblMinFee);
            this.Controls.Add(this.txtFixedFee);
            this.Controls.Add(this.lblFixedFee);
            this.Controls.Add(this.txtPercentFee);
            this.Controls.Add(this.lblPercentFee);
            this.Controls.Add(this.cmbTransactionType);
            this.Controls.Add(this.lblTransactionType);
            this.Controls.Add(this.cmbCardType);
            this.Controls.Add(this.lblCardType);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminFeeRuleEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fee Rule";
            this.Load += new System.EventHandler(this.AdminFeeRuleEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.ComboBox cmbCardType;
        private System.Windows.Forms.Label lblTransactionType;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.Label lblPercentFee;
        private System.Windows.Forms.TextBox txtPercentFee;
        private System.Windows.Forms.Label lblFixedFee;
        private System.Windows.Forms.TextBox txtFixedFee;
        private System.Windows.Forms.Label lblMinFee;
        private System.Windows.Forms.TextBox txtMinFee;
        private System.Windows.Forms.Label lblMaxFee;
        private System.Windows.Forms.TextBox txtMaxFee;
        private System.Windows.Forms.CheckBox chkIsActive;
        private Krypton.Toolkit.KryptonButton btnSave;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
