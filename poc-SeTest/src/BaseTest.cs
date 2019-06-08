using System.Drawing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Logger;

using static CsSeleniumFrame.src.Statics.CsSe;
using static CsSeleniumFrame.src.Statics.CsSeDriver;
using static CsSeleniumFrame.src.Statics.CsSeConfigurationManager;
using OpenQA.Selenium.Chrome;

namespace CsSeleniumImplExample.src
{
    [TestClass]
    public class BaseTest
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            NLog.LogManager.GetCurrentClassLogger().Debug("Enter Assembly Initialize method.");
            CsSeEventLog.addListener("report", new EventCollector());
        }

        [TestInitialize]
        public void Initialize()
        {
            /*
             * Standard init
             *     - Go to start page
             *     - do basic setup
             *     
             * In this case: demonstrate setting the cookies disclaimer popup.
             * 
             */


            GetConfig().WebDriverType = WebDriverTypes.Chrome;
            GetConfig().WebDriverOptions = new ChromeOptions();
            GetConfig().ContinueOnCsSeAssertionFail = true;
            GetConfig().ContinueOnWebDriverException = true;

            open("https://www.ordina.be");
            CsSeCookieManager.SetCookie("catAccCookies", "1");
            GetDriver().Navigate().Refresh();
            GetDriver().Manage().Window.Size = new Size(1920, 1080);
        }

        [TestCleanup]
        public void Cleanup()
        {
            QuitAndDestroy();
            
        }

        [AssemblyCleanup]
        public static void AssamblyCleanup ()
        {
            NLog.LogManager.GetCurrentClassLogger().Debug("Enter Assembly Cleanup method.");
            NLog.LogManager.GetCurrentClassLogger().Info(
                JsonConvert
                .SerializeObject(
                    CsSeEventLog.GetFlattenedSerializableEventCollectorForAllThreads("report"),
                    Formatting.Indented));
        }
    }
}
