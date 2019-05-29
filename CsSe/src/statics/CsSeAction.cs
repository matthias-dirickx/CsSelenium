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

using CsSeleniumFrame.src.Actions;
using CsSeleniumFrame.src.Conditions;

namespace CsSeleniumFrame.src.Statics
{
    public static class CsSeAction
    {
        public static ShouldAction Should(Condition[] conditions)
        {
            return new ShouldAction(conditions);
        }

        public static ShouldNotAction ShouldNot(Condition[] conditions)
        {
            return new ShouldNotAction(conditions);
        }

        public static WaitUntilAction WaitUntil(Condition condition, long timeoutMs, long pollMs)
        {
            return new WaitUntilAction(condition, timeoutMs, pollMs);
        }

        public static WaitWhileAction WaitWhile(Condition condition, long timeoutMs, long pollMs)
        {
            return new WaitWhileAction(condition, timeoutMs, pollMs);
        }
    }
}
