using System;
using System.Media;
using System.IO;

namespace Bank_ATM.Core
{
    public static class AudioManager
    {
        // Note: You will need to place these .wav files in your bin/Debug/Sounds folder
        // or update the paths to your existing sound assets.
        private static string SoundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");

        public static void PlayBeep()
        {
            PlaySound("beep.wav");
        }

        public static void PlayCash()
        {
            PlaySound("cash.wav");
        }

        public static void PlayError()
        {
            PlaySound("error.wav");
        }

        private static void PlaySound(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(SoundPath, fileName);
                if (File.Exists(fullPath))
                {
                    using (SoundPlayer player = new SoundPlayer(fullPath))
                    {
                        player.Play();
                    }
                }
            }
            catch { /* Silently fail if audio device is missing */ }
        }
    }
}
