using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CsSeleniumFrame.src.util
{
    public class Stopwatch
    {
        private readonly double endTime;

        private double startTime;
        private double chronoEnd;
        private double lastRoundTime;

        public Stopwatch()
        {

        }

        public Stopwatch(long timeoutMilliseconds)
        {
            this.endTime = (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds
                + timeoutMilliseconds;
        }

        public bool IsTimoutReached()
        {
            return (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds > endTime;
        }

        public void Start()
        {
            this.startTime = getMillis();
            this.lastRoundTime = this.startTime;
        }

        public double GetRound()
        {
            return getMillis() - this.lastRoundTime;
        }

        public double Stop()
        {
            return (this.chronoEnd = getMillis());
        }

        public double GetElapsedMs()
        {
            return endTime - startTime;
        }

        private double getMillis()
        {
            return (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
        }

        public void sleep(int milliseconds)
        {
            try
            {
                Thread.Sleep(milliseconds);
            } catch(ThreadInterruptedException e)
            {
                Thread.CurrentThread.Interrupt();
                throw new ThreadInterruptedException(e.ToString());
            }
        }
    }
}
