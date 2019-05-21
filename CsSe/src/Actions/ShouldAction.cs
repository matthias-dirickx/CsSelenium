using System;

using OpenQA.Selenium;

using CsSeleniumFrame.src.core;

namespace CsSeleniumFrame.src.Actions
{
    public class ShouldAction : Interaction
    {
        private readonly Condition[] conditions;
        public ShouldAction(Condition[] conditions) : base("should")
        {
            this.conditions = conditions;
        }

        public override CsSeElement Execute(IWebDriver driver, IWebElement element)
        {
            foreach(Condition c in conditions)
            {
                c.Apply(driver, element);
            }

            return new CsSeElement(element);
        }
    }
}
