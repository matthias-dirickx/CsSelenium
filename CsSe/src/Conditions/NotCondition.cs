using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class NotCondition : Condition
    {
        private readonly Condition condition;

        public NotCondition(Condition condition) : base("not")
        {
            this.condition = condition;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            if (condition.Apply(driver, element))
            {
                    return false;
            }
            return true;
        }
    }
}
