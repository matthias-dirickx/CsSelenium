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
using OpenQA.Selenium.Firefox;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeProperties
    {

        public bool IsHeadless { get; set; }
        public WebDriverTypes WebDriverType { get; set; }
        public Uri RemoteUrl { get; set; }
        public DriverOptions WebDriverOptions { get; set; }
        public PageLoadStrategy DriverPageLoadStrategy { get; set; }
        public TimeSpan TimeOutsPageLoad { get; set; }
        public TimeSpan TimeOutsJavaScript { get; set; }
        public TimeSpan TimeOutsImplicit { get; set; }

        /// <summary>
        /// Default is false.
        /// 
        /// When true all overhead type actions are written to the CsSeEventLog.
        /// 
        /// </summary>
        public bool LogOverheadEntries { get; set;
        }
        /// <summary>
        /// Overall timeout used in webdriver setup in milliseconds.
        /// This is specific to the CsSelenium code and is not part of the basic setup.
        /// 
        /// If you want to use webdriver default settings for handling timeouts use:
        /// - <see cref="TimeOutsPageLoad"/>
        /// - <see cref="TimeOutsJavaScript"/>
        /// - <see cref="TimeOutsImplicit"/>
        /// </summary>
        public double CsSeTimeout { get; set; }

        public int DefaultPollingIntervalMs { get; set; }
        public int DefaultTimeoutMs { get; set; }

        /// <summary>
        /// Defualt false.
        /// If true, then all assertions will be done and the thrown error will be logged.
        /// Please note that if this is true, the test in the test runner will be marked as Passed.
        /// </summary>
        public bool ContinueOnCsSeAssertionFail { get; set; }
        public bool ContinueOnWebDriverException { get; set; }
        public string BaseUrl {get; set;}
        public Uri ReportBasePath { get; set; }

        public bool ScreenshotOnFail { get; set; }
        public string ScreenshotBasePath { get; set; }

        public CsSeProperties()
        {
            // Browser
            IsHeadless = false;
            WebDriverType = WebDriverTypes.Firefox;
            RemoteUrl = new Uri("http://127.0.0.1:4444/wd/hub");
            WebDriverOptions = new FirefoxOptions();

            DriverPageLoadStrategy = PageLoadStrategy.None;

            TimeOutsPageLoad = new TimeSpan(0, 0, 0, 0, 30000);
            TimeOutsJavaScript = new TimeSpan(0, 0, 0, 0, 0);
            TimeOutsImplicit = new TimeSpan(0, 0, 0, 0, 0);
            CsSeTimeout = 10000;

            //Timeouts
            DefaultPollingIntervalMs = 100;
            DefaultTimeoutMs = 5000;

            //Meta
            ReportBasePath = new Uri("c:/CsSelenium/reports");

            //Framework
            LogOverheadEntries = true;

            BaseUrl = "";

            ContinueOnCsSeAssertionFail = false;
            
            ScreenshotOnFail = false;
            ScreenshotBasePath = "c:/screenshots";

            
        }

        public CsSeProperties(string path)
        {
            
        }

        public void Update(string path)
        {

        }
    }
}
