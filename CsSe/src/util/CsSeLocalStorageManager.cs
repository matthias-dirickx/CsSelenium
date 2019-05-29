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

using static CsSeleniumFrame.src.Statics.CsSeDriver;

namespace CsSeleniumFrame.src.Core
{
    class CsSeLocalStorageManager
    {
        private LocalStorageManager lsm;

        /// <summary>
        /// Creates a LocalStorageManager in the context of the CsSeDriver.
        /// This means that the LocalStorageManager is created with the instance retreived by CsSeDriver.GetDriver().
        /// This is thread-dependent, and should be threadsafe.
        /// 
        /// To do operations, Execute GetManager() on the object to expose the LocalStorageManger object.
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
