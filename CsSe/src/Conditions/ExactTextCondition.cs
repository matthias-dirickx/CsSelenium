using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;

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

        private string GetText(IWebDriver driver, IWebElement element)
        {
            if(readFromRootElementOnly)
            {
                return XmlUtils.GetRootElementTextValue(element.GetAttribute("outerHTML"), readRootElementStrict);
            }
            else
            {
                return element.Text;
            }
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            return GetText(driver, element) == text;
        }

        public override string ActualValue(IWebDriver driver, IWebElement element)
        {
            return GetText(driver, element);
        }
    }
}
