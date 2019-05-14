using OpenQA.Selenium;

using CsSeleniumFrame.src.util;

using static CsSeleniumFrame.src.statics.CsSeDriver;

namespace CsSeleniumFrame.src.statics
{
    public class CsSe
    {
        public static CsSeElement f(By by)
        {
            return new CsSeElement(by);
        }

        public static CsSeElement f(By by, int index)
        {
            return new CsSeElement(by, index);
        }
        public static CsSeElement f(string cssSelector)
        {
            return f(By.CssSelector(cssSelector));
        }

        public static CsSeElement f(string cssSelector, int index)
        {
            return f(By.CssSelector(cssSelector), index);
        }

        public static CsSeElement fx(string xpathSelector)
        {
            return f(By.XPath(xpathSelector));
        }

        public static CsSeElement fx(string xpathSelector, int index)
        {
            return f(By.XPath(xpathSelector), index);
        }

        public CsSeElementCollection ff(By by)
        {
            return new CsSeElementCollection(by);
        }
        public CsSeElementCollection ff(string cssSelector)
        {
            return ff(By.CssSelector(cssSelector));
        }
        public CsSeElementCollection ffx(string xpathSelector)
        {
            return ff(By.XPath(xpathSelector));
        }

        public static void TakeScreenshot()
        {
            new CsSeScreenshot(
                GetDriver())
                    .Save(
                        "C:/screenshots",
                        "poctest",
                        true);
        }

        public static void TakeScreenshot(string basePath, string name, bool addTimeStamp)
        {
            new CsSeScreenshot(
                GetDriver())
                    .Save(
                        basePath,
                        name,
                        addTimeStamp);
        }

        public static void open(string url)
        {
            GetDriver().Url = url;
        }
    }
}
