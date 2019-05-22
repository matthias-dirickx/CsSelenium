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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //Dictionary holding the driver instances.
        private ConcurrentDictionary<int, IWebDriver> driverThreads;

        //Managing singleton
        private static Lazy<CsSeDriver> instance = new Lazy<CsSeDriver>(() => new CsSeDriver());
        private static CsSeDriver Instance => instance.Value;

        private CsSeDriver()
        {
            logger.Info("Creating new CsSeDriver instance...");
            driverThreads = new ConcurrentDictionary<int, IWebDriver>();
            AddDriverThread();
            logger.Info("CsSeDriver instance instantiated.");
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
            int tid = GetThreadId();
            logger.Debug($"Get driver for thread {tid}");
            if (!Instance.driverThreads.ContainsKey(GetThreadId()))
            {
                logger.Debug($"Driver for thread {tid} not found. Adding new one...");
                Instance.AddDriverThread();
            }

            logger.Debug($"Return driver for thread {tid}");
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
            logger.Info("Starting add driver...");
            WebDriverTypes type = GetConfig().WebDriverType;
            IWebDriver driver;

            if(type != Remote)
            {
                logger.Debug("Driver is not of type remote.");
                driver = new WebDriverFactory().CreateWebDriver(type, GetConfig().WebDriverOptions);
                logger.Debug("Driver object is defined.");
            }
            else
            {
                logger.Debug("Driver is of type remote");
                logger.Debug("Instantiate Webdriver factory.");
                WebDriverFactory wdf = new WebDriverFactory();
                logger.Debug("Assign address from configuration.");
                wdf.RemoteAddress = GetConfig().RemoteUrl;

                driver = wdf.CreateWebDriver(type, GetConfig().WebDriverOptions);
                logger.Debug("Driver object is defined.");
            }

            int tid = GetThreadId();

            logger.Info($"Add driver to dictionary for thread {tid}.");
            driverThreads.AddOrUpdate(tid, driver, (key, oldValue) => driver);
            logger.Info($"Driver of type {type} added for thread {tid}.");
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
