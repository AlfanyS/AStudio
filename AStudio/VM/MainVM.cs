using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using AStudio.Commands;
using AStudio.Models;
using Microsoft.Win32;
using AStudio.Views;
using System.Drawing;

namespace AStudio.VM
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropetyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(name));

        public VisualizingProperties CurrentConfig = new VisualizingProperties();
        public ICommand CloseCommand { get; } = new CloseWindowCommand((sender)=> { MainWindow.Instance.Close(); });
        public ICommand MinimizeCommand { get; } = new MinimizeWindowCommand((sender) => { MainWindow.Instance.WindowState = System.Windows.WindowState.Minimized; });

        public ICommand OpenFileCommand
        {
            get => new OpenFileCommand((sender)=> {
                var openFile = new OpenFileDialog();
                openFile.Filter = "Image files|*.jpg;*.bmp;*.png";
                if (openFile.ShowDialog() == true)
                    CurrentConfig.Image = new Bitmap(openFile.FileName);
                OnPropetyChanged("Image");
            });
        }
        public ICommand VisualizeFileCommand
        {
            get => new VisualizeFileCommand((sender) =>{
                var vs = new ImageASCIIContstructor(CurrentConfig);
                new TextPresenterWindow(vs.CreateASCIIImage()).Show();
            });
        }

        public ImageSource Image
        {
            get => BitmapExtensions.ImageSourceFromBitmap(CurrentConfig.Image);
        }

        public int CurrentCameraId
        {
            get => CurrentConfig.CameraId;
            set
            {
                CurrentConfig.CameraId = value;
                OnPropetyChanged();
            }
        }

        public string Text 
        {
            get => CurrentConfig.Text;
            set
            {
                CurrentConfig.Text = value;
                OnPropetyChanged();
            }
        }

        public string ResizeMode 
        {
            get => CurrentConfig.ResizeMode;
            set
            {
                CurrentConfig.ResizeMode = value;
                OnPropetyChanged();
            }
        }
        public string Offset
        {
            get => CurrentConfig.Offset.ToString();
            set
            {
                CurrentConfig.Offset = double.TryParse(value, out double dbl) && dbl >= 0 ? dbl : 1;
                OnPropetyChanged();
            }
        }

        public string MaxWidth 
        {
            get => CurrentConfig.MaxWidth.ToString();
            set
            {
                CurrentConfig.MaxWidth = int.TryParse(value,out int nt) && nt>=0 ? nt : 100;
                OnPropetyChanged();
            }
        }

    }
}
