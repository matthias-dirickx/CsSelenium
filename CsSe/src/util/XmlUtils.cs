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

using System.Linq;
using System.Xml;

namespace CsSeleniumFrame.src.Core
{
    class XmlUtils
    {
        public static string GetRootElementTextValue(string xmlString, bool strict)
        {
            XmlDocument d = new XmlDocument();
            d.LoadXml(xmlString);

            return GetRootElementTextValue(d, strict);
        }

        public static string GetRootElementTextValue(XmlDocument xmlDoc, bool strict)
        {
            XmlNode root = xmlDoc.DocumentElement;

            string result = "";

            foreach (XmlNode n in root.ChildNodes)
            {
                if(GetWhiteList(strict).Contains(n.Name))
                {
                    result += n.InnerText;
                }
            }

            return result;
        }

        private static string[] GetWhiteList(bool strict)
        {
            if(strict)
            {
                return new string[]{
                "#text" //to protect the root text from being deleted.
                };
            }
            else
            {
                return new string[]{
                "p",
                "b",
                "i",
                "strong",
                "em",
                "mark",
                "small",
                "del",
                "ins",
                "sub",
                "sup",
                "#text" //to protect the root text from being deleted.
                };
            }
        }
    }
}
