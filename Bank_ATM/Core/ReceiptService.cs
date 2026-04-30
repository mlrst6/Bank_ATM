using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Bank_ATM.Models;
using Bank_ATM.Services;

namespace Bank_ATM.Core
{
    public static class ReceiptService
    {
        public static string GenerateReceipt(TransactionDto transaction, string fullName, decimal balance)
        {
            try
            {
                string receiptsDir = GetReceiptsDirectory();
                string fileName = $"Receipt_{SanitizeFilePart(transaction == null ? null : transaction.Type)}_{DateTime.Now:yyyyMMddHHmmss}.html";
                string filePath = Path.Combine(receiptsDir, fileName);

                string html = BuildHtmlReceipt(
                    "BANK ATM RECEIPT",
                    new[]
                    {
                        Tuple.Create("Date", transaction == null ? string.Empty : transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss")),
                        Tuple.Create("Customer", fullName ?? string.Empty),
                        Tuple.Create("Transaction Type", transaction == null ? string.Empty : transaction.Type ?? string.Empty),
                        Tuple.Create("Amount", transaction == null ? string.Empty : $"{transaction.Amount:N2} UZS"),
                        Tuple.Create("Description", transaction == null ? string.Empty : transaction.Description ?? string.Empty),
                        Tuple.Create("Remaining Balance", $"{balance:N2} UZS")
                    },
                    "Thank you for choosing our services.");

                File.WriteAllText(filePath, html, Encoding.UTF8);
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
                string receiptsDir = GetReceiptsDirectory();
                string safeReceiptType = SanitizeFilePart(receiptType);
                string fileName = $"Receipt_{safeReceiptType}_{DateTime.Now:yyyyMMddHHmmss}.html";
                string filePath = Path.Combine(receiptsDir, fileName);

                string html = BuildHtmlReceipt(
                    string.IsNullOrWhiteSpace(receiptType) ? "Guest transaction" : receiptType,
                    new[]
                    {
                        Tuple.Create("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                        Tuple.Create("Customer", "Guest")
                    },
                    "Thank you for using BANK ATM.",
                    lines);

                File.WriteAllText(filePath, html, Encoding.UTF8);
                AuditLogger.LogInfo($"Guest receipt generated: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                AuditLogger.LogError("Failed to generate guest receipt", ex);
                return null;
            }
        }

        public static string GenerateGuestExchangeReceipt(GuestExchangeResult exchange)
        {
            if (exchange == null)
            {
                return null;
            }

            var lines = new System.Collections.Generic.List<string>
            {
                LanguageManager.Format("ExchangeReceiptFrom", exchange.SourceAmount, exchange.FromCurrencyCode),
                LanguageManager.Format("ExchangeReceiptTo", exchange.TargetAmount, exchange.ToCurrencyCode),
                LanguageManager.Format("ExchangeReceiptRate", exchange.Rate, exchange.ToCurrencyCode, exchange.FromCurrencyCode),
                $"Fee: {exchange.FeePercent:N4}% ({exchange.FeeAmountUzs:N2} UZS)",
                $"Rate type: {exchange.RateKind}"
            };

            if (exchange.IsApproximateAmount)
            {
                lines.Add(LanguageManager.Format(
                    "ExchangeReceiptRequestedAmount",
                    exchange.RequestedTargetAmount,
                    exchange.ToCurrencyCode,
                    exchange.UnavailableAmount));
            }

            lines.Add(LanguageManager.GetString("CashAcceptedBreakdown") + ": " + FormatNotes(exchange.InsertedNotes));
            lines.Add(LanguageManager.GetString("CashDispensedBreakdown") + ": " + FormatNotes(exchange.DispensedNotes));

            return GenerateGuestReceipt(LanguageManager.GetString("Exchange"), lines.ToArray());
        }

        public static string GenerateGuestServicePaymentReceipt(
            ServiceDto service,
            ServiceAccountDto serviceAccount,
            string paymentReference,
            decimal amount,
            CashNoteDto[] notes)
        {
            var lines = new System.Collections.Generic.List<string>
            {
                LanguageManager.Format("ServiceReceiptService", service == null ? string.Empty : service.ServiceName),
                LanguageManager.Format("ServiceReceiptReference", paymentReference ?? string.Empty),
                LanguageManager.Format(
                    "ServiceReceiptCustomer",
                    serviceAccount == null
                        ? string.Empty
                        : (string.IsNullOrWhiteSpace(serviceAccount.CustomerName)
                            ? serviceAccount.ReferenceNumber
                            : serviceAccount.CustomerName)),
                LanguageManager.Format("ServiceReceiptAmount", amount, "UZS")
            };

            if (notes != null && notes.Length > 0)
            {
                lines.Add(LanguageManager.GetString("CashAcceptedBreakdown") + ": " + FormatNotes(notes));
            }

            return GenerateGuestReceipt(LanguageManager.GetString("GuestServicePayment"), lines.ToArray());
        }

        private static string BuildHtmlReceipt(
            string title,
            Tuple<string, string>[] fields,
            string footer,
            string[] extraLines = null)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<!DOCTYPE html>");
            builder.AppendLine("<html>");
            builder.AppendLine("<head>");
            builder.AppendLine("<meta charset=\"utf-8\" />");
            builder.AppendLine("<title>Bank ATM Receipt</title>");
            builder.AppendLine("<style>");
            builder.AppendLine("body{font-family:Segoe UI,Arial,sans-serif;background:#f3f6fb;margin:0;padding:24px;color:#0f172a;}");
            builder.AppendLine(".receipt{max-width:760px;margin:0 auto;background:#fff;border:1px solid #dbe4f0;border-radius:14px;padding:28px 32px;box-shadow:0 10px 30px rgba(15,23,42,.08);}");
            builder.AppendLine("h1{margin:0 0 6px;font-size:28px;}");
            builder.AppendLine(".sub{margin:0 0 20px;color:#475569;font-size:14px;}");
            builder.AppendLine(".rule{height:1px;background:#e2e8f0;margin:20px 0;}");
            builder.AppendLine(".grid{display:grid;grid-template-columns:220px 1fr;gap:10px 16px;}");
            builder.AppendLine(".label{color:#64748b;font-weight:600;}");
            builder.AppendLine(".value{color:#0f172a;word-break:break-word;}");
            builder.AppendLine(".lines{margin-top:18px;padding:16px 18px;background:#f8fafc;border-radius:10px;border:1px solid #e2e8f0;}");
            builder.AppendLine(".line{margin:0 0 8px;}");
            builder.AppendLine(".line:last-child{margin-bottom:0;}");
            builder.AppendLine(".footer{margin-top:20px;text-align:center;color:#475569;font-size:13px;}");
            builder.AppendLine("@media print{body{background:#fff;padding:0}.receipt{box-shadow:none;border:none;max-width:none;padding:0}}");
            builder.AppendLine("</style>");
            builder.AppendLine("</head>");
            builder.AppendLine("<body>");
            builder.AppendLine("<div class=\"receipt\">");
            builder.AppendLine($"<h1>{Encode(title)}</h1>");
            builder.AppendLine("<p class=\"sub\">BANK ATM receipt file</p>");
            builder.AppendLine("<div class=\"rule\"></div>");
            builder.AppendLine("<div class=\"grid\">");

            foreach (var field in fields ?? new Tuple<string, string>[0])
            {
                if (field == null || string.IsNullOrWhiteSpace(field.Item2))
                {
                    continue;
                }

                builder.AppendLine($"<div class=\"label\">{Encode(field.Item1)}</div><div class=\"value\">{Encode(field.Item2)}</div>");
            }

            builder.AppendLine("</div>");

            var filteredLines = (extraLines ?? new string[0])
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToArray();
            if (filteredLines.Length > 0)
            {
                builder.AppendLine("<div class=\"lines\">");
                foreach (string line in filteredLines)
                {
                    builder.AppendLine($"<p class=\"line\">{Encode(line)}</p>");
                }

                builder.AppendLine("</div>");
            }

            builder.AppendLine($"<div class=\"footer\">{Encode(footer)}</div>");
            builder.AppendLine("</div>");
            builder.AppendLine("</body>");
            builder.AppendLine("</html>");
            return builder.ToString();
        }

        private static string FormatNotes(CashNoteDto[] notes)
        {
            return string.Join(", ", (notes ?? new CashNoteDto[0])
                .Where(note => note != null)
                .Select(note => $"{note.DenominationValue:N2} {note.CurrencyCode} x {note.NoteCount}"));
        }

        private static string GetReceiptsDirectory()
        {
            string receiptsDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Bank_ATM",
                "Receipts");

            if (!Directory.Exists(receiptsDir))
            {
                Directory.CreateDirectory(receiptsDir);
            }

            return receiptsDir;
        }

        private static string SanitizeFilePart(string value)
        {
            string safe = string.IsNullOrWhiteSpace(value) ? "Receipt" : value.Trim().Replace(" ", "_");
            foreach (char invalid in Path.GetInvalidFileNameChars())
            {
                safe = safe.Replace(invalid, '_');
            }

            return safe;
        }

        private static string Encode(string value)
        {
            return WebUtility.HtmlEncode(value ?? string.Empty);
        }
    }
}
