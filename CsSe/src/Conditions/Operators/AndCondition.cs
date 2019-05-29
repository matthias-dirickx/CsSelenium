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

using CsSeleniumFrame.src.Conditions.Operators;

namespace CsSeleniumFrame.src.Conditions
{
    public class AndCondition : Condition, IAggregateCondition
    {
        public Condition[] Conditions { get; set; }
        private Condition lastFailedCondition;

        private bool conditionPassed;

        protected override string ResultValue { get; set; }

        public AndCondition(Condition[] conditions) : base($"AND: [{GetConditionSummary(conditions)}]")
        {
            this.Conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach(Condition c in Conditions)
            {
                if(!c.Apply(driver, element))
                {
                    lastFailedCondition = c;
                    conditionPassed = false;

                    return conditionPassed;
                }
            }
            conditionPassed = true;

            return conditionPassed;
        }

        protected override string ActualValue()
        {
            if (conditionPassed)
            {
                return $"The AND condition passed on all conditions. Actual values: {GetConditionsActualSummaryString(Conditions)}.";
            }
            else
            {
                return $"The AND received false on: [{lastFailedCondition.name} : {lastFailedCondition.Actual}].";
            }
        }

        protected override string ExpectedValue()
        {
            return $"All of these are true: [{GetConditionsExpectedSummaryString(Conditions)}]";
        }
    }
}
