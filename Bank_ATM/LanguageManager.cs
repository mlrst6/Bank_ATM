using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank_ATM
{
    public static class LanguageManager
    {
        public static string CurrentLang = "uz";
        static string connStr = @"Server=.;Database=ATM;Trusted_Connection=True;";
        static Dictionary<string, string> words = new Dictionary<string, string>();

        public static void Load()
        {
            words.Clear();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string q = $"Select [Key], {CurrentLang} From LangText";
                SqlCommand cmd = new SqlCommand(q, conn);
                var r = cmd.ExecuteReader();

                while (r.Read())
                    words[r["Key"].ToString()] = r[CurrentLang].ToString();
            }
        }
        public static void Apply(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (words.ContainsKey(c.Name))
                    c.Text = words[c.Name];
                Apply(c);
            }
        }
    }
}