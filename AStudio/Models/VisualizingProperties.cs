using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Media;

namespace AStudio.Models
{
    public class VisualizingProperties
    {
        public Bitmap Image = new Bitmap(Environment.CurrentDirectory + @"/Icons/NoImage.png");
        public string Text;
        public string VisualizingMode;
        public string ResizeMode = "Resize";
        public double Offset = 1;
        public int MaxWidth = 350;
        public int CameraId;
        public int Fps = 5;
    }
}
