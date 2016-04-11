using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HisViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ImageAdjustmentInterface ContrastAdjustment = new ContrastImageAdjustment();
        ImageSource OriginalSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandleOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "HIS files (*.his)|*.his;";
            if (openFileDialog.ShowDialog() == true)
            {
                Debug.WriteLine(openFileDialog.FileName);
                FileInfo info = new FileInfo(openFileDialog.FileName);
                ImageParserInterface parser = new HISImageParser();

                try
                {
                    MainImage.Source = parser.parseImageFile(info);
                    OriginalSource = MainImage.Source.Clone();
                    Title = String.Format("His Viewer: {0}", info.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error reading file", MessageBoxButton.OK);
                }
            }
        }

        private void HandleContrastChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (OriginalSource == null)
            {
                return;
            }

            MainImage.Source = ContrastAdjustment.apply((BitmapSource)OriginalSource, e.NewValue);
        }
    }
}
