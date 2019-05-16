using OpenQA.Selenium;

using static CsSeleniumFrame.src.statics.CsSeDriver;

namespace CsSeleniumFrame.src.util
{
    class CsSeLocalStorageManager
    {
        private LocalStorageManager lsm;

        /// <summary>
        /// Creates a LocalStorageManager in the context of the CsSeDriver.
        /// This means that the LocalStorageManager is created with the instance retreived by CsSeDriver.GetDriver().
        /// This is thread-dependent, and should be threadsafe.
        /// 
        /// To do operations, execute GetManager() on the object to expose the LocalStorageManger object.
        /// </summary>
        public CsSeLocalStorageManager()
        {
            lsm = new LocalStorageManager(GetDriver());
        }

        /// <summary>
        /// Exposes the LocalStorageManger object.
        /// This way you can access all of the available operations.
        /// </summary>
        /// <returns>LocalStorageManager</returns>
        public LocalStorageManager GetManager()
        {
            return lsm;
        }
    }
}
