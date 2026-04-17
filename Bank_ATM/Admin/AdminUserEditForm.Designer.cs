namespace Bank_ATM.Admin
{
    partial class AdminUserEditForm
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
            this.lblDialogSubtitle = new System.Windows.Forms.Label();
            this.lblDialogTitle = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPassNote = new System.Windows.Forms.Label();
            this.lblInitialPin = new System.Windows.Forms.Label();
            this.txtInitialPin = new System.Windows.Forms.TextBox();
            this.lblPinNote = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblDialogSubtitle);
            this.pnlHeader.Controls.Add(this.lblDialogTitle);
            this.pnlHeader.Location = new System.Drawing.Point(24, 18);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(452, 72);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblDialogSubtitle
            // 
            this.lblDialogSubtitle.AutoSize = true;
            this.lblDialogSubtitle.Location = new System.Drawing.Point(18, 41);
            this.lblDialogSubtitle.Name = "lblDialogSubtitle";
            this.lblDialogSubtitle.Size = new System.Drawing.Size(278, 16);
            this.lblDialogSubtitle.TabIndex = 1;
            this.lblDialogSubtitle.Text = "Create or update customer and admin profiles";
            // 
            // lblDialogTitle
            // 
            this.lblDialogTitle.AutoSize = true;
            this.lblDialogTitle.Location = new System.Drawing.Point(18, 12);
            this.lblDialogTitle.Name = "lblDialogTitle";
            this.lblDialogTitle.Size = new System.Drawing.Size(87, 16);
            this.lblDialogTitle.TabIndex = 0;
            this.lblDialogTitle.Text = "User Account";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(188, 122);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(288, 22);
            this.txtFullName.TabIndex = 1;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(188, 174);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(288, 22);
            this.txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(188, 226);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(288, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(188, 292);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(288, 22);
            this.txtPhone.TabIndex = 4;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "User",
            "Admin"});
            this.cmbRole.Location = new System.Drawing.Point(188, 344);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(288, 24);
            this.cmbRole.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(188, 452);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 44);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(340, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(136, 44);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Full Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Phone:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 347);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Role:";
            // 
            // lblPassNote
            // 
            this.lblPassNote.AutoSize = true;
            this.lblPassNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblPassNote.ForeColor = System.Drawing.Color.Gray;
            this.lblPassNote.Location = new System.Drawing.Point(185, 252);
            this.lblPassNote.Name = "lblPassNote";
            this.lblPassNote.Size = new System.Drawing.Size(186, 15);
            this.lblPassNote.TabIndex = 14;
            this.lblPassNote.Text = "Set a strong password for access";
            // 
            // lblInitialPin
            // 
            this.lblInitialPin.AutoSize = true;
            this.lblInitialPin.Location = new System.Drawing.Point(36, 399);
            this.lblInitialPin.Name = "lblInitialPin";
            this.lblInitialPin.Size = new System.Drawing.Size(65, 16);
            this.lblInitialPin.TabIndex = 15;
            this.lblInitialPin.Text = "Initial PIN:";
            // 
            // txtInitialPin
            // 
            this.txtInitialPin.Location = new System.Drawing.Point(188, 396);
            this.txtInitialPin.MaxLength = 4;
            this.txtInitialPin.Name = "txtInitialPin";
            this.txtInitialPin.PasswordChar = '*';
            this.txtInitialPin.Size = new System.Drawing.Size(288, 22);
            this.txtInitialPin.TabIndex = 6;
            // 
            // lblPinNote
            // 
            this.lblPinNote.AutoSize = true;
            this.lblPinNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblPinNote.ForeColor = System.Drawing.Color.Gray;
            this.lblPinNote.Location = new System.Drawing.Point(185, 422);
            this.lblPinNote.Name = "lblPinNote";
            this.lblPinNote.Size = new System.Drawing.Size(182, 15);
            this.lblPinNote.TabIndex = 16;
            this.lblPinNote.Text = "Used for the customer ATM card";
            // 
            // AdminUserEditForm
            // 
            this.ClientSize = new System.Drawing.Size(882, 603);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblPinNote);
            this.Controls.Add(this.txtInitialPin);
            this.Controls.Add(this.lblInitialPin);
            this.Controls.Add(this.lblPassNote);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtFullName);
            this.Name = "AdminUserEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.AdminUserEditForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblDialogTitle;
        private System.Windows.Forms.Label lblDialogSubtitle;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPassNote;
        private System.Windows.Forms.Label lblInitialPin;
        private System.Windows.Forms.TextBox txtInitialPin;
        private System.Windows.Forms.Label lblPinNote;
    }
}
