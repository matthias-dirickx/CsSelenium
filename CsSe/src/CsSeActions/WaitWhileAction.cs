﻿/*
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

using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.CsSeConditions;
using CsSeleniumFrame.src.Ex;
using CsSeleniumFrame.src.Logger;
using CsSeleniumFrame.src.Statics;
using System;

namespace CsSeleniumFrame.src.CsSeActions
{
    public class WaitWhileAction : CsSeAction<CsSeElement>
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Condition condition;
        private readonly long timeoutMs;
        private readonly long pollMs;

        public WaitWhileAction(Condition condition, long timeoutMs, long pollMs) : base("wait while")
        {
            logger.Debug($"Instantiate Wait While Action -- Set condition.");

            this.condition = condition;
            this.timeoutMs = timeoutMs;
            this.pollMs = pollMs;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            logger.Info($"Start Wait While: {condition.name} (element: {csSeElement.RecursiveBy})");
            logger.Debug("Instantiating events object...");

            CsSeLogEventEntry eventEntry = CsSeEventLog.GetNewEventEntry(csSeElement.GetFullByTrace(), $"Wait while: [{condition.name}]");

            eventEntry.Capas = CsSeDriver.GetDriverCapabilities(driver);
            eventEntry.EventType = CsSeEventType.CsSeCheckWait;
            eventEntry.Expected = $"The element will not be [{condition.name}] after {timeoutMs} ms.";

            logger.Debug("Events object instantiated.");

            Stopwatch stopwatch = new Stopwatch(timeoutMs);

            Exception lastWebDriverException = new Exception("No exception - placeholder");
            bool conditionOk = false;

            while (!stopwatch.IsTimoutReached() && conditionOk == false)
            {
                try
                {
                    if (!condition.Apply(driver, csSeElement))
                    {
                        logger.Debug("Condition OK - Commit log event");

                        eventEntry.Actual = $"The condition [{condition.Actual}] expired within {timeoutMs} ms.";

                        conditionOk = true;
                    }
                }
                catch (WebDriverException e)
                {
                    lastWebDriverException = e;
                    continue;
                }
                Sleep(pollMs);
            }

            if(conditionOk)
            {
                eventEntry = SetFinalActualAndExpected(eventEntry, condition);

                CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Pass);

                return csSeElement;
            }
            else
            {
                if(lastWebDriverException == null)
                {
                    eventEntry.Actual = $"The condition [{condition.name}] failed to expire witin {timeoutMs} ms.";
                }
                else
                {
                    eventEntry.Actual = $"The condition [{condition.name}] failed to expire due to {Util.DescriptionUtils.GenericErrorDescription(lastWebDriverException)}";
                    logger.Debug($"Condition not OK (WebDriverException). Assertion not completed. - Commit log event; Error:\n{lastWebDriverException.ToString()}");
                }

                eventEntry = SetFinalActualAndExpected(eventEntry, condition);

                CsSeEventLog.CommitEventEntry(eventEntry, lastWebDriverException);

                throw new CsSeElementShould(
                 $"\n\nElement expected to be [not {condition.Expected}] after {timeoutMs} ms., but actually was [{condition.Actual}]."
                + "\n\nContext info:"
                + $"\n\tSelector:\t{csSeElement.RecursiveBy}"
                + $"\n\tDriver info:\t{((RemoteWebDriver)driver).Capabilities.ToString()}",
                 lastWebDriverException
                );
            }
        }

        private CsSeLogEventEntry SetFinalActualAndExpected(CsSeLogEventEntry eventEntry, Condition condition)
        {
            if (condition is ImageEqualsCondition)
            {
                eventEntry.Actual = "Actual image -> images.ActualScreenshotBase64Image";
                eventEntry.ActualScreenshotBase64Image = condition.Actual;
                eventEntry.Expected = "Expected image -> images.ExpectedScreenshotBase64Image";
                eventEntry.ExpectedScreenshotBase64Image = condition.Expected;
            }
            else
            {
                eventEntry.Actual = condition.Actual;
                eventEntry.Expected = condition.Expected;
            }

            return eventEntry;
        }

        private void Sleep(long ms)
        {
            try
            {
                Thread.Sleep((int)ms);
            }
            catch (ThreadInterruptedException e)
            {
                Thread.CurrentThread.Interrupt();
                throw e;
            }
        }
    }
}
