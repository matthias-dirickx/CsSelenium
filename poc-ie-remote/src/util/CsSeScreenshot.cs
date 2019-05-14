using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

using static System.IO.Directory;

using OpenQA.Selenium;

namespace CsSeleniumFrame.src.util
{
    /// <summary>
    /// Class creates a screenshot item for selenium.
    /// Instead of using the selenium implementation we go straight to bitmaps.
    /// Goal:
    ///     - ready for selecting the element within the object
    ///     - Having one save function
    /// </summary>
    class CsSeScreenshot
    {
        private Bitmap bitmapScreen;
        private Screenshot screenshot;

        private IWebDriver driver;
        private IWebElement element;
        public CsSeScreenshot(IWebDriver driver)
        {
            this.driver = driver;

            TakeScreenshot();
            bitmapScreen = GetDriverScreenshot();
        }

        public CsSeScreenshot(IWebDriver driver, IWebElement element)
        {
            this.driver = driver;
            this.element = element;

            TakeScreenshot();
            bitmapScreen = GetElementScreenshot();
        }

        private void TakeScreenshot()
        {
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        }
        private Bitmap GetDriverScreenshot()
        {
            return new Bitmap(
                new MemoryStream(
                    screenshot
                    .AsByteArray
                    ));
        }

        private Bitmap GetElementScreenshot()
        {
            Bitmap screen = GetDriverScreenshot();

            return
                screen
                .Clone(
                    new Rectangle(
                        //Received some floats from firefox.
                        //This screwed with the rectangle, so cast it to int.
                        //This solved the issue
                        (int)element.Location.X,
                        (int)element.Location.Y,
                        (int)element.Size.Width,
                        (int)element.Size.Height),
                    screen.PixelFormat
                    );
        }

        public Bitmap Getbitmap()
        {
            return bitmapScreen;
        }

        public byte[] GetBytes()
        {
            BitmapData bmData =
                bitmapScreen
                .LockBits(
                    new Rectangle(
                        0,
                        0,
                        bitmapScreen.Width,
                        bitmapScreen.Height),
                ImageLockMode.ReadOnly,
                bitmapScreen.PixelFormat);

            IntPtr ptr = bmData.Scan0;
            int bytes = Math.Abs(bmData.Stride) * bitmapScreen.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            return rgbValues;
        }

        public string Save(string basePath, string theName, bool addTimeStamp)
        {
            string fullName = theName;
            if(addTimeStamp)
            {
                fullName =
                    DateTime.Now.ToString("yyyyMMddHHmmssfff")
                    + "_"
                    + theName;
            }

            string lastCharOfBasePath = basePath[basePath.Length - 1].ToString();

            if (lastCharOfBasePath != "/" || lastCharOfBasePath != "\\")
            {
                basePath += "/";
            }

            string fullPath = basePath + fullName + ".png";

            CreateDirectory(basePath);
            bitmapScreen.Save(fullPath, ImageFormat.Png);

            return fullPath;
        }
    }
}
