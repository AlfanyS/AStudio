using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
using System.IO;

namespace ASCIIDrawing
{
    class Program
    {
        private const double SIZE_OFFSET = 3;
        private const int MAX_WIDTH = 450;
        [STAThread]
        static void Main(string[] args)
        {
            switch ((string) new AppSettingsReader().GetValue("VisualizingMode", typeof(string)))
            {
                case "File":
                    StartFileMode();
                    break;
                case "Video":
                    StartVideoMode();
                    break;
                case "Photo":
                    StartPhotoMode();
                    break;
            }
        }

        private static void StartPhotoMode()
        {
            CameraPresenter.Photo();
        }

        static void StartFileMode()
        {
            Bitmap img = new Bitmap($"{Environment.CurrentDirectory}/Image.bmp");
            if ((string)new AppSettingsReader().GetValue("ResizeMode", typeof(string)) == "Scale")
                img = BitmapExtensions.Scale(img, (double)new AppSettingsReader().GetValue("Offset", typeof(double)));
            else
                img = BitmapExtensions.Resize(img, (int)new AppSettingsReader().GetValue("MaxWidth", typeof(int)),
                    (double)new AppSettingsReader().GetValue("Offset", typeof(double)));
            var rows = new BitmapToASCIIConverter(img).Convert();
            foreach(var row in rows)
                Console.WriteLine(row);
            Console.SetCursorPosition(0, 0);
            Console.ReadLine();
        }
        static void StartVideoMode()
        {
            var presenter = new CameraPresenter();
            bool power = false;
            while (true)
            {
                Console.ReadLine();
                power = !power;
                if (power)
                    presenter.Start();
                else
                    presenter.Stop();
            }
        }
        

    }
}
