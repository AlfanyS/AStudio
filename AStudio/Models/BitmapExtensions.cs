using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AStudio.Models
{
    public static class BitmapExtensions
    {
        public static void ToGrayGradation(this Bitmap bmp)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    var pixel = bmp.GetPixel(x, y);
                    var avg = (pixel.R + pixel.G + pixel.B) / 3;
                    bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(pixel.A, avg, avg, avg));
                }
            }
        }
        public static unsafe void ToGrayGradationUnsafe(this Bitmap bmp)
        {
            BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;
            byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

            for (int y = 0; y < heightInPixels; y++)
            {
                byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    int Blue = currentLine[x];
                    int Green = currentLine[x + 1];
                    int Red = currentLine[x + 2];
                    var avg = (Blue + Green + Red) / 3;
                    currentLine[x] = (byte)avg;
                    currentLine[x + 1] = (byte)avg;
                    currentLine[x + 2] = (byte)avg;
                }
            }
            bmp.UnlockBits(bitmapData);
            //bmp.Save(@"C:\Users\Alfany\Desktop\test.bmp");
        }
        public static void Reduce(ref Bitmap bmp, double offset)
        {
            bmp = new Bitmap(bmp, new System.Drawing.Size((int)(bmp.Width / offset), (int)(bmp.Height / offset)));
        }
        public static void Resize(ref Bitmap bmp, int maxWidth, double offset)
        {
            var newHeight = bmp.Height / offset * maxWidth / bmp.Width;
            bmp = new Bitmap(bmp, new System.Drawing.Size(maxWidth, (int)newHeight));
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        
        }
        public static ImageSource Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                ((System.Drawing.Bitmap)value).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = new MemoryStream(ms.ToArray());
                image.EndInit();

                return image;
            }
        }

    }
}
