using OpenQA.Selenium;

using static CsSeleniumFrame.src.statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeCookieManager
    {
        public static void SetCookie(string name, string value)
        {
            Cookie c = new Cookie(name, value);
            GetDriver().Manage().Cookies.AddCookie(c);
        }
    }
}
