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
    public class WaitWhileAction : Action
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

            logger.Debug("Events object instantiated.");

            Stopwatch stopwatch = new Stopwatch(timeoutMs);

            WebDriverException lastWebDriverException;
            lastWebDriverException = null;

            do
            {
                try
                {
                    if (!condition.Apply(driver, csSeElement))
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
                catch (WebDriverException e)
                {
                    lastWebDriverException = e;
                    continue;
                }

                Sleep(pollMs);
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
                 $"\n\nElement expected to be not {condition.Expected} after {timeoutMs} ms., but actually was {condition.Actual}."
                + "\n\nContext info:"
                + $"\n\tSelector:\t{csSeElement.RecursiveBy}"
                + $"\n\tDriver info:\t{((RemoteWebDriver)driver).Capabilities.ToString()}",
                 lastWebDriverException
                );
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
