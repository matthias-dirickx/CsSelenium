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

using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using static CsSeleniumFrame.src.Statics.CsSeConfigurationManager;

namespace CsSeleniumFrame.src.Core
{
    public class WebDriverFactory
    {
        public Uri RemoteAddress { get; set; }

        public WebDriverFactory()
        {

        }

        public IWebDriver CreateWebDriver(WebDriverTypes type, DriverOptions options)
        {
            switch(type)
            {
                case WebDriverTypes.Firefox:
                    return ADefaultFirefoxWebDriver((FirefoxOptions)options);
                case WebDriverTypes.Chrome:
                    return ADefaultChromeWebDriver((ChromeOptions)options);
                case WebDriverTypes.InternetExplorer:
                    return ADefaultIEDriver((InternetExplorerOptions)options);
                case WebDriverTypes.Remote:
                    return ADefaultRemoteDriver(RemoteAddress, options);
                default:
                    return ADefaultFirefoxWebDriver((FirefoxOptions)options);
            }
        }

        private static IWebDriver ADefaultFirefoxWebDriver(FirefoxOptions options)
        {
            if(GetConfig().IsHeadless)
            {
                options.AddArgument("--headless");
            }
            
            return new FirefoxDriver(
                        FirefoxDriverService
                        .CreateDefaultService(
                            @"C:/Tools/seleniumdrivers/geckodriver-v0.24.0-win64"),
                        options
                    );
        }

        private static IWebDriver ADefaultChromeWebDriver(ChromeOptions options)
        {
            if(GetConfig().IsHeadless)
            {
                options.AddArgument("--headless");
            }

            return new ChromeDriver(
                        ChromeDriverService
                        .CreateDefaultService(
                            @"C:/Tools/seleniumdrivers/chromedriver_win32"),
                        options
                    );
        }

        private static IWebDriver ADefaultIEDriver(InternetExplorerOptions options)
        {
            return new InternetExplorerDriver(
                InternetExplorerDriverService
                .CreateDefaultService(
                    @"C:/Tools/seleniumdrivers/chromedriver_win32"),
                options
                );
        }

        private static IWebDriver ADefaultRemoteDriver(Uri uri, DriverOptions options)
        {
            return new RemoteWebDriver(
                uri,
                options);
        }
    }
}
