using CsSeleniumFrame.src.Core;

using static CsSeleniumFrame.src.Statics.CsSe;
using static CsSeleniumFrame.src.Statics.CsSeCondition;

namespace CsSeleniumImplExample.pages
{
    class OrdinaBelgiumHeaderPage
    {
        /*
         * Elements
         */
        private CsSeElement Header() { return f("header#banner"); }
        private CsSeElement OrdinaLogo() { return Header().f("a.brand img"); }
        private CsSeElement SearchToggle() { return Header().f("button#search-toggle"); }
        private CsSeElement SearchInputField() { return Header().f("form.searchform input#header-search-field"); }
        private CsSeElement SearchButton() { return Header().f("form.searchform input#search-submit"); }


        /*
         * Actions
         */

        /// <summary>
        /// Click the Ordina logo in the header.
        /// Essentially this should return the home page as a whole, but for poc returns header.
        /// </summary>
        /// <returns>OrdinaBelgiumHeaderPage</returns>
        public OrdinaBelgiumHeaderPage ClickLogo()
        {
            OrdinaLogo().Click();

            return this;
        }

        /// <summary>
        /// Click the search toggle in the header.
        /// This method just clicks. There is no check whether it is already open.
        ///
        /// To assure the search to be in either open or closed without explicitly wanting to verifying the status:
        ///     - Open: use <seealso cref="OrdinaBelgiumHeaderPage.OpenHeaderSearch"/>
        ///     - Close: use <seealso cref="OrdinaBelgiumHeaderPage.CloseHeaderSearch"/>
        /// </summary>
        /// <returns>OrdinaBelgiumHeaderPage</returns>
        public OrdinaBelgiumHeaderPage ClickSearchToggle()
        {
            SearchToggle().Click();

            return this;
        }

        public OrdinaBelgiumHeaderPage InputTextInSearchField(string val)
        {
            SearchInputField().Click();
            SearchInputField().SendKeys(val);

            return this;
        }

        public OrdinaBelgiumSearchResultsPage ClickSearchButton()
        {
            SearchButton().Click();

            return new OrdinaBelgiumSearchResultsPage();
        }

        public OrdinaBelgiumHeaderPage OpenHeaderSearch()
        {
            if (!(SearchInputField().IsVisible()))
            {
                ClickSearchToggle();
            }

            return this;
        }

        public OrdinaBelgiumHeaderPage CloseHeaderSearch()
        {
            if (SearchInputField().IsVisible())
            {
                ClickSearchToggle();
            }

            return this;
        }

        public OrdinaBelgiumSearchResultsPage Action_SearchFor(string val)
        {
            OpenHeaderSearch()
            .InputTextInSearchField(val)
            .ClickSearchButton();

            return new OrdinaBelgiumSearchResultsPage();
        }

        public OrdinaBelgiumHeaderPage TakeScreenshotFromPage()
        {
            TakeScreenshot();
            return this;
        }

        public OrdinaBelgiumHeaderPage TakeScreenShotFromLogo()
        {
            OrdinaLogo().TakeScreenshot();

            //Assert.IsTrue(OrdinaLogo().LooksIdenticalTo("CsSeleniumImplExample", "20190520104326028_poctest.png"));

            return this;
        }

        /*
         * Verifications
         */
        public OrdinaBelgiumHeaderPage Verify_LogoIsVisible()
        {
            OrdinaLogo().ShouldBe(Visible);
            SearchInputField()
                .ShouldBe(
                Or(
                    Not(
                        And(
                            Visible,
                            ExactText("Search here", false))
                        )
                    ),
                Visible
                );
            OrdinaLogo().ShouldHave(ImageEquals(new System.Drawing.Bitmap("c:/screenshots/20190524151743982_poctest.png")));
            return this;
        }

    }
}
