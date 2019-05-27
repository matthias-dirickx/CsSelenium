using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class AndCondition : Condition
    {
        private readonly Condition[] conditions;
        private Condition lastFailedCondition;

        private bool conditionPassed;

        protected override string ResultValue { get; set; }

        public AndCondition(Condition[] conditions) : base($"AND: [{GetConditionSummary(conditions)}]")
        {
            this.conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach(Condition c in conditions)
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
                return $"The AND condition passed on all conditions. Actual values: {GetConditionsActualSummaryString(conditions)}.";
            }
            else
            {
                return $"There was a failed condition: [{lastFailedCondition.name} : {lastFailedCondition.Actual}].";
            }
        }

        protected override string ExpectedValue()
        {
            return $"All of these are true: [{GetConditionsExpectedSummaryString(conditions)}]";
        }
    }
}
