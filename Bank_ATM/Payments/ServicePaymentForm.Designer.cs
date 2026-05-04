namespace Bank_ATM.Payments
{
    partial class ServicePaymentForm
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
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlSelectedService = new System.Windows.Forms.Panel();
            this.lblServiceIcon = new System.Windows.Forms.Label();
            this.lblServiceName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCategoryValue = new System.Windows.Forms.Label();
            this.lblCashback = new System.Windows.Forms.Label();
            this.lblCashbackValue = new System.Windows.Forms.Label();
            this.lblReference = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblReferenceHint = new System.Windows.Forms.Label();
            this.pnlVerification = new System.Windows.Forms.Panel();
            this.lblReferenceStatusValue = new System.Windows.Forms.Label();
            this.lblReferenceStatusCaption = new System.Windows.Forms.Label();
            this.lblCustomerNameValue = new System.Windows.Forms.Label();
            this.lblCustomerNameCaption = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnPay = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.statusBanner = new Bank_ATM.UI.StatusBanner();
            this.pnlHeader.SuspendLayout();
            this.pnlSelectedService.SuspendLayout();
            this.pnlVerification.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(37)))), ((int)(((byte)(61)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(134, 22);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(832, 96);
            this.pnlHeader.TabIndex = 0;
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(22, 54);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(277, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Confirm details and complete payment";
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Service Payment";
            //
            // pnlSelectedService
            //
            this.pnlSelectedService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlSelectedService.Controls.Add(this.lblServiceIcon);
            this.pnlSelectedService.Controls.Add(this.lblServiceName);
            this.pnlSelectedService.Controls.Add(this.lblCategory);
            this.pnlSelectedService.Controls.Add(this.lblCategoryValue);
            this.pnlSelectedService.Controls.Add(this.lblCashback);
            this.pnlSelectedService.Controls.Add(this.lblCashbackValue);
            this.pnlSelectedService.Location = new System.Drawing.Point(134, 134);
            this.pnlSelectedService.Name = "pnlSelectedService";
            this.pnlSelectedService.Size = new System.Drawing.Size(832, 100);
            this.pnlSelectedService.TabIndex = 1;
            //
            // lblServiceIcon
            //
            this.lblServiceIcon.AutoSize = true;
            this.lblServiceIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceIcon.ForeColor = System.Drawing.Color.White;
            this.lblServiceIcon.Location = new System.Drawing.Point(20, 18);
            this.lblServiceIcon.Name = "lblServiceIcon";
            this.lblServiceIcon.Size = new System.Drawing.Size(64, 64);
            this.lblServiceIcon.TabIndex = 0;
            this.lblServiceIcon.Text = "•";
            //
            // lblServiceName
            //
            this.lblServiceName.AutoSize = true;
            this.lblServiceName.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceName.ForeColor = System.Drawing.Color.White;
            this.lblServiceName.Location = new System.Drawing.Point(110, 16);
            this.lblServiceName.Name = "lblServiceName";
            this.lblServiceName.Size = new System.Drawing.Size(150, 37);
            this.lblServiceName.TabIndex = 1;
            this.lblServiceName.Text = "Service";
            //
            // lblCategory
            //
            this.lblCategory.AutoSize = true;
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCategory.Location = new System.Drawing.Point(112, 58);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(79, 23);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category";
            //
            // lblCategoryValue
            //
            this.lblCategoryValue.AutoSize = true;
            this.lblCategoryValue.ForeColor = System.Drawing.Color.White;
            this.lblCategoryValue.Location = new System.Drawing.Point(195, 58);
            this.lblCategoryValue.Name = "lblCategoryValue";
            this.lblCategoryValue.Size = new System.Drawing.Size(17, 23);
            this.lblCategoryValue.TabIndex = 3;
            this.lblCategoryValue.Text = "-";
            //
            // lblCashback
            //
            this.lblCashback.AutoSize = true;
            this.lblCashback.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCashback.Location = new System.Drawing.Point(560, 58);
            this.lblCashback.Name = "lblCashback";
            this.lblCashback.Size = new System.Drawing.Size(86, 23);
            this.lblCashback.TabIndex = 4;
            this.lblCashback.Text = "Cashback";
            //
            // lblCashbackValue
            //
            this.lblCashbackValue.AutoSize = true;
            this.lblCashbackValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.lblCashbackValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashbackValue.Location = new System.Drawing.Point(652, 58);
            this.lblCashbackValue.Name = "lblCashbackValue";
            this.lblCashbackValue.Size = new System.Drawing.Size(38, 23);
            this.lblCashbackValue.TabIndex = 5;
            this.lblCashbackValue.Text = "0 %";
            //
            // lblReference
            //
            this.lblReference.AutoSize = true;
            this.lblReference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblReference.Location = new System.Drawing.Point(158, 252);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(104, 23);
            this.lblReference.TabIndex = 2;
            this.lblReference.Text = "Account/Ref";
            //
            // txtReference
            //
            this.txtReference.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReference.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReference.ForeColor = System.Drawing.Color.White;
            this.txtReference.Location = new System.Drawing.Point(158, 278);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(778, 32);
            this.txtReference.TabIndex = 3;
            //
            // lblReferenceHint
            //
            this.lblReferenceHint.AutoSize = true;
            this.lblReferenceHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblReferenceHint.Location = new System.Drawing.Point(158, 314);
            this.lblReferenceHint.Name = "lblReferenceHint";
            this.lblReferenceHint.Size = new System.Drawing.Size(0, 23);
            this.lblReferenceHint.TabIndex = 4;
            //
            // pnlVerification
            //
            this.pnlVerification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlVerification.Controls.Add(this.lblReferenceStatusValue);
            this.pnlVerification.Controls.Add(this.lblReferenceStatusCaption);
            this.pnlVerification.Controls.Add(this.lblCustomerNameValue);
            this.pnlVerification.Controls.Add(this.lblCustomerNameCaption);
            this.pnlVerification.Location = new System.Drawing.Point(158, 342);
            this.pnlVerification.Name = "pnlVerification";
            this.pnlVerification.Size = new System.Drawing.Size(778, 68);
            this.pnlVerification.TabIndex = 5;
            //
            // lblReferenceStatusValue
            //
            this.lblReferenceStatusValue.AutoSize = true;
            this.lblReferenceStatusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.lblReferenceStatusValue.Location = new System.Drawing.Point(462, 36);
            this.lblReferenceStatusValue.Name = "lblReferenceStatusValue";
            this.lblReferenceStatusValue.Size = new System.Drawing.Size(142, 23);
            this.lblReferenceStatusValue.TabIndex = 3;
            this.lblReferenceStatusValue.Text = "Waiting for check";
            //
            // lblReferenceStatusCaption
            //
            this.lblReferenceStatusCaption.AutoSize = true;
            this.lblReferenceStatusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblReferenceStatusCaption.Location = new System.Drawing.Point(462, 16);
            this.lblReferenceStatusCaption.Name = "lblReferenceStatusCaption";
            this.lblReferenceStatusCaption.Size = new System.Drawing.Size(135, 23);
            this.lblReferenceStatusCaption.TabIndex = 2;
            this.lblReferenceStatusCaption.Text = "Reference status";
            //
            // lblCustomerNameValue
            //
            this.lblCustomerNameValue.AutoSize = true;
            this.lblCustomerNameValue.ForeColor = System.Drawing.Color.White;
            this.lblCustomerNameValue.Location = new System.Drawing.Point(20, 36);
            this.lblCustomerNameValue.Name = "lblCustomerNameValue";
            this.lblCustomerNameValue.Size = new System.Drawing.Size(17, 23);
            this.lblCustomerNameValue.TabIndex = 1;
            this.lblCustomerNameValue.Text = "-";
            //
            // lblCustomerNameCaption
            //
            this.lblCustomerNameCaption.AutoSize = true;
            this.lblCustomerNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCustomerNameCaption.Location = new System.Drawing.Point(20, 16);
            this.lblCustomerNameCaption.Name = "lblCustomerNameCaption";
            this.lblCustomerNameCaption.Size = new System.Drawing.Size(132, 23);
            this.lblCustomerNameCaption.TabIndex = 0;
            this.lblCustomerNameCaption.Text = "Customer name";
            //
            // lblAmount
            //
            this.lblAmount.AutoSize = true;
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAmount.Location = new System.Drawing.Point(158, 426);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(108, 23);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount UZS";
            //
            // txtAmount
            //
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(158, 452);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(778, 32);
            this.txtAmount.TabIndex = 7;
            //
            // btnPay
            //
            this.btnPay.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.Location = new System.Drawing.Point(556, 626);
            this.btnPay.Name = "btnPay";
            this.btnPay.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPay.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnPay.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnPay.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPay.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnPay.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnPay.Size = new System.Drawing.Size(180, 56);
            this.btnPay.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPay.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnPay.StateCommon.Back.ColorAngle = 30F;
            this.btnPay.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnPay.StateCommon.Border.Rounding = 8F;
            this.btnPay.StateCommon.Border.Width = 2;
            this.btnPay.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnPay.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnPay.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnPay.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnPay.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPay.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnPay.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnPay.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.btnPay.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnPay.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnPay.TabIndex = 8;
            this.btnPay.TabStop = false;
            this.btnPay.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnPay.Values.Text = "PAY";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            //
            // btnCancel
            //
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(756, 626);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCancel.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCancel.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.Size = new System.Drawing.Size(180, 56);
            this.btnCancel.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCancel.StateCommon.Back.ColorAngle = 30F;
            this.btnCancel.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnCancel.StateCommon.Border.Rounding = 8F;
            this.btnCancel.StateCommon.Border.Width = 2;
            this.btnCancel.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnCancel.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCancel.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCancel.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnCancel.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.btnCancel.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnCancel.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnCancel.TabIndex = 9;
            this.btnCancel.TabStop = false;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // statusBanner
            //
            this.statusBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.statusBanner.Location = new System.Drawing.Point(158, 514);
            this.statusBanner.Name = "statusBanner";
            this.statusBanner.Padding = new System.Windows.Forms.Padding(18, 10, 42, 10);
            this.statusBanner.Size = new System.Drawing.Size(778, 96);
            this.statusBanner.TabIndex = 10;
            this.statusBanner.Visible = false;
            //
            // ServicePaymentForm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(23)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1100, 720);
            this.Controls.Add(this.statusBanner);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.pnlVerification);
            this.Controls.Add(this.lblReferenceHint);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.lblReference);
            this.Controls.Add(this.pnlSelectedService);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServicePaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Payment";
            this.Load += new System.EventHandler(this.ServicePaymentForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSelectedService.ResumeLayout(false);
            this.pnlSelectedService.PerformLayout();
            this.pnlVerification.ResumeLayout(false);
            this.pnlVerification.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSelectedService;
        private System.Windows.Forms.Label lblServiceIcon;
        private System.Windows.Forms.Label lblServiceName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCategoryValue;
        private System.Windows.Forms.Label lblCashback;
        private System.Windows.Forms.Label lblCashbackValue;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblReferenceHint;
        private System.Windows.Forms.Panel pnlVerification;
        private System.Windows.Forms.Label lblReferenceStatusValue;
        private System.Windows.Forms.Label lblReferenceStatusCaption;
        private System.Windows.Forms.Label lblCustomerNameValue;
        private System.Windows.Forms.Label lblCustomerNameCaption;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private Krypton.Toolkit.KryptonButton btnPay;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Bank_ATM.UI.StatusBanner statusBanner;
    }
}
