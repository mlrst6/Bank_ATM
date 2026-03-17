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
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnManageCards = new System.Windows.Forms.Button();
            this.btnAuditLogs = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.Location = new System.Drawing.Point(50, 30);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(110, 24);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "ADMIN PANEL";
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Location = new System.Drawing.Point(50, 100);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(150, 60);
            this.btnManageUsers.TabIndex = 1;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnManageCards
            // 
            this.btnManageCards.Location = new System.Drawing.Point(250, 100);
            this.btnManageCards.Name = "btnManageCards";
            this.btnManageCards.Size = new System.Drawing.Size(150, 60);
            this.btnManageCards.TabIndex = 2;
            this.btnManageCards.Text = "Manage Cards";
            this.btnManageCards.UseVisualStyleBackColor = true;
            this.btnManageCards.Click += new System.EventHandler(this.btnManageCards_Click);
            // 
            // btnAuditLogs
            // 
            this.btnAuditLogs.Location = new System.Drawing.Point(50, 200);
            this.btnAuditLogs.Name = "btnAuditLogs";
            this.btnAuditLogs.Size = new System.Drawing.Size(150, 60);
            this.btnAuditLogs.TabIndex = 3;
            this.btnAuditLogs.Text = "System Logs";
            this.btnAuditLogs.UseVisualStyleBackColor = true;
            this.btnAuditLogs.Click += new System.EventHandler(this.btnAuditLogs_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(150, 300);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 50);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // AdminActionsForm
            // 
            this.ClientSize = new System.Drawing.Size(464, 401);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnAuditLogs);
            this.Controls.Add(this.btnManageCards);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.lblAdminTitle);
            this.Name = "AdminActionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ATM - Administrator";
            this.Load += new System.EventHandler(this.AdminActionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnManageCards;
        private System.Windows.Forms.Button btnAuditLogs;
        private System.Windows.Forms.Button btnLogout;
    }
}
