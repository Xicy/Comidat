#region

#endregion

namespace Comidat.Model
{
    public interface IPacket
    {
        /// <summary>
        ///     Global Mac Address for ESPs
        /// </summary>
        /// <see>
        ///     <cref>https://tools.ietf.org/html/rfc2469</cref>
        /// </see>
        MacAddress MacAddress { get; }
    }
}