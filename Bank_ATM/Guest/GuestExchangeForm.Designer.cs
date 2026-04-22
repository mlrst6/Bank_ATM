namespace Bank_ATM
{
    partial class GuestExchangeForm
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
            this.pnlFromCard = new System.Windows.Forms.Panel();
            this.cmbFromCurrency = new System.Windows.Forms.ComboBox();
            this.lblFromCaption = new System.Windows.Forms.Label();
            this.pnlToCard = new System.Windows.Forms.Panel();
            this.cmbToCurrency = new System.Windows.Forms.ComboBox();
            this.lblToCaption = new System.Windows.Forms.Label();
            this.pnlAmountCard = new System.Windows.Forms.Panel();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmountCaption = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblBreakdownValue = new System.Windows.Forms.Label();
            this.lblBreakdownCaption = new System.Windows.Forms.Label();
            this.lblReceivedValue = new System.Windows.Forms.Label();
            this.lblReceivedCaption = new System.Windows.Forms.Label();
            this.lblInsertedValue = new System.Windows.Forms.Label();
            this.lblInsertedCaption = new System.Windows.Forms.Label();
            this.lblRateValue = new System.Windows.Forms.Label();
            this.lblRateCaption = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.btnSelectCash = new Krypton.Toolkit.KryptonButton();
            this.btnPreview = new Krypton.Toolkit.KryptonButton();
            this.btnConfirm = new Krypton.Toolkit.KryptonButton();
            this.btnBack = new Krypton.Toolkit.KryptonButton();
            this.pnlHeader.SuspendLayout();
            this.pnlFromCard.SuspendLayout();
            this.pnlToCard.SuspendLayout();
            this.pnlAmountCard.SuspendLayout();
            this.pnlSummary.SuspendLayout();
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
            this.lblTitle.Text = "Currency Exchange";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(184)))), ((int)(((byte)(204)))));
            this.lblSubtitle.Location = new System.Drawing.Point(74, 100);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(752, 38);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Insert guest cash, preview the conversion, and confirm only after checking the di" +
    "spense breakdown.";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFromCard
            // 
            this.pnlFromCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pnlFromCard.Controls.Add(this.cmbFromCurrency);
            this.pnlFromCard.Controls.Add(this.lblFromCaption);
            this.pnlFromCard.Location = new System.Drawing.Point(74, 160);
            this.pnlFromCard.Name = "pnlFromCard";
            this.pnlFromCard.Size = new System.Drawing.Size(350, 92);
            this.pnlFromCard.TabIndex = 2;
            // 
            // cmbFromCurrency
            // 
            this.cmbFromCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.cmbFromCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFromCurrency.ForeColor = System.Drawing.Color.White;
            this.cmbFromCurrency.FormattingEnabled = true;
            this.cmbFromCurrency.Location = new System.Drawing.Point(22, 47);
            this.cmbFromCurrency.Name = "cmbFromCurrency";
            this.cmbFromCurrency.Size = new System.Drawing.Size(304, 24);
            this.cmbFromCurrency.TabIndex = 1;
            this.cmbFromCurrency.SelectedIndexChanged += new System.EventHandler(this.CurrencySelectionChanged);
            // 
            // lblFromCaption
            // 
            this.lblFromCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblFromCaption.Location = new System.Drawing.Point(19, 18);
            this.lblFromCaption.Name = "lblFromCaption";
            this.lblFromCaption.Size = new System.Drawing.Size(200, 20);
            this.lblFromCaption.TabIndex = 0;
            this.lblFromCaption.Text = "From currency";
            // 
            // pnlToCard
            // 
            this.pnlToCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pnlToCard.Controls.Add(this.cmbToCurrency);
            this.pnlToCard.Controls.Add(this.lblToCaption);
            this.pnlToCard.Location = new System.Drawing.Point(476, 160);
            this.pnlToCard.Name = "pnlToCard";
            this.pnlToCard.Size = new System.Drawing.Size(350, 92);
            this.pnlToCard.TabIndex = 3;
            // 
            // cmbToCurrency
            // 
            this.cmbToCurrency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.cmbToCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbToCurrency.ForeColor = System.Drawing.Color.White;
            this.cmbToCurrency.FormattingEnabled = true;
            this.cmbToCurrency.Location = new System.Drawing.Point(22, 47);
            this.cmbToCurrency.Name = "cmbToCurrency";
            this.cmbToCurrency.Size = new System.Drawing.Size(304, 24);
            this.cmbToCurrency.TabIndex = 1;
            this.cmbToCurrency.SelectedIndexChanged += new System.EventHandler(this.CurrencySelectionChanged);
            // 
            // lblToCaption
            // 
            this.lblToCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblToCaption.Location = new System.Drawing.Point(19, 18);
            this.lblToCaption.Name = "lblToCaption";
            this.lblToCaption.Size = new System.Drawing.Size(200, 20);
            this.lblToCaption.TabIndex = 0;
            this.lblToCaption.Text = "To currency";
            // 
            // pnlAmountCard
            // 
            this.pnlAmountCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.pnlAmountCard.Controls.Add(this.txtAmount);
            this.pnlAmountCard.Controls.Add(this.lblAmountCaption);
            this.pnlAmountCard.Location = new System.Drawing.Point(74, 268);
            this.pnlAmountCard.Name = "pnlAmountCard";
            this.pnlAmountCard.Size = new System.Drawing.Size(752, 92);
            this.pnlAmountCard.TabIndex = 4;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.ForeColor = System.Drawing.Color.White;
            this.txtAmount.Location = new System.Drawing.Point(22, 46);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(704, 22);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAmountCaption
            // 
            this.lblAmountCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAmountCaption.Location = new System.Drawing.Point(19, 18);
            this.lblAmountCaption.Name = "lblAmountCaption";
            this.lblAmountCaption.Size = new System.Drawing.Size(200, 20);
            this.lblAmountCaption.TabIndex = 0;
            this.lblAmountCaption.Text = "Exchange amount";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlSummary.Controls.Add(this.lblBreakdownValue);
            this.pnlSummary.Controls.Add(this.lblBreakdownCaption);
            this.pnlSummary.Controls.Add(this.lblReceivedValue);
            this.pnlSummary.Controls.Add(this.lblReceivedCaption);
            this.pnlSummary.Controls.Add(this.lblInsertedValue);
            this.pnlSummary.Controls.Add(this.lblInsertedCaption);
            this.pnlSummary.Controls.Add(this.lblRateValue);
            this.pnlSummary.Controls.Add(this.lblRateCaption);
            this.pnlSummary.Location = new System.Drawing.Point(74, 380);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(752, 204);
            this.pnlSummary.TabIndex = 5;
            // 
            // lblBreakdownValue
            // 
            this.lblBreakdownValue.ForeColor = System.Drawing.Color.White;
            this.lblBreakdownValue.Location = new System.Drawing.Point(384, 107);
            this.lblBreakdownValue.Name = "lblBreakdownValue";
            this.lblBreakdownValue.Size = new System.Drawing.Size(332, 76);
            this.lblBreakdownValue.TabIndex = 7;
            this.lblBreakdownValue.Text = "-";
            // 
            // lblBreakdownCaption
            // 
            this.lblBreakdownCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBreakdownCaption.Location = new System.Drawing.Point(384, 78);
            this.lblBreakdownCaption.Name = "lblBreakdownCaption";
            this.lblBreakdownCaption.Size = new System.Drawing.Size(200, 20);
            this.lblBreakdownCaption.TabIndex = 6;
            this.lblBreakdownCaption.Text = "Dispense breakdown";
            // 
            // lblReceivedValue
            // 
            this.lblReceivedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.lblReceivedValue.Location = new System.Drawing.Point(384, 38);
            this.lblReceivedValue.Name = "lblReceivedValue";
            this.lblReceivedValue.Size = new System.Drawing.Size(332, 24);
            this.lblReceivedValue.TabIndex = 5;
            this.lblReceivedValue.Text = "-";
            // 
            // lblReceivedCaption
            // 
            this.lblReceivedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblReceivedCaption.Location = new System.Drawing.Point(384, 17);
            this.lblReceivedCaption.Name = "lblReceivedCaption";
            this.lblReceivedCaption.Size = new System.Drawing.Size(200, 20);
            this.lblReceivedCaption.TabIndex = 4;
            this.lblReceivedCaption.Text = "Guest receives";
            // 
            // lblInsertedValue
            // 
            this.lblInsertedValue.ForeColor = System.Drawing.Color.White;
            this.lblInsertedValue.Location = new System.Drawing.Point(22, 107);
            this.lblInsertedValue.Name = "lblInsertedValue";
            this.lblInsertedValue.Size = new System.Drawing.Size(332, 24);
            this.lblInsertedValue.TabIndex = 3;
            this.lblInsertedValue.Text = "No cash inserted";
            // 
            // lblInsertedCaption
            // 
            this.lblInsertedCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblInsertedCaption.Location = new System.Drawing.Point(22, 78);
            this.lblInsertedCaption.Name = "lblInsertedCaption";
            this.lblInsertedCaption.Size = new System.Drawing.Size(200, 20);
            this.lblInsertedCaption.TabIndex = 2;
            this.lblInsertedCaption.Text = "Inserted cash";
            // 
            // lblRateValue
            // 
            this.lblRateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblRateValue.Location = new System.Drawing.Point(22, 38);
            this.lblRateValue.Name = "lblRateValue";
            this.lblRateValue.Size = new System.Drawing.Size(332, 24);
            this.lblRateValue.TabIndex = 1;
            this.lblRateValue.Text = "Rate unavailable";
            // 
            // lblRateCaption
            // 
            this.lblRateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblRateCaption.Location = new System.Drawing.Point(22, 17);
            this.lblRateCaption.Name = "lblRateCaption";
            this.lblRateCaption.Size = new System.Drawing.Size(200, 20);
            this.lblRateCaption.TabIndex = 0;
            this.lblRateCaption.Text = "Current rate";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.lblStatusValue.Location = new System.Drawing.Point(74, 592);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(752, 30);
            this.lblStatusValue.TabIndex = 6;
            this.lblStatusValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectCash
            // 
            this.btnSelectCash.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnSelectCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectCash.Enabled = false;
            this.btnSelectCash.Location = new System.Drawing.Point(74, 624);
            this.btnSelectCash.Name = "btnSelectCash";
            this.btnSelectCash.Size = new System.Drawing.Size(180, 46);
            this.btnSelectCash.TabIndex = 7;
            this.btnSelectCash.TabStop = false;
            this.btnSelectCash.Values.Text = "Select Cash";
            this.btnSelectCash.Click += new System.EventHandler(this.SelectCashButton_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreview.Location = new System.Drawing.Point(274, 624);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(180, 46);
            this.btnPreview.TabIndex = 8;
            this.btnPreview.TabStop = false;
            this.btnPreview.Values.Text = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Enabled = false;
            this.btnConfirm.Location = new System.Drawing.Point(474, 624);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(180, 46);
            this.btnConfirm.TabIndex = 9;
            this.btnConfirm.TabStop = false;
            this.btnConfirm.Values.Text = "Confirm";
            this.btnConfirm.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // btnBack
            // 
            this.btnBack.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Location = new System.Drawing.Point(674, 624);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(152, 46);
            this.btnBack.TabIndex = 10;
            this.btnBack.TabStop = false;
            this.btnBack.Values.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // GuestExchangeForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.ControlBox = false;
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnSelectCash);
            this.Controls.Add(this.lblStatusValue);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlAmountCard);
            this.Controls.Add(this.pnlToCard);
            this.Controls.Add(this.pnlFromCard);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GuestExchangeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Currency Exchange";
            this.Load += new System.EventHandler(this.GuestExchangeForm_Load);
            StyleActionButton(this.btnSelectCash, System.Drawing.Color.FromArgb(48, 92, 180), System.Drawing.Color.FromArgb(125, 211, 252), System.Drawing.Color.White);
            StyleActionButton(this.btnPreview, System.Drawing.Color.FromArgb(8, 145, 178), System.Drawing.Color.FromArgb(103, 232, 249), System.Drawing.Color.White);
            StyleActionButton(this.btnConfirm, System.Drawing.Color.FromArgb(22, 163, 74), System.Drawing.Color.FromArgb(134, 239, 172), System.Drawing.Color.White);
            StyleActionButton(this.btnBack, System.Drawing.Color.FromArgb(75, 85, 99), System.Drawing.Color.FromArgb(248, 113, 113), System.Drawing.Color.FromArgb(254, 202, 202));
            this.pnlHeader.ResumeLayout(false);
            this.pnlFromCard.ResumeLayout(false);
            this.pnlToCard.ResumeLayout(false);
            this.pnlAmountCard.ResumeLayout(false);
            this.pnlAmountCard.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private static void StyleActionButton(Krypton.Toolkit.KryptonButton button, System.Drawing.Color backColor, System.Drawing.Color borderColor, System.Drawing.Color textColor)
        {
            button.StateCommon.Back.Color1 = backColor;
            button.StateCommon.Back.Color2 = backColor;
            button.StateCommon.Border.Color1 = borderColor;
            button.StateCommon.Border.Rounding = 8F;
            button.StateCommon.Border.Width = 2;
            button.StateCommon.Content.ShortText.Color1 = textColor;
            button.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            button.StateTracking.Back.Color1 = System.Windows.Forms.ControlPaint.Light(backColor);
            button.StateTracking.Back.Color2 = System.Windows.Forms.ControlPaint.Light(backColor);
            button.StatePressed.Back.Color1 = System.Windows.Forms.ControlPaint.Dark(backColor);
            button.StatePressed.Back.Color2 = System.Windows.Forms.ControlPaint.Dark(backColor);
            button.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(75, 85, 99);
            button.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(203, 213, 225);
            button.OverrideDefault.Back.Color1 = backColor;
            button.OverrideDefault.Back.Color2 = backColor;
            button.OverrideDefault.Border.Color1 = borderColor;
            button.OverrideFocus.Back.Color1 = backColor;
            button.OverrideFocus.Back.Color2 = backColor;
            button.OverrideFocus.Border.Color1 = borderColor;
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlFromCard;
        private System.Windows.Forms.ComboBox cmbFromCurrency;
        private System.Windows.Forms.Label lblFromCaption;
        private System.Windows.Forms.Panel pnlToCard;
        private System.Windows.Forms.ComboBox cmbToCurrency;
        private System.Windows.Forms.Label lblToCaption;
        private System.Windows.Forms.Panel pnlAmountCard;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmountCaption;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblBreakdownValue;
        private System.Windows.Forms.Label lblBreakdownCaption;
        private System.Windows.Forms.Label lblReceivedValue;
        private System.Windows.Forms.Label lblReceivedCaption;
        private System.Windows.Forms.Label lblInsertedValue;
        private System.Windows.Forms.Label lblInsertedCaption;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.Label lblRateCaption;
        private System.Windows.Forms.Label lblStatusValue;
        private Krypton.Toolkit.KryptonButton btnSelectCash;
        private Krypton.Toolkit.KryptonButton btnPreview;
        private Krypton.Toolkit.KryptonButton btnConfirm;
        private Krypton.Toolkit.KryptonButton btnBack;
    }
}
