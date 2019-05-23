using System;
using CsSeleniumFrame.src.Conditions;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Ex
{
    public class CsSeElementShould : CsSeAssertion
    {
        public CsSeElementShould(string message) : base(message)
        {
        }

        public CsSeElementShould(string message, Exception e) : base(message, e)
        {
        }

        /*
        public CsSeElementShould(
            IWebDriver driver,
            string searhCriteria,
            string prefix,
            string message,
            Condition condition,
            IWebElement element,
            Exception lastError) : base(
                "Element should " + prefix + " " + condition
                + " \n{" + searhCriteria + "\n}"
                + (message != null ? " because " + message : "")
                + "\nElement: '" + describeElement(driver, element) + "'"
                + actualValue(condition, driver, element), lastError)
        {
            }
            */
    }
}
