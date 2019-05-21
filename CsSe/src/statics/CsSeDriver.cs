using System.Collections.Concurrent;
using System;
using System.Threading;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Actions;

using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;
using static CsSeleniumFrame.src.Actions.WebDriverTypes;

namespace CsSeleniumFrame.src.statics
{
    /*
     * https://code-maze.com/singleton/
     */
    public sealed class CsSeDriver
    {
        //Dictionary holding the driver instances.
        private ConcurrentDictionary<int, IWebDriver> driverThreads;

        //Managing singleton
        private static Lazy<CsSeDriver> instance = new Lazy<CsSeDriver>(() => new CsSeDriver());
        private static CsSeDriver Instance => instance.Value;

        private CsSeDriver()
        {
            driverThreads = new ConcurrentDictionary<int, IWebDriver>();
            AddDriverThread();
        }

        /// <summary>
        /// <para>
        /// Get managed driver.
        /// </para>
        /// <para>
        /// The driver is managed per thread.
        /// </para>
        /// <para>
        /// Settings are taken from the Configuration object. Call this object by calling to <see cref="CsSeConfigurationManager"/></para>
        /// <para>
        /// To make efficient use, import the manager with <code>using static
        /// </code> to be able to call the configuration as <code>GetConfig()</code>
        /// </para>
        /// 
        /// </summary>
        /// <returns><see cref="OpenQA.Selenium.IWebDriver"/></returns>
        public static IWebDriver GetDriver()
        {
            if (!Instance.driverThreads.ContainsKey(GetThreadId()))
            {
                Instance.AddDriverThread();
            }

            return Instance.driverThreads[GetThreadId()];
        }

        /// <summary>
        /// Register custom driver object in pool.
        /// </summary>
        /// <param name="driver"></param>
        public static void SetDriver(IWebDriver driver)
        {
            int tid = GetThreadId();
            if (!Instance.driverThreads.ContainsKey(tid))
            {
                Instance.driverThreads[tid] = driver;
            }
            else
            {
                Instance.AddDriverThread(driver);
            }
        }

        // //////////////////////////////////////////////////////////

        /*
         * Utility functions to manage the driver dictionary.
         */
        private static int GetThreadId()
        {
            return Thread.CurrentThread.GetHashCode();
        }

        private void AddDriverThread()
        {
            WebDriverTypes type = GetConfig().WebDriverType;
            IWebDriver driver;

            if(type != Remote)
            {
                driver = new WebDriverFactory().CreateWebDriver(type, GetConfig().WebDriverOptions);
            }
            else
            {
                WebDriverFactory wdf = new WebDriverFactory();
                wdf.RemoteAddress = GetConfig().RemoteUrl;
                driver = wdf.CreateWebDriver(type, GetConfig().WebDriverOptions);
            }

            driverThreads.AddOrUpdate(GetThreadId(), driver, (key, oldValue) => driver);
        }

        private void AddDriverThread(IWebDriver driver)
        {
            driverThreads.AddOrUpdate(GetThreadId(), driver, (key, oldValue) => driver);
        }

        private static void ResetDriver()
        {
            if(Instance.driverThreads.ContainsKey(GetThreadId()))
            {
                IWebDriver d;
                Instance.driverThreads.TryRemove(GetThreadId(), out d);
            }
        }

        // ///////////////////////////////////////////////////////////////

        /*
         * Publicly available driver operations.
         */
        //TODO Externalise to CsSeDriverThreadInstanace class when bloating over 200 lines.
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
    }
}
