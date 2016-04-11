using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HisViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageSource OriginalSource;

        private readonly BackgroundWorker AdjustmentsWorker = new BackgroundWorker();

        private ImageParserInterface Parser = new HISImageParser();

        private ImageAdjustments Adjustments = new ImageAdjustments();

        public MainWindow()
        {
            InitializeComponent();
            Adjustments.Register(ImageAdjustments.CONTRAST, new ContrastImageAdjustment());
            AdjustmentsWorker.DoWork += HandleAdjustmentsWorkerDoWork;
        }

        private void HandleAdjustmentsWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            List<object> args = (List<object>)e.Argument;
            Dispatcher.Invoke(() => MainImage.Source = Adjustments.Get((string)args[0]).apply((BitmapSource)OriginalSource, (double)args[1]));
        }

        private void HandleOpenClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "HIS files (*.his)|*.his;";

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo info = new FileInfo(openFileDialog.FileName);

                try
                {
                    MainImageBorder.Visibility = Visibility.Visible;
                    MainImage.Source = Parser.parseImageFile(info);
                    OriginalSource = MainImage.Source.Clone();
                    Title = string.Format("His Viewer: {0}", info.Name);
                    FileName.Text = info.FullName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error reading file", MessageBoxButton.OK);
                }
            }
        }

        private void HandleContrastChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            List<object> arguments = new List<object>() { ImageAdjustments.CONTRAST, e.NewValue };
            if (OriginalSource != null && !AdjustmentsWorker.IsBusy)
            {
                AdjustmentsWorker.RunWorkerAsync(arguments);
            }
        }
    }
}
