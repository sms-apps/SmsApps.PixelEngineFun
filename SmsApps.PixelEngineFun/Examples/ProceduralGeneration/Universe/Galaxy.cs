using PixelEngine;
using PixelEngine.Utilities;

namespace SmsApps.PixelEngineFun.Examples.ProceduralGeneration.Universe
{
    public class Galaxy : Game
    {
        public Vector2 GalaxyOffset = new Vector2(0, 0);
        public uint SelectedStarSeed1 = 0;
        public uint SelectedStarSeed2 = 0;
        public bool StarSelected = false;

        public Galaxy()
        {
            AppName = "Pixel-Engine Galaxy";
        }

        public override void OnUpdate(float delta)
        {
            if (delta <= 0.0001f) return;
            Clear(Pixel.Presets.Black);

            // Handle mouse hover.
            if (GetKey(Key.W).Down) GalaxyOffset.y -= 50.0f * delta;
            if (GetKey(Key.S).Down) GalaxyOffset.y += 50.0f * delta;
            if (GetKey(Key.A).Down) GalaxyOffset.x -= 50.0f * delta;
            if (GetKey(Key.D).Down) GalaxyOffset.x += 50.0f * delta;

            int sectorsX = ScreenWidth / 16;
            int sectorsY = ScreenHeight / 16;

            Vector2 mouse = new Vector2(MouseX / 16, MouseY / 16);
            //Vector2 galaxy_mouse = mouse + vGalaxyOffset;
            Vector2 screenSector = new Vector2(0, 0);

            for (screenSector.x = 0; screenSector.x < sectorsX; screenSector.x++)
            {
                for (screenSector.y = 0; screenSector.y < sectorsY; screenSector.y++)
                {
                    uint seed1 = (uint)GalaxyOffset.x + (uint)screenSector.x;
                    uint seed2 = (uint)GalaxyOffset.y + (uint)screenSector.y;

                    StarSystem star = new StarSystem(seed1, seed2);
                    if (star.Exists)
                    {
                        var circlePoint = new Point((int)screenSector.x * 16 + 8, (int)screenSector.y * 16 + 8);
                        FillCircle(circlePoint, (int)star.Diameter / 8, star.Color);

                        // For convenience highlight hovered star
                        if (mouse.x == screenSector.x && mouse.y == screenSector.y)
                        {
                            DrawCircle(circlePoint, 12, Pixel.Presets.Yellow);
                        }
                    }
                }
            }

            // Handle Mouse Click
            if (GetMouse(0).Pressed)
            {
                uint seed1 = (uint)GalaxyOffset.x + (uint)mouse.x;
                uint seed2 = (uint)GalaxyOffset.y + (uint)mouse.y;

                var star = new StarSystem(seed1, seed2);
                if (star.Exists)
                {
                    StarSelected = true;
                    SelectedStarSeed1 = seed1;
                    SelectedStarSeed2 = seed2;
                }
                else
                {
                    StarSelected = false;
                }
            }

            // Draw Details of selected star system
            if (StarSelected)
            {
                // Generate full star system
                var star = new StarSystem(SelectedStarSeed1, SelectedStarSeed2, true);

                // Draw Window
                FillRect(new Point(8, 240), 496, 232, Pixel.Presets.DarkBlue);
                DrawRect(new Point(8, 240), 496, 232, Pixel.Presets.White);

                // Draw Star
                var starBody = new Vector2(14, 356);
                starBody.Set((float)(starBody.x + (star.Diameter * 1.375)), starBody.y);

                FillCircle(starBody, (int)(star.Diameter * 1.375), star.Color);
                starBody.Set((float)(starBody.x + (star.Diameter * 1.375) + 8), starBody.y);

                // Draw Planets
                foreach (var planet in star.Planets)
                {
                    if (starBody.x + planet.Diameter >= 496) break;

                    starBody.Set((float)(starBody.x + planet.Diameter), starBody.y);
                    FillCircle(starBody, (int)(planet.Diameter * 1.0), Pixel.Presets.Red);

                    var vMoon = starBody;
                    vMoon.Set(vMoon.x, (float)(vMoon.y + planet.Diameter + 10));

                    // Draw Moons
                    foreach (var moon in planet.Moons)
                    {
                        vMoon.Set(vMoon.x, (float)(vMoon.y + moon));
                        FillCircle(vMoon, (int)(moon * 1.0), Pixel.Presets.Grey);
                        vMoon.Set(vMoon.x, (float)(vMoon.y + moon + 10));
                    }

                    starBody.Set((float)(starBody.x + planet.Diameter + 8), starBody.y);
                }
            }
        }
    }
}