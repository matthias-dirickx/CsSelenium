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

using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Logger;
using CsSeleniumFrame.src.Statics;
using CsSeleniumFrame.src.Util;

namespace CsSeleniumFrame.src.CsSeActions
{
    public class DragAndDropAction : CsSeAction<CsSeElement>
    {
        private CsSeElement target;
        public DragAndDropAction(CsSeElement target) : base($"Drag and drop element to target {target.RecursiveBy}.")
        {
            this.target = target;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement element)
        {
            CsSeLogEventEntry entry = CsSeEventLog.GetNewEventEntry(element.RecursiveBy, $"{name}");

            entry.EventType = CsSeEventType.CsSeAction;
            entry.Expected = $"Can drag the source element [{element.RecursiveBy}] to the target element [{target.RecursiveBy}].";
            entry.Capas = CsSeDriver.GetDriverCapabilities(driver);

            try
            {
                Actions actionsProvider = new Actions(driver);

                actionsProvider
                    .DragAndDrop(element.WebElement, target.WebElement)
                    .Perform();

                CsSeEventLog.CommitEventEntry(entry, CsSeEventStatus.Pass);
            }
            catch(WebDriverException e)
            {
                entry.Actual = $"Could not click element - WebDriverException occured: {e.GetType().Name} due to {e.InnerException.GetType().Name}.";

                CsSeEventLog.CommitEventEntry(entry, e);

                throw e;
            }
            catch(Exception e)
            {
                entry.Actual = $"Could not click element - An exception occured: {DescriptionUtils.GenericErrorDescription(e)}.";

                CsSeEventLog.CommitEventEntry(entry, e);

                throw e;
            }
            
            return element;
        }
    }
}
