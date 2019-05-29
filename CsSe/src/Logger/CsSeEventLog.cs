using System;
using System.Collections.Generic;
using System.Threading;
using static CsSeleniumFrame.src.Logger.CsSeLogEventEntry;

namespace CsSeleniumFrame.src.Logger
{
    public class CsSeEventLog
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static ThreadLocal<Dictionary<string, EventCollector>> threadSafeCollectors = new ThreadLocal<Dictionary<string, EventCollector>>(true);

        /// <summary>
        /// Addes a listener with the provided name.
        /// 
        /// (!) If the same name is provided, the previous listener is replaced by an empty one.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="collector"></param>
        public static void addListener(String name, EventCollector collector)
        {
            if(threadSafeCollectors.Value == null)
            {
                threadSafeCollectors.Value = new Dictionary<string, EventCollector>();
            }

            Dictionary<string, EventCollector> threadListener = threadSafeCollectors.Value;

            if(threadListener.ContainsKey(name))
            {
                threadListener.Remove(name);
                threadListener.Add(name, collector);
            }
            else
            {
                threadListener.Add(name, collector);
            }
        }

        private static List<EventCollector> GetEventCollectors()
        {
            List<EventCollector> theList = new List<EventCollector>();
            Dictionary<string, EventCollector> threadListeners = threadSafeCollectors.Value;
            foreach(KeyValuePair<string, EventCollector> entry in threadListeners)
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
            return threadSafeCollectors.Value[name];
        }

        public static List<EventCollector> GetEventCollectorForAllThreads(string name)
        {
            IList<Dictionary<string, EventCollector>> allCollectors = threadSafeCollectors.Values;
            List<EventCollector> nameCollectors = new List<EventCollector>();

            foreach(Dictionary<string, EventCollector> collectors in allCollectors)
            {
                EventCollector collector = collectors[name];

                if(collector != null)
                {
                    nameCollectors.Add(collector);
                }
            }

            return nameCollectors;
        }

        public static List<List<CsSeSerializableLogEntry>> GetSerializableEventCollectorForAllThreads(string name)
        {
            IList<Dictionary<string, EventCollector>> allCollectors = threadSafeCollectors.Values;
            List<List<CsSeSerializableLogEntry>> nameCollectors = new List<List<CsSeSerializableLogEntry>>();

            foreach (Dictionary<string, EventCollector> collectors in allCollectors)
            {
                EventCollector collector = collectors[name];

                if (collector != null)
                {
                    nameCollectors.Add(collector.GetSerializableCsSeEventEntryList());
                }
            }

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
