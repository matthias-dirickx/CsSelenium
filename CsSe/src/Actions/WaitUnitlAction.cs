using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.Ex;
using CsSeleniumFrame.src.Logger;

using CsSeleniumFrame.src.Statics;

namespace CsSeleniumFrame.src.Actions
{
    public class WaitUntilAction : Action
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Condition condition;
        private readonly long timeoutMs;
        private readonly long pollingInterval;


        public WaitUntilAction(Condition condition, long timeoutMs, long pollingInnterval) : base("wait while")
        {
            this.condition = condition;
            this.timeoutMs = timeoutMs;
            this.pollingInterval = pollingInnterval;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            logger.Info($"Start Wait Until: {condition.name} (element: {csSeElement.RecursiveBy})");
            logger.Debug("Instantiating events object...");

            CsSeLogEventEntry eventEntry = CsSeEventLog.GetNewEventEntry(csSeElement.GetFullByTrace(), $"Wait until: [{condition.name}]");
            eventEntry.Capas = CsSeDriver.GetDriverCapabilities(driver);

            eventEntry.EventType = CsSeEventType.CsSeCheckWait;

            logger.Debug("Events object instantiated.");

            Stopwatch stopwatch = new Stopwatch(timeoutMs);

            WebDriverException lastWebDriverException;
            lastWebDriverException = null;

            do
            {
                try
                {
                    bool passed = condition.Apply(driver, csSeElement);

                    if (passed)
                    {
                        logger.Debug("Condition OK - Commit log event");

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

                        CsSeEventLog.CommitEventEntry(eventEntry, CsSeEventStatus.Pass);

                        return csSeElement;
                    }
                }
                catch(WebDriverException e)
                {
                    lastWebDriverException = e;
                    continue;
                }

                Sleep(pollingInterval);
            }
            while (!stopwatch.IsTimoutReached());

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

            logger.Debug($"Condition not OK (WebDriverException). Assertion not completed. - Commit log event; Error:\n{lastWebDriverException.ToString()}");
            CsSeEventLog.CommitEventEntry(eventEntry, lastWebDriverException);

            throw new CsSeElementShould(
                $"\n\nElement expected to be {condition.Expected} after {timeoutMs} ms., but actually was {condition.Actual}."
               + "\n\nContext info:"
               + $"\n\tSelector:\t{csSeElement.RecursiveBy}"
               + $"\n\tDriver info:\t{((RemoteWebDriver)driver).Capabilities.ToString()}",
                lastWebDriverException);
        }

        private void Sleep(long ms)
        {
            try
            {
                Thread.Sleep((int)ms);
            }
            catch(ThreadInterruptedException e)
            {
                Thread.CurrentThread.Interrupt();
                throw e;
            }
        }
    }
}
