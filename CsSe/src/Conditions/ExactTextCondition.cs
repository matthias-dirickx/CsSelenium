using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class ExactTextCondition : Condition
    {
        private readonly string text;
        private readonly bool readFromRootElementOnly;

        public ExactTextCondition(string text) : base("exact text")
        {
            this.text = text;
            this.readFromRootElementOnly = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly) : base("exact text")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            return element.Displayed;
        }
    }
}
