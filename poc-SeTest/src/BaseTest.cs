using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumFrame.src.util;

using static CsSeleniumFrame.src.statics.CsSe;
using static CsSeleniumFrame.src.statics.CsSeDriver;

namespace CsSelenium.src
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
    }
}
