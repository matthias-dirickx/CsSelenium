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
