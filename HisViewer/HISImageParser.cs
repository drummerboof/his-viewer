using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HisViewer
{
    class HISImageParser : ImageParserInterface
    {
        public static int HEADER_BYTE_LENGTH = 100;
        public static int IMAGE_WIDTH = 1024;
        public static int IMAGE_HEIGHT = 1024;

        public BitmapSource parseImageFile(FileInfo file)
        {
            ushort[] buffer = new ushort[IMAGE_WIDTH * IMAGE_HEIGHT];

            using (BinaryReader reader = new BinaryReader(file.OpenRead()))
            {
                reader.BaseStream.Seek(HEADER_BYTE_LENGTH, SeekOrigin.Begin);
                for (var i = 0; i < IMAGE_WIDTH * IMAGE_HEIGHT; i++)
                {
                    buffer[i] = reader.ReadUInt16();
                }
            }

            return BitmapSource.Create(IMAGE_WIDTH, IMAGE_HEIGHT, 72, 72, PixelFormats.Gray16, BitmapPalettes.Gray16, buffer, IMAGE_WIDTH * 2);
        }
    }
}
