using PixelEngine;
using PixelEngine.Utilities;

namespace SmsApps.PixelEngineFun.Examples.ProceduralGeneration.Universe
{
    public class Galaxy : Game
    {
        public bool bStarSelected = false;
        public uint nSelectedStarSeed1 = 0;
        public uint nSelectedStarSeed2 = 0;
        public Vector2 vGalaxyOffset = new Vector2(0, 0);

        public Galaxy()
        {
            AppName = "Pixel-Engine Galaxy";
        }

        public override void OnUpdate(float delta)
        {
            if (Clock.Elapsed <= 0.0001f) return;
            Clear(Pixel.Presets.Black);

            // Handle mouse hover.
            if (GetKey(Key.W).Pressed) vGalaxyOffset.y -= 50.0f * Clock.Elapsed;
            if (GetKey(Key.S).Pressed) vGalaxyOffset.y += 50.0f * Clock.Elapsed;
            if (GetKey(Key.A).Pressed) vGalaxyOffset.x -= 50.0f * Clock.Elapsed;
            if (GetKey(Key.D).Pressed) vGalaxyOffset.x += 50.0f * Clock.Elapsed;

            int nSectorsX = ScreenWidth / 16;
            int nSectorsY = ScreenHeight / 16;

            Vector2 mouse = new Vector2(MouseX / 16, MouseY / 16);
            Vector2 galaxy_mouse = mouse + vGalaxyOffset;
            Vector2 screen_sector = new Vector2(0, 0);

            for (screen_sector.x = 0; screen_sector.x < nSectorsX; screen_sector.x++)
            {
                for (screen_sector.y = 0; screen_sector.y < nSectorsY; screen_sector.y++)
                {
                    uint seed1 = (uint)vGalaxyOffset.x + (uint)screen_sector.x;
                    uint seed2 = (uint)vGalaxyOffset.y + (uint)screen_sector.y;

                    StarSystem star = new StarSystem(seed1, seed2);
                    if (star.Exists)
                    {
                        var circlePoint = new Point((int)screen_sector.x * 16 + 8, (int)screen_sector.y * 16 + 8);
                        FillCircle(circlePoint, (int)star.Diameter / 8, star.Color);

                        // For convenience highlight hovered star
                        if (mouse.x == screen_sector.x && mouse.y == screen_sector.y)
                        {
                            DrawCircle(circlePoint, 12, Pixel.Presets.Yellow);
                        }
                    }
                }
            }

            // Handle Mouse Click
            if (GetMouse(0).Pressed)
            {
                uint seed1 = (uint)vGalaxyOffset.x + (uint)mouse.x;
                uint seed2 = (uint)vGalaxyOffset.y + (uint)mouse.y;

                var star = new StarSystem(seed1, seed2);
                if (star.Exists)
                {
                    bStarSelected = true;
                    nSelectedStarSeed1 = seed1;
                    nSelectedStarSeed2 = seed2;
                }
                else
                {
                    bStarSelected = false;
                }
            }

            // Draw Details of selected star system
            if (bStarSelected)
            {
                // Generate full star system
                var star = new StarSystem(nSelectedStarSeed1, nSelectedStarSeed2, true);

                // Draw Window
                FillRect(new Point(8, 240), 496, 232, Pixel.Presets.DarkBlue);
                DrawRect(new Point(8, 240), 496, 232, Pixel.Presets.White);

                // Draw Star
                var vBody = new Vector2(14, 356);
                vBody.Set((float)(vBody.x + (star.Diameter * 1.375)), vBody.y);

                FillCircle(vBody, (int)(star.Diameter * 1.375), star.Color);
                vBody.Set((float)(vBody.x + (star.Diameter * 1.375) + 8), vBody.y);

                // Draw Planets
                foreach (var planet in star.Planets)
                {
                    if (vBody.x + planet.Diameter >= 496) break;

                    vBody.Set((float)(vBody.x + planet.Diameter), vBody.y);
                    FillCircle(vBody, (int)(planet.Diameter * 1.0), Pixel.Presets.Red);

                    var vMoon = vBody;
                    vMoon.Set(vMoon.x, (float)(planet.Diameter + 10));

                    // Draw Moons
                    foreach (var moon in planet.Moons)
                    {
                        vMoon.Set(vMoon.x, (float)(vMoon.y + moon));
                        FillCircle(vMoon, (int)(moon * 1.0), Pixel.Presets.Grey);
                        vMoon.Set(vMoon.x, (float)(vMoon.y + moon + 10));
                    }

                    vBody.Set((float)(vBody.x + planet.Diameter + 8), vBody.y);
                }
            }
        }
    }
}