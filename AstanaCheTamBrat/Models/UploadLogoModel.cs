using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace AstanaCheTamBrat.Models
{
    public class UploadLogoModel
    {
        public string data { get; set; }
        public string name { get; set; }
        public int imageOriginalWidth { get; set; }
        public int imageOriginalHeight { get; set; }
        public string imageWidth { get; set; }
        public string imageHeight { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public void Save(Guid provider_id)
        {

            string base64 = data;
            if (base64 != null && base64 != "" && base64.Substring(0, 4) == "data")
                base64 = data.Substring(data.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] image = Convert.FromBase64String(base64);

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            stream.Write(image, 0, image.Length);

            Image bmp = Bitmap.FromStream(stream, true);

            int _imageWidth = 0, _imageHeight = 0;

            if (imageHeight == "NaN" || imageHeight == "")
                _imageHeight = imageOriginalHeight;
            else
                _imageHeight = int.Parse(imageHeight);

            if (imageWidth == "NaN" || imageWidth == "")
                _imageWidth = imageOriginalWidth;
            else
                _imageWidth = int.Parse(imageWidth);

            using (Graphics g2 = Graphics.FromImage(bmp))
            {
                //g2.ScaleTransform(_imageWidth, _imageHeight);

                

               // g2.Save();
            }
            Image imageResult = (Image)new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(imageResult))
            {

                Rectangle r = new Rectangle(0, 0, width,height);
                g.Clear(Color.White);
                g.PageUnit = GraphicsUnit.Pixel;
                g.DrawImage(bmp, 0, 0, r, GraphicsUnit.Pixel);
                g.Save();

            }

            imageResult.Save(System.Web.HttpContext.Current.Server.MapPath("/Content/Uploads/" + provider_id.ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);

        }
    }

    public class UploadResult
    {
        public string filename;
        public string status;
        public string url;
    }
    public class UploadResultError
    {
        public string error;
        public string status;
    }

}