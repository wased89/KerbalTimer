using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KerbalTimer
{
    public class TimerFunctions
    {
        public static void StartTimer(int timer)
        {
            Timer timer1 = HeadMaster.timers[timer];
            timer1.maintimer.Start();
            timer1.lapTimer.Start();
            timer1.hasStopped = false;
        }
        public static void StopTimer(int timer)
        {
            Timer timer1 = HeadMaster.timers[timer];
            timer1.finaltime = timer1.maintimer.Elapsed.Hours + ":" + timer1.maintimer.Elapsed.Minutes + ":"
                    + timer1.maintimer.Elapsed.Seconds + "." + timer1.maintimer.Elapsed.Milliseconds;
            if (!timer1.hasStopped) //to prevent the addition of laps when the timer has already been stopped
            {
                timer1.laptimes.Add(timer1.lapTimer.Elapsed.Hours + ":" + timer1.lapTimer.Elapsed.Minutes + ":"
                + timer1.lapTimer.Elapsed.Seconds + "." + timer1.lapTimer.Elapsed.Milliseconds + "*");
            }
            timer1.maintimer.Stop();
            timer1.lapTimer.Stop();
            timer1.hasStopped = true;
        }
        public static void ResetTimer(int timer)
        {
            Timer timer1 = HeadMaster.timers[timer];
            timer1.maintimer.Reset();
            timer1.lapTimer.Reset();
            timer1.laptimes = new List<String>();
        }
        public static void LapTimer(int timer)
        {
            Timer timer1 = HeadMaster.timers[timer];
            timer1.laptimes.Add(timer1.lapTimer.Elapsed.Hours + ":" + timer1.lapTimer.Elapsed.Minutes + ":"
                    + timer1.lapTimer.Elapsed.Seconds + "." + timer1.lapTimer.Elapsed.Milliseconds);
            timer1.lapTimer.Reset();
            timer1.lapTimer.Start();
        }
        public static int AddTimer()
        {
            HeadMaster.timers.Add(new Timer());
            return HeadMaster.timers.Count;
        }
        public static bool DeleteTimer(int timer)
        {
            if(HeadMaster.timers.Count > timer)
            {
                return false;
            }
            else
            {
                HeadMaster.timers.RemoveAt(timer);
                return true;
            }
        }
        public static Timer GetTimer(int timer)
        {
            return HeadMaster.timers[timer];
        }
    }
}
