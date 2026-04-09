using System;
using System.Linq;
using System.Windows.Forms;

namespace Bank_ATM.Core
{
    public static class FormNavigator
    {
        public static void ShowNext(Form current, Form next)
        {
            next.StartPosition = FormStartPosition.Manual;
            next.Location = current.Location;
            next.Tag = current;
            next.Show();
            current.Hide();
        }

        public static void ReplaceCurrent(Form current, Form next, object backReference = null)
        {
            next.StartPosition = FormStartPosition.Manual;
            next.Location = current.Location;
            next.Tag = backReference;
            next.Show();
            current.Close();
        }

        public static void GoBack(Form current, Func<Form> fallbackFactory = null)
        {
            var previous = current.Tag as Form;
            if (previous != null && !previous.IsDisposed)
            {
                previous.StartPosition = FormStartPosition.Manual;
                previous.Location = current.Location;
                previous.Show();
            }
            else if (fallbackFactory != null)
            {
                var fallback = fallbackFactory();
                fallback.StartPosition = FormStartPosition.Manual;
                fallback.Location = current.Location;
                fallback.Show();
            }

            current.Close();
        }

        public static void ShowExistingOrNew<T>(Form current, Func<T> factory = null) where T : Form, new()
        {
            var existing = Application.OpenForms.OfType<T>().FirstOrDefault(f => !ReferenceEquals(f, current) && !f.IsDisposed);
            Form target = existing ?? (factory != null ? factory() : new T());
            target.StartPosition = FormStartPosition.Manual;
            target.Location = current.Location;
            target.Show();
            current.Close();
        }

        public static void CloseHiddenForm(Form form)
        {
            if (form != null && !form.IsDisposed && !form.Visible)
            {
                form.Close();
            }
        }
    }
}
