using System;

namespace CsSeleniumFrame.src.util
{
    public class TimeUtils
    {
        public static double NowMillis => (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
    }
}
