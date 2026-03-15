using System.Drawing;

namespace Bank_ATM
{
    partial class LanguageForm1
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
            this.label1 = new System.Windows.Forms.Label();
            this.UZ = new System.Windows.Forms.Button();
            this.RU = new System.Windows.Forms.Button();
            this.ENG = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 100);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bank ATM";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UZ
            // 
            this.UZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.UZ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UZ.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.UZ.FlatAppearance.BorderSize = 2;
            this.UZ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UZ.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.UZ.ForeColor = System.Drawing.Color.White;
            this.UZ.Location = new System.Drawing.Point(50, 150);
            this.UZ.Name = "UZ";
            this.UZ.Size = new System.Drawing.Size(220, 100);
            this.UZ.TabIndex = 1;
            this.UZ.Text = "O\'zbekcha";
            this.UZ.UseVisualStyleBackColor = false;
            this.UZ.Click += new System.EventHandler(this.button1_Click);
            // 
            // RU
            // 
            this.RU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.RU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RU.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.RU.FlatAppearance.BorderSize = 2;
            this.RU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RU.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.RU.ForeColor = System.Drawing.Color.White;
            this.RU.Location = new System.Drawing.Point(530, 150);
            this.RU.Name = "RU";
            this.RU.Size = new System.Drawing.Size(220, 100);
            this.RU.TabIndex = 1;
            this.RU.Text = "Русский";
            this.RU.UseVisualStyleBackColor = false;
            this.RU.Click += new System.EventHandler(this.button2_Click);
            // 
            // ENG
            // 
            this.ENG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.ENG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ENG.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.ENG.FlatAppearance.BorderSize = 2;
            this.ENG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ENG.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.ENG.ForeColor = System.Drawing.Color.White;
            this.ENG.Location = new System.Drawing.Point(290, 150);
            this.ENG.Name = "ENG";
            this.ENG.Size = new System.Drawing.Size(220, 100);
            this.ENG.TabIndex = 1;
            this.ENG.Text = "English";
            this.ENG.UseVisualStyleBackColor = false;
            this.ENG.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(0, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(800, 40);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tilni tanlang / Choose language / Выберите язык";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(760, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 29);
            this.label3.TabIndex = 3;
            this.label3.Text = "X";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 100);
            this.pnlHeader.TabIndex = 4;
            // 
            // LanguageForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.ControlBox = false;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ENG);
            this.Controls.Add(this.RU);
            this.Controls.Add(this.UZ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LanguageForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Language Selection";
            this.Load += new System.EventHandler(this.LanguageForm1_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UZ;
        private System.Windows.Forms.Button RU;
        private System.Windows.Forms.Button ENG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlHeader;
    }
}
