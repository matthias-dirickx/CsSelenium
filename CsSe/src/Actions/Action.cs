using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Actions
{
    /// <summary>
    /// Abstract class for an interaction.
    /// 
    /// An interactio is any Action you can do with WebDriver.
    /// All actions are available through the WebDriver object that you can obtain from the CsSeDriver, but the implemented actions can be manageded by the event logging implementations.
    /// </summary>
    public abstract class Action
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public abstract CsSeElement Execute(IWebDriver driver, CsSeElement csSeElement);

        protected readonly string name;

        public Action(string name)
        {
            logger.Info($"Start action - '{name}'");
            this.name = name;
        }
    }
}
