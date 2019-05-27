using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.Ex;

namespace CsSeleniumFrame.src.Actions
{
    public class WaitUntilAction : Interaction
    {
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
            Stopwatch stopwatch = new Stopwatch(timeoutMs);
            WebDriverException lastWebDriverException;
            lastWebDriverException = null;

            do
            {
                try
                {
                    if (condition.Apply(driver, csSeElement))
                    {
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
