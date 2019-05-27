using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Ex;
using CsSeleniumFrame.src.Logger;
using CsSeleniumFrame.src.statics;

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
        private readonly string fluentRead;

        public ShouldAction(Condition[] conditions) : base("should")
        {
            logger.Debug($"Instantiate ShouldAction -- Set conditions ({conditions.Length} in conditions list).");
            this.conditions = conditions;
            fluentRead = "be";
        }

        public ShouldAction(string fluentRead, Condition[] conditions) : base($"should")
        {
            logger.Debug($"Instantiate ShouldAction -- Set conditions ({conditions.Length} in conditions list).");
            this.conditions = conditions;
            this.fluentRead = fluentRead;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            logger.Debug("Execute checks...");

            foreach(Condition c in conditions)
            {
                logger.Info($"Start Check: {c.name} (element: {csSeElement.RecursiveBy})");
                logger.Debug("Instantiating events object...");

                CsSeEventEntry eventEntry = CsSeEventLog.GetNewEventEntry(csSeElement.GetFullByTrace(), c.name);

                eventEntry.Capas = CsSeDriver.GetDriverCapabilities(driver);
                eventEntry.EventType = CsSeEventType.CsSeCheck;

                logger.Debug("Events object instantiated.");

                try
                {
                    logger.Debug("Try evaluation for condition.");

                    if (c.Apply(driver, csSeElement))
                    {
                        logger.Debug("Condition OK - Commit log event");

                        if(c is ImageEqualsCondition)
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
                        
                        CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Pass);

                        return csSeElement;
                    }
                    else
                    {
                        throw new CsSeElementShould(
                            $"\n\nElement should {fluentRead} {c.Expected}, but actually was {c.Actual}."
                          + "\n\nContext info:"
                          + $"\n\tSelector:\t{csSeElement.RecursiveBy}"
                          + $"\n\tDriver info:\t{((RemoteWebDriver)driver).Capabilities.ToString()}");
                    }
                }
                catch (CsSeAssertion e)
                {
                    logger.Debug($"Condition not OK - Commit log event; Error:\n{e.ToString()}");
                    CsSeEventLog.CommitEventEntry(eventEntry, e);
                    logger.Debug($"Logevent committed.");
                    throw e;
                }
                catch (WebDriverException e)
                {
                    logger.Debug($"Condition not OK (WebDriverException). Assertion not completed. - Commit log event; Error:\n{e.ToString()}");
                    CsSeEventLog.CommitEventEntry(eventEntry, e);
                    throw e;
                }
                catch (Exception e)
                {
                    logger.Debug($"Exception other then WebDriverException or CsSeAssertion (custom Exception) - Commit log event; Error:\n{e.ToString()}");
                    eventEntry.Error = e;

                    CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Unknown);

                    throw e;
                }
            }
            return null;
        }
    }
}
