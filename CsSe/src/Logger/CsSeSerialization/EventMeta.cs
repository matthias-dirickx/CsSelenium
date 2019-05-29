using System;
using System.Collections.Generic;
using System.Text;

namespace CsSeleniumFrame.src.Logger.CsSeSerialization
{
    public class EventMeta
    {
        public string machineName { get; set; }
        public int testThreadId { get; set; }
        public string testModule { get; set; }
        public string testClass { get; set; }
        public string testMethod { get; set; }
    }
}
