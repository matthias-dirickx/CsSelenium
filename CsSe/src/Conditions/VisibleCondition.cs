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

namespace CsSeleniumFrame.src.Conditions
{
    public class VisibleCondition : Condition
    {
        protected override string ResultValue { get; set; }

        public VisibleCondition() : base("visible")
        {

        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            bool value = element.Displayed;
            ResultValue = value ? "visible" : "invisible";

            return value;
        }

        protected override string ActualValue()
        {
            return ResultValue;
        }

        protected override string ExpectedValue()
        {
            return "visible";
        }
    }
}
