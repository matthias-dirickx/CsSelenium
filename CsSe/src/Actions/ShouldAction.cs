using System;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Conditions;
using CsSeleniumFrame.src.Core;
using CsSeleniumFrame.src.Ex;

namespace CsSeleniumFrame.src.Actions
{
    public class ShouldAction : Interaction
    {
        private readonly Condition[] conditions;

        public ShouldAction(Condition[] conditions) : base("should")
        {
            this.conditions = conditions;
        }

        public override CsSeElement Execute(IWebDriver driver, IWebElement element)
        {
            foreach(Condition c in conditions)
            {
                if(c.Apply(driver, element))
                {
                    return new CsSeElement(element);
                }
                else
                {
                    throw new CsSeElementShould(
                        "Element should be " + c.name + " and actually was " + c.ActualValue(driver, element) + "."
                      + "\nElement info: \n" + element.GetAttribute("innerHTML"));
                }
            }
            stopwatch.Stop();
            return new CsSeElement(element);
        }
    }
}
