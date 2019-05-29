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

using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Conditions
{
    public class ExactTextCondition : Condition
    {
        private readonly string text;
        private readonly bool readFromRootElementOnly;
        private readonly bool readRootElementStrict;

        protected override string ResultValue { get ; set; }

        public ExactTextCondition(string text) : base($"exact text: '{text}'")
        {
            this.text = text;
            readFromRootElementOnly = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly) : base("exact text: '{text}'")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
            readRootElementStrict = false;
        }

        public ExactTextCondition(string text, bool readFromRootElementOnly, bool readRootElementStrict) : base("exact text: '{text}'")
        {
            this.text = text;
            this.readFromRootElementOnly = readFromRootElementOnly;
            this.readRootElementStrict = readRootElementStrict;
        }

        private string GetText(IWebElement element)
        {
            if(readFromRootElementOnly)
            {
                return XmlUtils.GetRootElementTextValue(element.GetAttribute("outerHTML"), readRootElementStrict);
            }
            else
            {
                return element.Text;
            }
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            string actualText = GetText(element);
            ResultValue = actualText;

            return actualText == text;
        }

        protected override string ActualValue()
        {
            return ResultValue;
        }

        protected override string ExpectedValue()
        {
            string onlyRootMessage = $"(Root only: {readRootElementStrict} (Strictly root and not even <b>, <i>, <p>, ...)";
            return $"'{text}' {onlyRootMessage}";
        }
    }
}
