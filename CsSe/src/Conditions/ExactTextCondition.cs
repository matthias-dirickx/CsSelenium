using OpenQA.Selenium;

using CsSeleniumFrame.src.util;
using CsSeleniumFrame.src.Actions;

namespace CsSeleniumFrame.src.Conditions
{
    public class ExactTextCondition : Condition
    {
        private readonly string text;
        private readonly bool readFromRootElementOnly;
        private readonly bool readRootElementStrict;

        public ExactTextCondition(string text) : base("exact text")
        {
            this.text = text;
            this.readFromRootElementOnly = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly) : base("exact text")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
            this.readRootElementStrict = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly, bool readRootElementStrict) : base("exact text")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
            this.readRootElementStrict = readRootElementStrict;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            if(readFromRootElementOnly)
            {
                return XmlUtils.GetRootElementTextValue(element.GetAttribute("outerHTML"), readRootElementStrict) == text;
            }
            else
            {
                return element.Text == text;
            }
        }
    }
}
