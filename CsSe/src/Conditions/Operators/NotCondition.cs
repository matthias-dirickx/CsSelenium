using System;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class NotCondition : Condition
    {
        private readonly Condition condition;

        protected override string ResultValue { get; set; }

        public NotCondition(Condition condition) : base($"NOT [{condition.name}]")
        {
            this.condition = condition;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            if (condition.Apply(driver, element))
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        protected override string ActualValue()
        {
            return condition.Actual;
        }

        protected override string ExpectedValue()
        {
            return "not " + condition.Expected;
        }
    }
}
