using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using CsSeleniumFrame.src.Actions;

namespace CsSeleniumFrame.src.util
{
    public class CsSeProperties
    {

        public bool IsHeadless { get; set; }
        public WebDriverTypes WebDriverType { get; set; }
        public Uri RemoteUrl { get; set; }
        public DriverOptions WebDriverOptions { get; set; }

        public int DefaultPollingIntervalMs { get; set; }
        public int DefaultTimeoutMs { get; set; }

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
        }

        public CsSeProperties(string path)
        {
            
        }

        public void Update(string path)
        {

        }
    }
}
