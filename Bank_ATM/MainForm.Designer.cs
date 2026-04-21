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
            this.MainFormGuest = new Krypton.Toolkit.KryptonButton();
            this.MainFormUser = new Krypton.Toolkit.KryptonButton();
            this.MainFormAdmin = new Krypton.Toolkit.KryptonButton();
            this.Back = new Krypton.Toolkit.KryptonButton();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.MainFormInfo = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainFormGuest
            // 
            this.MainFormGuest.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.MainFormGuest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainFormGuest.Location = new System.Drawing.Point(110, 236);
            this.MainFormGuest.Name = "MainFormGuest";
            this.MainFormGuest.Size = new System.Drawing.Size(320, 110);
            this.MainFormGuest.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormGuest.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormGuest.StateCommon.Back.ColorAngle = 30F;
            this.MainFormGuest.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.MainFormGuest.StateCommon.Border.Rounding = 8F;
            this.MainFormGuest.StateCommon.Border.Width = 2;
            this.MainFormGuest.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.MainFormGuest.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.MainFormGuest.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormGuest.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormGuest.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(112)))), ((int)(((byte)(210)))));
            this.MainFormGuest.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(83)))), ((int)(((byte)(170)))));
            this.MainFormGuest.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(76)))), ((int)(((byte)(160)))));
            this.MainFormGuest.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(55)))), ((int)(((byte)(120)))));
            this.MainFormGuest.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormGuest.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormGuest.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.MainFormGuest.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormGuest.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormGuest.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.MainFormGuest.TabIndex = 0;
            this.MainFormGuest.TabStop = false;
            this.MainFormGuest.Text = "GUEST SERVICES";
            this.MainFormGuest.Values.Text = "GUEST SERVICES";
            this.MainFormGuest.Click += new System.EventHandler(this.MainFormGuest_Click);
            // 
            // MainFormUser
            // 
            this.MainFormUser.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.MainFormUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainFormUser.Location = new System.Drawing.Point(470, 236);
            this.MainFormUser.Name = "MainFormUser";
            this.MainFormUser.Size = new System.Drawing.Size(320, 110);
            this.MainFormUser.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.MainFormUser.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.MainFormUser.StateCommon.Back.ColorAngle = 30F;
            this.MainFormUser.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.MainFormUser.StateCommon.Border.Rounding = 8F;
            this.MainFormUser.StateCommon.Border.Width = 2;
            this.MainFormUser.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.MainFormUser.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.MainFormUser.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.MainFormUser.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.MainFormUser.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.MainFormUser.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.MainFormUser.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.MainFormUser.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.MainFormUser.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.MainFormUser.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.MainFormUser.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.MainFormUser.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.MainFormUser.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.MainFormUser.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.MainFormUser.TabIndex = 1;
            this.MainFormUser.TabStop = false;
            this.MainFormUser.Text = "CARD LOGIN";
            this.MainFormUser.Values.Text = "CARD LOGIN";
            this.MainFormUser.Click += new System.EventHandler(this.MainFormUser_Click);
            // 
            // MainFormAdmin
            // 
            this.MainFormAdmin.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.MainFormAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MainFormAdmin.Location = new System.Drawing.Point(110, 366);
            this.MainFormAdmin.Name = "MainFormAdmin";
            this.MainFormAdmin.Size = new System.Drawing.Size(320, 110);
            this.MainFormAdmin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormAdmin.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormAdmin.StateCommon.Back.ColorAngle = 30F;
            this.MainFormAdmin.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.MainFormAdmin.StateCommon.Border.Rounding = 8F;
            this.MainFormAdmin.StateCommon.Border.Width = 2;
            this.MainFormAdmin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.MainFormAdmin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.MainFormAdmin.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormAdmin.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormAdmin.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(112)))), ((int)(((byte)(210)))));
            this.MainFormAdmin.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(83)))), ((int)(((byte)(170)))));
            this.MainFormAdmin.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(76)))), ((int)(((byte)(160)))));
            this.MainFormAdmin.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(55)))), ((int)(((byte)(120)))));
            this.MainFormAdmin.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormAdmin.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormAdmin.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.MainFormAdmin.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(92)))), ((int)(((byte)(180)))));
            this.MainFormAdmin.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(64)))), ((int)(((byte)(140)))));
            this.MainFormAdmin.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.MainFormAdmin.TabIndex = 2;
            this.MainFormAdmin.TabStop = false;
            this.MainFormAdmin.Text = "ADMINISTRATOR";
            this.MainFormAdmin.Values.Text = "ADMINISTRATOR";
            this.MainFormAdmin.Click += new System.EventHandler(this.MainFormAdmin_Click);
            // 
            // Back
            // 
            this.Back.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back.Location = new System.Drawing.Point(470, 366);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(320, 110);
            this.Back.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.Back.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.Back.StateCommon.Back.ColorAngle = 30F;
            this.Back.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.Back.StateCommon.Border.Rounding = 8F;
            this.Back.StateCommon.Border.Width = 2;
            this.Back.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.Back.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.Back.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.Back.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.Back.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.Back.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.Back.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.Back.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.Back.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.Back.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.Back.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.Back.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.Back.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.Back.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.Back.TabIndex = 3;
            this.Back.TabStop = false;
            this.Back.Text = "Back";
            this.Back.Values.Text = "Back";
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
            this.MainFormInfo.Text = "Select User Type";
            this.MainFormInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
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

        private Krypton.Toolkit.KryptonButton MainFormGuest;
        private Krypton.Toolkit.KryptonButton MainFormUser;
        private Krypton.Toolkit.KryptonButton MainFormAdmin;
        private Krypton.Toolkit.KryptonButton Back;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label MainFormInfo;
    }
}
