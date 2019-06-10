using CsSeleniumFrame.src.Core;

using static CsSeleniumFrame.src.Statics.CsSe;

namespace CsSeleniumPoc.pages
{
    public class OrdinaBelgiumFeaturedArticlesPage
    {
        private readonly int index;
        private static readonly string rootCss = "section.fc-featured";
        private static readonly string articleCss = "article";

        public OrdinaBelgiumFeaturedArticlesPage(int index)
        {
            this.index = index;
        }

        public CsSeElement RootContainer() { return f(rootCss, index);  }
        public CsSeElement Title() { return RootContainer().f("h2");  }
        public CsSeElementCollection AllArticles() { return RootContainer().ff(articleCss); }
        public CsSeElement ArticleInstance(int index) { return RootContainer().f(articleCss, index);  }
    }
}