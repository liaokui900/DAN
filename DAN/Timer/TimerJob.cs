using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DAN.Timer
{
    /// <summary>
    /// 
    /// </summary>
    public class TimerJob : IJob
    {
        private static System.Timers.Timer aTimer;
        private static Action _action;

        static TimerJob()
        {
            aTimer = new System.Timers.Timer(DANConfig.Interval);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public static void SetAction(Action action)
        {
            _action = action;
        }
        public static void Start()
        {
            aTimer.Start();
        }
        public static void Stop()
        {
            aTimer.Stop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (_action != null)
            {
                _action();
            }
            //DANQueue<IEntity>.TryTakeAll();
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }
}
