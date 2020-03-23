using PixelEngine;

namespace SmsApps.PixelEngineFun.Examples
{
    public class RandomPixels : Game
    {
        public RandomPixels()
        {
        }

        // Called once per frame
        public override void OnUpdate(float elapsed)
        {
            // Loop through all the pixels
            for (int i = 0; i < ScreenWidth; i++)
            {
                for (int j = 0; j < ScreenHeight; j++)
                {
                    Draw(i, j, Pixel.Random()); // Draw a random pixel
                }
            }
        }
    }
}