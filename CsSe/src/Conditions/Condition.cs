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

namespace CsSeleniumFrame.src.CsSeConditions
{
    public abstract class Condition
    {
        protected abstract string ResultValue { get; set; }
        public string Actual => ActualValue();
        public string Expected => ExpectedValue();

        public abstract bool Apply(IWebDriver driver, IWebElement element);
        protected abstract string ActualValue();
        protected abstract string ExpectedValue();

        public readonly string name;

        public Condition(string name)
        {
            this.name = name;
            ResultValue = "Condition was not applied. Apply condition before calling the Actual (actual value) variable.";
        }

        protected static string GetConditionSummary(Condition[] conditions)
        {
            string conditionsSummary = "";

            foreach (Condition c in conditions)
            {
                if (conditionsSummary != "")
                {
                    conditionsSummary += ", ";
                }
                conditionsSummary += c.name;
            }

            return conditionsSummary;
        }

        protected static string GetConditionsExpectedSummaryString(Condition[] conditions)
        {
            string conditionsSummary = "";

            foreach (Condition c in conditions)
            {
                if (conditionsSummary != "")
                {
                    conditionsSummary += ", ";
                }
                conditionsSummary += c.Expected;
            }

            return conditionsSummary;
        }

        protected static string GetConditionsActualSummaryString(Condition[] conditions)
        {
            string conditionsSummary = "";

            foreach (Condition c in conditions)
            {
                if (conditionsSummary != "")
                {
                    conditionsSummary += c.name + " : ";
                    conditionsSummary += c.Actual;
                }
            }

            return conditionsSummary;
        }
    }
}
