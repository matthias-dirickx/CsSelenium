using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using CsSeleniumFrame.src.core;

namespace CsSeleniumFrame.src.util
{
    public class CsSeProperties
    {

        public bool IsHeadless { get; set; }
        public WebDriverTypes WebDriverType { get; set; }
        public Uri RemoteUrl { get; set; }
        public DriverOptions WebDriverOptions { get; set; }

        public CsSeProperties()
        {
            IsHeadless = false;
            WebDriverType = WebDriverTypes.Firefox;
            RemoteUrl = new Uri("http://127.0.0.1:4444/wd/hub");
            WebDriverOptions = new FirefoxOptions();
        }

        public CsSeProperties(string path)
        {
            
        }

        public void Update(string path)
        {

        }
    }
}
