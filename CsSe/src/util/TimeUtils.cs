using System;

namespace CsSeleniumFrame.src.Util
{
    public class TimeUtils
    {
        public static double NowMillis => (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
    }
}
