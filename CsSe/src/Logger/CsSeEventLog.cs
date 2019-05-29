using CsSeleniumFrame.src.Logger.CsSeSerialization;
using System;
using System.Collections.Generic;

namespace CsSeleniumFrame.src.Logger
{
    public class CsSeEventLog
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private Dictionary<string, EventCollector> collectors;
        private static Lazy<CsSeEventLog> instance = new Lazy<CsSeEventLog>(() => new CsSeEventLog());
        private static CsSeEventLog Instance => instance.Value;

        private CsSeEventLog()
        {
            logger.Debug("Initialize logger with new Dictionary.");
            collectors = new Dictionary<string, EventCollector>();
        }

        public static CsSeEventLog GetLog()
        {
            return Instance;
        }

        /// <summary>
        /// Addes a listener with the provided name.
        /// 
        /// (!) If the same name is provided, the previous listener is replaced by an empty one.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="collector"></param>
        public static void addListener(string name, EventCollector collector)
        {
            NLog.LogManager.GetCurrentClassLogger().Debug("Start add listener...");

            Dictionary<string, EventCollector> collectors = Instance.collectors;
            if(collectors.ContainsKey(name))
            {
                collectors.Remove(name);
                collectors.Add(name, collector);
            }
            else
            {
                collectors.Add(name, collector);
            }

            Instance.collectors = collectors;
        }

        private static List<EventCollector> GetEventCollectors()
        {
            List<EventCollector> theList = new List<EventCollector>();
            Dictionary<string, EventCollector> collectors = Instance.collectors;

            foreach(KeyValuePair<string, EventCollector> entry in collectors)
            {
                theList.Add(entry.Value);
            }
            return theList;
        }

        public static CsSeLogEventEntry GetNewEventEntry(String source, String subject)
        {
            CsSeLogEventEntry eventEntry = new CsSeLogEventEntry(source, subject);

            return eventEntry;
        }

        public static void CommitEventEntry(CsSeLogEventEntry eventEntry, CsSeEventStatus status)
        {
            List<EventCollector> collectors = GetEventCollectors();
            eventEntry.EventStatus = status;

            foreach (EventCollector collector in collectors)
            {
                collector.CommitEntry(eventEntry);
            }
        }

        public static void CommitEventEntry(CsSeLogEventEntry eventEntry, Exception e)
        {
            NLog.LogManager.GetCurrentClassLogger().Debug($"Set error to current error '{e.GetType()}'.");
            eventEntry.Error = e;
            NLog.LogManager.GetCurrentClassLogger().Debug($"Set status to failed & commit.");
            CommitEventEntry(eventEntry, CsSeEventStatus.Fail);
        }

        public static EventCollector GetEventCollector(string name)
        {
            return Instance.collectors[name];
        }

        public static List<EventCollector> GetEventCollectorForAllThreads(string name)
        {
            Dictionary<string, EventCollector> allCollectors = Instance.collectors;
            EventCollector collector = allCollectors[name];

            List<EventCollector> collectors = new List<EventCollector>();

            collectors.Add(collector);

            return collectors;
        }

        public static List<List<CsSeSerializableLogEntry>> GetSerializableEventCollectorForAllThreads(string name)
        {
            Dictionary<string, EventCollector> allCollectors = Instance.collectors;
            EventCollector collector = allCollectors[name];
            List<List<CsSeSerializableLogEntry>> nameCollectors = new List<List<CsSeSerializableLogEntry>>();
            nameCollectors.Add(collector.GetSerializableCsSeEventEntryList());

            return nameCollectors;
        }

        public static List<CsSeSerializableLogEntry> GetFlattenedSerializableEventCollectorForAllThreads(string name)
        {
            List<List<CsSeSerializableLogEntry>> nameCollectors = GetSerializableEventCollectorForAllThreads(name);
            List<CsSeSerializableLogEntry> flatList = new List<CsSeSerializableLogEntry>();

            foreach(List<CsSeSerializableLogEntry> l in nameCollectors)
            {
                foreach(CsSeSerializableLogEntry e in l)
                {
                    flatList.Add(e);
                }
            }

            return flatList;
        }
    }
}
