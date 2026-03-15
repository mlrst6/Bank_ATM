using System.Drawing;

namespace Bank_ATM
{
    partial class ExchangeFromUzsToUsdForm
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
            this.ExchangeUzsPrice = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.OtherAmount = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.converter = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.pnlHeader.Controls.Add(this.ExchangeUzsPrice);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 80);
            this.pnlHeader.TabIndex = 2;
            // 
            // ExchangeUzsPrice
            // 
            this.ExchangeUzsPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExchangeUzsPrice.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.ExchangeUzsPrice.ForeColor = System.Drawing.Color.White;
            this.ExchangeUzsPrice.Location = new System.Drawing.Point(0, 0);
            this.ExchangeUzsPrice.Name = "ExchangeUzsPrice";
            this.ExchangeUzsPrice.Size = new System.Drawing.Size(800, 80);
            this.ExchangeUzsPrice.TabIndex = 0;
            this.ExchangeUzsPrice.Text = "Exchange from UZS to USD";
            this.ExchangeUzsPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(100, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 100);
            this.button1.TabIndex = 1;
            this.button1.Text = "1 USD";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(310, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 100);
            this.button2.TabIndex = 1;
            this.button2.Text = "5 USD";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(520, 120);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(180, 100);
            this.button3.TabIndex = 1;
            this.button3.Text = "10 USD";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(100, 240);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(180, 100);
            this.button5.TabIndex = 1;
            this.button5.Text = "50 USD";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(310, 240);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(180, 100);
            this.button6.TabIndex = 1;
            this.button6.Text = "100 USD";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // OtherAmount
            // 
            this.OtherAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.OtherAmount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OtherAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.OtherAmount.ForeColor = System.Drawing.Color.White;
            this.OtherAmount.Location = new System.Drawing.Point(520, 240);
            this.OtherAmount.Name = "OtherAmount";
            this.OtherAmount.Size = new System.Drawing.Size(180, 100);
            this.OtherAmount.TabIndex = 1;
            this.OtherAmount.Text = "Boshqa";
            this.OtherAmount.UseVisualStyleBackColor = false;
            this.OtherAmount.Click += new System.EventHandler(this.OtherAmount_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Back.ForeColor = System.Drawing.Color.IndianRed;
            this.Back.Location = new System.Drawing.Point(200, 360);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(180, 100);
            this.Back.TabIndex = 1;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // converter
            // 
            this.converter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.converter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.converter.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.converter.ForeColor = System.Drawing.Color.White;
            this.converter.Location = new System.Drawing.Point(420, 360);
            this.converter.Name = "converter";
            this.converter.Size = new System.Drawing.Size(180, 100);
            this.converter.TabIndex = 1;
            this.converter.Text = "converter";
            this.converter.UseVisualStyleBackColor = false;
            this.converter.Click += new System.EventHandler(this.converter_Click);
            // 
            // ExchangeFromUzsToUsdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.ControlBox = false;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.OtherAmount);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.converter);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExchangeFromUzsToUsdForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExchangeFromUzsToUsdForm";
            this.Load += new System.EventHandler(this.ExchangeFromUzsToUsdForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ExchangeUzsPrice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button OtherAmount;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button converter;
        private System.Windows.Forms.Panel pnlHeader;
    }
}
