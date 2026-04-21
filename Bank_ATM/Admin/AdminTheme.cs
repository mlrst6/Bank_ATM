using System.Drawing;
using System.Windows.Forms;

namespace Bank_ATM.Admin
{
    internal static class AdminTheme
    {
        private static readonly Color Background = Color.FromArgb(17, 24, 39);
        private static readonly Color Surface = Color.FromArgb(31, 41, 55);
        private static readonly Color SurfaceAlt = Color.FromArgb(55, 65, 81);
        private static readonly Color Accent = Color.FromArgb(14, 165, 233);
        private static readonly Color AccentAlt = Color.FromArgb(16, 185, 129);
        private static readonly Color Danger = Color.FromArgb(239, 68, 68);
        private static readonly Color TextPrimary = Color.FromArgb(243, 244, 246);
        private static readonly Color TextMuted = Color.FromArgb(156, 163, 175);

        public static void ApplyForm(Form form)
        {
            form.BackColor = Background;
            form.ForeColor = TextPrimary;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        public static void StyleTitle(Label label)
        {
            label.ForeColor = TextPrimary;
            label.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        }

        public static void StyleLabel(Label label, bool muted = false)
        {
            label.ForeColor = muted ? TextMuted : TextPrimary;
            if (!muted)
            {
                label.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }
        }

        public static void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = Surface;
            textBox.ForeColor = TextPrimary;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
        }

        public static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = Surface;
            comboBox.ForeColor = TextPrimary;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
        }

        public static void StyleCheckBox(CheckBox checkBox)
        {
            checkBox.ForeColor = TextPrimary;
            checkBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        public static void StylePrimaryButton(Button button)
        {
            StyleButton(button, Accent, TextPrimary);
        }

        public static void StyleSecondaryButton(Button button)
        {
            StyleButton(button, SurfaceAlt, TextPrimary);
        }

        public static void StyleSuccessButton(Button button)
        {
            StyleButton(button, AccentAlt, TextPrimary);
        }

        public static void StyleDangerButton(Button button)
        {
            StyleButton(button, Danger, TextPrimary);
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = SurfaceAlt;
            grid.RowHeadersVisible = false;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = TextPrimary;
            grid.DefaultCellStyle.SelectionBackColor = Accent;
            grid.DefaultCellStyle.SelectionForeColor = TextPrimary;
            grid.DefaultCellStyle.Padding = new Padding(6);
            grid.ColumnHeadersDefaultCellStyle.BackColor = SurfaceAlt;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = TextPrimary;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 42;
        }

        public static void StylePanel(Panel panel)
        {
            panel.BackColor = Surface;
        }

        public static void StyleStatValue(Label label)
        {
            label.ForeColor = TextPrimary;
            label.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
        }

        public static void StyleStatCaption(Label label)
        {
            label.ForeColor = TextMuted;
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        }

        private static void StyleButton(Button button, Color backColor, Color foreColor)
        {
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
    }
}
