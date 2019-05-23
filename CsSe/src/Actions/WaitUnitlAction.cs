using System.Threading;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Conditions;

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
                    continue;
                }

                Sleep(pollingInterval);
            }
            while (!stopwatch.IsTimoutReached());

            return csSeElement;
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
            }
        }
    }
}
