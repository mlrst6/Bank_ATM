using System;
using System.Windows.Forms;

namespace Bank_ATM.UI
{
    internal static class ReceiptWorkflow
    {
        public static string OfferPdfReceipt(IWin32Window owner, Func<string> receiptGenerator, string subtitle = null)
        {
            using (var dialog = new ReceiptChoiceDialog(
                string.IsNullOrWhiteSpace(subtitle)
                    ? LanguageManager.GetString("ReceiptChoiceSubtitle")
                    : subtitle))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK || dialog.Choice != ReceiptChoice.SavePdf)
                {
                    return null;
                }
            }

            string path = receiptGenerator == null ? null : receiptGenerator();
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            using (var savedDialog = new ReceiptSavedDialog(path))
            {
                savedDialog.ShowDialog(owner);
            }

            return LanguageManager.Format("ReceiptSavedTo", path);
        }
    }
}
