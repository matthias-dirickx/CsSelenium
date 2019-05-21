﻿using OpenQA.Selenium;

using CsSeleniumFrame.src.Conditions;

using static CsSeleniumFrame.src.statics.CsSeCondition;

namespace CsSeleniumFrame.src.Actions
{
    public class ShouldNotAction : Interaction
    {
        private readonly Condition[] conditions;

        public ShouldNotAction(Condition[] conditions) : base("should not")
        {
            this.conditions = conditions;
        }

        public override CsSeElement Execute(IWebDriver driver, IWebElement element)
        {
            foreach (Condition c in conditions)
            {
                Not(c).Apply(driver, element);
            }
            return new CsSeElement(element);
        }
    }
}
