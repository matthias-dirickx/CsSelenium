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

namespace CsSeleniumFrame.src.Logger.CsSeSerialization
{
    public class EventSummary
    {
        public string eventType { get; set; }
        public string source { get; set; }
        public string subject { get; set; }
        public string status { get; set; }
        public string expected { get; set; }
        public string actual { get; set; }
        public string error { get; set; }
        public double duration { get; set; }
        public TimeDuration dateAndTime { get; set; }
    }
}
