#region

using System;
using Comidat.Runtime;

#endregion

namespace Comidat.Model
{
    /// <summary>
    ///     Mac Address Controller
    /// </summary>
    /// <see>
    ///     <cref>https://msdn.microsoft.com/tr-tr/library/windows/desktop/aa450085.aspx</cref>
    /// </see>
    public class MacAddress : IEquatable<MacAddress>
    {
        /// <summary>
        ///     24-bit ID uniquely assigned to each manufacturer of network adapters. This is also known as the manufacturer ID.
        /// </summary>
        private readonly byte[] _companyId;

        /// <summary>
        ///     40-bit uniquely assigned to each network adapter at the time of assembly. This is also known as the board ID.
        /// </summary>
        private readonly byte[] _extensionId;

        private MacAddress()
        {
            _companyId = new byte[3];
            _extensionId = new byte[5];
        }

        /// <summary>
        ///     Constractor of mac address
        /// </summary>
        /// <param name="mac">String type of mac address.</param>
        /// <example>00:00:00:00:00:00</example>
        /// <example>11:11:11:11:11:11:11:11</example>
        public MacAddress(string mac) : this()
        {
            var keys = mac.Split(':', '-');
            if (!(keys.Length == 6 || keys.Length == 8))
                throw new ArgumentException(Localization.Get("Comidat.Util.MacAddress.Constructor.NotValidException"));

            var i = 0;
            for (; i < 3; i++)
                _companyId[i] = Convert.ToByte(keys[i], 16);

            if (keys.Length == 6)
            {
                _extensionId[0] = 0x00; //0xff;
                _extensionId[1] = 0x00; //0xfe;

                for (i += 2; i < 8; i++)
                    _extensionId[i - 3] = Convert.ToByte(keys[i - 2], 16);
                //_companyId[0] ^= 2;
            }
            else
            {
                for (; i < 8; i++)
                    _extensionId[i - 3] = Convert.ToByte(keys[i], 16);
            }
        }

        /// <summary>
        ///     ULong to mac address
        /// </summary>
        /// <param name="mac"></param>
        public MacAddress(ulong mac) : this()
        {
            var i = 0;
            for (; i < 40; i += 8)
                _extensionId[4 - i / 8] = (byte) ((mac >> i) & 0xFF);

            for (; i < 64; i += 8)
                _companyId[2 - (i / 8 - 5)] = (byte) ((mac >> i) & 0xFF);
        }

        public bool Equals(MacAddress other)
        {
            return other.GetLong() == GetLong();
        }

        /// <summary>
        ///     Get ulong of mac address
        /// </summary>
        /// <returns></returns>
        public ulong GetLong()
        {
            var a0 = _companyId[0] << 24;
            var a1 = _companyId[1] << 16;
            var a2 = _companyId[2] << 8;
            var a3 = _extensionId[0] << 0;

            var a4 = _extensionId[1] << 24;
            var a5 = _extensionId[2] << 16;
            var a6 = _extensionId[3] << 8;
            var a7 = _extensionId[4] << 0;

            return ((ulong) (a0 | a1 | a2 | a3) << 32) | (ulong) ((a4 | a5 | a6 | a7) & 0x00000000ffffffff);
        }


        public static implicit operator ulong(MacAddress a)
        {
            return a.GetLong();
        }

        public static implicit operator MacAddress(ulong a)
        {
            return new MacAddress(a);
        }

        public override string ToString()
        {
            return (BitConverter.ToString(_companyId) + '-' + BitConverter.ToString(_extensionId)).Replace('-', ':');
        }

        public override bool Equals(object obj)
        {
            return obj is MacAddress address && Equals(address);
        }

        public override int GetHashCode()
        {
            return GetLong().GetHashCode();
        }
    }
}