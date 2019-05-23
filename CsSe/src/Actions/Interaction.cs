using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Actions
{
    public abstract class Interaction
    {
        public abstract CsSeElement Execute(IWebDriver driver, IWebElement element);

        private readonly string name;
        protected Stopwatch stopwatch;

        public Interaction(string name)
        {
            this.name = name;
            this.stopwatch = new Stopwatch();
            stopwatch.Start();
        }
    }
}
