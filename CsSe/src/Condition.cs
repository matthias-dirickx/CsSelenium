using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;

using CsSeleniumFrame.src.Conditions;

namespace CsSeleniumFrame.src
{
    public abstract class Condition
    {
        //Elements level
        public static readonly VisibleCondition Visible = new VisibleCondition();

        public static ExactTextCondition ExactText(string text)
        {
            return new ExactTextCondition(text);
        }

        public static ExactTextCondition ExactText(string text, bool readFromRootElementOnly)
        {
            return new ExactTextCondition(text, readFromRootElementOnly);
        }

        //Webdriver level


        //Aggregators
        public static AndCondition And(params Condition[] conditions)
        {
            return new AndCondition(conditions);
        }

        public static OrCondition Or(params Condition[] conditions)
        {
            return new OrCondition(conditions);
        }

        //Inverter
        public static NotCondition Not(Condition condition)
        {
            return new NotCondition(condition);
        }


        public abstract bool Apply(IWebDriver driver, IWebElement element);

        private readonly string name;

        public Condition(string name)
        {
            this.name = name;
        }
    }
}
