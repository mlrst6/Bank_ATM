using System.Drawing;

namespace Bank_ATM
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.MainFormGuest = new System.Windows.Forms.Button();
            this.MainFormUser = new System.Windows.Forms.Button();
            this.MainFormAdmin = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.MainFormInfo = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormGuest
            // 
            this.MainFormGuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.MainFormGuest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainFormGuest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.MainFormGuest.FlatAppearance.BorderSize = 2;
            this.MainFormGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainFormGuest.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.MainFormGuest.ForeColor = System.Drawing.Color.White;
            this.MainFormGuest.Location = new System.Drawing.Point(110, 236);
            this.MainFormGuest.Name = "MainFormGuest";
            this.MainFormGuest.Size = new System.Drawing.Size(320, 110);
            this.MainFormGuest.TabIndex = 0;
            this.MainFormGuest.Text = "Withdraw";
            this.MainFormGuest.UseVisualStyleBackColor = false;
            this.MainFormGuest.Click += new System.EventHandler(this.MainFormGuest_Click);
            // 
            // MainFormUser
            // 
            this.MainFormUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.MainFormUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainFormUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.MainFormUser.FlatAppearance.BorderSize = 2;
            this.MainFormUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainFormUser.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.MainFormUser.ForeColor = System.Drawing.Color.White;
            this.MainFormUser.Location = new System.Drawing.Point(470, 236);
            this.MainFormUser.Name = "MainFormUser";
            this.MainFormUser.Size = new System.Drawing.Size(320, 110);
            this.MainFormUser.TabIndex = 1;
            this.MainFormUser.Text = "Deposit";
            this.MainFormUser.UseVisualStyleBackColor = false;
            this.MainFormUser.Click += new System.EventHandler(this.MainFormUser_Click);
            // 
            // MainFormAdmin
            // 
            this.MainFormAdmin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.MainFormAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainFormAdmin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.MainFormAdmin.FlatAppearance.BorderSize = 2;
            this.MainFormAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainFormAdmin.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.MainFormAdmin.ForeColor = System.Drawing.Color.White;
            this.MainFormAdmin.Location = new System.Drawing.Point(110, 366);
            this.MainFormAdmin.Name = "MainFormAdmin";
            this.MainFormAdmin.Size = new System.Drawing.Size(320, 110);
            this.MainFormAdmin.TabIndex = 2;
            this.MainFormAdmin.Text = "Balance";
            this.MainFormAdmin.UseVisualStyleBackColor = false;
            this.MainFormAdmin.Click += new System.EventHandler(this.MainFormAdmin_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back.FlatAppearance.BorderColor = System.Drawing.Color.IndianRed;
            this.Back.FlatAppearance.BorderSize = 2;
            this.Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.Back.ForeColor = System.Drawing.Color.IndianRed;
            this.Back.Location = new System.Drawing.Point(470, 366);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(320, 110);
            this.Back.TabIndex = 3;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.pnlHeader.Controls.Add(this.MainFormInfo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 80);
            this.pnlHeader.TabIndex = 4;
            // 
            // MainFormInfo
            // 
            this.MainFormInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFormInfo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.MainFormInfo.ForeColor = System.Drawing.Color.White;
            this.MainFormInfo.Location = new System.Drawing.Point(0, 0);
            this.MainFormInfo.Name = "MainFormInfo";
            this.MainFormInfo.Size = new System.Drawing.Size(900, 80);
            this.MainFormInfo.TabIndex = 0;
            this.MainFormInfo.Text = "ATM Terminal";
            this.MainFormInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.ControlBox = false;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.MainFormAdmin);
            this.Controls.Add(this.MainFormUser);
            this.Controls.Add(this.MainFormGuest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Terminal";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MainFormGuest;
        private System.Windows.Forms.Button MainFormUser;
        private System.Windows.Forms.Button MainFormAdmin;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label MainFormInfo;
    }
}
