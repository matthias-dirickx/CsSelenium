using System.Collections.Concurrent;
using System;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace CsSeleniumFrame.src.statics
{
    /*
     * https://code-maze.com/singleton/
     */
    public sealed class CsSeDriver
    {
        public WebDriverType type { get; set; }

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
            switch (type)
            {
                case WebDriverType.Firefox:
                    FirefoxOptions ffo = new FirefoxOptions();
                    IWebDriver ffwd = WebDriverFactory.CreateWebDriver(WebDriverType.Firefox, ffo);
                    //ffo.AddArguments("--headless");
                    driverThreads.AddOrUpdate(GetThreadId(), ffwd, (key, oldValue) => ffwd);
                    break;
                case WebDriverType.Remote:
                    //Do stuff
                    break;
                default:
                    FirefoxOptions ffod = new FirefoxOptions();
                    IWebDriver dwd = WebDriverFactory.CreateWebDriver(WebDriverType.Firefox, ffod);
                    //ffo.AddArguments("--headless");
                    driverThreads.AddOrUpdate(GetThreadId(), dwd, (key, oldValue) => dwd);
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
