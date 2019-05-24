using System;
using System.Collections.Concurrent;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;

using CsSeleniumFrame.src.Core;

using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;
using static CsSeleniumFrame.src.Core.WebDriverTypes;

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

            logger.Debug("Set Driver to return...");
            IWebDriver driver = Instance.driverThreads[GetThreadId()];
            logger.Debug($"Returning driver {GetDriverName(driver)} for thread {tid}.");

            return driver;
        }

        public static string GetDriverName(IWebDriver driver)
        {
            return driver.GetType().Name;
        }

        public static string GetDriverCapabilitiesAsString(IWebDriver driver)
        {
            return ((RemoteWebDriver)driver).Capabilities.ToString();
        }

        public static ICapabilities GetDriverCapabilities(IWebDriver driver)
        {
            return ((RemoteWebDriver)driver).Capabilities;
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
            logger.Info("Starting add driver to dictionary...");
            WebDriverTypes type = GetConfig().WebDriverType;
            IWebDriver driver;

            if(type != Remote)
            {
                logger.Debug("Driver is not of type remote.");
                logger.Debug("Instantiate Webdriver factory...");
                driver = new WebDriverFactory().CreateWebDriver(type, GetConfig().WebDriverOptions);
                logger.Debug("Driver object is defined.");
                logger.Debug($"Driver of type '{GetDriverName(driver)}'.\nFull Driver description and reference:\n{GetDriverCapabilitiesAsString(driver)}");
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
                logger.Debug($"Driver of type '{GetDriverName(driver)}'.\nFull Driver description and reference:\n{GetDriverCapabilitiesAsString(driver)}");
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

        private static void RemoveDriverThread()
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
            RemoveDriverThread();
        }
        
        public static void CloseCurrentWindow()
        {
            GetDriver().Close();
            if (GetDriver().WindowHandles.Count == 0)
            {
                RemoveDriverThread();
            }
        }
    }
}
