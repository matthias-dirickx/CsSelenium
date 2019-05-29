using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Logger.CsSeSerialization;

namespace CsSeleniumFrame.src.Logger
{
    public class CsSeLogEventEntry
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly double startMs;
        private double endMs;
        private CsSeEventStatus eventStatus;

        public CsSeEventType EventType { get; set; }
        public string MachineName { get; set; }
        public int TestThreadId { get; set; }
        public string TestModule { get; set; }
        public string TestClass { get; set; }
        public string TestMethod { get; set; }
        public string Source { get; set; }
        public string Subject { get; set; }
        public CsSeEventStatus EventStatus
        {
            get
            {
                return this.eventStatus;
            }
            set
            {
                endMs = GetNowMs();

                eventStatus = value;
            }
        }
        public ICapabilities Capas { get; set; }
        public double Duration { get { return endMs - startMs; } }

        public Exception Error { get; set; }

        public string FullScreenshotBase64Image { get; set; }

        public string Expected { get; set; }
        public string Actual { get; set; }
        public string ExpectedScreenshotBase64Image { get; set; }
        public string ActualScreenshotBase64Image { get; set; }
        private List<CsSeSerializableLogEntry> subEntriesList;

        public CsSeLogEventEntry(string source, string subject)
        {
            Source = source;
            Subject = subject;
            Error = new Exception("No exception - placeholder");

            startMs = GetNowMs();

            EventType = CsSeEventType.CsSeCondtion;
            EventStatus = CsSeEventStatus.Unknown;
            Expected = "Not applicable";
            Actual = "Not applicable";
            subEntriesList = new List<CsSeSerializableLogEntry>();
            TestClass = CsSeTestMetaFinder.GetTestClassName();
            TestMethod = CsSeTestMetaFinder.GetTestMethodName();
            TestModule = CsSeTestMetaFinder.GetTestModuleName();
            TestThreadId = CsSeTestMetaFinder.GetTestThreadId();
            MachineName = CsSeTestMetaFinder.GetMachineName();
        }

        public void AddSubEntry(CsSeSerializableLogEntry subEntry)
        {
            subEntriesList.Add(subEntry);
        }

        private static double GetNowMs()
        {
            return (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
        }

        private static string GetStringDateFromMs(double ms)
        {
            return (new DateTime(1, 1, 1)).AddMilliseconds(ms).ToString("o");
        }

        public override string ToString()
        {
            return JsonConvert
                .SerializeObject(
                    GetSerializableCsSeEventEntry(),
                    Formatting.Indented);
        }

        public CsSeSerializableLogEntry GetSerializableCsSeEventEntry()
        {
            return new CsSeSerializableLogEntry()
            {
                meta = new EventMeta()
                {
                    machineName = MachineName,
                    testThreadId = TestThreadId,
                    testModule = TestModule,
                    testClass = TestClass,
                    testMethod = TestMethod
                },
                summary = new EventSummary()
                {
                    eventType = EventType.ToString(),
                    source = Source,
                    subject = Subject,
                    status = EventStatus.ToString(),
                    expected = Expected,
                    actual = Actual,
                    error = Error.ToString(),
                    duration = Duration,
                    dateAndTime = new TimeDuration()
                    {
                        start = GetStringDateFromMs(startMs),
                        end = GetStringDateFromMs(endMs)
                    }
                },
                capabilities = JsonConvert.DeserializeObject(Capas.ToString()),
                screenshots = new Images()
                {
                    expected = ExpectedScreenshotBase64Image,
                    actual = ActualScreenshotBase64Image,
                    screenshot = FullScreenshotBase64Image
                },
                subEntries = subEntriesList
            };
        }
    }
}
