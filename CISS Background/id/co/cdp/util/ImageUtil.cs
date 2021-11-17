using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CISS.id.co.cdp.util
{
    public static class ImageUtil
    {
        private static string basic_path = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\').Last(), "");

        public static Image getLoadingImage()
        {
            Image image = Image.FromFile(basic_path + "loading.gif");
            return image;// new Bitmap(image, 50, 50);
        }
    }
}
