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

using OpenQA.Selenium;

using CsSeleniumFrame.src.Core;

namespace CsSeleniumFrame.src.Conditions
{
    public class ImageEqualsCondition : Condition
    {
        private Bitmap expectedBitmap;
        private Bitmap actualBitmap;

        protected override string ResultValue { get; set; }

        public ImageEqualsCondition(Bitmap bitmap) : base("Image equals")
        {
            expectedBitmap = bitmap;
        }

        public override bool Apply(IWebDriver driver, IWebElement element)
        {
            actualBitmap = new CsSeElement(element).GetScreenAsBitmap();

            ResultValue = util.ImageUtils.GetBitmapAsBase64(actualBitmap);

            return ImageCompare.AreIdentical(
                expectedBitmap,
                actualBitmap
                );
        }

        protected override string ActualValue()
        {
            return ResultValue;
        }

        protected override string ExpectedValue()
        {
            return util.ImageUtils.GetBitmapAsBase64(expectedBitmap);
        }
    }
}
