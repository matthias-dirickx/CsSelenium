using System;
using System.Collections.Generic;
using System.Text;

namespace CsSeleniumFrame.src.Logger.CsSeSerialization
{
    public class EventSummary
    {
        public string eventType { get; set; }
        public string source { get; set; }
        public string subject { get; set; }
        public string status { get; set; }
        public string error { get; set; }
        public double duration { get; set; }
        public TimeDuration dateAndTime { get; set; }
    }
}
