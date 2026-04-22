using System.Drawing;

namespace Bank_ATM.Admin
{
    partial class AdminTransactionDetailsForm
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
            this.panelDetails = new System.Windows.Forms.Panel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtPaymentReference = new System.Windows.Forms.TextBox();
            this.lblPaymentReference = new System.Windows.Forms.Label();
            this.txtServiceAccountId = new System.Windows.Forms.TextBox();
            this.lblServiceAccountId = new System.Windows.Forms.Label();
            this.txtServiceId = new System.Windows.Forms.TextBox();
            this.lblServiceId = new System.Windows.Forms.Label();
            this.txtTargetCardId = new System.Windows.Forms.TextBox();
            this.lblTargetCardId = new System.Windows.Forms.Label();
            this.txtCardId = new System.Windows.Forms.TextBox();
            this.lblCardId = new System.Windows.Forms.Label();
            this.txtTargetAccountId = new System.Windows.Forms.TextBox();
            this.lblTargetAccountId = new System.Windows.Forms.Label();
            this.txtAccountId = new System.Windows.Forms.TextBox();
            this.lblAccountId = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtTransactionDate = new System.Windows.Forms.TextBox();
            this.lblTransactionDate = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.lblTransactionId = new System.Windows.Forms.Label();
            this.btnClose = new Krypton.Toolkit.KryptonButton();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(28, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(520, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Transaction Details";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(31, 68);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(420, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Read-only audit information for the selected transaction";
            // 
            // panelDetails
            // 
            this.panelDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.panelDetails.Controls.Add(this.txtDescription);
            this.panelDetails.Controls.Add(this.lblDescription);
            this.panelDetails.Controls.Add(this.txtPaymentReference);
            this.panelDetails.Controls.Add(this.lblPaymentReference);
            this.panelDetails.Controls.Add(this.txtServiceAccountId);
            this.panelDetails.Controls.Add(this.lblServiceAccountId);
            this.panelDetails.Controls.Add(this.txtServiceId);
            this.panelDetails.Controls.Add(this.lblServiceId);
            this.panelDetails.Controls.Add(this.txtTargetCardId);
            this.panelDetails.Controls.Add(this.lblTargetCardId);
            this.panelDetails.Controls.Add(this.txtCardId);
            this.panelDetails.Controls.Add(this.lblCardId);
            this.panelDetails.Controls.Add(this.txtTargetAccountId);
            this.panelDetails.Controls.Add(this.lblTargetAccountId);
            this.panelDetails.Controls.Add(this.txtAccountId);
            this.panelDetails.Controls.Add(this.lblAccountId);
            this.panelDetails.Controls.Add(this.txtAmount);
            this.panelDetails.Controls.Add(this.lblAmount);
            this.panelDetails.Controls.Add(this.txtType);
            this.panelDetails.Controls.Add(this.lblType);
            this.panelDetails.Controls.Add(this.txtTransactionDate);
            this.panelDetails.Controls.Add(this.lblTransactionDate);
            this.panelDetails.Controls.Add(this.txtTransactionId);
            this.panelDetails.Controls.Add(this.lblTransactionId);
            this.panelDetails.Location = new System.Drawing.Point(28, 110);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(744, 458);
            this.panelDetails.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.ForeColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(380, 327);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(330, 100);
            this.txtDescription.TabIndex = 23;
            this.txtDescription.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblDescription.Location = new System.Drawing.Point(376, 303);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(89, 21);
            this.lblDescription.TabIndex = 22;
            this.lblDescription.Text = "Description";
            // 
            // txtPaymentReference
            // 
            this.txtPaymentReference.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtPaymentReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPaymentReference.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPaymentReference.ForeColor = System.Drawing.Color.White;
            this.txtPaymentReference.Location = new System.Drawing.Point(380, 256);
            this.txtPaymentReference.Name = "txtPaymentReference";
            this.txtPaymentReference.ReadOnly = true;
            this.txtPaymentReference.Size = new System.Drawing.Size(330, 30);
            this.txtPaymentReference.TabIndex = 21;
            this.txtPaymentReference.TabStop = false;
            // 
            // lblPaymentReference
            // 
            this.lblPaymentReference.AutoSize = true;
            this.lblPaymentReference.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPaymentReference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblPaymentReference.Location = new System.Drawing.Point(376, 232);
            this.lblPaymentReference.Name = "lblPaymentReference";
            this.lblPaymentReference.Size = new System.Drawing.Size(144, 21);
            this.lblPaymentReference.TabIndex = 20;
            this.lblPaymentReference.Text = "Payment Reference";
            // 
            // txtServiceAccountId
            // 
            this.txtServiceAccountId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtServiceAccountId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceAccountId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServiceAccountId.ForeColor = System.Drawing.Color.White;
            this.txtServiceAccountId.Location = new System.Drawing.Point(380, 185);
            this.txtServiceAccountId.Name = "txtServiceAccountId";
            this.txtServiceAccountId.ReadOnly = true;
            this.txtServiceAccountId.Size = new System.Drawing.Size(330, 30);
            this.txtServiceAccountId.TabIndex = 19;
            this.txtServiceAccountId.TabStop = false;
            // 
            // lblServiceAccountId
            // 
            this.lblServiceAccountId.AutoSize = true;
            this.lblServiceAccountId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblServiceAccountId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblServiceAccountId.Location = new System.Drawing.Point(376, 161);
            this.lblServiceAccountId.Name = "lblServiceAccountId";
            this.lblServiceAccountId.Size = new System.Drawing.Size(135, 21);
            this.lblServiceAccountId.TabIndex = 18;
            this.lblServiceAccountId.Text = "Service Account ID";
            // 
            // txtServiceId
            // 
            this.txtServiceId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtServiceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServiceId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServiceId.ForeColor = System.Drawing.Color.White;
            this.txtServiceId.Location = new System.Drawing.Point(380, 114);
            this.txtServiceId.Name = "txtServiceId";
            this.txtServiceId.ReadOnly = true;
            this.txtServiceId.Size = new System.Drawing.Size(330, 30);
            this.txtServiceId.TabIndex = 17;
            this.txtServiceId.TabStop = false;
            // 
            // lblServiceId
            // 
            this.lblServiceId.AutoSize = true;
            this.lblServiceId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblServiceId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblServiceId.Location = new System.Drawing.Point(376, 90);
            this.lblServiceId.Name = "lblServiceId";
            this.lblServiceId.Size = new System.Drawing.Size(81, 21);
            this.lblServiceId.TabIndex = 16;
            this.lblServiceId.Text = "Service ID";
            // 
            // txtTargetCardId
            // 
            this.txtTargetCardId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTargetCardId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTargetCardId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTargetCardId.ForeColor = System.Drawing.Color.White;
            this.txtTargetCardId.Location = new System.Drawing.Point(380, 43);
            this.txtTargetCardId.Name = "txtTargetCardId";
            this.txtTargetCardId.ReadOnly = true;
            this.txtTargetCardId.Size = new System.Drawing.Size(330, 30);
            this.txtTargetCardId.TabIndex = 15;
            this.txtTargetCardId.TabStop = false;
            // 
            // lblTargetCardId
            // 
            this.lblTargetCardId.AutoSize = true;
            this.lblTargetCardId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTargetCardId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblTargetCardId.Location = new System.Drawing.Point(376, 19);
            this.lblTargetCardId.Name = "lblTargetCardId";
            this.lblTargetCardId.Size = new System.Drawing.Size(109, 21);
            this.lblTargetCardId.TabIndex = 14;
            this.lblTargetCardId.Text = "Target Card ID";
            // 
            // txtCardId
            // 
            this.txtCardId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCardId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCardId.ForeColor = System.Drawing.Color.White;
            this.txtCardId.Location = new System.Drawing.Point(24, 398);
            this.txtCardId.Name = "txtCardId";
            this.txtCardId.ReadOnly = true;
            this.txtCardId.Size = new System.Drawing.Size(320, 30);
            this.txtCardId.TabIndex = 13;
            this.txtCardId.TabStop = false;
            // 
            // lblCardId
            // 
            this.lblCardId.AutoSize = true;
            this.lblCardId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblCardId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCardId.Location = new System.Drawing.Point(20, 374);
            this.lblCardId.Name = "lblCardId";
            this.lblCardId.Size = new System.Drawing.Size(59, 21);
            this.lblCardId.TabIndex = 12;
            this.lblCardId.Text = "Card ID";
            // 
            // txtTargetAccountId
            // 
            this.txtTargetAccountId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTargetAccountId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTargetAccountId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTargetAccountId.ForeColor = System.Drawing.Color.White;
            this.txtTargetAccountId.Location = new System.Drawing.Point(24, 327);
            this.txtTargetAccountId.Name = "txtTargetAccountId";
            this.txtTargetAccountId.ReadOnly = true;
            this.txtTargetAccountId.Size = new System.Drawing.Size(320, 30);
            this.txtTargetAccountId.TabIndex = 11;
            this.txtTargetAccountId.TabStop = false;
            // 
            // lblTargetAccountId
            // 
            this.lblTargetAccountId.AutoSize = true;
            this.lblTargetAccountId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTargetAccountId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblTargetAccountId.Location = new System.Drawing.Point(20, 303);
            this.lblTargetAccountId.Name = "lblTargetAccountId";
            this.lblTargetAccountId.Size = new System.Drawing.Size(129, 21);
            this.lblTargetAccountId.TabIndex = 10;
            this.lblTargetAccountId.Text = "Target Account ID";
            // 
            // txtAccountId
            // 
            this.txtAccountId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAccountId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAccountId.ForeColor = System.Drawing.Color.White;
            this.txtAccountId.Location = new System.Drawing.Point(24, 256);
            this.txtAccountId.Name = "txtAccountId";
            this.txtAccountId.ReadOnly = true;
            this.txtAccountId.Size = new System.Drawing.Size(320, 30);
            this.txtAccountId.TabIndex = 9;
            this.txtAccountId.TabStop = false;
            // 
            // lblAccountId
            // 
            this.lblAccountId.AutoSize = true;
            this.lblAccountId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAccountId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblAccountId.Location = new System.Drawing.Point(20, 232);
            this.lblAccountId.Name = "lblAccountId";
            this.lblAccountId.Size = new System.Drawing.Size(82, 21);
            this.lblAccountId.TabIndex = 8;
            this.lblAccountId.Text = "Account ID";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAmount.ForeColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(24, 185);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(320, 30);
            this.txtAmount.TabIndex = 7;
            this.txtAmount.TabStop = false;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblAmount.Location = new System.Drawing.Point(20, 161);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(66, 21);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount";
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtType.ForeColor = System.Drawing.Color.White;
            this.txtType.Location = new System.Drawing.Point(24, 114);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(320, 30);
            this.txtType.TabIndex = 5;
            this.txtType.TabStop = false;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblType.Location = new System.Drawing.Point(20, 90);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(41, 21);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type";
            // 
            // txtTransactionDate
            // 
            this.txtTransactionDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTransactionDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTransactionDate.ForeColor = System.Drawing.Color.White;
            this.txtTransactionDate.Location = new System.Drawing.Point(380, 398);
            this.txtTransactionDate.Name = "txtTransactionDate";
            this.txtTransactionDate.ReadOnly = true;
            this.txtTransactionDate.Size = new System.Drawing.Size(330, 30);
            this.txtTransactionDate.TabIndex = 3;
            this.txtTransactionDate.TabStop = false;
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.AutoSize = true;
            this.lblTransactionDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTransactionDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblTransactionDate.Location = new System.Drawing.Point(376, 374);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new System.Drawing.Size(126, 21);
            this.lblTransactionDate.TabIndex = 2;
            this.lblTransactionDate.Text = "Transaction Date";
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtTransactionId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransactionId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTransactionId.ForeColor = System.Drawing.Color.White;
            this.txtTransactionId.Location = new System.Drawing.Point(24, 43);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.ReadOnly = true;
            this.txtTransactionId.Size = new System.Drawing.Size(320, 30);
            this.txtTransactionId.TabIndex = 1;
            this.txtTransactionId.TabStop = false;
            // 
            // lblTransactionId
            // 
            this.lblTransactionId.AutoSize = true;
            this.lblTransactionId.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTransactionId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblTransactionId.Location = new System.Drawing.Point(20, 19);
            this.lblTransactionId.Name = "lblTransactionId";
            this.lblTransactionId.Size = new System.Drawing.Size(107, 21);
            this.lblTransactionId.TabIndex = 0;
            this.lblTransactionId.Text = "Transaction ID";
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(612, 586);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(160, 48);
            this.btnClose.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClose.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClose.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnClose.StateCommon.Border.Rounding = 10F;
            this.btnClose.StateCommon.Border.Width = 2;
            this.btnClose.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnClose.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.TabIndex = 3;
            this.btnClose.Values.Text = "BACK";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AdminTransactionDetailsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminTransactionDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AdminTransactionDetailsForm_Load);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblTransactionId;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Label lblTransactionDate;
        private System.Windows.Forms.TextBox txtTransactionDate;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAccountId;
        private System.Windows.Forms.TextBox txtAccountId;
        private System.Windows.Forms.Label lblTargetAccountId;
        private System.Windows.Forms.TextBox txtTargetAccountId;
        private System.Windows.Forms.Label lblCardId;
        private System.Windows.Forms.TextBox txtCardId;
        private System.Windows.Forms.Label lblTargetCardId;
        private System.Windows.Forms.TextBox txtTargetCardId;
        private System.Windows.Forms.Label lblServiceId;
        private System.Windows.Forms.TextBox txtServiceId;
        private System.Windows.Forms.Label lblServiceAccountId;
        private System.Windows.Forms.TextBox txtServiceAccountId;
        private System.Windows.Forms.Label lblPaymentReference;
        private System.Windows.Forms.TextBox txtPaymentReference;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private Krypton.Toolkit.KryptonButton btnClose;
    }
}
