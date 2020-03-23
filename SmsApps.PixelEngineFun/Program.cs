using SmsApps.PixelEngineFun.Examples;
using SmsApps.PixelEngineFun.Examples.ProceduralGeneration.Universe;

namespace SmsApps.PixelEngineFun
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //RandomPixels(100, 100, 5, 5);
            Galaxy(512, 480, 2, 2);
        }

        #region Example Setup

        private static void Galaxy(int width, int height, int pixelWidth, int pixelHeight)
        {
            var galaxy = new Galaxy();
            galaxy.Construct(width, height, pixelWidth, pixelHeight);
            galaxy.Start();
        }

        private static void RandomPixels(int width, int height, int pixelWidth, int pixelHeight)
        {
            var rp = new RandomPixels();
            rp.Construct(width, height, pixelWidth, pixelHeight);
            rp.Start();
        }

        #endregion Example Setup
    }
}