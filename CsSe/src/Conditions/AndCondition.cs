using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class AndCondition : Condition
    {
        private readonly Condition[] conditions;

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
                    return false;
                }
            }
            return true;
        }

        public override string ActualValue(IWebDriver driver, IWebElement element)
        {
            throw new NotImplementedException();
        }

        public override string ExpectedValue()
        {
            throw new NotImplementedException();
        }
    }
}
