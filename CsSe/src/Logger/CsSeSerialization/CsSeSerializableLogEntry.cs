using System.Collections.Generic;

namespace CsSeleniumFrame.src.Logger.CsSeSerialization
{
    public class CsSeSerializableLogEntry
    {
        public EventMeta meta { get; set; }
        public EventSummary summary { get; set; }
        public object capabilities { get; set; }
        public Images screenshots { get; set; }
        public List<CsSeSerializableLogEntry> subEntries { get; set; }
    }
}
