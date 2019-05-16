using System;

using CsSeleniumFrame.src.util;

namespace CsSeleniumFrame.src.statics
{
    public sealed class CsSeConfigurationManager
    {
        /*
         * Singleton Lazy instantiation and creation.
         */
        private static object myLock = new object();
        private static volatile CsSeConfigurationManager cm = null;

        //Actual fields from class;
        private CsSeProperties cp;

        private CsSeConfigurationManager()
        {
            cp = new CsSeProperties();
            LoadDefaultConfig();
        }

        //Double-checked locking
        //Lazy<T> instantiation caused issues.
        private static CsSeConfigurationManager Instance()
        {
            if(cm == null)
            {
                lock (myLock)
                {
                    if(cm == null)
                    {
                        cm = new CsSeConfigurationManager();
                    }
                }
            }
            return cm;
        }

        public void LoadDefaultConfig()
        {
            Instance().cp.webDriverType = core.WebDriverType.Firefox;
        }

        public void LoadConfig(string defaultConfigUrl)
        {
            Instance().cp = new CsSeProperties(defaultConfigUrl);
        }

        public static CsSeProperties GetConfig()
        {
            return Instance().cp;
        }
    }
}
