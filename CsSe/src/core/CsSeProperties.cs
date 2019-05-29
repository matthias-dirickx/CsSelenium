using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using CsSeleniumFrame.src.Actions;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeProperties
    {

        public bool IsHeadless { get; set; }
        public WebDriverTypes WebDriverType { get; set; }
        public Uri RemoteUrl { get; set; }
        public DriverOptions WebDriverOptions { get; set; }

        public int DefaultPollingIntervalMs { get; set; }
        public int DefaultTimeoutMs { get; set; }

        /// <summary>
        /// Defualt false.
        /// If true, then all assertions will be done and the thrown error will be logged.
        /// Please note that if this is true, the test in the test runner will be marked as Passed.
        /// </summary>
        public bool ContinueOnCsSeAssertionFail { get; set; }
        public bool ContinueOnWebDriverException { get; set; }

        public Uri ReportBasePath { get; set; }

        public CsSeProperties()
        {
            // Browser
            IsHeadless = false;
            WebDriverType = WebDriverTypes.Firefox;
            RemoteUrl = new Uri("http://127.0.0.1:4444/wd/hub");
            WebDriverOptions = new FirefoxOptions();

            //Timeouts
            DefaultPollingIntervalMs = 100;
            DefaultTimeoutMs = 5000;

            //Meta
            ReportBasePath = new Uri("c:/CsSelenium/reports");

            //Framework
            ContinueOnCsSeAssertionFail = false;
        }

        public CsSeProperties(string path)
        {
            
        }

        public void Update(string path)
        {

        }
    }
}
