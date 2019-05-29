using OpenQA.Selenium;

using static CsSeleniumFrame.src.Statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    public class CsSeCookieManager
    {
        public static void SetCookie(string name, string value)
        {
            Cookie c = new Cookie(name, value);
            SetCookie(c);
        }

        public static void SetCookie(Cookie c)
        {
            GetDriver().Manage().Cookies.AddCookie(c);
        }

        public static string GetCookieValue(string name)
        {
            return GetDriver()
                .Manage()
                .Cookies
                .GetCookieNamed(name)
                .Value;
        }

        public static Cookie GetCookie(string name)
        {
            return GetDriver()
                .Manage()
                .Cookies
                .GetCookieNamed(name);
        }
    }
}
