using System.Drawing;

namespace Bank_ATM
{
    partial class ExchangeRateForm
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
            this.ExchangeRate = new System.Windows.Forms.Label();
            this.btnUzsToUsd = new System.Windows.Forms.Button();
            this.btnUsdToUzs = new System.Windows.Forms.Button();
            this.UZSUSD = new System.Windows.Forms.Label();
            this.USDUZS = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExchangeRate
            // 
            this.ExchangeRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExchangeRate.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.ExchangeRate.ForeColor = System.Drawing.Color.White;
            this.ExchangeRate.Location = new System.Drawing.Point(0, 0);
            this.ExchangeRate.Name = "ExchangeRate";
            this.ExchangeRate.Size = new System.Drawing.Size(900, 80);
            this.ExchangeRate.TabIndex = 0;
            this.ExchangeRate.Text = "Exchange Rate";
            this.ExchangeRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUzsToUsd
            // 
            this.btnUzsToUsd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.btnUzsToUsd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUzsToUsd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnUzsToUsd.ForeColor = System.Drawing.Color.White;
            this.btnUzsToUsd.Location = new System.Drawing.Point(400, 120);
            this.btnUzsToUsd.Name = "btnUzsToUsd";
            this.btnUzsToUsd.Size = new System.Drawing.Size(200, 80);
            this.btnUzsToUsd.TabIndex = 1;
            this.btnUzsToUsd.Text = "GO";
            this.btnUzsToUsd.UseVisualStyleBackColor = false;
            this.btnUzsToUsd.Click += new System.EventHandler(this.btnUzsToUsd_Click);
            // 
            // btnUsdToUzs
            // 
            this.btnUsdToUzs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.btnUsdToUzs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsdToUzs.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnUsdToUzs.ForeColor = System.Drawing.Color.White;
            this.btnUsdToUzs.Location = new System.Drawing.Point(400, 220);
            this.btnUsdToUzs.Name = "btnUsdToUzs";
            this.btnUsdToUzs.Size = new System.Drawing.Size(200, 80);
            this.btnUsdToUzs.TabIndex = 1;
            this.btnUsdToUzs.Text = "GO";
            this.btnUsdToUzs.UseVisualStyleBackColor = false;
            // 
            // UZSUSD
            // 
            this.UZSUSD.AutoSize = true;
            this.UZSUSD.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.UZSUSD.ForeColor = System.Drawing.Color.White;
            this.UZSUSD.Location = new System.Drawing.Point(150, 140);
            this.UZSUSD.Name = "UZSUSD";
            this.UZSUSD.Size = new System.Drawing.Size(205, 37);
            this.UZSUSD.TabIndex = 2;
            this.UZSUSD.Text = "So`m -> Dollar";
            // 
            // USDUZS
            // 
            this.USDUZS.AutoSize = true;
            this.USDUZS.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.USDUZS.ForeColor = System.Drawing.Color.White;
            this.USDUZS.Location = new System.Drawing.Point(150, 240);
            this.USDUZS.Name = "USDUZS";
            this.USDUZS.Size = new System.Drawing.Size(205, 37);
            this.USDUZS.TabIndex = 3;
            this.USDUZS.Text = "Dollar -> So`m";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.IndianRed;
            this.btnBack.Location = new System.Drawing.Point(300, 340);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(200, 60);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.pnlHeader.Controls.Add(this.ExchangeRate);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 80);
            this.pnlHeader.TabIndex = 4;
            // 
            // ExchangeRateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(900, 650);
            this.ControlBox = false;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.USDUZS);
            this.Controls.Add(this.UZSUSD);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnUsdToUzs);
            this.Controls.Add(this.btnUzsToUsd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExchangeRateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExchangeRateForm";
            this.Load += new System.EventHandler(this.ExchangeRateForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ExchangeRate;
        private System.Windows.Forms.Button btnUzsToUsd;
        private System.Windows.Forms.Button btnUsdToUzs;
        private System.Windows.Forms.Label UZSUSD;
        private System.Windows.Forms.Label USDUZS;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlHeader;
    }
}
