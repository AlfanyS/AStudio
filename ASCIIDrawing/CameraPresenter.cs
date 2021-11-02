using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Drawing;
using System.Configuration;

namespace ASCIIDrawing
{
    class CameraPresenter
    {
        private VideoCapture capture;

        public void Start()
        {
            capture = new VideoCapture((int)new AppSettingsReader().GetValue("CameraId", typeof(int)), VideoCapture.API.Any);
            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
        }

        public void Stop() => capture.Stop();

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            Mat m = new Mat();
            capture.Retrieve(m);
            var img = m.ToBitmap();
            if ((string)new AppSettingsReader().GetValue("ResizeMode", typeof(string)) == "Scale")
                img = BitmapExtensions.Scale(img, (double)new AppSettingsReader().GetValue("Offset", typeof(double)));
            else
                img = BitmapExtensions.Resize(img, (int)new AppSettingsReader().GetValue("MaxWidth", typeof(int)),
                    (double)new AppSettingsReader().GetValue("Offset", typeof(double)));

            var converter = new BitmapToASCIIConverter(img);
            foreach(var row in converter.Convert())
                Console.WriteLine(row);
            Thread.Sleep((int)new AppSettingsReader().GetValue("FramesDelay", typeof(int)));
            Console.Clear();
        }
        public static void Photo()
        {
            var cam = new VideoCapture((int)new AppSettingsReader().GetValue("CameraId", typeof(int)), VideoCapture.API.Any);
            cam.Start();
            cam.ImageGrabbed += (s,e) => {
                Mat m = new Mat();
                cam.Retrieve(m);
                m.ToBitmap().Save("./Image.bmp");
                cam.Stop();
            };
            Thread.Sleep(1500);
        }
    }
}
