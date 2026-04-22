namespace Bank_ATM.User
{
    partial class UserSettingsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlProfileCard = new System.Windows.Forms.Panel();
            this.lblAccountStatusValue = new System.Windows.Forms.Label();
            this.lblAccountStatusCaption = new System.Windows.Forms.Label();
            this.lblBalanceValue = new System.Windows.Forms.Label();
            this.lblBalanceCaption = new System.Windows.Forms.Label();
            this.lblAccountValue = new System.Windows.Forms.Label();
            this.lblAccountCaption = new System.Windows.Forms.Label();
            this.lblPhoneValue = new System.Windows.Forms.Label();
            this.lblPhoneCaption = new System.Windows.Forms.Label();
            this.lblUsernameValue = new System.Windows.Forms.Label();
            this.lblUsernameCaption = new System.Windows.Forms.Label();
            this.lblFullNameValue = new System.Windows.Forms.Label();
            this.lblFullNameCaption = new System.Windows.Forms.Label();
            this.lblProfileTitle = new System.Windows.Forms.Label();
            this.pnlCardCard = new System.Windows.Forms.Panel();
            this.lblSessionTimeoutValue = new System.Windows.Forms.Label();
            this.lblSessionTimeoutCaption = new System.Windows.Forms.Label();
            this.lblCardStatusValue = new System.Windows.Forms.Label();
            this.lblCardStatusCaption = new System.Windows.Forms.Label();
            this.lblCardExpiryValue = new System.Windows.Forms.Label();
            this.lblCardExpiryCaption = new System.Windows.Forms.Label();
            this.lblCardTypeValue = new System.Windows.Forms.Label();
            this.lblCardTypeCaption = new System.Windows.Forms.Label();
            this.lblCardNumberValue = new System.Windows.Forms.Label();
            this.lblCardNumberCaption = new System.Windows.Forms.Label();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.pnlSecurityCard = new System.Windows.Forms.Panel();
            this.pnlPinEditor = new System.Windows.Forms.Panel();
            this.lblPinStatus = new System.Windows.Forms.Label();
            this.btnCancelPin = new Krypton.Toolkit.KryptonButton();
            this.btnSubmitPin = new Krypton.Toolkit.KryptonButton();
            this.txtConfirmPin = new System.Windows.Forms.TextBox();
            this.lblConfirmPinCaption = new System.Windows.Forms.Label();
            this.txtNewPin = new System.Windows.Forms.TextBox();
            this.lblNewPinCaption = new System.Windows.Forms.Label();
            this.txtCurrentPin = new System.Windows.Forms.TextBox();
            this.lblCurrentPinCaption = new System.Windows.Forms.Label();
            this.btnChangePin = new Krypton.Toolkit.KryptonButton();
            this.lblSecuritySubtitle = new System.Windows.Forms.Label();
            this.lblSecurityTitle = new System.Windows.Forms.Label();
            this.btnClose = new Krypton.Toolkit.KryptonButton();
            this.pnlHeader.SuspendLayout();
            this.pnlProfileCard.SuspendLayout();
            this.pnlCardCard.SuspendLayout();
            this.pnlSecurityCard.SuspendLayout();
            this.pnlPinEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new System.Drawing.Point(28, 22);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(844, 92);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(25, 59);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(786, 24);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Review your profile, linked card, and security details.";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(790, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "User Settings";
            // 
            // pnlProfileCard
            // 
            this.pnlProfileCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlProfileCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProfileCard.Controls.Add(this.lblAccountStatusValue);
            this.pnlProfileCard.Controls.Add(this.lblAccountStatusCaption);
            this.pnlProfileCard.Controls.Add(this.lblBalanceValue);
            this.pnlProfileCard.Controls.Add(this.lblBalanceCaption);
            this.pnlProfileCard.Controls.Add(this.lblAccountValue);
            this.pnlProfileCard.Controls.Add(this.lblAccountCaption);
            this.pnlProfileCard.Controls.Add(this.lblPhoneValue);
            this.pnlProfileCard.Controls.Add(this.lblPhoneCaption);
            this.pnlProfileCard.Controls.Add(this.lblUsernameValue);
            this.pnlProfileCard.Controls.Add(this.lblUsernameCaption);
            this.pnlProfileCard.Controls.Add(this.lblFullNameValue);
            this.pnlProfileCard.Controls.Add(this.lblFullNameCaption);
            this.pnlProfileCard.Controls.Add(this.lblProfileTitle);
            this.pnlProfileCard.Location = new System.Drawing.Point(28, 136);
            this.pnlProfileCard.Name = "pnlProfileCard";
            this.pnlProfileCard.Size = new System.Drawing.Size(404, 344);
            this.pnlProfileCard.TabIndex = 1;
            // 
            // lblAccountStatusValue
            // 
            this.lblAccountStatusValue.ForeColor = System.Drawing.Color.White;
            this.lblAccountStatusValue.Location = new System.Drawing.Point(24, 286);
            this.lblAccountStatusValue.Name = "lblAccountStatusValue";
            this.lblAccountStatusValue.Size = new System.Drawing.Size(352, 24);
            this.lblAccountStatusValue.TabIndex = 12;
            this.lblAccountStatusValue.Text = "-";
            // 
            // lblAccountStatusCaption
            // 
            this.lblAccountStatusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAccountStatusCaption.Location = new System.Drawing.Point(24, 264);
            this.lblAccountStatusCaption.Name = "lblAccountStatusCaption";
            this.lblAccountStatusCaption.Size = new System.Drawing.Size(140, 20);
            this.lblAccountStatusCaption.TabIndex = 11;
            this.lblAccountStatusCaption.Text = "Account status";
            // 
            // lblBalanceValue
            // 
            this.lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(211)))), ((int)(((byte)(252)))));
            this.lblBalanceValue.Location = new System.Drawing.Point(24, 236);
            this.lblBalanceValue.Name = "lblBalanceValue";
            this.lblBalanceValue.Size = new System.Drawing.Size(352, 24);
            this.lblBalanceValue.TabIndex = 10;
            this.lblBalanceValue.Text = "-";
            // 
            // lblBalanceCaption
            // 
            this.lblBalanceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBalanceCaption.Location = new System.Drawing.Point(24, 214);
            this.lblBalanceCaption.Name = "lblBalanceCaption";
            this.lblBalanceCaption.Size = new System.Drawing.Size(140, 20);
            this.lblBalanceCaption.TabIndex = 9;
            this.lblBalanceCaption.Text = "Balance";
            // 
            // lblAccountValue
            // 
            this.lblAccountValue.ForeColor = System.Drawing.Color.White;
            this.lblAccountValue.Location = new System.Drawing.Point(24, 186);
            this.lblAccountValue.Name = "lblAccountValue";
            this.lblAccountValue.Size = new System.Drawing.Size(352, 24);
            this.lblAccountValue.TabIndex = 8;
            this.lblAccountValue.Text = "-";
            // 
            // lblAccountCaption
            // 
            this.lblAccountCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAccountCaption.Location = new System.Drawing.Point(24, 164);
            this.lblAccountCaption.Name = "lblAccountCaption";
            this.lblAccountCaption.Size = new System.Drawing.Size(140, 20);
            this.lblAccountCaption.TabIndex = 7;
            this.lblAccountCaption.Text = "Account number";
            // 
            // lblPhoneValue
            // 
            this.lblPhoneValue.ForeColor = System.Drawing.Color.White;
            this.lblPhoneValue.Location = new System.Drawing.Point(24, 136);
            this.lblPhoneValue.Name = "lblPhoneValue";
            this.lblPhoneValue.Size = new System.Drawing.Size(352, 24);
            this.lblPhoneValue.TabIndex = 6;
            this.lblPhoneValue.Text = "-";
            // 
            // lblPhoneCaption
            // 
            this.lblPhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblPhoneCaption.Location = new System.Drawing.Point(24, 114);
            this.lblPhoneCaption.Name = "lblPhoneCaption";
            this.lblPhoneCaption.Size = new System.Drawing.Size(140, 20);
            this.lblPhoneCaption.TabIndex = 5;
            this.lblPhoneCaption.Text = "Phone";
            // 
            // lblUsernameValue
            // 
            this.lblUsernameValue.ForeColor = System.Drawing.Color.White;
            this.lblUsernameValue.Location = new System.Drawing.Point(24, 86);
            this.lblUsernameValue.Name = "lblUsernameValue";
            this.lblUsernameValue.Size = new System.Drawing.Size(352, 24);
            this.lblUsernameValue.TabIndex = 4;
            this.lblUsernameValue.Text = "-";
            // 
            // lblUsernameCaption
            // 
            this.lblUsernameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblUsernameCaption.Location = new System.Drawing.Point(24, 64);
            this.lblUsernameCaption.Name = "lblUsernameCaption";
            this.lblUsernameCaption.Size = new System.Drawing.Size(140, 20);
            this.lblUsernameCaption.TabIndex = 3;
            this.lblUsernameCaption.Text = "Username";
            // 
            // lblFullNameValue
            // 
            this.lblFullNameValue.ForeColor = System.Drawing.Color.White;
            this.lblFullNameValue.Location = new System.Drawing.Point(24, 40);
            this.lblFullNameValue.Name = "lblFullNameValue";
            this.lblFullNameValue.Size = new System.Drawing.Size(352, 24);
            this.lblFullNameValue.TabIndex = 2;
            this.lblFullNameValue.Text = "-";
            // 
            // lblFullNameCaption
            // 
            this.lblFullNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblFullNameCaption.Location = new System.Drawing.Point(24, 18);
            this.lblFullNameCaption.Name = "lblFullNameCaption";
            this.lblFullNameCaption.Size = new System.Drawing.Size(140, 20);
            this.lblFullNameCaption.TabIndex = 1;
            this.lblFullNameCaption.Text = "Full name";
            // 
            // lblProfileTitle
            // 
            this.lblProfileTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblProfileTitle.Location = new System.Drawing.Point(22, 0);
            this.lblProfileTitle.Name = "lblProfileTitle";
            this.lblProfileTitle.Size = new System.Drawing.Size(180, 20);
            this.lblProfileTitle.TabIndex = 0;
            this.lblProfileTitle.Text = "Profile and account";
            // 
            // pnlCardCard
            // 
            this.pnlCardCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlCardCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardCard.Controls.Add(this.lblSessionTimeoutValue);
            this.pnlCardCard.Controls.Add(this.lblSessionTimeoutCaption);
            this.pnlCardCard.Controls.Add(this.lblCardStatusValue);
            this.pnlCardCard.Controls.Add(this.lblCardStatusCaption);
            this.pnlCardCard.Controls.Add(this.lblCardExpiryValue);
            this.pnlCardCard.Controls.Add(this.lblCardExpiryCaption);
            this.pnlCardCard.Controls.Add(this.lblCardTypeValue);
            this.pnlCardCard.Controls.Add(this.lblCardTypeCaption);
            this.pnlCardCard.Controls.Add(this.lblCardNumberValue);
            this.pnlCardCard.Controls.Add(this.lblCardNumberCaption);
            this.pnlCardCard.Controls.Add(this.lblCardTitle);
            this.pnlCardCard.Location = new System.Drawing.Point(468, 136);
            this.pnlCardCard.Name = "pnlCardCard";
            this.pnlCardCard.Size = new System.Drawing.Size(404, 344);
            this.pnlCardCard.TabIndex = 2;
            // 
            // lblSessionTimeoutValue
            // 
            this.lblSessionTimeoutValue.ForeColor = System.Drawing.Color.White;
            this.lblSessionTimeoutValue.Location = new System.Drawing.Point(24, 236);
            this.lblSessionTimeoutValue.Name = "lblSessionTimeoutValue";
            this.lblSessionTimeoutValue.Size = new System.Drawing.Size(352, 24);
            this.lblSessionTimeoutValue.TabIndex = 10;
            this.lblSessionTimeoutValue.Text = "-";
            // 
            // lblSessionTimeoutCaption
            // 
            this.lblSessionTimeoutCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSessionTimeoutCaption.Location = new System.Drawing.Point(24, 214);
            this.lblSessionTimeoutCaption.Name = "lblSessionTimeoutCaption";
            this.lblSessionTimeoutCaption.Size = new System.Drawing.Size(140, 20);
            this.lblSessionTimeoutCaption.TabIndex = 9;
            this.lblSessionTimeoutCaption.Text = "Session timeout";
            // 
            // lblCardStatusValue
            // 
            this.lblCardStatusValue.ForeColor = System.Drawing.Color.White;
            this.lblCardStatusValue.Location = new System.Drawing.Point(24, 186);
            this.lblCardStatusValue.Name = "lblCardStatusValue";
            this.lblCardStatusValue.Size = new System.Drawing.Size(352, 24);
            this.lblCardStatusValue.TabIndex = 8;
            this.lblCardStatusValue.Text = "-";
            // 
            // lblCardStatusCaption
            // 
            this.lblCardStatusCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardStatusCaption.Location = new System.Drawing.Point(24, 164);
            this.lblCardStatusCaption.Name = "lblCardStatusCaption";
            this.lblCardStatusCaption.Size = new System.Drawing.Size(140, 20);
            this.lblCardStatusCaption.TabIndex = 7;
            this.lblCardStatusCaption.Text = "Card status";
            // 
            // lblCardExpiryValue
            // 
            this.lblCardExpiryValue.ForeColor = System.Drawing.Color.White;
            this.lblCardExpiryValue.Location = new System.Drawing.Point(24, 136);
            this.lblCardExpiryValue.Name = "lblCardExpiryValue";
            this.lblCardExpiryValue.Size = new System.Drawing.Size(352, 24);
            this.lblCardExpiryValue.TabIndex = 6;
            this.lblCardExpiryValue.Text = "-";
            // 
            // lblCardExpiryCaption
            // 
            this.lblCardExpiryCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardExpiryCaption.Location = new System.Drawing.Point(24, 114);
            this.lblCardExpiryCaption.Name = "lblCardExpiryCaption";
            this.lblCardExpiryCaption.Size = new System.Drawing.Size(140, 20);
            this.lblCardExpiryCaption.TabIndex = 5;
            this.lblCardExpiryCaption.Text = "Card expiry";
            // 
            // lblCardTypeValue
            // 
            this.lblCardTypeValue.ForeColor = System.Drawing.Color.White;
            this.lblCardTypeValue.Location = new System.Drawing.Point(24, 86);
            this.lblCardTypeValue.Name = "lblCardTypeValue";
            this.lblCardTypeValue.Size = new System.Drawing.Size(352, 24);
            this.lblCardTypeValue.TabIndex = 4;
            this.lblCardTypeValue.Text = "-";
            // 
            // lblCardTypeCaption
            // 
            this.lblCardTypeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardTypeCaption.Location = new System.Drawing.Point(24, 64);
            this.lblCardTypeCaption.Name = "lblCardTypeCaption";
            this.lblCardTypeCaption.Size = new System.Drawing.Size(140, 20);
            this.lblCardTypeCaption.TabIndex = 3;
            this.lblCardTypeCaption.Text = "Card type";
            // 
            // lblCardNumberValue
            // 
            this.lblCardNumberValue.ForeColor = System.Drawing.Color.White;
            this.lblCardNumberValue.Location = new System.Drawing.Point(24, 40);
            this.lblCardNumberValue.Name = "lblCardNumberValue";
            this.lblCardNumberValue.Size = new System.Drawing.Size(352, 24);
            this.lblCardNumberValue.TabIndex = 2;
            this.lblCardNumberValue.Text = "-";
            // 
            // lblCardNumberCaption
            // 
            this.lblCardNumberCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCardNumberCaption.Location = new System.Drawing.Point(24, 18);
            this.lblCardNumberCaption.Name = "lblCardNumberCaption";
            this.lblCardNumberCaption.Size = new System.Drawing.Size(140, 20);
            this.lblCardNumberCaption.TabIndex = 1;
            this.lblCardNumberCaption.Text = "Card number";
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblCardTitle.Location = new System.Drawing.Point(22, 0);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(180, 20);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "Connected card";
            // 
            // pnlSecurityCard
            // 
            this.pnlSecurityCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(32)))), ((int)(((byte)(56)))));
            this.pnlSecurityCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSecurityCard.Controls.Add(this.pnlPinEditor);
            this.pnlSecurityCard.Controls.Add(this.btnChangePin);
            this.pnlSecurityCard.Controls.Add(this.lblSecuritySubtitle);
            this.pnlSecurityCard.Controls.Add(this.lblSecurityTitle);
            this.pnlSecurityCard.Location = new System.Drawing.Point(28, 500);
            this.pnlSecurityCard.Name = "pnlSecurityCard";
            this.pnlSecurityCard.Size = new System.Drawing.Size(844, 122);
            this.pnlSecurityCard.TabIndex = 3;
            // 
            // pnlPinEditor
            // 
            this.pnlPinEditor.Controls.Add(this.lblPinStatus);
            this.pnlPinEditor.Controls.Add(this.btnCancelPin);
            this.pnlPinEditor.Controls.Add(this.btnSubmitPin);
            this.pnlPinEditor.Controls.Add(this.txtConfirmPin);
            this.pnlPinEditor.Controls.Add(this.lblConfirmPinCaption);
            this.pnlPinEditor.Controls.Add(this.txtNewPin);
            this.pnlPinEditor.Controls.Add(this.lblNewPinCaption);
            this.pnlPinEditor.Controls.Add(this.txtCurrentPin);
            this.pnlPinEditor.Controls.Add(this.lblCurrentPinCaption);
            this.pnlPinEditor.Location = new System.Drawing.Point(14, 22);
            this.pnlPinEditor.Name = "pnlPinEditor";
            this.pnlPinEditor.Size = new System.Drawing.Size(796, 164);
            this.pnlPinEditor.TabIndex = 3;
            this.pnlPinEditor.Visible = false;
            // 
            // lblPinStatus
            // 
            this.lblPinStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.lblPinStatus.Location = new System.Drawing.Point(0, 122);
            this.lblPinStatus.Name = "lblPinStatus";
            this.lblPinStatus.Size = new System.Drawing.Size(796, 24);
            this.lblPinStatus.TabIndex = 8;
            // 
            // btnCancelPin
            // 
            this.btnCancelPin.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnCancelPin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelPin.Location = new System.Drawing.Point(558, 72);
            this.btnCancelPin.Name = "btnCancelPin";
            this.btnCancelPin.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancelPin.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancelPin.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancelPin.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancelPin.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancelPin.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancelPin.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancelPin.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancelPin.Size = new System.Drawing.Size(112, 38);
            this.btnCancelPin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancelPin.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancelPin.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancelPin.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCancelPin.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCancelPin.StateCommon.Border.Rounding = 10F;
            this.btnCancelPin.StateCommon.Border.Width = 2;
            this.btnCancelPin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnCancelPin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelPin.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnCancelPin.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnCancelPin.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(127)))));
            this.btnCancelPin.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(127)))));
            this.btnCancelPin.TabIndex = 7;
            this.btnCancelPin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancelPin.Values.Text = "Cancel";
            this.btnCancelPin.Click += new System.EventHandler(this.btnCancelPin_Click);
            // 
            // btnSubmitPin
            // 
            this.btnSubmitPin.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnSubmitPin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmitPin.Location = new System.Drawing.Point(680, 72);
            this.btnSubmitPin.Name = "btnSubmitPin";
            this.btnSubmitPin.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSubmitPin.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSubmitPin.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSubmitPin.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSubmitPin.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSubmitPin.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSubmitPin.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSubmitPin.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSubmitPin.Size = new System.Drawing.Size(112, 38);
            this.btnSubmitPin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSubmitPin.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnSubmitPin.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSubmitPin.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.btnSubmitPin.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnSubmitPin.StateCommon.Border.Rounding = 10F;
            this.btnSubmitPin.StateCommon.Border.Width = 2;
            this.btnSubmitPin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnSubmitPin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSubmitPin.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(56)))));
            this.btnSubmitPin.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(56)))));
            this.btnSubmitPin.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(198)))), ((int)(((byte)(104)))));
            this.btnSubmitPin.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(198)))), ((int)(((byte)(104)))));
            this.btnSubmitPin.TabIndex = 6;
            this.btnSubmitPin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSubmitPin.Values.Text = "Change PIN";
            this.btnSubmitPin.Click += new System.EventHandler(this.btnSubmitPin_Click);
            // 
            // txtConfirmPin
            // 
            this.txtConfirmPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtConfirmPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPin.ForeColor = System.Drawing.Color.White;
            this.txtConfirmPin.Location = new System.Drawing.Point(536, 34);
            this.txtConfirmPin.MaxLength = 4;
            this.txtConfirmPin.Name = "txtConfirmPin";
            this.txtConfirmPin.PasswordChar = '*';
            this.txtConfirmPin.Size = new System.Drawing.Size(256, 22);
            this.txtConfirmPin.TabIndex = 5;
            this.txtConfirmPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblConfirmPinCaption
            // 
            this.lblConfirmPinCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblConfirmPinCaption.Location = new System.Drawing.Point(536, 10);
            this.lblConfirmPinCaption.Name = "lblConfirmPinCaption";
            this.lblConfirmPinCaption.Size = new System.Drawing.Size(160, 20);
            this.lblConfirmPinCaption.TabIndex = 4;
            this.lblConfirmPinCaption.Text = "Confirm new PIN";
            // 
            // txtNewPin
            // 
            this.txtNewPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtNewPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPin.ForeColor = System.Drawing.Color.White;
            this.txtNewPin.Location = new System.Drawing.Point(268, 34);
            this.txtNewPin.MaxLength = 4;
            this.txtNewPin.Name = "txtNewPin";
            this.txtNewPin.PasswordChar = '*';
            this.txtNewPin.Size = new System.Drawing.Size(240, 22);
            this.txtNewPin.TabIndex = 3;
            this.txtNewPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNewPinCaption
            // 
            this.lblNewPinCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblNewPinCaption.Location = new System.Drawing.Point(268, 10);
            this.lblNewPinCaption.Name = "lblNewPinCaption";
            this.lblNewPinCaption.Size = new System.Drawing.Size(160, 20);
            this.lblNewPinCaption.TabIndex = 2;
            this.lblNewPinCaption.Text = "New PIN";
            // 
            // txtCurrentPin
            // 
            this.txtCurrentPin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtCurrentPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentPin.ForeColor = System.Drawing.Color.White;
            this.txtCurrentPin.Location = new System.Drawing.Point(0, 34);
            this.txtCurrentPin.MaxLength = 4;
            this.txtCurrentPin.Name = "txtCurrentPin";
            this.txtCurrentPin.PasswordChar = '*';
            this.txtCurrentPin.Size = new System.Drawing.Size(240, 22);
            this.txtCurrentPin.TabIndex = 1;
            this.txtCurrentPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCurrentPinCaption
            // 
            this.lblCurrentPinCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCurrentPinCaption.Location = new System.Drawing.Point(0, 10);
            this.lblCurrentPinCaption.Name = "lblCurrentPinCaption";
            this.lblCurrentPinCaption.Size = new System.Drawing.Size(160, 20);
            this.lblCurrentPinCaption.TabIndex = 0;
            this.lblCurrentPinCaption.Text = "Current PIN";
            // 
            // btnChangePin
            // 
            this.btnChangePin.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnChangePin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePin.Location = new System.Drawing.Point(650, 22);
            this.btnChangePin.Name = "btnChangePin";
            this.btnChangePin.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnChangePin.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnChangePin.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnChangePin.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnChangePin.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnChangePin.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnChangePin.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnChangePin.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnChangePin.Size = new System.Drawing.Size(168, 42);
            this.btnChangePin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnChangePin.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnChangePin.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnChangePin.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(180)))), ((int)(((byte)(252)))));
            this.btnChangePin.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnChangePin.StateCommon.Border.Rounding = 12F;
            this.btnChangePin.StateCommon.Border.Width = 2;
            this.btnChangePin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnChangePin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnChangePin.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(52)))), ((int)(((byte)(171)))));
            this.btnChangePin.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(52)))), ((int)(((byte)(171)))));
            this.btnChangePin.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(104)))), ((int)(((byte)(239)))));
            this.btnChangePin.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(104)))), ((int)(((byte)(239)))));
            this.btnChangePin.TabIndex = 2;
            this.btnChangePin.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnChangePin.Values.Text = "Change PIN";
            this.btnChangePin.Click += new System.EventHandler(this.btnChangePin_Click);
            // 
            // lblSecuritySubtitle
            // 
            this.lblSecuritySubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSecuritySubtitle.Location = new System.Drawing.Point(22, 44);
            this.lblSecuritySubtitle.Name = "lblSecuritySubtitle";
            this.lblSecuritySubtitle.Size = new System.Drawing.Size(590, 22);
            this.lblSecuritySubtitle.TabIndex = 1;
            this.lblSecuritySubtitle.Text = "Open PIN controls only when you want to update the card PIN.";
            // 
            // lblSecurityTitle
            // 
            this.lblSecurityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblSecurityTitle.Location = new System.Drawing.Point(22, 22);
            this.lblSecurityTitle.Name = "lblSecurityTitle";
            this.lblSecurityTitle.Size = new System.Drawing.Size(180, 20);
            this.lblSecurityTitle.TabIndex = 0;
            this.lblSecurityTitle.Text = "Change PIN";
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = Krypton.Toolkit.ButtonStyle.Custom1;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(650, 692);
            this.btnClose.Name = "btnClose";
            this.btnClose.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.OverrideFocus.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.OverrideFocus.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.OverrideFocus.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.OverrideFocus.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.Size = new System.Drawing.Size(222, 44);
            this.btnClose.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClose.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnClose.StateCommon.Border.Rounding = 12F;
            this.btnClose.StateCommon.Border.Width = 2;
            this.btnClose.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.btnClose.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnClose.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(49)))), ((int)(((byte)(64)))));
            this.btnClose.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(127)))));
            this.btnClose.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(127)))));
            this.btnClose.TabIndex = 4;
            this.btnClose.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnClose.Values.Text = "Back";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UserSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(14)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(900, 748);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlSecurityCard);
            this.Controls.Add(this.pnlCardCard);
            this.Controls.Add(this.pnlProfileCard);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Settings";
            this.Load += new System.EventHandler(this.UserSettingsForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlProfileCard.ResumeLayout(false);
            this.pnlCardCard.ResumeLayout(false);
            this.pnlSecurityCard.ResumeLayout(false);
            this.pnlPinEditor.ResumeLayout(false);
            this.pnlPinEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlProfileCard;
        private System.Windows.Forms.Label lblAccountStatusValue;
        private System.Windows.Forms.Label lblAccountStatusCaption;
        private System.Windows.Forms.Label lblBalanceValue;
        private System.Windows.Forms.Label lblBalanceCaption;
        private System.Windows.Forms.Label lblAccountValue;
        private System.Windows.Forms.Label lblAccountCaption;
        private System.Windows.Forms.Label lblPhoneValue;
        private System.Windows.Forms.Label lblPhoneCaption;
        private System.Windows.Forms.Label lblUsernameValue;
        private System.Windows.Forms.Label lblUsernameCaption;
        private System.Windows.Forms.Label lblFullNameValue;
        private System.Windows.Forms.Label lblFullNameCaption;
        private System.Windows.Forms.Label lblProfileTitle;
        private System.Windows.Forms.Panel pnlCardCard;
        private System.Windows.Forms.Label lblSessionTimeoutValue;
        private System.Windows.Forms.Label lblSessionTimeoutCaption;
        private System.Windows.Forms.Label lblCardStatusValue;
        private System.Windows.Forms.Label lblCardStatusCaption;
        private System.Windows.Forms.Label lblCardExpiryValue;
        private System.Windows.Forms.Label lblCardExpiryCaption;
        private System.Windows.Forms.Label lblCardTypeValue;
        private System.Windows.Forms.Label lblCardTypeCaption;
        private System.Windows.Forms.Label lblCardNumberValue;
        private System.Windows.Forms.Label lblCardNumberCaption;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Panel pnlSecurityCard;
        private System.Windows.Forms.Panel pnlPinEditor;
        private System.Windows.Forms.Label lblPinStatus;
        private Krypton.Toolkit.KryptonButton btnCancelPin;
        private Krypton.Toolkit.KryptonButton btnSubmitPin;
        private System.Windows.Forms.TextBox txtConfirmPin;
        private System.Windows.Forms.Label lblConfirmPinCaption;
        private System.Windows.Forms.TextBox txtNewPin;
        private System.Windows.Forms.Label lblNewPinCaption;
        private System.Windows.Forms.TextBox txtCurrentPin;
        private System.Windows.Forms.Label lblCurrentPinCaption;
        private Krypton.Toolkit.KryptonButton btnChangePin;
        private System.Windows.Forms.Label lblSecuritySubtitle;
        private System.Windows.Forms.Label lblSecurityTitle;
        private Krypton.Toolkit.KryptonButton btnClose;
    }
}
