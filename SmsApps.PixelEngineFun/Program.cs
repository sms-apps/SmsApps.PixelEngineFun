using SmsApps.PixelEngineFun.Examples;

namespace SmsApps.PixelEngineFun
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RandomPixels(100, 100, 5, 5);
        }

        private static void RandomPixels(int width, int height, int pixelWidth, int pixelHeight)
        {
            var rp = new RandomPixels();

            // Construct the 100x100 game window with 5x5 pixels
            rp.Construct(width, height, pixelWidth, pixelHeight);

            // Start and show a window
            rp.Start();
        }
    }
}