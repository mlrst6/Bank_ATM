using System;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Bank_ATM.Models;

namespace Bank_ATM.Core
{
    public static class ReceiptService
    {
        public static string GenerateReceipt(TransactionDto transaction, string fullName, decimal balance)
        {
            try
            {
                string receiptsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Receipts");
                if (!Directory.Exists(receiptsDir)) Directory.CreateDirectory(receiptsDir);

                string fileName = $"Receipt_{transaction.Type}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = Path.Combine(receiptsDir, fileName);

                using (PdfWriter writer = new PdfWriter(filePath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        document.Add(new Paragraph("BANK ATM RECEIPT")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(20));

                        document.Add(new Paragraph("--------------------------------------------------")
                            .SetTextAlignment(TextAlignment.CENTER));

                        document.Add(new Paragraph($"Date: {transaction.TransactionDate:yyyy-MM-dd HH:mm:ss}"));
                        document.Add(new Paragraph($"Customer: {fullName}"));
                        document.Add(new Paragraph($"Transaction Type: {transaction.Type}"));
                        document.Add(new Paragraph($"Amount: {transaction.Amount:N2} UZS"));
                        
                        if (!string.IsNullOrEmpty(transaction.Description))
                            document.Add(new Paragraph($"Description: {transaction.Description}"));

                        document.Add(new Paragraph("--------------------------------------------------")
                            .SetTextAlignment(TextAlignment.CENTER));

                        document.Add(new Paragraph($"Remaining Balance: {balance:N2} UZS")
                            .SetFontSize(14));

                        document.Add(new Paragraph("\nThank you for choosing our services!")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(10));

                        document.Close();
                    }
                }

                AuditLogger.LogInfo($"Receipt generated: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Failed to generate receipt", ex);
                return null;
            }
        }

        public static string GenerateGuestReceipt(string receiptType, string[] lines)
        {
            try
            {
                string receiptsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Receipts");
                if (!Directory.Exists(receiptsDir)) Directory.CreateDirectory(receiptsDir);

                string safeReceiptType = string.IsNullOrWhiteSpace(receiptType)
                    ? "Guest"
                    : receiptType.Replace(" ", "_");
                string fileName = $"Receipt_{safeReceiptType}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string filePath = Path.Combine(receiptsDir, fileName);

                using (PdfWriter writer = new PdfWriter(filePath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);
                        PdfFont monoFont = PdfFontFactory.CreateFont(StandardFonts.COURIER);
                        PdfFont headerFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                        string receiptNumber = DateTime.Now.ToString("yyyyMMddHHmmss");

                        document.SetMargins(28, 32, 28, 32);
                        document.Add(new Paragraph("BANK ATM")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(18)
                            .SetFont(headerFont));
                        document.Add(new Paragraph(receiptType ?? "Guest transaction")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(12)
                            .SetFontColor(ColorConstants.DARK_GRAY));
                        document.Add(new Paragraph("RECEIPT NO: " + receiptNumber)
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFont(monoFont)
                            .SetFontSize(10));
                        document.Add(new Paragraph(new string('-', 42))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFont(monoFont)
                            .SetFontSize(10));

                        var detailsTable = new Table(UnitValue.CreatePercentArray(new float[] { 1f, 2f }))
                            .UseAllAvailableWidth();
                        detailsTable.SetBorder(Border.NO_BORDER);
                        detailsTable.AddCell(CreateReceiptLabelCell("Date"));
                        detailsTable.AddCell(CreateReceiptValueCell(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), monoFont));
                        detailsTable.AddCell(CreateReceiptLabelCell("Customer"));
                        detailsTable.AddCell(CreateReceiptValueCell("Guest", monoFont));

                        document.Add(detailsTable);
                        document.Add(new Paragraph(" "));

                        foreach (string line in lines ?? new string[0])
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                document.Add(new Paragraph(line)
                                    .SetFont(monoFont)
                                    .SetFontSize(10)
                                    .SetMarginBottom(6));
                            }
                        }

                        document.Add(new Paragraph(new string('-', 42))
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFont(monoFont)
                            .SetFontSize(10));
                        document.Add(new Paragraph("Thank you for using BANK ATM")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(10));
                        document.Close();
                    }
                }

                AuditLogger.LogInfo($"Guest receipt generated: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Failed to generate guest receipt", ex);
                return null;
            }
        }

        private static Cell CreateReceiptLabelCell(string value)
        {
            return new Cell()
                .Add(new Paragraph(value)
                    .SetFontSize(10)
                    .SetFontColor(ColorConstants.GRAY))
                .SetBorder(Border.NO_BORDER)
                .SetPadding(0)
                .SetPaddingBottom(4);
        }

        private static Cell CreateReceiptValueCell(string value, PdfFont font)
        {
            return new Cell()
                .Add(new Paragraph(value ?? string.Empty)
                    .SetFont(font)
                    .SetFontSize(10))
                .SetBorder(Border.NO_BORDER)
                .SetPadding(0)
                .SetPaddingBottom(4);
        }
    }
}
