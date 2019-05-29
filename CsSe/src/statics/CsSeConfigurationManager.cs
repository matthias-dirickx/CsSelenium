﻿/*
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
