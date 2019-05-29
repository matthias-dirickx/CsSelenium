using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;
using System.Collections.Generic;

namespace CsSeleniumFrame.src.Conditions
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
