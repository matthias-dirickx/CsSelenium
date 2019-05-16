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
            bitmapScreen = GetDriverScreenshotAsBitmap();
        }

        public CsSeScreenshot(IWebDriver driver, IWebElement element)
        {
            this.driver = driver;
            this.element = element;

            TakeScreenshot();
            bitmapScreen = GetElementScreenshotAsBitmap();
        }

        private void TakeScreenshot()
        {
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        }
        private Bitmap GetDriverScreenshotAsBitmap()
        {
            Bitmap ss = Image.FromStream(new MemoryStream(screenshot.AsByteArray)) as Bitmap;
            System.Diagnostics.Debug.Write(ss.PixelFormat.ToString() + "\n\n");
            System.Diagnostics.Debug.Write(ss.PhysicalDimension.ToString() + "\n\n");
            System.Diagnostics.Debug.Write(ss.HorizontalResolution.ToString() + "\n" + ss.VerticalResolution.ToString() + "\n\n");
            return ss;
        }

        private Bitmap GetElementScreenshotAsBitmap()
        {
            Bitmap screen = GetDriverScreenshotAsBitmap();
            System.Diagnostics.Debug.Write(
                "The location of the element:\n "
                + element.Location.ToString()
                + "\nThe size of the element: "
                + element.Size.ToString()
                + "\nThe pixel format is: "
                + screen.PixelFormat.ToString());

            return
                screen
                .Clone(
                    new Rectangle(
                        //Received some floats from firefox.
                        //This screwed with the rectangle, so cast it to int.
                        //This solved the issue
                        element.Location.X,
                        element.Location.Y,
                        (int)(element.Size.Width),
                        (int)(element.Size.Height)),
                    screen.PixelFormat
                );
        }

        public Bitmap Getbitmap()
        {
            return bitmapScreen;
        }

        public byte[] GetBytes()
        {
            return screenshot.AsByteArray;
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
