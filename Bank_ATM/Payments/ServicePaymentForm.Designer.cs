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
            this.lblService = new System.Windows.Forms.Label();
            this.cmbServices = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCategoryValue = new System.Windows.Forms.Label();
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
            this.pnlHeader.SuspendLayout();
            this.pnlVerification.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(34, 22);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(832, 96);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(22, 54);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(331, 16);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Choose a service and payment info";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(22, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(109, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Service Payment";
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.Location = new System.Drawing.Point(58, 150);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(53, 16);
            this.lblService.TabIndex = 1;
            this.lblService.Text = "Service";
            // 
            // cmbServices
            // 
            this.cmbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServices.FormattingEnabled = true;
            this.cmbServices.Location = new System.Drawing.Point(58, 174);
            this.cmbServices.Name = "cmbServices";
            this.cmbServices.Size = new System.Drawing.Size(778, 24);
            this.cmbServices.TabIndex = 2;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(58, 220);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(62, 16);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Category";
            // 
            // lblCategoryValue
            // 
            this.lblCategoryValue.AutoSize = true;
            this.lblCategoryValue.Location = new System.Drawing.Point(129, 220);
            this.lblCategoryValue.Name = "lblCategoryValue";
            this.lblCategoryValue.Size = new System.Drawing.Size(11, 16);
            this.lblCategoryValue.TabIndex = 4;
            this.lblCategoryValue.Text = "-";
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(58, 262);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(80, 16);
            this.lblReference.TabIndex = 5;
            this.lblReference.Text = "Account/Ref";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(58, 286);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(778, 22);
            this.txtReference.TabIndex = 6;
            // 
            // lblReferenceHint
            // 
            this.lblReferenceHint.AutoSize = true;
            this.lblReferenceHint.Location = new System.Drawing.Point(58, 314);
            this.lblReferenceHint.Name = "lblReferenceHint";
            this.lblReferenceHint.Size = new System.Drawing.Size(0, 16);
            this.lblReferenceHint.TabIndex = 7;
            // 
            // pnlVerification
            // 
            this.pnlVerification.Controls.Add(this.lblReferenceStatusValue);
            this.pnlVerification.Controls.Add(this.lblReferenceStatusCaption);
            this.pnlVerification.Controls.Add(this.lblCustomerNameValue);
            this.pnlVerification.Controls.Add(this.lblCustomerNameCaption);
            this.pnlVerification.Location = new System.Drawing.Point(58, 342);
            this.pnlVerification.Name = "pnlVerification";
            this.pnlVerification.Size = new System.Drawing.Size(778, 68);
            this.pnlVerification.TabIndex = 8;
            // 
            // lblReferenceStatusValue
            // 
            this.lblReferenceStatusValue.AutoSize = true;
            this.lblReferenceStatusValue.Location = new System.Drawing.Point(462, 36);
            this.lblReferenceStatusValue.Name = "lblReferenceStatusValue";
            this.lblReferenceStatusValue.Size = new System.Drawing.Size(115, 16);
            this.lblReferenceStatusValue.TabIndex = 3;
            this.lblReferenceStatusValue.Text = "Waiting for check";
            // 
            // lblReferenceStatusCaption
            // 
            this.lblReferenceStatusCaption.AutoSize = true;
            this.lblReferenceStatusCaption.Location = new System.Drawing.Point(462, 16);
            this.lblReferenceStatusCaption.Name = "lblReferenceStatusCaption";
            this.lblReferenceStatusCaption.Size = new System.Drawing.Size(103, 16);
            this.lblReferenceStatusCaption.TabIndex = 2;
            this.lblReferenceStatusCaption.Text = "Reference status";
            // 
            // lblCustomerNameValue
            // 
            this.lblCustomerNameValue.AutoSize = true;
            this.lblCustomerNameValue.Location = new System.Drawing.Point(20, 36);
            this.lblCustomerNameValue.Name = "lblCustomerNameValue";
            this.lblCustomerNameValue.Size = new System.Drawing.Size(11, 16);
            this.lblCustomerNameValue.TabIndex = 1;
            this.lblCustomerNameValue.Text = "-";
            // 
            // lblCustomerNameCaption
            // 
            this.lblCustomerNameCaption.AutoSize = true;
            this.lblCustomerNameCaption.Location = new System.Drawing.Point(20, 16);
            this.lblCustomerNameCaption.Name = "lblCustomerNameCaption";
            this.lblCustomerNameCaption.Size = new System.Drawing.Size(94, 16);
            this.lblCustomerNameCaption.TabIndex = 0;
            this.lblCustomerNameCaption.Text = "Customer name";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(58, 426);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(82, 16);
            this.lblAmount.TabIndex = 9;
            this.lblAmount.Text = "Amount UZS";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(58, 450);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(778, 22);
            this.txtAmount.TabIndex = 10;
            // 
            // btnPay
            // 
            this.btnPay.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPay.Location = new System.Drawing.Point(450, 553);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(180, 46);
            this.btnPay.TabIndex = 11;
            this.btnPay.TabStop = false;
            this.btnPay.Text = "PAY";
            this.btnPay.Values.Text = "PAY";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(656, 553);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 46);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.Values.Text = "CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ServicePaymentForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.pnlVerification);
            this.Controls.Add(this.lblReferenceHint);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.lblReference);
            this.Controls.Add(this.lblCategoryValue);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbServices);
            this.Controls.Add(this.lblService);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ServicePaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Service Payment";
            this.Load += new System.EventHandler(this.ServicePaymentForm_Load);
            StylePrimaryButton(this.btnPay);
            StyleBackButton(this.btnCancel);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlVerification.ResumeLayout(false);
            this.pnlVerification.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.ComboBox cmbServices;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCategoryValue;
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

        private static void StylePrimaryButton(Krypton.Toolkit.KryptonButton button)
        {
            button.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(22, 163, 74);
            button.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(21, 128, 61);
            button.StateCommon.Back.ColorAngle = 30F;
            button.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(134, 239, 172);
            button.StateCommon.Border.Rounding = 8F;
            button.StateCommon.Border.Width = 2;
            button.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            button.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            button.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(22, 163, 74);
            button.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(21, 128, 61);
            button.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(34, 197, 94);
            button.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(22, 163, 74);
            button.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(21, 128, 61);
            button.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(20, 83, 45);
            button.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(203, 213, 225);
            button.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(22, 163, 74);
            button.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(21, 128, 61);
            button.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(134, 239, 172);
            button.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(22, 163, 74);
            button.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(21, 128, 61);
            button.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(134, 239, 172);
        }

        private static void StyleBackButton(Krypton.Toolkit.KryptonButton button)
        {
            button.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.StateCommon.Back.ColorAngle = 30F;
            button.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(248, 113, 113);
            button.StateCommon.Border.Rounding = 8F;
            button.StateCommon.Border.Width = 2;
            button.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(254, 202, 202);
            button.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            button.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(107, 114, 128);
            button.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(31, 41, 55);
            button.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(203, 213, 225);
            button.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(248, 113, 113);
            button.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(55, 65, 81);
            button.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(248, 113, 113);
        }
    }
}
