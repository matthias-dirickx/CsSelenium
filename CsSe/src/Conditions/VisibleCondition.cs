using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
