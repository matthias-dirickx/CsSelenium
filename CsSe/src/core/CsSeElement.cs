/*
 * Copyright 2019 Matthias Dirickx
 * 
 * This file is part of CsSeSelenium.
 * 
 * CsSeSelenium is free software:
 * you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License,
 * or (at your option) any later version.
 * 
 * CsSeSelenium is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * WITHOUT even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with CsSeSelenium.
 * 
 * If not, see http://www.gnu.org/licenses/.
 */

using System.Collections.ObjectModel;
using System.Drawing;

using OpenQA.Selenium;

using CsSeleniumFrame.src.CsSeActions;
using CsSeleniumFrame.src.CsSeConditions;
using CsSeleniumFrame.src.Statics;

using static CsSeleniumFrame.src.Statics.CsSeConfigurationManager;
using static CsSeleniumFrame.src.Statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeElement : IWebElement
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //Core element
        /// <summary>
        /// Initial WebElement on creating the CsSeElement type.
        /// 
        /// GetWebElement returns dynamically researched element.
        /// </summary>

        public IWebElement WebElement { get; private set; }

        public readonly By by;
        public readonly int index;
        public readonly CsSeElement parent;

        /**************************************************
         * CONSTRUCTORS
         ****************************************/

        /// <summary>
        /// Create CsSeElement wrapper for a IWebElement.
        /// </summary>
        /// <param name="by"></param>
        public CsSeElement(By by)
        {
            this.parent = null;
            this.by = by;
            this.index = 0;
            SetWebElement();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webElement"></param>
        public CsSeElement(IWebElement webElement)
        {
            this.parent = null;
            this.by = null;
            this.index = 0;
            SetWebElement();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <param name="index"></param>
        public CsSeElement(By by, int index)
        {
            this.parent = null;
            this.by = by;
            this.index = index;
            SetWebElement();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="by"></param>
        public CsSeElement(CsSeElement parent, By by)
        {
            this.parent = parent;
            this.by = by;
            this.index = 0;
            SetWebElement();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="by"></param>
        /// <param name="index"></param>
        public CsSeElement(CsSeElement parent, By by, int index)
        {
            this.parent = parent;
            this.by = by;
            this.index = index;
            SetWebElement();
        }

        private bool HasParent()
        {
            if (parent == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetWebElement()
        {
            if(HasParent())
            {
                WebElement = new FindElementAction().Execute(GetDriver(), by, index, parent.Refresh());
            }
            else
            {
                WebElement = new FindElementAction().Execute(GetDriver(), by, index, null);
            }
            
        }

        public CsSeElement Refresh()
        {
            SetWebElement();
            return this;
        }

        private string GetRecursiveElementIdentifier(string recursiveLocation, CsSeElement csSeElement)
        {
            string newRecursiveLocation;
            newRecursiveLocation = $"{csSeElement.by.ToString()}[{csSeElement.index}]";

            if (csSeElement.parent != null)
            {
                return $"{GetRecursiveElementIdentifier(newRecursiveLocation, csSeElement.parent)} -> {newRecursiveLocation}";
            }

            return newRecursiveLocation;
        }

        public string GetFullByTrace()
        {
            return GetRecursiveElementIdentifier("", this);
        }

        /**************************************************
         * SEARCH CHAINING
         ****************************************/

        /*
         * The functions are created with this instance as the parent element.
         */

        //By search operations
        public CsSeElement f(By by)
        {
            return new CsSeElement(this, by);
        }

        public CsSeElement f(By by, int index)
        {
            return new CsSeElement(this, by, index);
        }

        public CsSeElementCollection ff(By by)
        {
            return new CsSeElementCollection(WebElement.FindElements(by));
        }

        //CSS Search operations
        public CsSeElement f(string cssSelector)
        {
            return new CsSeElement(this, By.CssSelector(cssSelector));
        }

        public CsSeElement f(string cssSelector, int index)
        {
            return new CsSeElement(this, By.CssSelector(cssSelector), index);
        }

        public CsSeElementCollection ff(string cssSelector)
        {
            return new CsSeElementCollection(WebElement.FindElements(By.CssSelector(cssSelector)));
        }

        //XPath search operations
        public CsSeElement fx(string xpathSelector)
        {
            return new CsSeElement(this, By.XPath(xpathSelector));
        }

        public CsSeElement fx(string xpathSelector, int index)
        {
            return new CsSeElement(this, By.XPath(xpathSelector), index);
        }

        public CsSeElementCollection ffx(string xpathSelector)
        {
            return new CsSeElementCollection(WebElement.FindElements(By.XPath(xpathSelector)));
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
            return condition.Apply(GetDriver(), this);
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
            return CsSeActionList.Should(conditions).Execute(GetDriver(), this);
        }

        public CsSeElement ShouldHave(params Condition[] conditions)
        {
            return CsSeActionList.Should(conditions).Execute(GetDriver(), this);
        }

        /*
         * Should not
         */

        //Should not aliases
        public CsSeElement ShouldNotBe(params Condition[] conditions)
        {
            return CsSeActionList.ShouldNot(conditions).Execute(GetDriver(), this);
        }

        public CsSeElement ShouldNotHave(params Condition[] conditions)
        {
            return CsSeActionList.ShouldNot(conditions).Execute(GetDriver(), this);
        }

        /*
         * Coded waits
         */
        public CsSeElement WaitUntilHas(Condition condition)
        {
            return WaitUntilHas(
                condition,
                GetConfig().DefaultTimeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitUntilHas(Condition condition, long timeoutMs)
        {
            return WaitUntilHas(
                condition,
                timeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitUntilHas(Condition condition, long timeoutMs, long pollIntervalMs)
        {
            return CsSeActionList.WaitUntil(
                condition,
                timeoutMs,
                pollIntervalMs
                ).Execute(GetDriver(), this);
        }

        public CsSeElement WaitUntilIs(Condition condition)
        {
            return WaitUntilHas(
                condition,
                GetConfig().DefaultTimeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitUntilIs(Condition condition, long timeoutMs)
        {
            return WaitUntilHas(
                condition,
                timeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitUntilIs(Condition condition, long timeoutMs, long pollIntervalMs)
        {
            return CsSeActionList.WaitUntil(
                condition,
                timeoutMs,
                pollIntervalMs
                ).Execute(GetDriver(), this);
        }

        public CsSeElement WaitWhileHas(Condition condition)
        {
            return WaitWhileHas(
                condition,
                GetConfig().DefaultTimeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitWhileHas(Condition condition, long timeoutMs)
        {
            return WaitWhileHas(
                condition,
                timeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitWhileHas(Condition condition, long timeoutMs, long pollIntervalms)
        {
            return CsSeActionList.WaitWhile(
                condition,
                timeoutMs,
                pollIntervalms
                ).Execute(GetDriver(), this);
        }

        public CsSeElement WaitWhileIs(Condition condition)
        {
            return WaitWhileHas(
                condition,
                GetConfig().DefaultTimeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitWhileIs(Condition condition, long timeoutMs)
        {
            return WaitWhileIs(
                condition,
                timeoutMs,
                GetConfig().DefaultPollingIntervalMs
                );
        }

        public CsSeElement WaitWhileIs(Condition condition, long timeoutMs, long pollIntervalms)
        {
            return CsSeActionList.WaitWhile(
                condition,
                timeoutMs,
                pollIntervalms
                ).Execute(GetDriver(), this);
        }


        /*
         * IWebElement implementation
         */

        public string RecursiveBy => GetFullByTrace();

        public string TagName => WebElement.TagName;
        public string Text => WebElement.Text;
        public bool Enabled => WebElement.Enabled;
        public bool Selected => WebElement.Selected;
        public Point Location => WebElement.Location;
        public Size Size => WebElement.Size;
        public bool Displayed => WebElement.Displayed;
        public void Click()
        {
            Statics.CsSeActionList.Click().Execute(GetDriver(), this);
        }

        public void SendKeys(string val)
        {
            Statics.CsSeActionList.SendKeys(val).Execute(GetDriver(), this);
        }

        public string GetText()
        {
            return WebElement.Text;
        }

        public bool IsVisible()
        {
            return (WebElement.Displayed);
        }

        public bool IsDisplayed(bool strict)
        {
            if(strict)
            {
                return (WebElement.Displayed && WebElement.Enabled);
            }
            else
            {
                return IsVisible();
            }
        }

        public string GetTextRootOnly(bool isStrict)
        {
            //Get element HTML, including outer tags
            string xmlElement = WebElement.GetAttribute("outerHTML");

            //Wrap xml element html source in xml object
            return XmlUtils.GetRootElementTextValue(xmlElement, isStrict);
        }

        public void Clear()
        {
            WebElement.Clear();
        }

        public void Submit()
        {
            WebElement.Submit();
        }

        public string GetAttribute(string attributeName)
        {
            return WebElement.GetAttribute(attributeName);
        }

        public string GetProperty(string propertyName)
        {
            return WebElement.GetProperty(propertyName);
        }

        public string GetCssValue(string propertyName)
        {
            return WebElement.GetCssValue(propertyName);
        }

        public IWebElement FindElement(By by)
        {
            return WebElement.FindElement(by);
        }

        /// <summary>
        /// In the framework preferred to work with ff() to return a CsSeElementCollection rather than the ReadOnlyCollection.
        /// Supported because of flexibility reasons.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WebElement.FindElements(by);
        }

        /*
         * Custom implementations
         */
        public void TakeScreenshot()
        {
            new CsSeScreenshot(GetDriver(), WebElement).Save("C:/screenshots/", "poctest", true);
        }

        public void TakeScreenshot(string basePath, string name, bool addTimeStamp)
        {
            new CsSeScreenshot(GetDriver(), WebElement).Save(basePath, name, addTimeStamp);
        }

        public Bitmap GetScreenAsBitmap()
        {
            return new CsSeScreenshot(
                GetDriver(),
                WebElement)
                .GetBitmap();
        }

        /*
         * Extra implementations
         * */
         public CsSeElement DragAndDropTo(CsSeElement target)
        {
            return CsSeActionList.DragAndDrop(target).Execute(GetDriver(), this);
        }

        public CsSeElement ScrollIntoView()
        {
            return CsSeActionList.ScrollIntoView().Execute(GetDriver(), this);
        }
    }
}
