using CsSeleniumFrame.src.Logger.CsSeSerialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static CsSeleniumFrame.src.Logger.CsSeLogEventEntry;

namespace CsSeleniumFrame.src.Logger
{
    public class EventCollector
    {
        private List<CsSeLogEventEntry> eventEntries;

        public List<CsSeLogEventEntry> Events => eventEntries;

        public EventCollector()
        {
            eventEntries = new List<CsSeLogEventEntry>();
        }

        public void CommitEntry(CsSeLogEventEntry eventEntry)
        {
            eventEntries.Add(eventEntry);
        }

        public override string ToString()
        {
            string events = "";
            foreach (CsSeLogEventEntry entry in Events)
            {
                if(events != "")
                {
                    events += ", ";
                }

                events += entry.ToString();
            }

            return $"[{events}]";
        }

        public List<CsSeSerializableLogEntry> GetSerializableCsSeEventEntryList()
        {
            List<CsSeSerializableLogEntry> serializableEntries = new List<CsSeSerializableLogEntry>();

            foreach(CsSeLogEventEntry e in Events)
            {
                serializableEntries.Add(e.GetSerializableCsSeEventEntry());
            }

            return serializableEntries;
        }
    }
}
