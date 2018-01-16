namespace Comidat.Model
{
    public interface IReader : IPacket
    {
        /// <summary>
        ///     Frequency ranges: 2.4 GHz, 3.6 GHz, 4.9 GHz, 5 GHz, and 5.9 GHz bands.
        /// </summary>
        /// <see>
        ///     <cref>https://stackoverflow.com/questions/11217674/how-to-calculate-distance-from-wifi-router-using-signal-strength</cref>
        /// </see>
        /// <example>
        ///     2412 - 2462 - 3660 - 5040 - 5865 MHz
        ///     2412000 KHz
        /// </example>
        uint Frequency { set; get; }

        /// <summary>
        ///     X cordinate of reader Esp
        /// </summary>
        double X { get; }

        /// <summary>
        ///     Y cordinate of reader Esp
        /// </summary>
        double Y { get; }
    }
}