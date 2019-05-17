using Microsoft.VisualStudio.TestTools.UnitTesting;

using CsSeleniumFrame.src.core;
using static CsSeleniumFrame.src.statics.CsSeConfigurationManager;

namespace CsSeleniumPoc.tests
{
    class Class1
    {
        [TestClass]
        class CsSeConfigurationManagerTest
        {
            [TestInitialize]
            private void Initialize()
            {

            }

            [TestMethod]
            public void BasicCsSeConfigurationManagerTest()
            {
                Assert.AreEqual(WebDriverTypes.Firefox, GetConfig().webDriverType);
            }

            [TestCleanup]
            public void Cleanup()
            {

            }
        }
    }
}
