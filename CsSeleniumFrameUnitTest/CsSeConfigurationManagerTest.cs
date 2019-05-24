using System;

using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

using CsSeleniumFrame.src.Core;

using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Diagnostics;

namespace CsSeleniumFrameUnitTest
{
    [TestClass]
    public class CsSeConfigurationManagerTest
    {
        [TestMethod]
        public void BasicCsSeConfigurationManagerNotNullTest()
        {
            Assert.IsNotNull(GetConfig());
        }

        [TestMethod]
        public void BasicCsSeConfigurationManagerDefaultValueTest()
        {
            Assert.AreEqual(GetConfig().WebDriverType, WebDriverTypes.Firefox);
            Assert.AreEqual(GetConfig().RemoteUrl, new Uri("http://127.0.0.1:4444/wd/hub"));
            Assert.IsInstanceOfType(GetConfig().WebDriverOptions, typeof(FirefoxOptions));
            Assert.IsFalse(GetConfig().IsHeadless);
        }

        [TestMethod]
        public void BasicCsSeConfigurationManagerDefaultValueNotEqualsTest()
        {
            Assert.AreNotEqual(WebDriverTypes.Firefox, GetConfig().WebDriverType);
        }

        [TestMethod]
        public void TestSerializableConfigurationForWebDriverTest()
        {
            GetConfig().IsHeadless = true;
            IWebDriver driver = new WebDriverFactory().CreateWebDriver(WebDriverTypes.Firefox, new FirefoxOptions());
            ICapabilities capas = (((RemoteWebDriver)driver).Capabilities);
            
            Debug.Write(JsonConvert.SerializeObject(driver).ToString());
        }
    }
}
