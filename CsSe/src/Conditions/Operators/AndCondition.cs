using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class AndCondition : Condition
    {
        private readonly Condition[] conditions;
        private Condition lastFailedCondition;

        protected override string ResultValue { get; set; }

        public AndCondition(Condition[] conditions) : base("and")
        {
            this.conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach(Condition c in conditions)
            {
                if(!c.Apply(driver, element))
                {
                    lastFailedCondition = c;
                    return false;
                }
            }
            return true;
        }

        protected override string ActualValue()
        {
            return lastFailedCondition == null ? null : lastFailedCondition.Actual;
        }

        protected override string ExpectedValue()
        {
            return lastFailedCondition == null ? null : lastFailedCondition.Expected;
        }
    }
}
