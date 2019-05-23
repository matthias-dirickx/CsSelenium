using System.Threading;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Conditions;

namespace CsSeleniumFrame.src.Actions
{
    public class WaitWhileAction : Interaction
    {
        private readonly Condition condition;
        private readonly long timeoutMs;
        private readonly long pollMs;

        public WaitWhileAction(Condition condition, long timeoutMs, long pollMs) : base("wait while")
        {
            this.condition = condition;
            this.timeoutMs = timeoutMs;
            this.pollMs = pollMs;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            Stopwatch stopwatch = new Stopwatch(timeoutMs);

            do
            {
                try
                {
                    if (!condition.Apply(driver, csSeElement))
                    {
                        return new CsSeElement(csSeElement);
                    }
                }
                catch (WebDriverException e)
                {
                    continue;
                }

                Sleep(pollMs);
            }
            while (!stopwatch.IsTimoutReached());

            return new CsSeElement(csSeElement);
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
            }
        }
    }
}
