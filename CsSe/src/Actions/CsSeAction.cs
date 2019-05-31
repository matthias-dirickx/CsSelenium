/*
 * Copyright 2019 Matthias Dirickx
 * 
 * This file is part of CsSeSelenium.
 * 
 * CsSeSelenium is free software:
 * you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License,
 * or (at your option) any later version.
 * 
 * CsSeSelenium is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * WITHOUT even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with CsSeSelenium.
 * 
 * If not, see http://www.gnu.org/licenses/.
 */

using System;

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
    public abstract class CsSeAction<T>
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public abstract T Execute(IWebDriver driver, CsSeElement csSeElement);

        protected readonly string name;

        public CsSeAction(string name)
        {
            logger.Info($"Start action - '{name}'");
            this.name = name;
        }

        protected static string GetActionGenericExecptionDescription(Exception e)
        {
            string exName = e.GetType().Name;
            string innerExName = e.InnerException == null ? "" : $" due to {e.InnerException.GetType().Name}";

            return $"{exName}{innerExName}.";
        }
    }
}
