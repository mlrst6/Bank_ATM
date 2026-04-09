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
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageCards = new System.Windows.Forms.Button();
            this.btnManageServices = new System.Windows.Forms.Button();
            this.btnAuditLogs = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlUsers.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.pnlServices.SuspendLayout();
            this.pnlTransactions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.Location = new System.Drawing.Point(36, 28);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(110, 24);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "ADMIN PANEL";
            // 
            // lblAdminSubtitle
            // 
            this.lblAdminSubtitle.AutoSize = true;
            this.lblAdminSubtitle.Location = new System.Drawing.Point(39, 65);
            this.lblAdminSubtitle.Name = "lblAdminSubtitle";
            this.lblAdminSubtitle.Size = new System.Drawing.Size(224, 17);
            this.lblAdminSubtitle.TabIndex = 1;
            this.lblAdminSubtitle.Text = "Manage customers, cards, and logs";
            // 
            // pnlUsers
            // 
            this.pnlUsers.Controls.Add(this.lblUsersCount);
            this.pnlUsers.Controls.Add(this.lblUsersCaption);
            this.pnlUsers.Location = new System.Drawing.Point(40, 105);
            this.pnlUsers.Name = "pnlUsers";
            this.pnlUsers.Size = new System.Drawing.Size(180, 110);
            this.pnlUsers.TabIndex = 2;
            // 
            // lblUsersCount
            // 
            this.lblUsersCount.AutoSize = true;
            this.lblUsersCount.Location = new System.Drawing.Point(18, 18);
            this.lblUsersCount.Name = "lblUsersCount";
            this.lblUsersCount.Size = new System.Drawing.Size(24, 17);
            this.lblUsersCount.TabIndex = 1;
            this.lblUsersCount.Text = "00";
            // 
            // lblUsersCaption
            // 
            this.lblUsersCaption.AutoSize = true;
            this.lblUsersCaption.Location = new System.Drawing.Point(18, 72);
            this.lblUsersCaption.Name = "lblUsersCaption";
            this.lblUsersCaption.Size = new System.Drawing.Size(80, 17);
            this.lblUsersCaption.TabIndex = 0;
            this.lblUsersCaption.Text = "Total users";
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.lblCardsCount);
            this.pnlCards.Controls.Add(this.lblCardsCaption);
            this.pnlCards.Location = new System.Drawing.Point(238, 105);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(180, 110);
            this.pnlCards.TabIndex = 3;
            // 
            // lblCardsCount
            // 
            this.lblCardsCount.AutoSize = true;
            this.lblCardsCount.Location = new System.Drawing.Point(18, 18);
            this.lblCardsCount.Name = "lblCardsCount";
            this.lblCardsCount.Size = new System.Drawing.Size(24, 17);
            this.lblCardsCount.TabIndex = 1;
            this.lblCardsCount.Text = "00";
            // 
            // lblCardsCaption
            // 
            this.lblCardsCaption.AutoSize = true;
            this.lblCardsCaption.Location = new System.Drawing.Point(18, 72);
            this.lblCardsCaption.Name = "lblCardsCaption";
            this.lblCardsCaption.Size = new System.Drawing.Size(79, 17);
            this.lblCardsCaption.TabIndex = 0;
            this.lblCardsCaption.Text = "Active cards";
            // 
            // pnlServices
            // 
            this.pnlServices.Controls.Add(this.lblServicesCount);
            this.pnlServices.Controls.Add(this.lblServicesCaption);
            this.pnlServices.Location = new System.Drawing.Point(436, 105);
            this.pnlServices.Name = "pnlServices";
            this.pnlServices.Size = new System.Drawing.Size(180, 110);
            this.pnlServices.TabIndex = 4;
            // 
            // lblServicesCount
            // 
            this.lblServicesCount.AutoSize = true;
            this.lblServicesCount.Location = new System.Drawing.Point(18, 18);
            this.lblServicesCount.Name = "lblServicesCount";
            this.lblServicesCount.Size = new System.Drawing.Size(24, 17);
            this.lblServicesCount.TabIndex = 1;
            this.lblServicesCount.Text = "00";
            // 
            // lblServicesCaption
            // 
            this.lblServicesCaption.AutoSize = true;
            this.lblServicesCaption.Location = new System.Drawing.Point(18, 72);
            this.lblServicesCaption.Name = "lblServicesCaption";
            this.lblServicesCaption.Size = new System.Drawing.Size(98, 17);
            this.lblServicesCaption.TabIndex = 0;
            this.lblServicesCaption.Text = "Active services";
            // 
            // pnlTransactions
            // 
            this.pnlTransactions.Controls.Add(this.lblTransactionsCount);
            this.pnlTransactions.Controls.Add(this.lblTransactionsCaption);
            this.pnlTransactions.Location = new System.Drawing.Point(634, 105);
            this.pnlTransactions.Name = "pnlTransactions";
            this.pnlTransactions.Size = new System.Drawing.Size(180, 110);
            this.pnlTransactions.TabIndex = 5;
            // 
            // lblTransactionsCount
            // 
            this.lblTransactionsCount.AutoSize = true;
            this.lblTransactionsCount.Location = new System.Drawing.Point(18, 18);
            this.lblTransactionsCount.Name = "lblTransactionsCount";
            this.lblTransactionsCount.Size = new System.Drawing.Size(24, 17);
            this.lblTransactionsCount.TabIndex = 1;
            this.lblTransactionsCount.Text = "00";
            // 
            // lblTransactionsCaption
            // 
            this.lblTransactionsCaption.AutoSize = true;
            this.lblTransactionsCaption.Location = new System.Drawing.Point(18, 72);
            this.lblTransactionsCaption.Name = "lblTransactionsCaption";
            this.lblTransactionsCaption.Size = new System.Drawing.Size(123, 17);
            this.lblTransactionsCaption.TabIndex = 0;
            this.lblTransactionsCaption.Text = "Recent transactions";
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Location = new System.Drawing.Point(40, 245);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(250, 78);
            this.btnManageUsers.TabIndex = 5;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageCards
            // 
            this.btnManageCards.Location = new System.Drawing.Point(302, 245);
            this.btnManageCards.Name = "btnManageCards";
            this.btnManageCards.Size = new System.Drawing.Size(250, 78);
            this.btnManageCards.TabIndex = 6;
            this.btnManageCards.Text = "Manage Cards";
            this.btnManageCards.UseVisualStyleBackColor = true;
            this.btnManageCards.Click += new System.EventHandler(this.btnManageCards_Click);
            // 
            // btnManageServices
            // 
            this.btnManageServices.Location = new System.Drawing.Point(564, 245);
            this.btnManageServices.Name = "btnManageServices";
            this.btnManageServices.Size = new System.Drawing.Size(250, 78);
            this.btnManageServices.TabIndex = 7;
            this.btnManageServices.Text = "Manage Services";
            this.btnManageServices.UseVisualStyleBackColor = true;
            this.btnManageServices.Click += new System.EventHandler(this.btnManageServices_Click);
            // 
            // btnAuditLogs
            // 
            this.btnAuditLogs.Location = new System.Drawing.Point(40, 339);
            this.btnAuditLogs.Name = "btnAuditLogs";
            this.btnAuditLogs.Size = new System.Drawing.Size(774, 64);
            this.btnAuditLogs.TabIndex = 8;
            this.btnAuditLogs.Text = "System Logs";
            this.btnAuditLogs.UseVisualStyleBackColor = true;
            this.btnAuditLogs.Click += new System.EventHandler(this.btnAuditLogs_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(664, 423);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 48);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // AdminActionsForm
            // 
            this.ClientSize = new System.Drawing.Size(860, 500);
            this.Controls.Add(this.btnLogout);
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
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnManageCards;
        private System.Windows.Forms.Button btnManageServices;
        private System.Windows.Forms.Button btnAuditLogs;
        private System.Windows.Forms.Button btnLogout;
    }
}
