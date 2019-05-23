using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class OrCondition : Condition
    {
        private readonly Condition[] conditions;

        public OrCondition(params Condition[] conditions) : base("or")
        {
            this.conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach (Condition c in conditions)
            {
                if (c.Apply(driver, element))
                {
                    return true;
                }
            }
            return false;
        }

        public override string ActualValue(IWebDriver driver, IWebElement element)
        {
            throw new NotImplementedException();
        }
    }
}
