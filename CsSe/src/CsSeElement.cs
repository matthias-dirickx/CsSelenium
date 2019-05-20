﻿using OpenQA.Selenium;

using CsSeleniumFrame.src.util;
using static CsSeleniumFrame.src.statics.CsSeDriver;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System.Resources;
using System.Drawing;

namespace CsSeleniumFrame.src
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
            ResourceManager rm = new ResourceManager(resourceNameSpace, typeof(Resources).Assembly);

            Bitmap expected = (Bitmap)rm.GetObject(resourceName);
            Bitmap actual = GetScreenAsBitmap();

            return ImageCompare.AreIdentical(expected, actual);
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
