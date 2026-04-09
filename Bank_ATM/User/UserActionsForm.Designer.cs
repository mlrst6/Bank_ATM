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
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnServices = new System.Windows.Forms.Button();
            this.btnBalance = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlAccountCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Location = new System.Drawing.Point(28, 22);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(728, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new System.Drawing.Point(4, 62);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(273, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Manage cash, transfers, and bill payments.";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(3, 16);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(108, 17);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome back";
            // 
            // pnlAccountCard
            // 
            this.pnlAccountCard.Controls.Add(this.lblHelper);
            this.pnlAccountCard.Controls.Add(this.lblStatusValue);
            this.pnlAccountCard.Controls.Add(this.lblStatusCaption);
            this.pnlAccountCard.Controls.Add(this.lblBalanceValue);
            this.pnlAccountCard.Controls.Add(this.lblBalanceCaption);
            this.pnlAccountCard.Controls.Add(this.lblAccountValue);
            this.pnlAccountCard.Controls.Add(this.lblAccountCaption);
            this.pnlAccountCard.Controls.Add(this.lblCardTitle);
            this.pnlAccountCard.Location = new System.Drawing.Point(28, 142);
            this.pnlAccountCard.Name = "pnlAccountCard";
            this.pnlAccountCard.Size = new System.Drawing.Size(728, 178);
            this.pnlAccountCard.TabIndex = 1;
            // 
            // lblHelper
            // 
            this.lblHelper.AutoSize = true;
            this.lblHelper.Location = new System.Drawing.Point(31, 132);
            this.lblHelper.Name = "lblHelper";
            this.lblHelper.Size = new System.Drawing.Size(411, 17);
            this.lblHelper.TabIndex = 7;
            this.lblHelper.Text = "Use the actions below to manage cash, transfers, and service payments.";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Location = new System.Drawing.Point(563, 88);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(44, 17);
            this.lblStatusValue.TabIndex = 6;
            this.lblStatusValue.Text = "Active";
            // 
            // lblStatusCaption
            // 
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.Location = new System.Drawing.Point(562, 56);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(46, 17);
            this.lblStatusCaption.TabIndex = 5;
            this.lblStatusCaption.Text = "Status";
            // 
            // lblBalanceValue
            // 
            this.lblBalanceValue.AutoSize = true;
            this.lblBalanceValue.Location = new System.Drawing.Point(286, 76);
            this.lblBalanceValue.Name = "lblBalanceValue";
            this.lblBalanceValue.Size = new System.Drawing.Size(77, 17);
            this.lblBalanceValue.TabIndex = 4;
            this.lblBalanceValue.Text = "0.00 UZS";
            // 
            // lblBalanceCaption
            // 
            this.lblBalanceCaption.AutoSize = true;
            this.lblBalanceCaption.Location = new System.Drawing.Point(285, 44);
            this.lblBalanceCaption.Name = "lblBalanceCaption";
            this.lblBalanceCaption.Size = new System.Drawing.Size(101, 17);
            this.lblBalanceCaption.TabIndex = 3;
            this.lblBalanceCaption.Text = "Available funds";
            // 
            // lblAccountValue
            // 
            this.lblAccountValue.AutoSize = true;
            this.lblAccountValue.Location = new System.Drawing.Point(30, 88);
            this.lblAccountValue.Name = "lblAccountValue";
            this.lblAccountValue.Size = new System.Drawing.Size(80, 17);
            this.lblAccountValue.TabIndex = 2;
            this.lblAccountValue.Text = "0000000000";
            // 
            // lblAccountCaption
            // 
            this.lblAccountCaption.AutoSize = true;
            this.lblAccountCaption.Location = new System.Drawing.Point(30, 56);
            this.lblAccountCaption.Name = "lblAccountCaption";
            this.lblAccountCaption.Size = new System.Drawing.Size(107, 17);
            this.lblAccountCaption.TabIndex = 1;
            this.lblAccountCaption.Text = "Account number";
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Location = new System.Drawing.Point(29, 20);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(118, 17);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Account overview";
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.Location = new System.Drawing.Point(28, 351);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(220, 72);
            this.btnWithdraw.TabIndex = 1;
            this.btnWithdraw.Text = "Withdraw Cash";
            this.btnWithdraw.UseVisualStyleBackColor = true;
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // btnDeposit
            // 
            this.btnDeposit.Location = new System.Drawing.Point(282, 351);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(220, 72);
            this.btnDeposit.TabIndex = 2;
            this.btnDeposit.Text = "Deposit Funds";
            this.btnDeposit.UseVisualStyleBackColor = true;
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(536, 351);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(220, 72);
            this.btnTransfer.TabIndex = 3;
            this.btnTransfer.Text = "Transfer Money";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnServices
            // 
            this.btnServices.Location = new System.Drawing.Point(28, 441);
            this.btnServices.Name = "btnServices";
            this.btnServices.Size = new System.Drawing.Size(347, 72);
            this.btnServices.TabIndex = 4;
            this.btnServices.Text = "Pay Services";
            this.btnServices.UseVisualStyleBackColor = true;
            this.btnServices.Click += new System.EventHandler(this.btnServices_Click);
            // 
            // btnBalance
            // 
            this.btnBalance.Location = new System.Drawing.Point(409, 441);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(347, 72);
            this.btnBalance.TabIndex = 5;
            this.btnBalance.Text = "View Balance";
            this.btnBalance.UseVisualStyleBackColor = true;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(576, 540);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(180, 48);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // UserActionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 621);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnServices);
            this.Controls.Add(this.btnBalance);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnDeposit);
            this.Controls.Add(this.btnWithdraw);
            this.Controls.Add(this.pnlAccountCard);
            this.Controls.Add(this.pnlHeader);
            this.Name = "UserActionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM - User Services";
            this.Load += new System.EventHandler(this.UserActionsForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlAccountCard.ResumeLayout(false);
            this.pnlAccountCard.PerformLayout();
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
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnServices;
        private System.Windows.Forms.Button btnBalance;
        private System.Windows.Forms.Button btnLogout;
    }
}
