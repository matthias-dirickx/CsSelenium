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

using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Logger;
using CsSeleniumFrame.src.Statics;
using CsSeleniumFrame.src.Util;

namespace CsSeleniumFrame.src.CsSeActions
{
    public class OpenUrlAction : CsSeAction<bool>
    {
        private readonly string url;

        public OpenUrlAction(string url) : base($"open url: {url}")
        {
            this.url = url;
        }

        public override bool Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            CsSeLogEventEntry entry = CsSeEventLog.GetNewEventEntry("browser", $"{name}");

            entry.EventType = CsSeEventType.CsSeAction;
            entry.Expected = $"Can open url ('{url}').";
            entry.Capas = CsSeDriver.GetDriverCapabilities(driver);

            try
            {
                driver.Url = url;
                entry.Actual = $"Url '{url}' opened.";
                CsSeEventLog.CommitEventEntry(entry, CsSeEventStatus.Pass);
            }
            catch (WebDriverException e)
            {
                entry.Actual = $"Failed to open the url: {DescriptionUtils.GenericErrorDescription(e)}.";

                CsSeEventLog.CommitEventEntry(entry, e);

                throw e;
            }

            return true;
        }
    }
}
