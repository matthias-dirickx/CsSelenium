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

using static CsSeleniumFrame.src.Statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeCookieManager
    {
        public static void SetCookie(string name, string value)
        {
            Cookie c = new Cookie(name, value);
            SetCookie(c);
        }

        public static void SetCookie(Cookie c)
        {
            GetDriver().Manage().Cookies.AddCookie(c);
        }

        public static string GetCookieValue(string name)
        {
            return GetDriver()
                .Manage()
                .Cookies
                .GetCookieNamed(name)
                .Value;
        }

        public static Cookie GetCookie(string name)
        {
            return GetDriver()
                .Manage()
                .Cookies
                .GetCookieNamed(name);
        }
    }
}
