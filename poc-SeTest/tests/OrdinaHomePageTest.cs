using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumImplExample.src;
using CsSeleniumImplExample.pages;
using CsSeleniumPoc.pages;

using static CsSeleniumFrame.src.Statics.CsSeCondition;

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

        [TestMethod]
        public void OrdinaScrollDownAndDoSomethingTest()
        {
            new OrdinaBelgiumHomePage().RelatedArticles.ArticleInstance(4).ScrollIntoView().ShouldBe(Visible).WaitWhileIs(Not(Visible), 1000);
        }

        [TestMethod]
        public void OrdinaChooseLanguageSelectTest()
        {
        }
    }
}
