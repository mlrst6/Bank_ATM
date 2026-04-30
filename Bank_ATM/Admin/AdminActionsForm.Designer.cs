using System.Drawing;

namespace Bank_ATM.Admin
{
    partial class AdminActionsForm
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
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.lblAdminSubtitle = new System.Windows.Forms.Label();
            this.pnlUsers = new System.Windows.Forms.Panel();
            this.lblUsersCount = new System.Windows.Forms.Label();
            this.lblUsersCaption = new System.Windows.Forms.Label();
            this.pnlCards = new System.Windows.Forms.Panel();
            this.lblCardsCount = new System.Windows.Forms.Label();
            this.lblCardsCaption = new System.Windows.Forms.Label();
            this.pnlServices = new System.Windows.Forms.Panel();
            this.lblServicesCount = new System.Windows.Forms.Label();
            this.lblServicesCaption = new System.Windows.Forms.Label();
            this.pnlTransactions = new System.Windows.Forms.Panel();
            this.lblTransactionsCount = new System.Windows.Forms.Label();
            this.lblTransactionsCaption = new System.Windows.Forms.Label();
            this.btnManageUsers = new Krypton.Toolkit.KryptonButton();
            this.btnManageCards = new Krypton.Toolkit.KryptonButton();
            this.btnManageServices = new Krypton.Toolkit.KryptonButton();
            this.btnAuditLogs = new Krypton.Toolkit.KryptonButton();
            this.btnManageCurrencies = new Krypton.Toolkit.KryptonButton();
            this.btnRefillAtm = new Krypton.Toolkit.KryptonButton();
            this.btnManageFees = new Krypton.Toolkit.KryptonButton();
            this.btnLogout = new Krypton.Toolkit.KryptonButton();
            this.pnlUsers.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.pnlServices.SuspendLayout();
            this.pnlTransactions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.ForeColor = System.Drawing.Color.White;
            this.lblAdminTitle.Location = new System.Drawing.Point(34, 28);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(285, 54);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "ADMIN PANEL";
            // 
            // lblAdminSubtitle
            // 
            this.lblAdminSubtitle.AutoSize = true;
            this.lblAdminSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblAdminSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAdminSubtitle.Location = new System.Drawing.Point(40, 88);
            this.lblAdminSubtitle.Name = "lblAdminSubtitle";
            this.lblAdminSubtitle.Size = new System.Drawing.Size(294, 25);
            this.lblAdminSubtitle.TabIndex = 1;
            this.lblAdminSubtitle.Text = "Manage customers, cards, and logs";
            // 
            // pnlUsers
            // 
            this.pnlUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUsers.Controls.Add(this.lblUsersCount);
            this.pnlUsers.Controls.Add(this.lblUsersCaption);
            this.pnlUsers.Location = new System.Drawing.Point(40, 146);
            this.pnlUsers.Name = "pnlUsers";
            this.pnlUsers.Size = new System.Drawing.Size(186, 116);
            this.pnlUsers.TabIndex = 2;
            // 
            // lblUsersCount
            // 
            this.lblUsersCount.AutoSize = true;
            this.lblUsersCount.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblUsersCount.ForeColor = System.Drawing.Color.White;
            this.lblUsersCount.Location = new System.Drawing.Point(16, 16);
            this.lblUsersCount.Name = "lblUsersCount";
            this.lblUsersCount.Size = new System.Drawing.Size(61, 54);
            this.lblUsersCount.TabIndex = 1;
            this.lblUsersCount.Text = "00";
            // 
            // lblUsersCaption
            // 
            this.lblUsersCaption.AutoSize = true;
            this.lblUsersCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsersCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblUsersCaption.Location = new System.Drawing.Point(19, 78);
            this.lblUsersCaption.Name = "lblUsersCaption";
            this.lblUsersCaption.Size = new System.Drawing.Size(87, 23);
            this.lblUsersCaption.TabIndex = 0;
            this.lblUsersCaption.Text = "Total users";
            // 
            // pnlCards
            // 
            this.pnlCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlCards.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCards.Controls.Add(this.lblCardsCount);
            this.pnlCards.Controls.Add(this.lblCardsCaption);
            this.pnlCards.Location = new System.Drawing.Point(240, 146);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(186, 116);
            this.pnlCards.TabIndex = 3;
            // 
            // lblCardsCount
            // 
            this.lblCardsCount.AutoSize = true;
            this.lblCardsCount.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblCardsCount.ForeColor = System.Drawing.Color.White;
            this.lblCardsCount.Location = new System.Drawing.Point(16, 16);
            this.lblCardsCount.Name = "lblCardsCount";
            this.lblCardsCount.Size = new System.Drawing.Size(61, 54);
            this.lblCardsCount.TabIndex = 1;
            this.lblCardsCount.Text = "00";
            // 
            // lblCardsCaption
            // 
            this.lblCardsCaption.AutoSize = true;
            this.lblCardsCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCardsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardsCaption.Location = new System.Drawing.Point(19, 78);
            this.lblCardsCaption.Name = "lblCardsCaption";
            this.lblCardsCaption.Size = new System.Drawing.Size(95, 23);
            this.lblCardsCaption.TabIndex = 0;
            this.lblCardsCaption.Text = "Active cards";
            // 
            // pnlServices
            // 
            this.pnlServices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlServices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlServices.Controls.Add(this.lblServicesCount);
            this.pnlServices.Controls.Add(this.lblServicesCaption);
            this.pnlServices.Location = new System.Drawing.Point(440, 146);
            this.pnlServices.Name = "pnlServices";
            this.pnlServices.Size = new System.Drawing.Size(186, 116);
            this.pnlServices.TabIndex = 4;
            // 
            // lblServicesCount
            // 
            this.lblServicesCount.AutoSize = true;
            this.lblServicesCount.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblServicesCount.ForeColor = System.Drawing.Color.White;
            this.lblServicesCount.Location = new System.Drawing.Point(16, 16);
            this.lblServicesCount.Name = "lblServicesCount";
            this.lblServicesCount.Size = new System.Drawing.Size(61, 54);
            this.lblServicesCount.TabIndex = 1;
            this.lblServicesCount.Text = "00";
            // 
            // lblServicesCaption
            // 
            this.lblServicesCaption.AutoSize = true;
            this.lblServicesCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblServicesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblServicesCaption.Location = new System.Drawing.Point(19, 78);
            this.lblServicesCaption.Name = "lblServicesCaption";
            this.lblServicesCaption.Size = new System.Drawing.Size(111, 23);
            this.lblServicesCaption.TabIndex = 0;
            this.lblServicesCaption.Text = "Active services";
            // 
            // pnlTransactions
            // 
            this.pnlTransactions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlTransactions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTransactions.Controls.Add(this.lblTransactionsCount);
            this.pnlTransactions.Controls.Add(this.lblTransactionsCaption);
            this.pnlTransactions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlTransactions.Location = new System.Drawing.Point(640, 146);
            this.pnlTransactions.Name = "pnlTransactions";
            this.pnlTransactions.Size = new System.Drawing.Size(186, 116);
            this.pnlTransactions.TabIndex = 5;
            this.pnlTransactions.Click += new System.EventHandler(this.pnlTransactions_Click);
            // 
            // lblTransactionsCount
            // 
            this.lblTransactionsCount.AutoSize = true;
            this.lblTransactionsCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTransactionsCount.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            this.lblTransactionsCount.ForeColor = System.Drawing.Color.White;
            this.lblTransactionsCount.Location = new System.Drawing.Point(16, 16);
            this.lblTransactionsCount.Name = "lblTransactionsCount";
            this.lblTransactionsCount.Size = new System.Drawing.Size(61, 54);
            this.lblTransactionsCount.TabIndex = 1;
            this.lblTransactionsCount.Text = "00";
            this.lblTransactionsCount.Click += new System.EventHandler(this.pnlTransactions_Click);
            // 
            // lblTransactionsCaption
            // 
            this.lblTransactionsCaption.AutoSize = true;
            this.lblTransactionsCaption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTransactionsCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransactionsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblTransactionsCaption.Location = new System.Drawing.Point(19, 78);
            this.lblTransactionsCaption.Name = "lblTransactionsCaption";
            this.lblTransactionsCaption.Size = new System.Drawing.Size(153, 23);
            this.lblTransactionsCaption.TabIndex = 0;
            this.lblTransactionsCaption.Text = "ATM cash available";
            this.lblTransactionsCaption.Click += new System.EventHandler(this.pnlTransactions_Click);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnManageUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageUsers.Location = new System.Drawing.Point(40, 290);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnManageUsers.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnManageUsers.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnManageUsers.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnManageUsers.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnManageUsers.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnManageUsers.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnManageUsers.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnManageUsers.Size = new System.Drawing.Size(250, 86);
            this.btnManageUsers.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnManageUsers.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnManageUsers.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnManageUsers.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253)))));
            this.btnManageUsers.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnManageUsers.StateCommon.Border.Rounding = 12F;
            this.btnManageUsers.StateCommon.Border.Width = 2;
            this.btnManageUsers.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnManageUsers.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageUsers.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageUsers.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageUsers.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnManageUsers.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(74)))), ((int)(((byte)(177)))));
            this.btnManageUsers.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(74)))), ((int)(((byte)(177)))));
            this.btnManageUsers.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(141)))), ((int)(((byte)(249)))));
            this.btnManageUsers.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(141)))), ((int)(((byte)(249)))));
            this.btnManageUsers.TabIndex = 6;
            this.btnManageUsers.Values.Text = "Manage Users";
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageCards
            // 
            this.btnManageCards.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnManageCards.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageCards.Location = new System.Drawing.Point(308, 290);
            this.btnManageCards.Name = "btnManageCards";
            this.btnManageCards.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageCards.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageCards.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageCards.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageCards.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageCards.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageCards.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageCards.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageCards.Size = new System.Drawing.Size(250, 86);
            this.btnManageCards.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageCards.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageCards.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageCards.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageCards.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnManageCards.StateCommon.Border.Rounding = 12F;
            this.btnManageCards.StateCommon.Border.Width = 2;
            this.btnManageCards.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnManageCards.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageCards.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageCards.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageCards.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnManageCards.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(109)))), ((int)(((byte)(134)))));
            this.btnManageCards.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(109)))), ((int)(((byte)(134)))));
            this.btnManageCards.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(195)))), ((int)(((byte)(226)))));
            this.btnManageCards.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(195)))), ((int)(((byte)(226)))));
            this.btnManageCards.TabIndex = 7;
            this.btnManageCards.Values.Text = "Manage Cards";
            this.btnManageCards.Click += new System.EventHandler(this.btnManageCards_Click);
            // 
            // btnManageServices
            // 
            this.btnManageServices.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnManageServices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageServices.Location = new System.Drawing.Point(576, 290);
            this.btnManageServices.Name = "btnManageServices";
            this.btnManageServices.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnManageServices.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnManageServices.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnManageServices.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnManageServices.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnManageServices.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnManageServices.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnManageServices.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnManageServices.Size = new System.Drawing.Size(250, 86);
            this.btnManageServices.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnManageServices.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnManageServices.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnManageServices.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnManageServices.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnManageServices.StateCommon.Border.Rounding = 12F;
            this.btnManageServices.StateCommon.Border.Width = 2;
            this.btnManageServices.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnManageServices.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageServices.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageServices.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageServices.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnManageServices.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnManageServices.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnManageServices.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnManageServices.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnManageServices.TabIndex = 8;
            this.btnManageServices.Values.Text = "Manage Services";
            this.btnManageServices.Click += new System.EventHandler(this.btnManageServices_Click);
            // 
            // btnAuditLogs
            // 
            this.btnAuditLogs.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnAuditLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAuditLogs.Location = new System.Drawing.Point(40, 394);
            this.btnAuditLogs.Name = "btnAuditLogs";
            this.btnAuditLogs.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnAuditLogs.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnAuditLogs.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnAuditLogs.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnAuditLogs.Size = new System.Drawing.Size(786, 60);
            this.btnAuditLogs.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnAuditLogs.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnAuditLogs.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnAuditLogs.StateCommon.Border.Rounding = 12F;
            this.btnAuditLogs.StateCommon.Border.Width = 2;
            this.btnAuditLogs.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnAuditLogs.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnAuditLogs.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnAuditLogs.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnAuditLogs.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnAuditLogs.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnAuditLogs.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnAuditLogs.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnAuditLogs.TabIndex = 9;
            this.btnAuditLogs.Values.Text = "System Logs";
            this.btnAuditLogs.Click += new System.EventHandler(this.btnAuditLogs_Click);
            // 
            // btnManageCurrencies
            // 
            this.btnManageCurrencies.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnManageCurrencies.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageCurrencies.Location = new System.Drawing.Point(40, 472);
            this.btnManageCurrencies.Name = "btnManageCurrencies";
            this.btnManageCurrencies.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnManageCurrencies.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnManageCurrencies.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnManageCurrencies.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnManageCurrencies.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnManageCurrencies.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnManageCurrencies.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnManageCurrencies.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnManageCurrencies.Size = new System.Drawing.Size(386, 52);
            this.btnManageCurrencies.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnManageCurrencies.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnManageCurrencies.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnManageCurrencies.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnManageCurrencies.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnManageCurrencies.StateCommon.Border.Rounding = 12F;
            this.btnManageCurrencies.StateCommon.Border.Width = 2;
            this.btnManageCurrencies.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnManageCurrencies.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnManageCurrencies.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageCurrencies.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnManageCurrencies.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnManageCurrencies.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(52)))), ((int)(((byte)(171)))));
            this.btnManageCurrencies.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(52)))), ((int)(((byte)(171)))));
            this.btnManageCurrencies.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(104)))), ((int)(((byte)(239)))));
            this.btnManageCurrencies.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(104)))), ((int)(((byte)(239)))));
            this.btnManageCurrencies.TabIndex = 10;
            this.btnManageCurrencies.Values.Text = "Manage Currencies";
            this.btnManageCurrencies.Click += new System.EventHandler(this.btnManageCurrencies_Click);
            // 
            // btnRefillAtm
            // 
            this.btnRefillAtm.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnRefillAtm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefillAtm.Location = new System.Drawing.Point(440, 472);
            this.btnRefillAtm.Name = "btnRefillAtm";
            this.btnRefillAtm.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnRefillAtm.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnRefillAtm.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnRefillAtm.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnRefillAtm.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnRefillAtm.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnRefillAtm.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnRefillAtm.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnRefillAtm.Size = new System.Drawing.Size(386, 52);
            this.btnRefillAtm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnRefillAtm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnRefillAtm.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnRefillAtm.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(186)))), ((int)(((byte)(116)))));
            this.btnRefillAtm.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnRefillAtm.StateCommon.Border.Rounding = 12F;
            this.btnRefillAtm.StateCommon.Border.Width = 2;
            this.btnRefillAtm.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnRefillAtm.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnRefillAtm.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnRefillAtm.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnRefillAtm.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefillAtm.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(67)))), ((int)(((byte)(9)))));
            this.btnRefillAtm.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(67)))), ((int)(((byte)(9)))));
            this.btnRefillAtm.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(129)))), ((int)(((byte)(69)))));
            this.btnRefillAtm.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(129)))), ((int)(((byte)(69)))));
            this.btnRefillAtm.TabIndex = 11;
            this.btnRefillAtm.Values.Text = "Refill ATM Cash";
            this.btnRefillAtm.Click += new System.EventHandler(this.btnRefillAtm_Click);
            // 
            // btnManageFees
            // 
            this.btnManageFees.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnManageFees.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageFees.Location = new System.Drawing.Point(40, 544);
            this.btnManageFees.Name = "btnManageFees";
            this.btnManageFees.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageFees.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageFees.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageFees.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageFees.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageFees.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageFees.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageFees.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageFees.Size = new System.Drawing.Size(386, 48);
            this.btnManageFees.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageFees.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(145)))), ((int)(((byte)(178)))));
            this.btnManageFees.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageFees.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.btnManageFees.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnManageFees.StateCommon.Border.Rounding = 12F;
            this.btnManageFees.StateCommon.Border.Width = 2;
            this.btnManageFees.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnManageFees.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnManageFees.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(109)))), ((int)(((byte)(134)))));
            this.btnManageFees.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(109)))), ((int)(((byte)(134)))));
            this.btnManageFees.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(195)))), ((int)(((byte)(226)))));
            this.btnManageFees.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(195)))), ((int)(((byte)(226)))));
            this.btnManageFees.TabIndex = 13;
            this.btnManageFees.Values.Text = "Fee Rules";
            this.btnManageFees.Click += new System.EventHandler(this.btnManageFees_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Location = new System.Drawing.Point(640, 544);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.Size = new System.Drawing.Size(186, 48);
            this.btnLogout.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnLogout.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.btnLogout.StateCommon.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.All;
            this.btnLogout.StateCommon.Border.Rounding = 12F;
            this.btnLogout.StateCommon.Border.Width = 2;
            this.btnLogout.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnLogout.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLogout.StateDisabled.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnLogout.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLogout.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.btnLogout.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.btnLogout.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.btnLogout.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.btnLogout.TabIndex = 12;
            this.btnLogout.Values.Text = "Logout";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // AdminActionsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(882, 620);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnManageFees);
            this.Controls.Add(this.btnRefillAtm);
            this.Controls.Add(this.btnManageCurrencies);
            this.Controls.Add(this.btnAuditLogs);
            this.Controls.Add(this.btnManageServices);
            this.Controls.Add(this.btnManageCards);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.pnlTransactions);
            this.Controls.Add(this.pnlServices);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.pnlUsers);
            this.Controls.Add(this.lblAdminSubtitle);
            this.Controls.Add(this.lblAdminTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminActionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ATM - Administrator";
            this.Load += new System.EventHandler(this.AdminActionsForm_Load);
            this.pnlUsers.ResumeLayout(false);
            this.pnlUsers.PerformLayout();
            this.pnlCards.ResumeLayout(false);
            this.pnlCards.PerformLayout();
            this.pnlServices.ResumeLayout(false);
            this.pnlServices.PerformLayout();
            this.pnlTransactions.ResumeLayout(false);
            this.pnlTransactions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Label lblAdminSubtitle;
        private System.Windows.Forms.Panel pnlUsers;
        private System.Windows.Forms.Label lblUsersCount;
        private System.Windows.Forms.Label lblUsersCaption;
        private System.Windows.Forms.Panel pnlCards;
        private System.Windows.Forms.Label lblCardsCount;
        private System.Windows.Forms.Label lblCardsCaption;
        private System.Windows.Forms.Panel pnlServices;
        private System.Windows.Forms.Label lblServicesCount;
        private System.Windows.Forms.Label lblServicesCaption;
        private System.Windows.Forms.Panel pnlTransactions;
        private System.Windows.Forms.Label lblTransactionsCount;
        private System.Windows.Forms.Label lblTransactionsCaption;
        private Krypton.Toolkit.KryptonButton btnManageUsers;
        private Krypton.Toolkit.KryptonButton btnManageCards;
        private Krypton.Toolkit.KryptonButton btnManageServices;
        private Krypton.Toolkit.KryptonButton btnAuditLogs;
        private Krypton.Toolkit.KryptonButton btnManageCurrencies;
        private Krypton.Toolkit.KryptonButton btnRefillAtm;
        private Krypton.Toolkit.KryptonButton btnManageFees;
        private Krypton.Toolkit.KryptonButton btnLogout;
    }
}
