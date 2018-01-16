#region

#endregion

namespace Comidat.Model
{
    public class Tag : ITag
    {
        /// <summary>
        ///     Tag constractor
        /// </summary>
        /// <param name="macAddress">mac address of client</param>
        /// <param name="readerMacAddress">Reader Macaddress</param>
        /// <param name="rSSI">signal strength</param>
        /// <param name="sSID">name of wifi</param>
        // ReSharper disable InconsistentNaming
        public Tag(MacAddress macAddress, MacAddress readerMacAddress, byte rSSI, string sSID)
            // ReSharper restore InconsistentNaming
        {
            MacAddress = macAddress;
            RSSI = rSSI;
            SSID = sSID;
            ReaderMacAddress = readerMacAddress;
        }

        /// <inheritdoc />
        public MacAddress MacAddress { get; }

        /// <inheritdoc />
        public byte RSSI { get; set; }

        /// <inheritdoc />
        public string SSID { get; set; }

        public MacAddress ReaderMacAddress { get; }

        public bool Equals(ITag other)
        {
            return MacAddress.Equals(other.MacAddress) && ReaderMacAddress.Equals(other.ReaderMacAddress);
        }

        public override bool Equals(object obj)
        {
            return obj is ITag tag && Equals(tag);
        }

        public override int GetHashCode()
        {
            return MacAddress.GetHashCode() ^ ReaderMacAddress.GetHashCode();
        }
    }
}