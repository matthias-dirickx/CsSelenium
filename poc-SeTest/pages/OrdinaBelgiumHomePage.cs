using CsSeleniumImplExample.pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsSeleniumPoc.pages
{
    public class OrdinaBelgiumHomePage
    {
        public OrdinaBelgiumHeaderPage Header = new OrdinaBelgiumHeaderPage();
        public OrdinaBelgiumRecommandedArticlesPage RelatedArticles = new OrdinaBelgiumRecommandedArticlesPage();
        public OrdinaBelgiumHighlightArticlesPage HighlightArticles = new OrdinaBelgiumHighlightArticlesPage();
    }
}
