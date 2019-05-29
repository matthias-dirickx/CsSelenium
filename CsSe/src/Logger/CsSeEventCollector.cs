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

using System.Collections.Generic;

using CsSeleniumFrame.src.Logger.CsSeSerialization;

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
