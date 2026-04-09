using System;
using System.Windows.Forms;
using Bank_ATM.Core;

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
            FormNavigator.ShowNext(this, nextForm);
        }

        protected void NavigateBack()
        {
            FormNavigator.GoBack(this);
        }
    }
}
