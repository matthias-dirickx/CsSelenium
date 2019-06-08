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

using CsSeleniumFrame.src.CsSeConditions.Operators;

namespace CsSeleniumFrame.src.CsSeConditions
{
    public class OrCondition : Condition, IAggregateCondition
    {
        public Condition[] Conditions { get; set; }
        private bool conditionPassed;
        private Condition passCondition;

        protected override string ResultValue { get; set; }

        public OrCondition(params Condition[] conditions) : base($"OR: [{GetConditionSummary(conditions)}]")
        {
            this.Conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach (Condition c in Conditions)
            {
                if (c.Apply(driver, element))
                {
                    conditionPassed = true;
                    passCondition = c;

                    return conditionPassed;
                }
            }

            conditionPassed = false;

            return conditionPassed;
        }

        protected override string ActualValue()
        {
            if(conditionPassed)
            {
                return $"The OR condition passed on the contained '{passCondition.name}' condition. Actual value: {passCondition.Actual}.";
            }
            else
            {
                return $"None of the conditions applied. Actual values: [{GetConditionsActualSummaryString(Conditions)}].";
            }
        }

        protected override string ExpectedValue()
        {
            return $"Any of these is true: [{GetConditionsExpectedSummaryString(Conditions)}].";
        }
    }
}
