namespace Comidat.Model
{
    /// <summary>
    ///     Free-space path loss in decibels constants
    /// </summary>
    /// <seealso>
    ///     <cref>https://en.wikipedia.org/wiki/Free-space_path_loss</cref>
    /// </seealso>
    // ReSharper disable once InconsistentNaming
    public abstract class FSPL
    {
        /// <summary>
        ///     Distance Meter and Frequency KHz
        /// </summary>
        public const float MeterAndKiloHertz = -87.55F;

        /// <summary>
        ///     Distance Meter and Frequency MHz
        /// </summary>
        public const float MeterAndMegaHertz = -27.55F;

        /// <summary>
        ///     Distance Kilometer and Frequency MHz
        /// </summary>
        public const float KiloMeterAndMegaHertz = 32.45F;
    }
}