using System;

using OpenQA.Selenium.Firefox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumFrame.src.Actions;

using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;

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
            Assert.AreNotEqual(WebDriverTypes.Chrome, GetConfig().WebDriverType);
        }
    }
}
