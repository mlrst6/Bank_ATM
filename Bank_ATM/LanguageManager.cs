using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Bank_ATM
{
    public static class LanguageManager
    {
        public static string CurrentLang = "uz";
        private static Dictionary<string, string> words = new Dictionary<string, string>();
        private static Dictionary<string, Dictionary<string, string>> allTranslations = new Dictionary<string, Dictionary<string, string>>();

        public static void Load()
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "languages.json");
                
                // Fallback for development if bin/Debug doesn't have it yet
                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Bank_ATM", "Resources", "languages.json");
                }

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    allTranslations = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);

                    if (allTranslations.ContainsKey(CurrentLang))
                    {
                        words = allTranslations[CurrentLang];
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Language loading failed: {ex.Message}");
            }
        }

        public static void Apply(Control parent)
        {
            if (words == null || words.Count == 0) Load();

            foreach (Control c in parent.Controls)
            {
                if (!string.IsNullOrEmpty(c.Name) && words.ContainsKey(c.Name))
                {
                    c.Text = words[c.Name];
                }
                
                // Handle specific controls like TabControl or panels
                if (c.HasChildren)
                {
                    Apply(c);
                }
            }
        }

        public static string GetString(string key)
        {
            if (words.ContainsKey(key)) return words[key];
            return key;
        }
    }
}
