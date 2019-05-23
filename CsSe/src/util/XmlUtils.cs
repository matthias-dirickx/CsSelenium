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
