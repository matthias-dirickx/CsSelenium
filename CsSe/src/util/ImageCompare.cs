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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CsSeleniumFrame.src.Core
{
    public class ImageCompare
    {
        /// <summary>
        /// Hard pixelbased compare.
        /// 
        /// Size not equal --> False
        /// Pixel differenece --> False
        /// 
        /// Designed for pixelperfect imaging. Formats must be managed by the source.
        /// 
        /// To compare in different ways, please use other methods if available.
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static bool AreIdentical(Bitmap expected, Bitmap actual)
        {

            //First the simple checks.
            if (expected.Height != actual.Height || expected.Width != actual.Width)
            {
                return false;
            }

            if (expected == null || actual ==null)
            {
                return false;
            }

            //If none of the above is true:

            int bytes = expected.Width * expected.Height * (Image.GetPixelFormatSize(expected.PixelFormat) / 8);

            bool result = true;

            byte[] expBytes = GetBytes(expected);
            byte[] actBytes = GetBytes(actual);

            for (int n = 0; n < bytes; n++)
            {
                if (expBytes[n] != actBytes[n])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool HaveDistanceLessThen(double cutOff, string type, Bitmap actual, Bitmap expected)
        {
            return false;
        }

        private double DistanceOfImages(Bitmap actual, Bitmap expected)
        {

            return 0.0;
        }

        private static byte[] GetBytes(Bitmap bm)
        {
            int bytes = bm.Width * bm.Height * (Image.GetPixelFormatSize(bm.PixelFormat) / 8);
            byte[] bmBytes = new byte[bytes];

            BitmapData bd = bm.LockBits(
                new Rectangle(
                    0,
                    0,
                    bm.Width,
                    bm.Height),
                ImageLockMode.ReadOnly,
                bm.PixelFormat);

            Marshal.Copy(bd.Scan0, bmBytes, 0, bytes);

            return bmBytes;
        }
    }
}
