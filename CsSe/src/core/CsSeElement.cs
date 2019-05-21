using System.Drawing;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.util;

using static CsSeleniumFrame.src.statics.CsSeDriver;
using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;
using static CsSeleniumFrame.src.statics.CsSeAction;
using static CsSeleniumFrame.src.statics.CsSeCondition;

namespace CsSeleniumFrame.src.Actions
{
    public class CsSeElement
    {
        //Core element
        private readonly IWebElement el;

        /*
         * Constructors
         */
        public CsSeElement(By by)
        {
            el = GetDriver().FindElement(by);
        }

        public CsSeElement(IWebElement webElement)
        {
            el = webElement;
        }

        public CsSeElement(By by, int index)
        {
            el = GetDriver().FindElements(by)[index];
        }


        /*
         * Functions for chained searches
         */
        public CsSeElement f(string cssSelector)
        {
            return new CsSeElement(el.FindElement(By.CssSelector(cssSelector)));
        }

        public CsSeElement f(string cssSelector, int index)
        {
            return new CsSeElement(el.FindElements(By.CssSelector(cssSelector))[index]);
        }

        public CsSeElementCollection ff(string cssSelector)
        {
            return new CsSeElementCollection(el.FindElements(By.CssSelector(cssSelector)));
        }

        public CsSeElement fx(string xpathSelector)
        {
            return new CsSeElement(el.FindElement(By.XPath(xpathSelector)));
        }

        public CsSeElement fx(string xpathSelector, int index)
        {
            return new CsSeElement(el.FindElements(By.XPath(xpathSelector))[index]);
        }

        public CsSeElementCollection ffx(string xpathSelector)
        {
            return new CsSeElementCollection(el.FindElements(By.XPath(xpathSelector)));
        }


        /*
         * CsSeElement operations
         */

        public void Click()
        {
            el.Click();
        }

        public void SendKeys(string val)
        {
            el.SendKeys(val);
        }

        public bool IsVisible()
        {
            return (el.Displayed);
        }

        public bool IsDisplayed(bool strict)
        {
            if(strict)
            {
                return (el.Displayed && el.Enabled);
            }
            else
            {
                return IsVisible();
            }
        }

        public string GetText()
        {
            return el.Text;
        }

        public string GetTextRootOnly(bool isStrict)
        {
            //Get element HTML, including outer tags
            string xmlElement = el.GetAttribute("outerHTML");

            //Wrap xml element html source in xml object
            return XmlUtils.GetRootElementTextValue(xmlElement, isStrict);
        }

        public void TakeScreenshot()
        {
            new CsSeScreenshot(GetDriver(), el).Save("C:/screenshots/", "poctest", true);
        }

        public void TakeScreenshot(string basePath, string name, bool addTimeStamp)
        {
            new CsSeScreenshot(GetDriver(), el).Save(basePath, name, addTimeStamp);
        }

        public Bitmap GetScreenAsBitmap()
        {
            return new CsSeScreenshot(
                GetDriver(),
                el)
                .GetBitmap();
        }

        public bool LooksIdenticalTo(string resourceNameSpace, string resourceName)
        {
            return false;
        }

        /*
         * Conditions - object-oriented.
         * Why: have one place to maintain it +  shorten this class to not include all logic.
         */

        /*
         * Is
         */
        public bool Is(Condition condition)
        {
            return condition.Apply(GetDriver(), el);
        }

        // HAS Aliases
        public bool Has(Condition condition)
        {
            return Is(condition);
        }

        /*
         * Should
         */

        //Should aliases
        public CsSeElement ShouldBe(params Condition[] conditions)
        {
            return Should(conditions).Execute(GetDriver(), el);
        }

        public CsSeElement ShouldHave(params Condition[] conditions)
        {
            return Should(conditions).Execute(GetDriver(), el);
        }

        /*
         * Should not
         */

        //Should not aliases
        public CsSeElement ShouldNotBe(params Condition[] conditions)
        {
            return ShouldNot(conditions).Execute(GetDriver(), el);
        }

        public CsSeElement ShouldNotHave(params Condition[] conditions)
        {
            return ShouldNot(conditions).Execute(GetDriver(), el);
        }

        /*
         * Coded waits
         */
        public CsSeElement WaitUntilHas(Condition condition)
        {
            return WaitUntil(
                condition,
                GetConfig().DefaultTimeoutMs,
                GetConfig().DefaultPollingIntervalMs
                ).Execute(GetDriver(), el);
        }

        public CsSeElement WaitUntilHas(Condition condition, long timeoutMs)
        {
            return WaitUntil(
                condition,
                timeoutMs,
                GetConfig().DefaultPollingIntervalMs
                ).Execute(GetDriver(), el);
        }

        public CsSeElement WaitUntilHas(Condition condition, long timeoutMs, long pollIntervalMs)
        {
            return WaitUntil(
                condition,
                timeoutMs,
                pollIntervalMs
                ).Execute(GetDriver(), el);
        }

        public CsSeElement WaitWhileHas(Condition condition)
        {
            return WaitWhile(
                condition,
                GetConfig().DefaultTimeoutMs,
                GetConfig().DefaultPollingIntervalMs
                ).Execute(GetDriver(), el);
        }

        public CsSeElement WaitWhileHas(Condition condition, long timeoutMs)
        {
            return WaitWhile(
                condition,
                timeoutMs,
                GetConfig().DefaultPollingIntervalMs
                ).Execute(GetDriver(), el);
        }

        public CsSeElement WaitWhileHas(Condition condition, long timeoutMs, long pollIntervalms)
        {
            return WaitWhile(
                condition,
                timeoutMs,
                pollIntervalms
                ).Execute(GetDriver(), el);
        }

        /*
         * Return webelement
         */
        public IWebElement GetWebElement()
        {
            return el;
        }
    }
}
