using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AStudio.Views
{
    /// <summary>
    /// Логика взаимодействия для TextPresenterWindow.xaml
    /// </summary>
    public partial class TextPresenterWindow : Window
    {
        public static Window Instance;
        public TextPresenterWindow(List<string> text = null)
        {
            Instance = this;
            InitializeComponent();
            foreach (var line in text)
                TextPresenter.Text += line+"\n";
            
            List<string> brushes = new List<string>();
            foreach ( PropertyInfo propInfo in typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static) )
                if (propInfo.PropertyType == typeof(SolidColorBrush) && propInfo.Name != "Transparent")
                    brushes.Add(propInfo.Name);
            FColorBox.ItemsSource = brushes;
            BColorBox.ItemsSource = brushes;

        }
    }
}
