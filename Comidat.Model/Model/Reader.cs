#region

#endregion

namespace Comidat.Model
{
    public class Reader : IReader
    {
        /// <summary>
        ///     Reader Constracter for reader info
        /// </summary>
        /// <param name="macAddress">Mac Address of reader</param>
        /// <param name="frequency">Signal Frequency of reader</param>
        /// <param name="x">X Cordinate of Reader</param>
        /// <param name="y">Y Cordinate of Reader</param>
        public Reader(MacAddress macAddress, uint frequency, int x, int y)
        {
            MacAddress = macAddress;
            Frequency = frequency;
            X = x;
            Y = y;
        }

        /// <inheritdoc />
        public MacAddress MacAddress { get; }

        /// <inheritdoc />
        public uint Frequency { get; set; }

        /// <inheritdoc />
        public double X { get; }

        /// <inheritdoc />
        public double Y { get; }
    }
}