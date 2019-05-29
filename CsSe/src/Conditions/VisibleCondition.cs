using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class VisibleCondition : Condition
    {
        protected override string ResultValue { get; set; }

        public VisibleCondition() : base("visible")
        {

        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            bool value = element.Displayed;
            ResultValue = value ? "visible" : "invisible";

            return value;
        }

        protected override string ActualValue()
        {
            return ResultValue;
        }

        protected override string ExpectedValue()
        {
            return "visible";
        }
    }
}
