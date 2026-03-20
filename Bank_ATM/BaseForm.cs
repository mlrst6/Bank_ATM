using System;
using System.Windows.Forms;

namespace Bank_ATM
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            this.StartPosition = FormStartPosition.Manual;
        }

        protected void NavigateTo(Form nextForm)
        {
            nextForm.Location = this.Location;
            nextForm.Show();
            this.Hide();
        }

        protected void NavigateBack(Form previousForm)
        {
            previousForm.Location = this.Location;
            previousForm.Show();
            this.Close();
        }
    }
}
