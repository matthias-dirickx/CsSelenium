using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CsSeleniumFrame.src.util
{
    class ImageUtils
    {
        public static string GetBitmapAsBase64(Bitmap bm)
        {
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();

            return Convert.ToBase64String(byteImage);
        }
    }
}
