/*
 * Copyright 2019 Matthias Dirickx
 * 
 * This file is part of CsSeSelenium.
 * 
 * CsSeSelenium is free software:
 * you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License,
 * or (at your option) any later version.
 * 
 * CsSeSelenium is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * WITHOUT even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with CsSeSelenium.
 * 
 * If not, see http://www.gnu.org/licenses/.
 */

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Logger.CsSeSerialization;
using CsSeleniumFrame.src.util;

namespace CsSeleniumFrame.src.Logger
{
    public class CsSeLogEventEntry
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly double startMs;
        private double endMs;
        private CsSeEventStatus eventStatus;

        public string StartTime => GetFileFormatStartTime();
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

        private static string GetFileFormatStartTime(double ms)
        {
            return (new DateTime(1, 1, 1)).AddMilliseconds(ms).ToString("yyyyMMddHHmmssFFF");
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
                    error = Error.Message,
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
