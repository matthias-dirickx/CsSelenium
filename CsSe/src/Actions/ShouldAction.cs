using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

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

        public override CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement)
        {
            foreach(Condition c in conditions)
            {
                if(c.Apply(driver, csSeElement))
                {
                    return csSeElement;
                }
                else
                {
                    throw new CsSeElementShould(
                        $"\n\nElement should be {c.ExpectedValue()}, but actually was {c.ActualValue(driver, csSeElement)}."
                      + "\n\nContext info:"
                      + $"\n\tSelector:\t{RecursiveElementIdentifier("", csSeElement)}"
                      + $"\n\tDriver info:\t{((RemoteWebDriver)driver).Capabilities.ToString()}");
                }
            }
            stopwatch.Stop();
            return csSeElement;
        }

        private string RecursiveElementIdentifier(string recursiveLocation, CsSeElement csSeElement)
        {
            string newRecursiveLocation;
            newRecursiveLocation = $"{csSeElement.by.ToString()}[{csSeElement.index}]";

            if (csSeElement.parent != null)
            {
                return $"{RecursiveElementIdentifier(newRecursiveLocation, csSeElement.parent)} -> {newRecursiveLocation}";
            }

            return newRecursiveLocation;
        }
    }
}
