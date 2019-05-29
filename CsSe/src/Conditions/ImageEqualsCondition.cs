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
