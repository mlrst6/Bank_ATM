using System.Drawing;

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
            this.lblBuyRateToUzs = new System.Windows.Forms.Label();
            this.txtBuyRateToUzs = new System.Windows.Forms.TextBox();
            this.lblSellRateToUzs = new System.Windows.Forms.Label();
            this.txtSellRateToUzs = new System.Windows.Forms.TextBox();
            this.lblCashAvailable = new System.Windows.Forms.Label();
            this.txtCashAvailable = new System.Windows.Forms.TextBox();
            this.lblDenominations = new System.Windows.Forms.Label();
            this.txtDenominations = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(28, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(245, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create Currency";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCode.Location = new System.Drawing.Point(30, 92);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(49, 23);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "Code";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCode.ForeColor = System.Drawing.Color.White;
            this.txtCode.Location = new System.Drawing.Point(33, 116);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(422, 32);
            this.txtCode.TabIndex = 2;
            // 
            // lblCurrencyName
            // 
            this.lblCurrencyName.AutoSize = true;
            this.lblCurrencyName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrencyName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCurrencyName.Location = new System.Drawing.Point(30, 158);
            this.lblCurrencyName.Name = "lblCurrencyName";
            this.lblCurrencyName.Size = new System.Drawing.Size(113, 23);
            this.lblCurrencyName.TabIndex = 3;
            this.lblCurrencyName.Text = "Currency name";
            // 
            // txtCurrencyName
            // 
            this.txtCurrencyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCurrencyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrencyName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCurrencyName.ForeColor = System.Drawing.Color.White;
            this.txtCurrencyName.Location = new System.Drawing.Point(33, 182);
            this.txtCurrencyName.Name = "txtCurrencyName";
            this.txtCurrencyName.Size = new System.Drawing.Size(422, 32);
            this.txtCurrencyName.TabIndex = 4;
            // 
            // lblRateToUzs
            // 
            this.lblRateToUzs.AutoSize = true;
            this.lblRateToUzs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRateToUzs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblRateToUzs.Location = new System.Drawing.Point(30, 224);
            this.lblRateToUzs.Name = "lblRateToUzs";
            this.lblRateToUzs.Size = new System.Drawing.Size(93, 23);
            this.lblRateToUzs.TabIndex = 5;
            this.lblRateToUzs.Text = "Middle rate to UZS";
            // 
            // txtRateToUzs
            // 
            this.txtRateToUzs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtRateToUzs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRateToUzs.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtRateToUzs.ForeColor = System.Drawing.Color.White;
            this.txtRateToUzs.Location = new System.Drawing.Point(33, 248);
            this.txtRateToUzs.Name = "txtRateToUzs";
            this.txtRateToUzs.Size = new System.Drawing.Size(422, 32);
            this.txtRateToUzs.TabIndex = 6;
            // 
            // lblBuyRateToUzs
            // 
            this.lblBuyRateToUzs.AutoSize = true;
            this.lblBuyRateToUzs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBuyRateToUzs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblBuyRateToUzs.Location = new System.Drawing.Point(30, 290);
            this.lblBuyRateToUzs.Name = "lblBuyRateToUzs";
            this.lblBuyRateToUzs.Size = new System.Drawing.Size(135, 23);
            this.lblBuyRateToUzs.TabIndex = 14;
            this.lblBuyRateToUzs.Text = "Buy rate to UZS";
            // 
            // txtBuyRateToUzs
            // 
            this.txtBuyRateToUzs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtBuyRateToUzs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuyRateToUzs.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBuyRateToUzs.ForeColor = System.Drawing.Color.White;
            this.txtBuyRateToUzs.Location = new System.Drawing.Point(33, 314);
            this.txtBuyRateToUzs.Name = "txtBuyRateToUzs";
            this.txtBuyRateToUzs.Size = new System.Drawing.Size(422, 32);
            this.txtBuyRateToUzs.TabIndex = 15;
            // 
            // lblSellRateToUzs
            // 
            this.lblSellRateToUzs.AutoSize = true;
            this.lblSellRateToUzs.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSellRateToUzs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblSellRateToUzs.Location = new System.Drawing.Point(30, 356);
            this.lblSellRateToUzs.Name = "lblSellRateToUzs";
            this.lblSellRateToUzs.Size = new System.Drawing.Size(128, 23);
            this.lblSellRateToUzs.TabIndex = 16;
            this.lblSellRateToUzs.Text = "Sell rate to UZS";
            // 
            // txtSellRateToUzs
            // 
            this.txtSellRateToUzs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtSellRateToUzs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSellRateToUzs.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSellRateToUzs.ForeColor = System.Drawing.Color.White;
            this.txtSellRateToUzs.Location = new System.Drawing.Point(33, 380);
            this.txtSellRateToUzs.Name = "txtSellRateToUzs";
            this.txtSellRateToUzs.Size = new System.Drawing.Size(422, 32);
            this.txtSellRateToUzs.TabIndex = 17;
            // 
            // lblCashAvailable
            // 
            this.lblCashAvailable.AutoSize = true;
            this.lblCashAvailable.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCashAvailable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCashAvailable.Location = new System.Drawing.Point(30, 422);
            this.lblCashAvailable.Name = "lblCashAvailable";
            this.lblCashAvailable.Size = new System.Drawing.Size(144, 23);
            this.lblCashAvailable.TabIndex = 7;
            this.lblCashAvailable.Text = "ATM cash available";
            // 
            // txtCashAvailable
            // 
            this.txtCashAvailable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCashAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCashAvailable.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCashAvailable.ForeColor = System.Drawing.Color.White;
            this.txtCashAvailable.Location = new System.Drawing.Point(33, 446);
            this.txtCashAvailable.Name = "txtCashAvailable";
            this.txtCashAvailable.Size = new System.Drawing.Size(422, 32);
            this.txtCashAvailable.TabIndex = 8;
            // 
            // lblDenominations
            // 
            this.lblDenominations.AutoSize = true;
            this.lblDenominations.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDenominations.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblDenominations.Location = new System.Drawing.Point(30, 488);
            this.lblDenominations.Name = "lblDenominations";
            this.lblDenominations.Size = new System.Drawing.Size(113, 23);
            this.lblDenominations.TabIndex = 9;
            this.lblDenominations.Text = "Denominations";
            // 
            // txtDenominations
            // 
            this.txtDenominations.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtDenominations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDenominations.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDenominations.ForeColor = System.Drawing.Color.White;
            this.txtDenominations.Location = new System.Drawing.Point(33, 512);
            this.txtDenominations.Multiline = true;
            this.txtDenominations.Name = "txtDenominations";
            this.txtDenominations.Size = new System.Drawing.Size(422, 72);
            this.txtDenominations.TabIndex = 10;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkIsActive.ForeColor = System.Drawing.Color.White;
            this.chkIsActive.Location = new System.Drawing.Point(33, 602);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(76, 27);
            this.chkIsActive.TabIndex = 11;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(189, 650);
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
            this.btnSave.TabIndex = 12;
            this.btnSave.Values.Text = "SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(327, 650);
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
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Values.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AdminCurrencyEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(500, 722);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.txtDenominations);
            this.Controls.Add(this.lblDenominations);
            this.Controls.Add(this.txtCashAvailable);
            this.Controls.Add(this.lblCashAvailable);
            this.Controls.Add(this.txtSellRateToUzs);
            this.Controls.Add(this.lblSellRateToUzs);
            this.Controls.Add(this.txtBuyRateToUzs);
            this.Controls.Add(this.lblBuyRateToUzs);
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
        private System.Windows.Forms.Label lblBuyRateToUzs;
        private System.Windows.Forms.TextBox txtBuyRateToUzs;
        private System.Windows.Forms.Label lblSellRateToUzs;
        private System.Windows.Forms.TextBox txtSellRateToUzs;
        private System.Windows.Forms.Label lblCashAvailable;
        private System.Windows.Forms.TextBox txtCashAvailable;
        private System.Windows.Forms.Label lblDenominations;
        private System.Windows.Forms.TextBox txtDenominations;
        private System.Windows.Forms.CheckBox chkIsActive;
        private Krypton.Toolkit.KryptonButton btnSave;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
