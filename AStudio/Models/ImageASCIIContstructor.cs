using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AStudio.Models
{
    class ImageASCIIContstructor
    {
        private VisualizingProperties visualizingProperties;

        public ImageASCIIContstructor(VisualizingProperties vp)
        {
            visualizingProperties = vp;
        }

        public List<string> CreateASCIIImage()
        {
            List<string> result = new List<string>();
            var img = new Bitmap(visualizingProperties.Image);
            if (visualizingProperties.ResizeMode == "Reduce")
                BitmapExtensions.Reduce(ref img, visualizingProperties.Offset);
            else
                BitmapExtensions.Resize(ref img, visualizingProperties.MaxWidth,visualizingProperties.Offset);
            //img.Save(@"C:\Users\Alfany\Desktop\test.bmp");
            BitmapToASCIIConverter converter = new BitmapToASCIIConverter(img);
            foreach (char[] row in converter.Convert())
            {
                string line = "";
                foreach (var symbol in row)
                    line += symbol;
                result.Add(line);
            }
            return result;
        }
    }
}

