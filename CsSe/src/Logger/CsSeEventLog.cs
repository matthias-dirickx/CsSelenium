using System;
using System.Collections.Generic;
using System.Threading;
using static CsSeleniumFrame.src.Logger.CsSeEventEntry;

namespace CsSeleniumFrame.src.Logger
{
    public class CsSeEventLog
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private static ThreadLocal<Dictionary<string, EventCollector>> threadSafeCollectors = new ThreadLocal<Dictionary<string, EventCollector>>(true);

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

        public static CsSeEventEntry GetNewEventEntry(String source, String subject)
        {
            CsSeEventEntry eventEntry = new CsSeEventEntry(source, subject);

            return eventEntry;
        }

        public static void CommitEventEntry(CsSeEventEntry eventEntry, EventStatus status)
        {
            List<EventCollector> collectors = GetEventCollectors();
            eventEntry.SetStatus(status);

            foreach (EventCollector collector in collectors)
            {
                collector.CommitEntry(eventEntry);
            }
        }

        public static void CommitEventEntry(CsSeEventEntry eventEntry, Exception e)
        {
            NLog.LogManager.GetCurrentClassLogger().Debug($"Set error to current error '{e.GetType()}'.");
            eventEntry.Error = e;
            NLog.LogManager.GetCurrentClassLogger().Debug($"Set status to failed & commit.");
            CommitEventEntry(eventEntry, EventStatus.Fail);
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

        public static List<List<CsSeSerializableItem>> GetSerializableEventCollectorForAllThreads(string name)
        {
            IList<Dictionary<string, EventCollector>> allCollectors = threadSafeCollectors.Values;
            List<List<CsSeSerializableItem>> nameCollectors = new List<List<CsSeSerializableItem>>();

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
    }
}
