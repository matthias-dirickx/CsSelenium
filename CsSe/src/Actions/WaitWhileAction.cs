using System.Threading;

using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.core;
using CsSeleniumFrame.src.util;

using OpenQA.Selenium;

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

        public override CsSeElement Execute(IWebDriver driver, IWebElement element)
        {
            Stopwatch stopwatch = new Stopwatch(timeoutMs);

            do
            {
                try
                {
                    if (!condition.Apply(driver, element))
                    {
                        return new CsSeElement(element);
                    }
                }
                catch (WebDriverException e)
                {
                    continue;
                }

                Sleep(pollMs);
            }
            while (!stopwatch.IsTimoutReached());

            return new CsSeElement(element);
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
