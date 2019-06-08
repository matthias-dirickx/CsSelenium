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

using System.Drawing;

using CsSeleniumFrame.src.CsSeConditions;

namespace CsSeleniumFrame.src.Statics
{
    public static class CsSeCondition
    {
        //Elements level
        public static readonly VisibleCondition Visible = new VisibleCondition();

        public static ExactTextCondition ExactText(string text)
        {
            return new ExactTextCondition(text);
        }

        public static ExactTextCondition ExactText(string text, bool readFromRootElementOnly)
        {
            return new ExactTextCondition(text, readFromRootElementOnly);
        }

        public static ImageEqualsCondition ImageEquals(Bitmap bm)
        {
            return new ImageEqualsCondition(bm);
        }

        //Webdriver level


        //Aggregators
        public static AndCondition And(params Condition[] conditions)
        {
            return new AndCondition(conditions);
        }

        public static OrCondition Or(params Condition[] conditions)
        {
            return new OrCondition(conditions);
        }

        //Inverter
        public static NotCondition Not(Condition condition)
        {
            return new NotCondition(condition);
        }
    }
}
