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

using static CsSeleniumFrame.src.Statics.CsSeConfigurationManager;
using static CsSeleniumFrame.src.util.CsSeTestMetaFinder;

namespace CsSeleniumFrame.src.Actions
{
    public class SendKeysAction : Action
    {
        private readonly string value;

        public SendKeysAction(string value) : base($"Send keys: {value}")
        {
            this.value = value;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            CsSeLogEventEntry entry = CsSeEventLog.GetNewEventEntry(csSeElement.RecursiveBy, $"{name}");

            entry.EventType = CsSeEventType.CsSeAction;
            entry.Expected = $"Can send value {value} to the source element.";

            try
            {
                csSeElement.WebElement.SendKeys(value);

                entry.Actual = $"Succeeded to send value {value} to element.";

                CsSeEventLog.CommitEventEntry(entry, CsSeEventStatus.Pass);
            }
            catch(WebDriverException e)
            {
                entry.Actual = $"Could not sent {value} to element - WebDriverException occured: {e.GetType().Name} due to {e.InnerException.GetType().Name}.";

                CsSeEventLog.CommitEventEntry(entry, e);

                if (GetConfig().ScreenshotOnFail)
                {
                    csSeElement.TakeScreenshot($"{GetConfig().ScreenshotBasePath}/{GetTestModuleName()}/{GetTestClassName()}", $"{GetTestMethodName()}_{name.Replace(":", "").Replace(" ", "")}_{entry.StartTime}_error", false);
                }

                throw e;
            }

            return csSeElement;
        }
    }
}
