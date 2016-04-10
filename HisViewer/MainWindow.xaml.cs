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
                    this.MainImage.Source = parser.parseImageFile(info);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error reading file", MessageBoxButton.OK);
                }
            }
        }
    }
}
