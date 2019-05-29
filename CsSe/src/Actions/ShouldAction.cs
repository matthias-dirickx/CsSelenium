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
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.Conditions.Operators;
using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Ex;
using CsSeleniumFrame.src.Logger;
using CsSeleniumFrame.src.Statics;

using static CsSeleniumFrame.src.Statics.CsSeConfigurationManager;

namespace CsSeleniumFrame.src.Actions
{
    /// <summary>
    /// Action to start an assertion.
    /// 
    /// Assertions work with the objects extnding the <see cref="Condition"/> object.
    /// 
    /// Call the actions on CsSeElements with the implemented 'ShouldBe' and 'ShouldHave' (synonym) methods.
    /// </summary>
    public class ShouldAction : Action
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Condition[] conditions;

        /*
         * Constructors
         */
        public ShouldAction(Condition[] conditions) : base("should be")
        {
            this.conditions = conditions;
        }

        public ShouldAction(string fluentRead, Condition[] conditions) : base($"should {fluentRead}")
        {
            this.conditions = conditions;
        }

        /*
         * Do checks
         */
        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            foreach(Condition c in conditions)
            {
                CsSeLogEventEntry eventEntry = CsSeEventLog.GetNewEventEntry(csSeElement.GetFullByTrace(), $"{name} {c.name}");
                eventEntry.Capas = CsSeDriver.GetDriverCapabilities(driver);

                if (c is IAggregateCondition)
                {
                    eventEntry.EventType = CsSeEventType.CsSeCheckAggregate;
                }
                else
                {
                    eventEntry.EventType = CsSeEventType.CsSeCondtion;
                }

                try
                {
                    bool passed = c.Apply(driver, csSeElement);

                    if (c is ImageEqualsCondition)
                    {
                        eventEntry.Actual = "Actual image -> images.ActualScreenshotBase64Image";
                        eventEntry.ActualScreenshotBase64Image = c.Actual;
                        eventEntry.Expected = "Expected image -> images.ExpectedScreenshotBase64Image";
                        eventEntry.ExpectedScreenshotBase64Image = c.Expected;
                    }
                    else
                    {
                        eventEntry.Actual = c.Actual;
                        eventEntry.Expected = c.Expected;
                    }

                    if (passed)
                    {
                        eventEntry.EventStatus = CsSeEventStatus.Pass;
                        CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Pass);
                    }
                    else
                    {
                        throw new CsSeElementShould(
                            $"\n\nElement {name} {c.Expected}, but actually was {c.Actual}."
                          + "\n\nContext info:"
                          + $"\n\tSelector:\t{csSeElement.RecursiveBy}"
                          + $"\n\tDriver info:\t{((RemoteWebDriver)driver).Capabilities.ToString()}");
                    }
                }
                catch (CsSeAssertion e)
                {
                    CsSeEventLog.CommitEventEntry(eventEntry, e);

                    if (!GetConfig().ContinueOnCsSeAssertionFail)
                    {
                        throw e;
                    }
                }
                catch (WebDriverException e)
                {
                    eventEntry.Error = e;
                    CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Unknown);

                    if (!GetConfig().ContinueOnWebDriverException)
                    {
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    eventEntry.Error = e;
                    CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Unknown);
                    throw e;
                }
            }
            return csSeElement;
        }

        private CsSeLogEventEntry UpdatedConditionEventEntry(CsSeLogEventEntry eventEntry, Condition c)
        {
            if (c is ImageEqualsCondition)
            {
                eventEntry.Actual = "Actual image -> images.ActualScreenshotBase64Image";
                eventEntry.ActualScreenshotBase64Image = c.Actual;
                eventEntry.Expected = "Expected image -> images.ExpectedScreenshotBase64Image";
                eventEntry.ExpectedScreenshotBase64Image = c.Expected;
            }
            else
            {
                eventEntry.Actual = c.Actual;
                eventEntry.Expected = c.Expected;
            }

            return eventEntry;
        }

        private string GetTheCompositExpectedConditions()
        {
            string str = "";
            foreach (Condition c in conditions)
            {   
                if(str != "")
                {
                    str += ", ";
                }
                str += c.Expected;
            }

            return $"[{str}]";
        }
    }
}
