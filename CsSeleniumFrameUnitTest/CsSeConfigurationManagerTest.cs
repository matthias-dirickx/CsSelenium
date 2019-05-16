using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumFrame.src.core;
using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;

namespace CsSeleniumFrameUnitTest
{
    [TestClass]
    class CsSeConfigurationManagerTest
    {
        [TestMethod]
        public void BasicCsSeConfigurationManagerTest()
        {
            Assert.AreEqual(WebDriverType.Firefox, GetConfig().webDriverType);
        }
    }
}
