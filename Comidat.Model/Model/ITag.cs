using System;

namespace Comidat.Model
{
    public interface ITag : IPacket, IEquatable<ITag>
    {
        /// <summary>
        ///     Received signal strength indication
        /// </summary>
        /// <seealso>
        ///     <cref>https://en.wikipedia.org/wiki/Received_signal_strength_indication</cref>
        /// </seealso>
        // ReSharper disable once InconsistentNaming
        byte RSSI { set; get; }

        /// <summary>
        ///     SSID is short for service set identifier.
        ///     SSID is a case sensitive, 32 alphanumeric character unique identifier attached to the header of packets sent over a
        ///     wireless local-area network (WLAN). The SSID acts as a password when a mobile device tries to connect to the basic
        ///     service set (BSS) -- a component of the IEEE 802.11 WLAN architecture.
        /// </summary>
        /// <seealso>
        ///     <cref>https://www.webopedia.com/TERM/S/SSID.html</cref>
        /// </seealso>
        // ReSharper disable once InconsistentNaming
        string SSID { set; get; }

        /// <summary>
        ///     Reader which readed from
        /// </summary>
        MacAddress ReaderMacAddress { get; }
    }
}