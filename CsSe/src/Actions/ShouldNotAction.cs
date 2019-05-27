using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Conditions;

using static CsSeleniumFrame.src.statics.CsSeCondition;

namespace CsSeleniumFrame.src.Actions
{
    public class ShouldNotAction : Interaction
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Condition[] conditions;


        public ShouldNotAction(Condition[] conditions) : base("should not")
        {
            this.conditions = conditions;
        }

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            foreach (Condition c in conditions)
            {
                Not(c).Apply(driver, csSeElement);
            }
            return new CsSeElement(csSeElement);
        }
    }
}
