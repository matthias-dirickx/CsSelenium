using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CsSeleniumFrame.src
{
    class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(WebDriverType type, DriverOptions options)
        {
            switch(type)
            {
                case WebDriverType.Firefox:
                    return ADefaultFirefoxWebDriver((FirefoxOptions)options);
                case WebDriverType.Chrome:
                    return ADefaultChromeWebDriver((ChromeOptions)options);
                case WebDriverType.InternetExplorer:
                    return ADefaultIEDriver((InternetExplorerOptions)options);
                default:
                    return ADefaultFirefoxWebDriver((FirefoxOptions)options);
            }
        }

        private static IWebDriver ADefaultFirefoxWebDriver(FirefoxOptions options)
        {
            
            return new FirefoxDriver(
                        FirefoxDriverService
                        .CreateDefaultService(
                            @"C:/Tools/seleniumdrivers/geckodriver-v0.24.0-win64"),
                        options
                    );
        }

        private static IWebDriver ADefaultChromeWebDriver(ChromeOptions options)
        {
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
    }
}
