using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class GoogleSearchBasicTest
    {
        IWebDriver driver;

        [TestInitialize]
        public void Initialize()
        {
            FirefoxDriverService ffs = FirefoxDriverService.CreateDefaultService(@"C:/Tools/seleniumdrivers/geckodriver-v0.24.0-win64");
            driver = new FirefoxDriver(ffs);
        }

        [TestMethod]
        public void GoogleSearchPocTest()
        {
            FirefoxDriverService ffs = FirefoxDriverService.CreateDefaultService(@"C:/Tools/seleniumdrivers/geckodriver-v0.24.0-win64");

            IWebDriver driver = new FirefoxDriver(ffs);
            driver.Url = "http://google.com";
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Close();
        }
    }
}
