namespace SmsApps.PixelEngineFun.Examples
{
    public class LehmerRandomizer
    {
        private uint _seed;

        public LehmerRandomizer(uint seed) => _seed = seed;

        public uint Random()
        {
            _seed += 0xe120fc15;
            ulong tmp = (ulong)_seed * 0x4a39b70d;
            ulong m1 = (tmp >> 32) ^ tmp;
            tmp = m1 * 0x12fad5c9;
            ulong m2 = (tmp >> 32) ^ tmp;
            return (uint)m2;
        }

        public double RandomDouble(double min, double max)
        {
            return Random() / (double)(0x7FFFFFFF) * (max - min) + min;
        }

        public int RandomInt(int min, int max)
        {
            return (int)(Random() % (max - min)) + min;
        }
    }
}