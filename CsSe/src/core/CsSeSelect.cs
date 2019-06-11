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

using OpenQA.Selenium;

using CsSeleniumFrame.src.CsSeConditions;

using static CsSeleniumFrame.src.Statics.CsSe;
using static CsSeleniumFrame.src.Statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeSelect
    {
        private By selectButtonBy;
        private By selectOptionsContainerBy;
        private By selectOptionBy;
        private By selectSelectedOptionBy;

        public CsSeSelect(By selectButtonBy, By selectOptionsContainerBy, By selectOptionBy, By selectSelectedOptionBy)
        {
            this.selectButtonBy = selectButtonBy;
            this.selectOptionsContainerBy = selectOptionsContainerBy;
            this.selectOptionBy = selectOptionBy;
            this.selectSelectedOptionBy = selectSelectedOptionBy;
        }

        public CsSeSelect Open()
        {
            if(!f(selectOptionsContainerBy).IsVisible())
            {
                f(selectButtonBy).Click();
            }
            return this;
        }

        public bool OptionsDisplayed()
        {
            return f(selectOptionsContainerBy).IsVisible();
        }

        public CsSeElementCollection GetOptions()
        {
            return f(selectOptionsContainerBy).ff(selectOptionBy);
        }

        public CsSeSelect SelectOption(Condition condition)
        {
            CsSeElementCollection els = Open().GetOptions();
            
            for(int i = 0; i < els.Size(); i++)
            {
                IWebElement el = els.Get(i);

                if(condition.Apply(GetDriver(), el))
                {
                    el.Click();
                    break;
                }
            }

            return this;
        }
    }
}
