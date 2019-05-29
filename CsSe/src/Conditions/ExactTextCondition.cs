using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Conditions
{
    public class ExactTextCondition : Condition
    {
        private readonly string text;
        private readonly bool readFromRootElementOnly;
        private readonly bool readRootElementStrict;

        protected override string ResultValue { get ; set; }

        public ExactTextCondition(string text) : base($"exact text: '{text}'")
        {
            this.text = text;
            readFromRootElementOnly = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly) : base("exact text: '{text}'")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
            readRootElementStrict = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly, bool readRootElementStrict) : base("exact text: '{text}'")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
            this.readRootElementStrict = readRootElementStrict;
        }

        private string GetText(IWebElement element)
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
            string actualText = GetText(element);
            ResultValue = actualText;

            return actualText == text;
        }

        protected override string ActualValue()
        {
            return ResultValue;
        }

        protected override string ExpectedValue()
        {
            string onlyRootMessage = $"(Root only: {readRootElementStrict} (Strictly root and not even <b>, <i>, <p>, ...)";
            return $"'{text}' {onlyRootMessage}";
        }
    }
}
