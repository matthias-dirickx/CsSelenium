using System.Collections.Concurrent;
using System;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.core;

using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;

namespace CsSeleniumFrame.src.statics
{
    /*
     * https://code-maze.com/singleton/
     */
    public sealed class CsSeDriver
    {
        private ConcurrentDictionary<int, IWebDriver> driverThreads = new ConcurrentDictionary<int, IWebDriver>();
        private static Lazy<CsSeDriver> instance = new Lazy<CsSeDriver>(() => new CsSeDriver());
        private static CsSeDriver Instance => instance.Value;

        /*
         * Private constructor to create ConcurrentDictionary and add the first driver.
         */
        private CsSeDriver()
        {
            AddDriverThread();
        }

        private static int GetThreadId()
        {
            return Thread.CurrentThread.GetHashCode();
        }

        private void AddDriverThread()
        {
            WebDriverTypes type = WebDriverTypes.Firefox;
            IWebDriver driver;

            switch (type)
            {
                case WebDriverTypes.Firefox:
                    FirefoxOptions ffo = new FirefoxOptions();
                    driver = new WebDriverFactory().CreateWebDriver(WebDriverTypes.Firefox, ffo);
                    //ffo.AddArguments("--headless");
                    driverThreads.AddOrUpdate(GetThreadId(), driver, (key, oldValue) => driver);
                    break;
                case WebDriverTypes.Chrome:
                    ChromeOptions co = new ChromeOptions();
                    driver = new WebDriverFactory().CreateWebDriver(type, co);
                    driverThreads.AddOrUpdate(GetThreadId(), driver, (key, oldValue) => driver);
                    break;
                case WebDriverTypes.InternetExplorer:
                    InternetExplorerOptions ieo = new InternetExplorerOptions();
                    ieo.IgnoreZoomLevel = true;
                    ieo.EnableNativeEvents = true;
                    driver = new WebDriverFactory().CreateWebDriver(type, ieo);
                    driverThreads.AddOrUpdate(GetThreadId(), driver, (key, oldValue) => driver);
                    break;
                case WebDriverTypes.Remote:
                    WebDriverFactory wdf = new WebDriverFactory();
                    wdf.RemoteAddress = new Uri("http://127.0.0.1:4444/wd/hub");
                    DriverOptions ro = new FirefoxOptions();
                    driver = wdf.CreateWebDriver(type, ro);
                    break;
                default:
                    FirefoxOptions ffod = new FirefoxOptions();
                    driver = new WebDriverFactory().CreateWebDriver(WebDriverTypes.Firefox, ffod);
                    //ffo.AddArguments("--headless");
                    driverThreads.AddOrUpdate(GetThreadId(), driver, (key, oldValue) => driver);
                    break;
            }
        }

        private static void ResetDriver()
        {
            if(Instance.driverThreads.ContainsKey(GetThreadId()))
            {
                IWebDriver d;
                Instance.driverThreads.TryRemove(GetThreadId(), out d);
            }
        }

        /*
         * Publicly available driver operations.
         */
        public static void QuitAndDestroy()
        {
            GetDriver().Quit();
            ResetDriver();
        }

        public static void CloseCurrentWindow()
        {
            GetDriver().Close();
            if (GetDriver().WindowHandles.Count == 0)
            {
                ResetDriver();
            }
        }

        /*
         * Publicly availabe GetDriver() Function.
         * Should be thread safef.
         */
        public static IWebDriver GetDriver()
        {
            if (!Instance.driverThreads.ContainsKey(GetThreadId()))
            {
                Instance.AddDriverThread();
            }
            
            return Instance.driverThreads[GetThreadId()];
        }
    }
}
