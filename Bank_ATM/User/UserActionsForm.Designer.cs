namespace Bank_ATM.User
{
    partial class UserActionsForm
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlAccountCard = new System.Windows.Forms.Panel();
            this.lblHelper = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblBalanceValue = new System.Windows.Forms.Label();
            this.lblBalanceCaption = new System.Windows.Forms.Label();
            this.lblAccountValue = new System.Windows.Forms.Label();
            this.lblAccountCaption = new System.Windows.Forms.Label();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.statusBanner = new Bank_ATM.UI.StatusBanner();
            this.btnWithdraw = new Krypton.Toolkit.KryptonButton();
            this.btnDeposit = new Krypton.Toolkit.KryptonButton();
            this.btnTransfer = new Krypton.Toolkit.KryptonButton();
            this.btnServices = new Krypton.Toolkit.KryptonButton();
            this.btnBalance = new Krypton.Toolkit.KryptonButton();
            this.btnSettings = new Krypton.Toolkit.KryptonButton();
            this.btnTransactions = new Krypton.Toolkit.KryptonButton();
            this.btnBack = new Krypton.Toolkit.KryptonButton();
            this.btnLogout = new Krypton.Toolkit.KryptonButton();
            this.pnlHeader.SuspendLayout();
            this.pnlAccountCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Location = new System.Drawing.Point(28, 22);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(844, 106);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(27, 68);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(788, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage cash, transfers, and bill payments.";
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(24, 14);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(792, 54);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome back";
            // 
            // pnlAccountCard
            // 
            this.pnlAccountCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlAccountCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAccountCard.Controls.Add(this.lblHelper);
            this.pnlAccountCard.Controls.Add(this.lblStatusValue);
            this.pnlAccountCard.Controls.Add(this.lblStatusCaption);
            this.pnlAccountCard.Controls.Add(this.lblBalanceValue);
            this.pnlAccountCard.Controls.Add(this.lblBalanceCaption);
            this.pnlAccountCard.Controls.Add(this.lblAccountValue);
            this.pnlAccountCard.Controls.Add(this.lblAccountCaption);
            this.pnlAccountCard.Controls.Add(this.lblCardTitle);
            this.pnlAccountCard.Location = new System.Drawing.Point(28, 134);
            this.pnlAccountCard.Name = "pnlAccountCard";
            this.pnlAccountCard.Size = new System.Drawing.Size(844, 136);
            this.pnlAccountCard.TabIndex = 1;
            // 
            // lblHelper
            // 
            this.lblHelper.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHelper.Location = new System.Drawing.Point(17, 97);
            this.lblHelper.Name = "lblHelper";
            this.lblHelper.Size = new System.Drawing.Size(786, 21);
            this.lblHelper.TabIndex = 7;
            this.lblHelper.Text = "Use the dashboard below to withdraw, deposit, transfer money, pay services, or op" +
    "en card settings.";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblStatusValue.Location = new System.Drawing.Point(628, 53);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(186, 24);
            this.lblStatusValue.TabIndex = 6;
            this.lblStatusValue.Text = "Active";
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblStatusCaption.Location = new System.Drawing.Point(628, 29);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(120, 20);
            this.lblStatusCaption.TabIndex = 5;
            this.lblStatusCaption.Text = "Status";
            // 
            // lblBalanceValue
            // 
            this.lblBalanceValue.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.lblBalanceValue.Location = new System.Drawing.Point(336, 45);
            this.lblBalanceValue.Name = "lblBalanceValue";
            this.lblBalanceValue.Size = new System.Drawing.Size(240, 43);
            this.lblBalanceValue.TabIndex = 4;
            this.lblBalanceValue.Text = "0.00 UZS";
            // 
            // lblBalanceCaption
            // 
            this.lblBalanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBalanceCaption.Location = new System.Drawing.Point(338, 29);
            this.lblBalanceCaption.Name = "lblBalanceCaption";
            this.lblBalanceCaption.Size = new System.Drawing.Size(140, 20);
            this.lblBalanceCaption.TabIndex = 3;
            this.lblBalanceCaption.Text = "Available funds";
            // 
            // lblAccountValue
            // 
            this.lblAccountValue.ForeColor = System.Drawing.Color.White;
            this.lblAccountValue.Location = new System.Drawing.Point(28, 53);
            this.lblAccountValue.Name = "lblAccountValue";
            this.lblAccountValue.Size = new System.Drawing.Size(252, 24);
            this.lblAccountValue.TabIndex = 2;
            this.lblAccountValue.Text = "0000000000";
            // 
            // lblAccountCaption
            // 
            this.lblAccountCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAccountCaption.Location = new System.Drawing.Point(28, 29);
            this.lblAccountCaption.Name = "lblAccountCaption";
            this.lblAccountCaption.Size = new System.Drawing.Size(140, 20);
            this.lblAccountCaption.TabIndex = 1;
            this.lblAccountCaption.Text = "Account number";
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCardTitle.Location = new System.Drawing.Point(26, 8);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(160, 20);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Account overview";
            // 
            // statusBanner
            // 
            this.statusBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.statusBanner.Location = new System.Drawing.Point(28, 276);
            this.statusBanner.Name = "statusBanner";
            this.statusBanner.Padding = new System.Windows.Forms.Padding(18, 10, 42, 10);
            this.statusBanner.Size = new System.Drawing.Size(844, 72);
            this.statusBanner.TabIndex = 2;
            this.statusBanner.Visible = false;
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnWithdraw.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWithdraw.Location = new System.Drawing.Point(28, 370);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnWithdraw.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnWithdraw.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnWithdraw.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnWithdraw.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnWithdraw.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnWithdraw.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnWithdraw.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnWithdraw.Size = new System.Drawing.Size(260, 92);
            this.btnWithdraw.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnWithdraw.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnWithdraw.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnWithdraw.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnWithdraw.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnWithdraw.StateCommon.Border.Rounding = 12F;
            this.btnWithdraw.StateCommon.Border.Width = 2;
            this.btnWithdraw.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnWithdraw.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnWithdraw.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnWithdraw.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnWithdraw.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnWithdraw.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(74)))), ((int)(((byte)(177)))));
            this.btnWithdraw.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(74)))), ((int)(((byte)(177)))));
            this.btnWithdraw.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(141)))), ((int)(((byte)(249)))));
            this.btnWithdraw.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(141)))), ((int)(((byte)(249)))));
            this.btnWithdraw.TabIndex = 3;
            this.btnWithdraw.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnWithdraw.Values.Text = "Withdraw Cash";
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // btnDeposit
            // 
            this.btnDeposit.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnDeposit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeposit.Location = new System.Drawing.Point(320, 370);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnDeposit.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnDeposit.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnDeposit.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnDeposit.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnDeposit.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnDeposit.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnDeposit.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnDeposit.Size = new System.Drawing.Size(260, 92);
            this.btnDeposit.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnDeposit.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnDeposit.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnDeposit.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnDeposit.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnDeposit.StateCommon.Border.Rounding = 12F;
            this.btnDeposit.StateCommon.Border.Width = 2;
            this.btnDeposit.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnDeposit.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnDeposit.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnDeposit.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnDeposit.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnDeposit.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(109)))), ((int)(((byte)(134)))));
            this.btnDeposit.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(109)))), ((int)(((byte)(134)))));
            this.btnDeposit.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(195)))), ((int)(((byte)(226)))));
            this.btnDeposit.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(195)))), ((int)(((byte)(226)))));
            this.btnDeposit.TabIndex = 4;
            this.btnDeposit.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnDeposit.Values.Text = "Deposit Funds";
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransfer.Location = new System.Drawing.Point(612, 370);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTransfer.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTransfer.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnTransfer.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnTransfer.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTransfer.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTransfer.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnTransfer.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnTransfer.Size = new System.Drawing.Size(260, 92);
            this.btnTransfer.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTransfer.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.btnTransfer.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnTransfer.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(181)))), ((int)(((byte)(253)))));
            this.btnTransfer.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTransfer.StateCommon.Border.Rounding = 12F;
            this.btnTransfer.StateCommon.Border.Width = 2;
            this.btnTransfer.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnTransfer.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnTransfer.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTransfer.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTransfer.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTransfer.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(43)))), ((int)(((byte)(177)))));
            this.btnTransfer.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(43)))), ((int)(((byte)(177)))));
            this.btnTransfer.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(102)))), ((int)(((byte)(245)))));
            this.btnTransfer.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(102)))), ((int)(((byte)(245)))));
            this.btnTransfer.TabIndex = 5;
            this.btnTransfer.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTransfer.Values.Text = "Transfer Money";
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnServices
            // 
            this.btnServices.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnServices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnServices.Location = new System.Drawing.Point(28, 478);
            this.btnServices.Name = "btnServices";
            this.btnServices.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnServices.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnServices.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnServices.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnServices.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnServices.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnServices.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnServices.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnServices.Size = new System.Drawing.Size(260, 92);
            this.btnServices.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnServices.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnServices.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnServices.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnServices.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnServices.StateCommon.Border.Rounding = 12F;
            this.btnServices.StateCommon.Border.Width = 2;
            this.btnServices.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnServices.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnServices.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnServices.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnServices.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnServices.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(56)))));
            this.btnServices.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(56)))));
            this.btnServices.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(198)))), ((int)(((byte)(104)))));
            this.btnServices.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(198)))), ((int)(((byte)(104)))));
            this.btnServices.TabIndex = 6;
            this.btnServices.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnServices.Values.Text = "Pay Services";
            this.btnServices.Click += new System.EventHandler(this.btnServices_Click);
            // 
            // btnBalance
            // 
            this.btnBalance.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBalance.Location = new System.Drawing.Point(320, 478);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnBalance.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnBalance.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnBalance.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnBalance.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnBalance.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnBalance.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnBalance.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnBalance.Size = new System.Drawing.Size(260, 92);
            this.btnBalance.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnBalance.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnBalance.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnBalance.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnBalance.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBalance.StateCommon.Border.Rounding = 12F;
            this.btnBalance.StateCommon.Border.Width = 2;
            this.btnBalance.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnBalance.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnBalance.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBalance.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBalance.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnBalance.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(67)))), ((int)(((byte)(9)))));
            this.btnBalance.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(67)))), ((int)(((byte)(9)))));
            this.btnBalance.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(129)))), ((int)(((byte)(69)))));
            this.btnBalance.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(129)))), ((int)(((byte)(69)))));
            this.btnBalance.TabIndex = 7;
            this.btnBalance.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnBalance.Values.Text = "View Balance";
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Location = new System.Drawing.Point(612, 478);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSettings.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSettings.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnSettings.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnSettings.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSettings.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSettings.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnSettings.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnSettings.Size = new System.Drawing.Size(260, 92);
            this.btnSettings.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSettings.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSettings.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnSettings.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnSettings.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSettings.StateCommon.Border.Rounding = 12F;
            this.btnSettings.StateCommon.Border.Width = 2;
            this.btnSettings.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSettings.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnSettings.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnSettings.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnSettings.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnSettings.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(52)))), ((int)(((byte)(171)))));
            this.btnSettings.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(52)))), ((int)(((byte)(171)))));
            this.btnSettings.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(104)))), ((int)(((byte)(239)))));
            this.btnSettings.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(104)))), ((int)(((byte)(239)))));
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSettings.Values.Text = "Settings";
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            //
            // btnTransactions
            //
            this.btnTransactions.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnTransactions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransactions.Location = new System.Drawing.Point(28, 584);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTransactions.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTransactions.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnTransactions.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnTransactions.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTransactions.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTransactions.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnTransactions.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnTransactions.Size = new System.Drawing.Size(844, 44);
            this.btnTransactions.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTransactions.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(118)))), ((int)(((byte)(110)))));
            this.btnTransactions.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnTransactions.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(234)))), ((int)(((byte)(212)))));
            this.btnTransactions.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom)
            | Krypton.Toolkit.PaletteDrawBorders.Left)
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnTransactions.StateCommon.Border.Rounding = 12F;
            this.btnTransactions.StateCommon.Border.Width = 2;
            this.btnTransactions.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnTransactions.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnTransactions.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTransactions.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnTransactions.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTransactions.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(87)))), ((int)(((byte)(108)))));
            this.btnTransactions.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(87)))), ((int)(((byte)(108)))));
            this.btnTransactions.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(158)))), ((int)(((byte)(148)))));
            this.btnTransactions.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(158)))), ((int)(((byte)(148)))));
            this.btnTransactions.TabIndex = 11;
            this.btnTransactions.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnTransactions.Values.Text = "My Transactions";
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            //
            // btnBack
            //
            this.btnBack.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Location = new System.Drawing.Point(28, 642);
            this.btnBack.Name = "btnBack";
            this.btnBack.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.Size = new System.Drawing.Size(412, 44);
            this.btnBack.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnBack.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnBack.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnBack.StateCommon.Border.Rounding = 12F;
            this.btnBack.StateCommon.Border.Width = 2;
            this.btnBack.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnBack.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBack.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnBack.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnBack.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnBack.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnBack.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(127)))));
            this.btnBack.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(127)))));
            this.btnBack.TabIndex = 9;
            this.btnBack.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnBack.Values.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Location = new System.Drawing.Point(460, 642);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.Size = new System.Drawing.Size(412, 44);
            this.btnLogout.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnLogout.StateCommon.Border.Rounding = 12F;
            this.btnLogout.StateCommon.Border.Width = 2;
            this.btnLogout.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnLogout.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogout.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLogout.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLogout.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLogout.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.btnLogout.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.btnLogout.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.btnLogout.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnLogout.Values.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // UserActionsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(900, 706);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnTransactions);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnBalance);
            this.Controls.Add(this.btnServices);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnDeposit);
            this.Controls.Add(this.btnWithdraw);
            this.Controls.Add(this.statusBanner);
            this.Controls.Add(this.pnlAccountCard);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserActionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - User Services";
            this.Load += new System.EventHandler(this.UserActionsForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlAccountCard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnlAccountCard;
        private System.Windows.Forms.Label lblHelper;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblBalanceValue;
        private System.Windows.Forms.Label lblBalanceCaption;
        private System.Windows.Forms.Label lblAccountValue;
        private System.Windows.Forms.Label lblAccountCaption;
        private System.Windows.Forms.Label lblCardTitle;
        private Bank_ATM.UI.StatusBanner statusBanner;
        private Krypton.Toolkit.KryptonButton btnWithdraw;
        private Krypton.Toolkit.KryptonButton btnDeposit;
        private Krypton.Toolkit.KryptonButton btnTransfer;
        private Krypton.Toolkit.KryptonButton btnServices;
        private Krypton.Toolkit.KryptonButton btnBalance;
        private Krypton.Toolkit.KryptonButton btnSettings;
        private Krypton.Toolkit.KryptonButton btnTransactions;
        private Krypton.Toolkit.KryptonButton btnBack;
        private Krypton.Toolkit.KryptonButton btnLogout;
    }
}
