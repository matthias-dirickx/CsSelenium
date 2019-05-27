using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace CsSeleniumFrame.src.Conditions
{
    public class OrCondition : Condition
    {
        private readonly Condition[] conditions;

        protected override string ResultValue { get; set; }

        public OrCondition(params Condition[] conditions) : base("or")
        {
            this.conditions = conditions;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            foreach (Condition c in conditions)
            {
                if (c.Apply(driver, element))
                {
                    return true;
                }
            }
            return false;
        }

        private string GetConditionsExpectedSummaryString()
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

        private string GetConditionsActualSummaryString()
        {
            string conditionsSummary = "";

            foreach(Condition c in conditions)
            {
                if(conditionsSummary != "")
                {
                    conditionsSummary += c.name + " : ";
                    conditionsSummary += c.Actual;
                }
            }

            return conditionsSummary;
        }

        protected override string ActualValue()
        {
            return $"None of the expected values were true: {GetConditionsActualSummaryString()}.";
        }

        protected override string ExpectedValue()
        {
            return $"Any of these is true: {GetConditionsExpectedSummaryString()}.";
        }
    }
}
