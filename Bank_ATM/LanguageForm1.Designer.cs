namespace Bank_ATM
{
    partial class LanguageForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.UZ = new System.Windows.Forms.Button();
            this.RU = new System.Windows.Forms.Button();
            this.ENG = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(347, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bank ATM";
            // 
            // UZ
            // 
            this.UZ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UZ.Location = new System.Drawing.Point(124, 331);
            this.UZ.Name = "UZ";
            this.UZ.Size = new System.Drawing.Size(193, 71);
            this.UZ.TabIndex = 1;
            this.UZ.Text = "UZ";
            this.UZ.UseVisualStyleBackColor = true;
            this.UZ.Click += new System.EventHandler(this.button1_Click);
            // 
            // RU
            // 
            this.RU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RU.Location = new System.Drawing.Point(613, 331);
            this.RU.Name = "RU";
            this.RU.Size = new System.Drawing.Size(193, 71);
            this.RU.TabIndex = 1;
            this.RU.Text = "RU";
            this.RU.UseVisualStyleBackColor = true;
            this.RU.Click += new System.EventHandler(this.button2_Click);
            // 
            // ENG
            // 
            this.ENG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ENG.Location = new System.Drawing.Point(370, 331);
            this.ENG.Name = "ENG";
            this.ENG.Size = new System.Drawing.Size(193, 71);
            this.ENG.TabIndex = 1;
            this.ENG.Text = "ENG";
            this.ENG.UseVisualStyleBackColor = true;
            this.ENG.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(185, 490);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(571, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tilni tanlang / Choose language / Выберите язык";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(960, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "X";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // LanguageForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ENG);
            this.Controls.Add(this.RU);
            this.Controls.Add(this.UZ);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "LanguageForm1";
            this.Text = "LanguageForm1";
            this.Load += new System.EventHandler(this.LanguageForm1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UZ;
        private System.Windows.Forms.Button RU;
        private System.Windows.Forms.Button ENG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}