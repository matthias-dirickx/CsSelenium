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
    public class ShouldAction : Interaction
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Condition[] conditions;
        private Exception caughtError;
        private readonly string fluentRead;

        public ShouldAction(Condition[] conditions) : base("should be")
        {
            logger.Debug($"Instantiate ShouldAction -- Set conditions ({conditions.Length} in conditions list).");
            this.conditions = conditions;
            fluentRead = "be ";
        }

        public ShouldAction(string fluentRead, Condition[] conditions) : base($"should {fluentRead}")
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
                eventEntry.Actual = c.ActualValue(driver, csSeElement);
                eventEntry.Expected = c.ExpectedValue();

                logger.Debug("Events object instantiated.");

                try
                {
                    logger.Debug("Try evaluation for condition.");

                    if (c.Apply(driver, csSeElement))
                    {
                        logger.Debug("Condition OK - Commit log event");

                        CsSeEventLog.CommitEventEntry(eventEntry, EventStatus.Pass);

                        return csSeElement;
                    }
                    else
                    {
                        throw new CsSeElementShould(
                            $"\n\nElement should {fluentRead} {c.ExpectedValue()}, but actually was {c.ActualValue(driver, csSeElement)}."
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
                    logger.Debug($"Condition not OK (WebDriverException) - Commit log event; Error:\n{e.ToString()}");
                    CsSeEventLog.CommitEventEntry(eventEntry, e);
                    throw e;
                }
                catch (Exception e)
                {
                    logger.Debug($"Exception other then WebDriverException or CsSeAssertion (custom Exception) - Commit log event; Error:\n{e.ToString()}");
                    CsSeEventLog.CommitEventEntry(eventEntry, e);
                    throw e;
                }
            }
            return null;
        }
    }
}
