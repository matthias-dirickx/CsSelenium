﻿/*
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

using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;

using static CsSeleniumFrame.src.Statics.CsSeDriver;
using CsSeleniumFrame.src.CsSeActions;

namespace CsSeleniumFrame.src.Statics
{
    /// <summary>
    /// This class contains a set of static references.
    /// The references contain implementations to ease up on the boilerplating.
    /// 
    /// The class is best imported as using static.
    /// 
    /// This way, to find an element, type:
    /// f("my.scc selector")
    /// 
    /// To open a page type:
    /// open("http://something.com")
    /// 
    /// The command will open a webdriver as defined in the configuration.
    /// </summary>
    public class CsSe
    {
        /// <summary>
        /// Find element by Selenium.By command.
        /// 
        /// Before calling this, the desired page should be opened.
        /// <see cref="open(string)"/>
        /// </summary>
        /// <code>
        /// f(By.CssSelector("my.css selector"))</code>
        /// <param name="by"></param>
        /// <returns><see cref="CsSeElement"/></returns>
        public static CsSeElement f(By by)
        {
            return new CsSeElement(by);
        }

        /// <summary>
        /// Find element by Selenium.By command at defined index.
        /// 
        /// Before calling this, the desired page should be opened.
        /// <see cref="open(string)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// f(By.CssSelector("my.css selector"), 2)</code>
        /// </example>
        /// <param name="by"></param>
        /// <param name="index"></param>
        /// <returns><see cref="CsSeElement"/></returns>
        public static CsSeElement f(By by, int index)
        {
            return new CsSeElement(by, index);
        }

        /// <summary>
        /// Find element by css selector.
        /// 
        /// Before calling this, the desired page should be opened.
        /// <see cref="open(string)"/>
        /// </summary>
        /// <example>
        /// <code>
        /// f("my.css selector")
        /// </code></example>
        /// <param name="cssSelector"></param>
        /// <returns><see cref="CsSeElement"/></returns>
        public static CsSeElement f(string cssSelector)
        {
            return f(By.CssSelector(cssSelector));
        }

        /// <summary>
        /// Find element by css selector at defined index.
        /// 
        /// Before calling this, the desired page should be opened.
        /// </summary>
        /// <example>
        /// <code>
        /// private CsSeElement GetMySelectorElement() { f("my.css selector", 3); }
        /// </code></example>
        /// <param name="cssSelector"></param>
        /// <param name="index"></param>
        /// <returns><see cref="CsSeElement"/></returns>
        public static CsSeElement f(string cssSelector, int index)
        {
            return f(By.CssSelector(cssSelector), index);
        }

        public static CsSeElement fx(string xpathSelector)
        {
            return f(By.XPath(xpathSelector));
        }

        public static CsSeElement fx(string xpathSelector, int index)
        {
            return f(By.XPath(xpathSelector), index);
        }

        public static CsSeElementCollection ff(By by)
        {
            return new CsSeElementCollection(by);
        }
        public static CsSeElementCollection ff(string cssSelector)
        {
            return ff(By.CssSelector(cssSelector));
        }
        public static CsSeElementCollection ffx(string xpathSelector)
        {
            return ff(By.XPath(xpathSelector));
        }

        /// <summary>
        /// Take a screenshot and save it in the default location.
        /// 
        /// Warning: For Windows 10 make sure that the OS-level settings are set at 100% rather than the default 125% on Windows 10 systems with high-resolution screens.
        /// 
        /// The screenshot will be stored in a standard location:
        /// C:/screenshots (with a datetimestamp in png format)
        /// 
        /// To define your own location, see <see cref="TakeScreenshot(string, string, bool)"/>
        /// </summary>
        public static void TakeScreenshot()
        {
            new CsSeScreenshot(
                GetDriver())
                    .Save(
                        "C:/screenshots",
                        "",
                        true);
        }

        /// <summary>
        /// Take a screenshot and save it to the provided location;
        /// 
        /// Format: PNG
        /// Name: parameter name
        /// Add datetime stamp with bool parameter addTimeStamp.
        ///     - True  --> Add timestamp (prepend to name)
        ///     - False --> Do not add timestamp
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="name"></param>
        /// <param name="addTimeStamp"></param>
        public static void TakeScreenshot(string basePath, string name, bool addTimeStamp)
        {
            new CsSeScreenshot(
                GetDriver())
                    .Save(
                        basePath,
                        name,
                        addTimeStamp);
        }

        /// <summary>
        /// Open a url.
        /// 
        /// Make sure to include the protocol:
        ///     - http://
        ///     - https://
        ///     
        /// </summary>
        /// <param name="url"></param>
        public static void open(string url)
        {
            new OpenUrlAction(url).Execute(GetDriver(), null);
        }
    }
}
