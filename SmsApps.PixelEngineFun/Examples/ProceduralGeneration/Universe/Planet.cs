using System;
using System.Collections.Generic;

namespace SmsApps.PixelEngineFun.Examples.ProceduralGeneration.Universe
{
    public class Planet
    {
        public double Diameter = 0.0;
        public double Distance = 0.0;
        public double Foliage = 0.0;
        public double Gases = 0.0;
        public Guid Id = Guid.NewGuid();
        public double Minerals = 0.0;
        public List<double> Moons;
        public double Population = 0.0;
        public bool Ring = false;
        public double Temperature = 0.0;
        public double Water = 0.0;
    }
}