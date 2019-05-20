using OpenQA.Selenium;

namespace CsSeleniumFrame.src.util
{
    class LocalStorageManager
    {
        private IJavaScriptExecutor js;
        
        /// <summary>
        /// Creates LocalStorageManager with the assigned IWebDriver.
        /// Operations include getting and setting values in the local storage space.
        /// The operations are done by javascript executed by the Javascript Executor from Selenium WebDriver.
        /// </summary>
        /// <param name="driver"></param>
        public LocalStorageManager(IWebDriver driver)
        {
            this.js = (IJavaScriptExecutor)driver;
        }

        public void RemoveItemFromLocalStorage(string item)
        {
            js.ExecuteScript(
                $"window.localStorage.removeItem'{item}');"
                );
        }

        public bool IsItemPresentInLocalStorage(string item)
        {
            return !(js.ExecuteScript(
                $"return window.localStorage.getItem('{item}');") == null);
        }

        public string GetItemFromLocalStorage(string key)
        {
            return (string)js.ExecuteScript(
                $"return window.localStorage.key('{key}')");
        }

        public string GetKeyFromLocalStorage(int key)
        {
            return (string)js.ExecuteScript(
                $"return window.localStorage.key('{key}')");
        }

        public long GetLocalStorageLength()
        {
            return (long)js.ExecuteScript(
                "return window.localStorage.length;");
        }

        public void SetItemInLocalStorage(string item, string value)
        {
            js.ExecuteScript(
                $"window.localStorage.setItem('{item}', '{value}');");
        }

        public void ClearLocalStorage()
        {
            js.ExecuteScript(
                "window.localStorage.clear();");
        }
    }
}
