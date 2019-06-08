/*
 * Copyright 2019 Matthias Dirickx
 * 
 * This file is part of CsSeSelenium.
 * 
 * CsSeSelenium is free software:
 * you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License,
 * or (at your option) any later version.
 * 
 * CsSeSelenium is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * WITHOUT even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with CsSeSelenium.
 * 
 * If not, see http://www.gnu.org/licenses/.
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

using OpenQA.Selenium;

using static CsSeleniumFrame.src.Statics.CsSeDriver;

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

            ReadOnlyCollection<IWebElement> elements = parent.WebElement.FindElements(by);

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
