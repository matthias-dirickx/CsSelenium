using System.Drawing;

using CsSeleniumFrame.src.Conditions;

namespace CsSeleniumFrame.src.Statics
{
    public static class CsSeCondition
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

        public static ImageEqualsCondition ImageEquals(Bitmap bm)
        {
            return new ImageEqualsCondition(bm);
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
    }
}
