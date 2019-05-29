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

using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Core
{
    class LocalStorageManager
    {
        private IJavaScriptExecutor js;
        
        /// <summary>
        /// Creates LocalStorageManager with the assigned IWebDriver.
        /// Operations include getting and setting values in the local storage space.
        /// The operations are done by javascript executed by the Javascript Executor from Selenium WebDriver.
        /// </summary>
        /// <param name="driver"></param>
        public LocalStorageManager(IWebDriver driver)
        {
            this.js = (IJavaScriptExecutor)driver;
        }

        public void RemoveItemFromLocalStorage(string item)
        {
            js.ExecuteScript(
                $"window.localStorage.removeItem'{item}');"
                );
        }

        public bool IsItemPresentInLocalStorage(string item)
        {
            return !(js.ExecuteScript(
                $"return window.localStorage.getItem('{item}');") == null);
        }

        public string GetItemFromLocalStorage(string key)
        {
            return (string)js.ExecuteScript(
                $"return window.localStorage.key('{key}')");
        }

        public string GetKeyFromLocalStorage(int key)
        {
            return (string)js.ExecuteScript(
                $"return window.localStorage.key('{key}')");
        }

        public long GetLocalStorageLength()
        {
            return (long)js.ExecuteScript(
                "return window.localStorage.length;");
        }

        public void SetItemInLocalStorage(string item, string value)
        {
            js.ExecuteScript(
                $"window.localStorage.setItem('{item}', '{value}');");
        }

        public void ClearLocalStorage()
        {
            js.ExecuteScript(
                "window.localStorage.clear();");
        }
    }
}
