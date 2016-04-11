using System.Windows.Media.Imaging;

namespace HisViewer
{
    interface ImageAdjustmentInterface
    {
        BitmapSource apply(BitmapSource source, double adjustmentVector);
    }
}
