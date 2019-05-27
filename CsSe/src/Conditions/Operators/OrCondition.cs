using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class OrCondition : Condition
    {
        private readonly Condition[] conditions;
        private bool conditionPassed;
        private Condition passCondition;

        protected override string ResultValue { get; set; }

        public OrCondition(params Condition[] conditions) : base($"OR: [{GetConditionSummary(conditions)}]")
        {
            this.conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach (Condition c in conditions)
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
                return $"None of the conditions applied. Actual values: [{GetConditionsActualSummaryString(conditions)}].";
            }
        }

        protected override string ExpectedValue()
        {
            return $"Any of these is true: [{GetConditionsExpectedSummaryString(conditions)}].";
        }
    }
}
