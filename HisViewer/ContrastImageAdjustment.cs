using System;
using System.Windows.Media.Imaging;

namespace HisViewer
{
    class ContrastImageAdjustment : ImageAdjustmentInterface
    {
        public BitmapSource apply(BitmapSource source, double adjustmentVector)
        {
            ushort[] pixels = new ushort[source.PixelWidth * source.PixelHeight];
            source.CopyPixels(pixels, source.PixelWidth * 2, 0);

            double threshold = Math.Pow((1 + adjustmentVector) / 1, 2);
            double max = ushort.MaxValue;

            for (var i = 0; i < pixels.Length; i++)
            {
                pixels[i] = (ushort)Clamp(((((pixels[i] / max) - 0.5) * threshold) + 0.5) * max);
            }

            return BitmapSource.Create(source.PixelWidth, source.PixelHeight, 72, 72, source.Format, source.Palette, pixels, source.PixelWidth * 2);
        }

        private double Clamp (double value)
        {
            return (value > ushort.MaxValue) ? ushort.MaxValue : (value < 0) ? 0 : value;
        }
    }
}
