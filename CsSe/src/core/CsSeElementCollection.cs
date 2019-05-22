using System.Collections.Generic;
using System.Collections.ObjectModel;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Actions;

using static CsSeleniumFrame.src.statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeElementCollection
    {
        private List<CsSeElement> collection;

        public CsSeElementCollection(By by)
        {
            this.collection = WrapIWebElements(GetDriver().FindElements(by));
        }

        public CsSeElementCollection(CsSeElement parent, By by)
        {
            List<CsSeElement> tempCollection = new List<CsSeElement>();

            ReadOnlyCollection<IWebElement> elements = parent.GetWebElement().FindElements(by);

            foreach(IWebElement element in elements)
            {
                collection.Add(new CsSeElement(parent, by, elements.IndexOf(element)));
            }

            this.collection = tempCollection;
        }

        public CsSeElementCollection(ReadOnlyCollection<IWebElement> els)
        {
            this.collection = WrapIWebElements(els);
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
            return collection[index];
        }

        public int Size()
        {
            return collection.Count;
        }

    }
}
