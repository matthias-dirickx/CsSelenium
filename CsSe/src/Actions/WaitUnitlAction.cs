using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CsSeleniumFrame.src.util;
using CsSeleniumFrame.src.core;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

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

        public override CsSeElement Execute(IWebDriver driver, IWebElement element)
        {
            Stopwatch stopwatch = new Stopwatch(timeoutMs);

            do
            {
                try
                {
                    if (condition.Apply(driver, element))
                    {
                        return new CsSeElement(element);
                    }
                }
                catch(WebDriverException e)
                {
                    continue;
                }

                Sleep(pollingInterval);
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
            catch(ThreadInterruptedException e)
            {
                Thread.CurrentThread.Interrupt();
            }
        }
    }
}
