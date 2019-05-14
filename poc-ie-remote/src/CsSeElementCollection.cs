using System.Collections.Generic;
using System.Collections.ObjectModel;

using OpenQA.Selenium;
using static CsSeleniumFrame.src.statics.CsSeDriver;

namespace CsSeleniumFrame.src
{
    public class CsSeElementCollection
    {
        private List<CsSeElement> col;
        public CsSeElementCollection(By by)
        {
            this.col = WrapIWebElements(GetDriver().FindElements(by));
        }

        public CsSeElementCollection(ReadOnlyCollection<IWebElement> els)
        {
            this.col = WrapIWebElements(els);
        }

        private List<CsSeElement> WrapIWebElements(ReadOnlyCollection<IWebElement> els)
        {
            List<CsSeElement> csSeElements = new List<CsSeElement>();
            foreach(IWebElement el in els)
            {
                csSeElements.Add(new CsSeElement(el));
            }
            return csSeElements;
        }

        public CsSeElement Get(int index)
        {
            return col[index];
        }

        public int Size()
        {
            return col.Count;
        }

    }
}
