using System;

using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CsSeleniumFrame.src.Logger
{
    public class CsSeEventEntry
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly double startMs;
        private double endMs;
        private EventStatus eventStatus;

        public string Source { get; set; }
        public string Subject { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }
        public Exception Error { get; set; }
        public ICapabilities Capas { get; set; }

        public CsSeEventEntry(string source, string subject)
        {
            logger.Debug("CsSeEventEntry objected - Instantiation");
            logger.Debug("Set Source");
            Source = source;
            logger.Debug("Set subject");
            Subject = subject;

            //Default
            eventStatus = EventStatus.Unknown;
            Expected = "Not set";
            Actual = "Not set";
            Error = new Exception("No exception added");

            //Calculated on start
            startMs = GetNowMs();
            logger.Debug("Default values set.");
        }

        private double GetNowMs()
        {
            return (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
        }

        public void SetStatus(EventStatus eventStatus)
        {
            this.eventStatus = eventStatus;
            endMs = GetNowMs();
        }

        public EventStatus GetStatus()
        {
            return this.eventStatus;
        }

        public double GetDurationMs()
        {
            return endMs - startMs;
        }

        private string GetStringDateFromMs(double ms)
        {
            return (new DateTime(1, 1, 1)).AddMilliseconds(ms).ToString("o");
        }

        public class CsSeSerializableItem
        {
            public string source { get; set; }
            public string status { get; set; }
            public string subject { get; set; }
            public string expected { get; set; }
            public string actual { get; set; }
            public string error { get; set; }
            public double duration { get; set; }
            public Duration dateAndTime { get; set; }
            public ICapabilities capabilities { get; set; }
        }

        public class Duration
        {
            public string start { get; set; }
            public string end { get; set; }
        }

        public override string ToString()
        {
            return JsonConvert
                .SerializeObject(
                    GetSerializableCsSeEventEntry(),
                    Formatting.Indented);
        }

        public CsSeSerializableItem GetSerializableCsSeEventEntry()
        {
            CsSeSerializableItem entry = new CsSeSerializableItem()
            {
                source = Source,
                status = eventStatus.ToString(),
                subject = Subject,
                expected = Expected,
                actual = Actual,
                error = Error.ToString(),
                duration = GetDurationMs(),
                dateAndTime = new Duration()
                {
                    start = GetStringDateFromMs(startMs),
                    end = GetStringDateFromMs(endMs)
                },
                capabilities = Capas
            };
            return entry;
        }
    }
}
