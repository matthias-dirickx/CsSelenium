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

namespace CsSeleniumFrame.src.Logger
{
    /// <summary>
    /// Indication for the type of events logged in the logger library.
    /// </summary>
    public enum CsSeEventType
    {
        /// <summary>
        /// Intended for a Test run level operation.
        /// Typically this will be the Assembly level (before anything starts)
        /// </summary>
        CsSeRun,
        /// <summary>
        /// Intended for a Test Class level operation
        /// </summary>
        CsSeSuite,
        /// <summary>
        /// Intended for a Test Method level operation.
        /// </summary>
        CsSeTest,
        /// <summary>
        /// Intended for an action level operation in a Test.
        /// </summary>
        CsSeAction,
        /// <summary>
        /// Intended for an assertion in a Test.
        /// </summary>
        CsSeCondtion,
        /// <summary>
        /// Intended for the ports AND and OR to aggregate conditions.
        /// </summary>
        CsSeCheckAggregate,
        /// <summary>
        /// Intended for the wait actions.
        /// </summary>
        CsSeCheckWait,
        /// <summary>
        /// Intended for the overhead actions indication.
        /// </summary>
        CsSeOverhead
    }
}
