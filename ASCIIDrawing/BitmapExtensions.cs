using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIDrawing
{
    public static class BitmapExtensions
    {
        public static void ToGrayGradation(this Bitmap btp)
        {
            for(int x =0;x< btp.Width;x++)
            {
                for(int y =0;y< btp.Height;y++)
                {
                    var pixel = btp.GetPixel(x, y);
                    var avg = (pixel.R + pixel.G + pixel.B) / 3;
                    btp.SetPixel(x, y, Color.FromArgb(pixel.A,avg,avg,avg));
                }
            }
            
        }
        public static Bitmap Scale(Bitmap bmp,double offset)
        {
            return new Bitmap(bmp, new Size((int)(bmp.Width / offset), (int)(bmp.Height / offset)));
        }
        public static Bitmap Resize(Bitmap bmp, int maxWidth, double offset)
        {
            var newHeight = bmp.Height / offset * maxWidth / bmp.Width;
            return new Bitmap(bmp, new Size(maxWidth, (int)newHeight));
        }

    }

}
