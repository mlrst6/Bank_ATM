using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bank_ATM
{
    public class DatabaseSetupHelpForm : Form
    {
        private readonly string _details;

        public DatabaseSetupHelpForm(string connectionTarget, Exception exception)
        {
            Text = "Database Setup Required";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = true;
            BackColor = Color.White;
            ClientSize = new Size(860, 620);
            Font = new Font("Segoe UI", 10F);

            _details = BuildDetails(connectionTarget, exception);
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            var lblTitle = new Label
            {
                Text = "Database connection failed",
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(153, 27, 27),
                Location = new Point(24, 20)
            };

            var lblSubtitle = new Label
            {
                Text = "The application could not start because SQL Server is not reachable with the current configuration.",
                AutoSize = false,
                Size = new Size(800, 48),
                Location = new Point(24, 62),
                ForeColor = Color.FromArgb(55, 65, 81)
            };

            var txtDetails = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                WordWrap = true,
                Font = new Font("Consolas", 10F),
                BackColor = Color.FromArgb(248, 250, 252),
                Location = new Point(24, 120),
                Size = new Size(812, 420),
                Text = _details
            };

            var btnCopy = new Button
            {
                Text = "Copy Details",
                Size = new Size(140, 38),
                Location = new Point(542, 560),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCopy.FlatAppearance.BorderSize = 0;
            btnCopy.Click += (sender, e) =>
            {
                Clipboard.SetText(_details);
                MessageBox.Show("Database setup details copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            var btnClose = new Button
            {
                Text = "Close",
                Size = new Size(120, 38),
                Location = new Point(716, 560),
                BackColor = Color.FromArgb(75, 85, 99),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            btnClose.FlatAppearance.BorderSize = 0;

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(txtDetails);
            Controls.Add(btnCopy);
            Controls.Add(btnClose);

            AcceptButton = btnClose;
        }

        private static string BuildDetails(string connectionTarget, Exception exception)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Configured target");
            builder.AppendLine("-----------------");
            builder.AppendLine(connectionTarget);
            builder.AppendLine();
            builder.AppendLine("Error");
            builder.AppendLine("-----");
            builder.AppendLine(exception.Message);
            builder.AppendLine();
            builder.AppendLine("What to check");
            builder.AppendLine("-------------");
            builder.AppendLine("1. Make sure the SQL Server instance in App.config or BANK_ATM_CONNECTION_STRING actually exists.");
            builder.AppendLine("2. If you want LocalDB, verify that '(localdb)\\MSSQLLocalDB' can start on this machine.");
            builder.AppendLine("3. If you want full SQL Server, update the connection string to the correct instance name.");
            builder.AppendLine("4. If you are using SQL Authentication, verify username and password.");
            builder.AppendLine("5. If the server works but the ATM database does not exist yet, enable bootstrap or create the database manually.");
            builder.AppendLine();
            builder.AppendLine("Examples");
            builder.AppendLine("--------");
            builder.AppendLine("LocalDB:");
            builder.AppendLine("Server=(localdb)\\MSSQLLocalDB;Database=ATM;Integrated Security=True;TrustServerCertificate=True;");
            builder.AppendLine();
            builder.AppendLine("Default SQL Server:");
            builder.AppendLine("Server=.;Database=ATM;Integrated Security=True;TrustServerCertificate=True;");
            builder.AppendLine();
            builder.AppendLine("Named SQL Server instance:");
            builder.AppendLine("Server=.\\MSSQLSERVER01;Database=ATM;Integrated Security=True;TrustServerCertificate=True;");

            return builder.ToString();
        }
    }
}
