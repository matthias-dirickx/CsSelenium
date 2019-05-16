using OpenQA.Selenium;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSelenium.src;
using CsSelenium.pages;

namespace CsSelenium.tests
{
    [TestClass]
    public class OrdinaHomePageTest : BaseTest
    {

        [TestMethod]
        public void OrdinaPocTest()
        {
            new OrdinaBelgiumHeaderPage()
                .Action_SearchFor("test automation")
                .GetHeader()
                .TakeScreenshotFromPage()
                .TakeScreenShotFromLogo()
                .ClickLogo();
        }

        [TestMethod]
        public void OrdinaTakeScreenshotTest()
        {
            new OrdinaBelgiumHeaderPage()
                .TakeScreenShotFromLogo();
        }

        [TestMethod]
        public void OrdinaClickHomeLogo()
        {
            new OrdinaBelgiumHeaderPage()
                .ClickLogo();
        }
    }
}
