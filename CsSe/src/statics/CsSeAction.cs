using CsSeleniumFrame.src.Actions;
using CsSeleniumFrame.src.Conditions;

namespace CsSeleniumFrame.src.statics
{
    public static class CsSeAction
    {
        public static ShouldAction Should(Condition[] conditions)
        {
            return new ShouldAction(conditions);
        }

        public static ShouldNotAction ShouldNot(Condition[] conditions)
        {
            return new ShouldNotAction(conditions);
        }

        public static WaitUntilAction WaitUntil(Condition condition, long timeoutMs, long pollMs)
        {
            return new WaitUntilAction(condition, timeoutMs, pollMs);
        }

        public static WaitWhileAction WaitWhile(Condition condition, long timeoutMs, long pollMs)
        {
            return new WaitWhileAction(condition, timeoutMs, pollMs);
        }
    }
}
