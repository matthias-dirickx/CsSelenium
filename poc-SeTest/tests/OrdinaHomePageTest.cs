using System.Resources;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumImplExample.src;
using CsSeleniumImplExample.pages;

namespace CsSeleniumPoc.tests
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
                .Verify_LogoIsVisible()
                .TakeScreenShotFromLogo();
        }

        [TestMethod]
        public void OrdinaClickHomeLogo()
        {
            new OrdinaBelgiumHeaderPage()
                .Verify_LogoIsVisible()
                .ClickLogo();
        }
    }
}
