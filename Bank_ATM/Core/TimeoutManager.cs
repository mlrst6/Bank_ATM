using System;
using System.Windows.Forms;

namespace Bank_ATM.Core
{
    /// <summary>
    /// Monitors user activity. If the user does not click or type anything for a set 
    /// amount of time, they are automatically logged out for security.
    /// </summary>
    public static class TimeoutManager
    {
        private static Timer _timeoutTimer;
        private const int TIMEOUT_MILLISECONDS = 30000; // 30 seconds for ATM realism

        // Event that forms can subscribe to so they know when to close themselves
        public static event Action OnTimeout;

        static TimeoutManager()
        {
            _timeoutTimer = new Timer();
            _timeoutTimer.Interval = TIMEOUT_MILLISECONDS;
            _timeoutTimer.Tick += (s, e) => 
            {
                Stop();
                OnTimeout?.Invoke();
            };
        }

        public static void Start()
        {
            _timeoutTimer.Stop();
            _timeoutTimer.Start();
        }

        public static void Stop()
        {
            _timeoutTimer.Stop();
        }

        /// <summary>
        /// Call this method whenever the user clicks a button or types something.
        /// </summary>
        public static void ResetTimer()
        {
            if (_timeoutTimer.Enabled)
            {
                _timeoutTimer.Stop();
                _timeoutTimer.Start();
            }
        }
    }
}
