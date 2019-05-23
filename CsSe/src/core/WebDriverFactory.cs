using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;

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
