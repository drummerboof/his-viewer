using System.IO;
using System.Windows.Media.Imaging;

namespace HisViewer
{
    interface ImageParserInterface
    {
        BitmapSource parseImageFile(FileInfo file);
    }
}
