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
                .TakeScreenshot()
                .TakeScreenShotFromLogo()
                .ClickLogo();
        }

        [TestMethod]
        public void OrdinaClickHomeLogo()
        {
            new OrdinaBelgiumHeaderPage()
                .ClickLogo();
        }
    }
}
