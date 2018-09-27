using DAN.CollectionQueue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DAN.Timer
{
    /// <summary>
    /// 
    /// </summary>
   public class TimerJob:IJob
    {
        public System.Timers.Timer aTimer;
        public TimerJob() {
            aTimer = new System.Timers.Timer(DANConfig.Interval);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //DANQueue<IEntity>.TryTakeAll();
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }
}
