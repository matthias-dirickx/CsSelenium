using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class ImageEqualsCondition : Condition
    {
        public ImageEqualsCondition() : base("Image equals")
        {

        }
        public override string ActualValue(IWebDriver driver, IWebElement element)
        {
            throw new NotImplementedException();
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            throw new NotImplementedException();
        }

        public override string ExpectedValue()
        {
            throw new NotImplementedException();
        }
    }
}
