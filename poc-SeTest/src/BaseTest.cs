using System.Drawing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Logger;

using static CsSeleniumFrame.src.Statics.CsSe;
using static CsSeleniumFrame.src.Statics.CsSeDriver;
using static CsSeleniumFrame.src.Statics.CsSeConfigurationManager;


namespace CsSeleniumImplExample.src
{
    public class BaseTest
    {
        [AssemblyInitialize]
        public void AssemblyInitialize(TestContext testContext)
        {
            
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

            CsSeEventLog.addListener("report", new EventCollector());

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
            NLog.LogManager.GetCurrentClassLogger().Info(
                JsonConvert
                .SerializeObject(
                    CsSeEventLog.GetFlattenedSerializableEventCollectorForAllThreads("report"),
                    Formatting.Indented));
        }

        [AssemblyCleanup]
        public void AssamblyCleanup ()
        {
            
        }
    }
}
