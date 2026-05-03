namespace Bank_ATM
{
    partial class GuestCardTopUpForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.pnlCardInfo = new System.Windows.Forms.Panel();
            this.lblCardHolderCaption = new System.Windows.Forms.Label();
            this.lblCardHolderValue = new System.Windows.Forms.Label();
            this.lblCardStatusCaption = new System.Windows.Forms.Label();
            this.lblCardStatusValue = new System.Windows.Forms.Label();
            this.statusBanner = new Bank_ATM.UI.StatusBanner();
            this.btnDeposit = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.pnlHeader.SuspendLayout();
            this.pnlCardInfo.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 80);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Card Top-Up";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSubtitle
            //
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(184)))), ((int)(((byte)(204)))));
            this.lblSubtitle.Location = new System.Drawing.Point(60, 96);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(780, 38);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Enter a card number and insert cash to deposit funds to any card.";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblCardNumber
            //
            this.lblCardNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardNumber.Location = new System.Drawing.Point(60, 150);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(400, 22);
            this.lblCardNumber.TabIndex = 2;
            this.lblCardNumber.Text = "Card Number";
            //
            // txtCardNumber
            //
            this.txtCardNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.txtCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCardNumber.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCardNumber.ForeColor = System.Drawing.Color.White;
            this.txtCardNumber.Location = new System.Drawing.Point(60, 174);
            this.txtCardNumber.MaxLength = 19;
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(500, 36);
            this.txtCardNumber.TabIndex = 3;
            this.txtCardNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            //
            // pnlCardInfo
            //
            this.pnlCardInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlCardInfo.Controls.Add(this.lblCardHolderCaption);
            this.pnlCardInfo.Controls.Add(this.lblCardHolderValue);
            this.pnlCardInfo.Controls.Add(this.lblCardStatusCaption);
            this.pnlCardInfo.Controls.Add(this.lblCardStatusValue);
            this.pnlCardInfo.Location = new System.Drawing.Point(60, 228);
            this.pnlCardInfo.Name = "pnlCardInfo";
            this.pnlCardInfo.Size = new System.Drawing.Size(780, 80);
            this.pnlCardInfo.TabIndex = 4;
            //
            // lblCardHolderCaption
            //
            this.lblCardHolderCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardHolderCaption.Location = new System.Drawing.Point(20, 12);
            this.lblCardHolderCaption.Name = "lblCardHolderCaption";
            this.lblCardHolderCaption.Size = new System.Drawing.Size(200, 20);
            this.lblCardHolderCaption.TabIndex = 0;
            this.lblCardHolderCaption.Text = "Card Number";
            //
            // lblCardHolderValue
            //
            this.lblCardHolderValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardHolderValue.ForeColor = System.Drawing.Color.White;
            this.lblCardHolderValue.Location = new System.Drawing.Point(18, 38);
            this.lblCardHolderValue.Name = "lblCardHolderValue";
            this.lblCardHolderValue.Size = new System.Drawing.Size(340, 28);
            this.lblCardHolderValue.TabIndex = 1;
            this.lblCardHolderValue.Text = "-";
            //
            // lblCardStatusCaption
            //
            this.lblCardStatusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardStatusCaption.Location = new System.Drawing.Point(420, 12);
            this.lblCardStatusCaption.Name = "lblCardStatusCaption";
            this.lblCardStatusCaption.Size = new System.Drawing.Size(200, 20);
            this.lblCardStatusCaption.TabIndex = 2;
            this.lblCardStatusCaption.Text = "Status";
            //
            // lblCardStatusValue
            //
            this.lblCardStatusValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardStatusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblCardStatusValue.Location = new System.Drawing.Point(418, 38);
            this.lblCardStatusValue.Name = "lblCardStatusValue";
            this.lblCardStatusValue.Size = new System.Drawing.Size(340, 28);
            this.lblCardStatusValue.TabIndex = 3;
            this.lblCardStatusValue.Text = "Waiting for verification";
            //
            // statusBanner
            //
            this.statusBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.statusBanner.Location = new System.Drawing.Point(60, 326);
            this.statusBanner.Name = "statusBanner";
            this.statusBanner.Padding = new System.Windows.Forms.Padding(18, 10, 42, 10);
            this.statusBanner.Size = new System.Drawing.Size(778, 100);
            this.statusBanner.TabIndex = 5;
            this.statusBanner.Visible = false;
            //
            // btnDeposit
            //
            this.btnDeposit.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnDeposit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeposit.Location = new System.Drawing.Point(60, 462);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDeposit.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDeposit.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnDeposit.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDeposit.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDeposit.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnDeposit.Size = new System.Drawing.Size(190, 46);
            this.btnDeposit.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDeposit.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnDeposit.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnDeposit.StateCommon.Border.Rounding = 8F;
            this.btnDeposit.StateCommon.Border.Width = 2;
            this.btnDeposit.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnDeposit.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeposit.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnDeposit.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnDeposit.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnDeposit.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(55)))));
            this.btnDeposit.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(55)))));
            this.btnDeposit.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(197)))), ((int)(((byte)(98)))));
            this.btnDeposit.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(197)))), ((int)(((byte)(98)))));
            this.btnDeposit.TabIndex = 6;
            this.btnDeposit.TabStop = false;
            this.btnDeposit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDeposit.Values.Text = "Verify Card";
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            //
            // btnCancel
            //
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(706, 462);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.Size = new System.Drawing.Size(134, 46);
            this.btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.StateCommon.Border.Rounding = 8F;
            this.btnCancel.StateCommon.Border.Width = 2;
            this.btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(73)))));
            this.btnCancel.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(62)))), ((int)(((byte)(73)))));
            this.btnCancel.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(119)))), ((int)(((byte)(131)))));
            this.btnCancel.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(119)))), ((int)(((byte)(131)))));
            this.btnCancel.TabIndex = 7;
            this.btnCancel.TabStop = false;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // GuestCardTopUpForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(900, 532);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDeposit);
            this.Controls.Add(this.statusBanner);
            this.Controls.Add(this.pnlCardInfo);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.lblCardNumber);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GuestCardTopUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Card Top-Up";
            this.Load += new System.EventHandler(this.GuestCardTopUpForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlCardInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Panel pnlCardInfo;
        private System.Windows.Forms.Label lblCardHolderCaption;
        private System.Windows.Forms.Label lblCardHolderValue;
        private System.Windows.Forms.Label lblCardStatusCaption;
        private System.Windows.Forms.Label lblCardStatusValue;
        private Bank_ATM.UI.StatusBanner statusBanner;
        private Krypton.Toolkit.KryptonButton btnDeposit;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
