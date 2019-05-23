using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class VisibleCondition : Condition
    {
        public VisibleCondition() : base("visible")
        {

        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            return element.Displayed;
        }

        public override string ActualValue(IWebDriver driver, IWebElement element)
        {
            return element.Displayed ? "visible" : "invisible";
        }

        public override string ExpectedValue()
        {
            return "visible";
        }
    }
}
