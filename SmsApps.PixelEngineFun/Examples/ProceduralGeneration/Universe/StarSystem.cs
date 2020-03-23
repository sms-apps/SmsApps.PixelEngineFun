﻿using PixelEngine;
using System;
using System.Collections.Generic;

namespace SmsApps.PixelEngineFun.Examples.ProceduralGeneration.Universe
{
    public class StarSystem
    {
        private readonly uint[] starColours = new uint[8]
        {
            0xFFFFFFFF, 0xFFD9FFFF, 0xFFA3FFFF, 0xFFFFC8C8,
            0xFFFFCB9D, 0xFF9F9FFF, 0xFF415EFF, 0xFF28199D
        };

        #region Properties

        public Pixel Color = Pixel.Presets.White;
        public uint ProcGen = 0;
        public double Diameter { get; set; }
        public bool Exists { get; set; }
        public List<Planet> Planets { get; set; } = new List<Planet>();

        #endregion Properties

        #region Construction, Destruction

        public StarSystem(uint x, uint y, bool generateFullSystem = false)
        {
            // Set seed based on location of star system
            ProcGen = (x & 0xFFFF) << 16 | (y & 0xFFFF);

            // Not all locations contain a system
            Exists = (RandomInt(0, 20) == 1);
            if (!Exists) return;

            // Generate Star
            Diameter = RandomDouble(10.0, 40.0);
            Color = Pixel.FromRgb(starColours[RandomInt(0, 8)]);

            // When viewing the galaxy map, we only care about the star so abort early.
            if (!generateFullSystem) return;

            // If we are viewing the system map, we need to generate the full system.
            double distanceFromStar = RandomDouble(60.0, 200.0);
            int planets = RandomInt(0, 10);

            for (int i = 0; i < planets; i++)
            {
                Planet planet = new Planet { Distance = distanceFromStar };

                // bump distance from star for next iteration
                distanceFromStar += RandomDouble(20.0, 200.0);
                planet.Diameter = RandomDouble(4.0, 20.0);

                // Could make temeprature a function of distance from star
                planet.Temperature = RandomDouble(-200.0, 300.0);

                // Composition of planet
                planet.Foliage = RandomDouble(0.0, 1.0);
                planet.Minerals = RandomDouble(0.0, 1.0);
                planet.Gases = RandomDouble(0.0, 1.0);
                planet.Water = RandomDouble(0.0, 1.0);

                // Normalise to 100%
                double dSum = 1.0 / (planet.Foliage + planet.Minerals + planet.Gases + planet.Water);
                planet.Foliage *= dSum;
                planet.Minerals *= dSum;
                planet.Gases *= dSum;
                planet.Water *= dSum;

                // Population could be a function of other habitat encouraging properties, such as temperature and water
                planet.Population = Math.Max(RandomInt(-5000000, 20000000), 0);

                // 10% of planets have a ring
                planet.Ring = RandomInt(0, 10) == 1;

                // Satellites (Moons)
                int moons = Math.Max(RandomInt(-5, 5), 0);
                for (int n = 0; n < moons; n++)
                {
                    // A moon is just a diameter for now, but it could be whatever you want!
                    planet.Moons.Add(RandomDouble(1.0, 5.0));
                }

                // Add planet to list
                Planets.Add(planet);
            }
        }

        #endregion Construction, Destruction

        #region Private Support Methods

        private uint Random()
        {
            ProcGen += 0xe120fc15;
            ulong tmp = (ulong)ProcGen * 0x4a39b70d;
            ulong m1 = (tmp >> 32) ^ tmp;
            tmp = m1 * 0x12fad5c9;
            ulong m2 = (tmp >> 32) ^ tmp;
            return (uint)m2;
        }

        private double RandomDouble(double min, double max)
        {
            return Random() / (double)(0x7FFFFFFF) * (max - min) + min;
        }

        private int RandomInt(int min, int max)
        {
            return (int)(Random() % (max - min)) + min;
        }

        #endregion Private Support Methods
    }
}