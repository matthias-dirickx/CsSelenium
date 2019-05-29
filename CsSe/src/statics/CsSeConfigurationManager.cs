using System;

using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Statics
{
    public sealed class CsSeConfigurationManager
    {
        /*
         * Singleton Lazy instantiation and creation.
         */
        private static Lazy<CsSeConfigurationManager> instance =
            new Lazy<CsSeConfigurationManager>(() => new CsSeConfigurationManager());
        private static CsSeConfigurationManager Instance => instance.Value;

        //Actual fields from class;
        private CsSeProperties cp;

        private CsSeConfigurationManager()
        {
            cp = new CsSeProperties();
        }

        public void OverwriteConfig(string defaultConfigUrl)
        {
            Instance.cp = new CsSeProperties(defaultConfigUrl);
        }

        public void UpdateConfig(string defaultConfigUrl)
        {
            Instance.cp.Update(defaultConfigUrl);
        }

        public static CsSeProperties GetConfig()
        {
            return Instance.cp;
        }
    }
}
