using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static CsSeleniumFrame.src.Logger.CsSeEventEntry;

namespace CsSeleniumFrame.src.Logger
{
    public class EventCollector
    {
        private List<CsSeEventEntry> eventEntries;

        public List<CsSeEventEntry> Events => eventEntries;

        public EventCollector()
        {
            eventEntries = new List<CsSeEventEntry>();
        }

        public void CommitEntry(CsSeEventEntry eventEntry)
        {
            eventEntries.Add(eventEntry);
        }

        public override string ToString()
        {
            string events = "";
            foreach (CsSeEventEntry entry in Events)
            {
                if(events != "")
                {
                    events += ", ";
                }

                events += entry.ToString();
            }

            return $"[{events}]";
        }

        public List<CsSeSerializableItem> GetSerializableCsSeEventEntryList()
        {
            List<CsSeSerializableItem> serializableEntries = new List<CsSeSerializableItem>();

            foreach(CsSeEventEntry e in Events)
            {
                serializableEntries.Add(e.GetSerializableCsSeEventEntry());
            }

            return serializableEntries;
        }
    }
}
