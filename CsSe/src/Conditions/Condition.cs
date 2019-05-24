using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Conditions
{
    public abstract class Condition
    {
        public abstract bool Apply(IWebDriver driver, IWebElement element);
        public abstract string ActualValue(IWebDriver driver, IWebElement element);
        public abstract string ExpectedValue();

        public readonly string name;

        public Condition(string name)
        {
            this.name = name;
        }
    }
}
