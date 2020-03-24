namespace SmsApps.PixelEngineFun.Common
{
    /// <summary> Represents a custom random number generator. </summary>
    public interface IRandomizer
    {
        /// <summary> Generate and return a random unsigned integer. </summary>
        /// <returns> uint </returns>
        uint Random();

        /// <summary> Generate and return a random double between <paramref name="min"/> and <paramref name="max"/>. </summary>
        /// <param name="min"> Minimum double to return. </param>
        /// <param name="max"> Maximum double to return. </param>
        /// <returns> double </returns>
        double RandomDouble(double min, double max);

        /// <summary> Generate and return a random integer between <paramref name="min"/> and <paramref name="max"/>. </summary>
        /// <param name="min"> Minimum integer to return. </param>
        /// <param name="max"> Maximum integer to return. </param>
        /// <returns> int </returns>
        int RandomInt(int min, int max);
    }
}