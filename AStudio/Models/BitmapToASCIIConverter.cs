using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace AStudio.Models
{
    public class BitmapToASCIIConverter
    {
        private char[] asciiTable = {'.', ',', ':', '+', '*', '?', '%', '$', '&', '#', '@' };
        private Bitmap Image;
        public BitmapToASCIIConverter(Bitmap img)
        {
            Image = img;
        }

        public char[][] Convert()
        {
            Image.ToGrayGradationUnsafe();
            var output = new char[Image.Height][];
            for (int y = 0; y < Image.Height; y++)
            {
                output[y] = new char[Image.Width];
                for (int x = 0; x < Image.Width; x++)
                {
                    var r = (int)Map(Image.GetPixel(x, y).R, 0, 225, 0, asciiTable.Length - 1);
                    if (r >= asciiTable.Length)
                        r = asciiTable.Length - 1;
                    output[y][x] = asciiTable[r];
                }
            }
            return output;
        }
        public unsafe char[][] ConvertUnsafe()
        {
            Image.ToGrayGradationUnsafe();
            BitmapData bd = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            var output = new char[Image.Height][];
            byte* ptr = (byte*)bd.Scan0;
            for (int y = 0; y < Image.Height; y++)
            {
                output[y] = new char[Image.Width];
                for (int x = 0; x < Image.Width; x++)
                {
                    var mapedPixel = (int)Map(Image.GetPixel(x, y).R, 0, 226, 0, asciiTable.Length - 1);
                    if (mapedPixel >= asciiTable.Length)
                        mapedPixel = asciiTable.Length - 1;
                    output[y][x] = asciiTable[mapedPixel];
                }
            }
            return output;
        }
        private float Map(float valueToMap, float start1,float stop1,float start2,float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2-start2) + start2;
        }
    }
}
