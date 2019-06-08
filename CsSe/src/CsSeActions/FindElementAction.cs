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

namespace CsSeleniumFrame.src.CsSeActions
{
    public class FindElementAction : CsSeAction<IWebElement>
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public FindElementAction() : base("find element")
        {
            logger.Debug("Initiate FindElementAction.");
        }

        public override IWebElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            logger.Debug("Execute FindElementAction.");

            IWebElement target = null;

            logger.Debug("Set initial values for parent, index and by locator.");

            logger.Debug($"Initial value - index.");
            int index = csSeElement.index;

            logger.Debug($"Set initial value - parent.");
            CsSeElement parent = csSeElement.parent;

            logger.Debug($"Set initial value - by.");
            By by = csSeElement.by;

            //logger.Debug($"Values are:\n    - index: {index}\n    - parent: {((parent == null) ? "null" : parent.RecursiveBy)}\n    - by: {by.ToString()}");

            logger.Debug("Update event name with locator and parent.");

            name += $" with locator[{ by.ToString()}] ";
            name += parent == null ? "in WebDriver DOM." : "in element.";

            logger.Debug("Create log entry.");

            CsSeLogEventEntry entry = CsSeEventLog.GetNewEventEntry(csSeElement.RecursiveBy, $"{name}");

            logger.Debug("Set values for event entry: event type, expected and capabilities.");

            entry.EventType = CsSeEventType.CsSeAction;
            entry.Expected = $"Can find element with locator {csSeElement.RecursiveBy}";
            entry.Capas = CsSeDriver.GetDriverCapabilities(driver);

            logger.Debug("Try to find element.");

            try
            {
                if(parent == null)
                {
                    logger.Debug("Parent is null. Start from driver.");

                    target = findFromDriver(driver, csSeElement.by, index);

                    logger.Debug("Target to return set.");
                }
                else
                {
                    logger.Debug("Parent is not null. Start from the parent element.");

                    target = findFromElement(parent, csSeElement.by, index);

                    logger.Debug("Target to return set.");
                    logger.Debug($"Element ToString() is {target.ToString()}");
                }

                logger.Debug("Set actual value for event.");

                entry.Actual = $"Found the element with locator {csSeElement.RecursiveBy}.";

                logger.Debug("Commit event entry.");

                CsSeEventLog.CommitEventEntry(entry, CsSeEventStatus.Pass);

                logger.Debug("Entry committed.");
            }
            catch (WebDriverException e)
            {
                entry.Actual = $"Could not find the element - WebDriverException occured: {e.GetType().Name}{((e.InnerException == null) ? "" : " due to " + e.InnerException.GetType().Name)}.";

                CsSeEventLog.CommitEventEntry(entry, e);

                throw e;
            }

            if(target == null)
            {
                throw new System.NullReferenceException("Element was not found and set to null.");
            }

            return target;
        }

        public IWebElement Execute(IWebDriver driver, By by, int index, CsSeElement parent)
        {
            IWebElement target = null;
            string recursiveBy = parent == null ? by.ToString() : parent.RecursiveBy + by.ToString();

            logger.Debug("Execute FindElementAction.");

            logger.Debug("Update event name with locator and parent.");

            name += $" with locator[{ by.ToString()}] ";
            name += parent == null ? "in WebDriver DOM." : "in element.";

            logger.Debug("Create log entry.");

            CsSeLogEventEntry entry = CsSeEventLog.GetNewEventEntry(recursiveBy, $"{name}");

            logger.Debug("Set values for event entry: event type, expected and capabilities.");

            entry.EventType = CsSeEventType.CsSeAction;
            entry.Expected = $"Can find element with locator {recursiveBy}.";
            entry.Capas = CsSeDriver.GetDriverCapabilities(driver);

            logger.Debug("Try to find element.");

            try
            {
                if (parent == null)
                {
                    logger.Debug("Parent is null. Start from driver.");

                    target = findFromDriver(driver, by, index);

                    logger.Debug("Target to return set.");
                }
                else
                {
                    logger.Debug("Parent is not null. Start from the parent element.");

                    target = findFromElement(parent, by, index);

                    logger.Debug("Target to return set.");
                    logger.Debug($"Element ToString() is {target.ToString()}");
                }

                logger.Debug("Set actual value for event.");

                entry.Actual = $"Found the element with locator {recursiveBy}.";

                logger.Debug("Commit event entry.");

                CsSeEventLog.CommitEventEntry(entry, CsSeEventStatus.Pass);

                logger.Debug("Entry committed.");
            }
            catch(WebDriverException e)
            {
                entry.Actual = $"Could not find the element - WebDriverException occured: {e.GetType().Name}{((e.InnerException == null) ? "" : " due to " + e.InnerException.GetType().Name)}.";

                CsSeEventLog.CommitEventEntry(entry, e);

                throw e;
            }
            

            return target;
        }

        private IWebElement findFromDriver(IWebDriver driver, By by, int index)
        {
            if (index == 0)
                return driver.FindElement(by);
            return driver.FindElements(by)[index];
        }

        private IWebElement findFromElement(IWebElement parent, By by, int index)
        {
            if (index == 0)
                return parent.FindElement(by);
            return parent.FindElements(by)[index];
        }
    }
}
