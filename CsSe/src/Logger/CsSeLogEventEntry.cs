using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            TestClass = GetTestClassName();
            TestMethod = GetTestMethodName();
            TestModule = GetTestModuleName();
            TestThreadId = Thread.CurrentThread.ManagedThreadId;
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

        private string GetTestMethodName()
        {
            // for when it runs via Visual Studio locally
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodBase methodBase = stackFrame.GetMethod();
                Object[] attributes = methodBase.GetCustomAttributes(typeof(TestMethodAttribute), false);
                if (attributes.Length >= 1)
                {
                    return methodBase.Name;
                }
            }
            return "Not called from a test method";
        }

        private string GetTestClassName()
        {
            // for when it runs via Visual Studio locally
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodBase methodBase = stackFrame.GetMethod();
                Object[] attributes = methodBase.GetCustomAttributes(typeof(TestMethodAttribute), false);
                if (attributes.Length >= 1)
                {
                    return methodBase.DeclaringType.Name;
                }
            }
            return "Not able to identify test class.";
        }

        private string GetTestModuleName()
        {
            // for when it runs via Visual Studio locally
            var stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodBase methodBase = stackFrame.GetMethod();
                Object[] attributes = methodBase.GetCustomAttributes(typeof(TestMethodAttribute), false);
                if (attributes.Length >= 1)
                {
                    return methodBase.Module.Name;
                }
            }
            return "Not able to identify test module.";
        }

        public class CsSeSerializableLogEntry
        {
            public int testThreadId { get; set; }
            public string testModule { get; set; }
            public string testClass { get; set; }
            public string testMethod { get; set; }
            public EventSummary summary { get; set; }
            public string expected { get; set; }
            public string actual { get; set; }
            public object capabilities { get; set; }
            public Images screenshots { get; set; }
            public List<CsSeSerializableLogEntry> subEntries { get; set; }
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
                testThreadId = TestThreadId,
                testModule = TestModule,
                testClass = TestClass,
                testMethod = TestMethod,
                summary = new EventSummary()
                {
                    eventType = EventType.ToString(),
                    source = Source,
                    status = EventStatus.ToString(),
                    subject = Subject,
                    error = Error.ToString(),
                    duration = Duration,
                    dateAndTime = new TimeDuration()
                    {
                        start = GetStringDateFromMs(startMs),
                        end = GetStringDateFromMs(endMs)
                    }
                },
                expected = Expected,
                actual = Actual,
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
