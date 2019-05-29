using OpenQA.Selenium;

using CsSeleniumFrame.src.Conditions.Operators;

namespace CsSeleniumFrame.src.Conditions
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
