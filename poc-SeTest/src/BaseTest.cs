using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumFrame.src.Core;

using static CsSeleniumFrame.src.statics.CsSe;
using static CsSeleniumFrame.src.statics.CsSeDriver;
using CsSeleniumFrame.src.Logger;
using Newtonsoft.Json;

namespace CsSeleniumImplExample.src
{
    public class BaseTest
    {
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
                    CsSeEventLog.GetSerializableEventCollectorForAllThreads("report"),
                    Formatting.Indented));
        }
    }
}
