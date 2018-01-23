#region

using System;
using System.Collections;
using System.Collections.Generic;
using Comidat.Diagnostics;

#endregion

namespace Comidat.Model
{
    public class Packet : IList<ITag>
    {
        /// <summary>
        ///     Seperator for packet data
        /// </summary>
        private const char Seperator = ';';

        /// <summary>
        ///     List of client packet
        /// </summary>
        private readonly List<ITag> _clientPackets;

        public Packet(string data)
        {
            var sdata = data.Replace("-", "").Replace("\r", "").Replace("\n", "")
                .Split(new[] {Seperator}, StringSplitOptions.RemoveEmptyEntries);

            //list of client
            _clientPackets = new List<ITag>();

            //start from 2 up to last item and increase 3 for each step
            for (var i = 0; i < sdata.Length; i += 3)
                //create and add client packet 
                try
                {
                    _clientPackets.Add(new Tag(new MacAddress(ulong.Parse(sdata[i + 1])), new MacAddress(sdata[i + 2]),
                        byte.Parse(sdata[i]), ""));
                }
                catch (Exception e) when (e is ArgumentOutOfRangeException || e is ArgumentException || e is IndexOutOfRangeException || e is FormatException)
                {
                    Logger.Exception(e,data);
                }
        }

        public IEnumerator<ITag> GetEnumerator()
        {
            return _clientPackets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _clientPackets).GetEnumerator();
        }

        public void Add(ITag item)
        {
            _clientPackets.Add(item);
        }

        public void Clear()
        {
            _clientPackets.Clear();
        }

        public bool Contains(ITag item)
        {
            return _clientPackets.Contains(item);
        }

        public void CopyTo(ITag[] array, int arrayIndex)
        {
            _clientPackets.CopyTo(array, arrayIndex);
        }

        public bool Remove(ITag item)
        {
            return _clientPackets.Remove(item);
        }

        public int Count => _clientPackets.Count;

        public bool IsReadOnly => (_clientPackets as ICollection<ITag>).IsReadOnly;

        public int IndexOf(ITag item)
        {
            return _clientPackets.IndexOf(item);
        }

        public void Insert(int index, ITag item)
        {
            _clientPackets.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _clientPackets.RemoveAt(index);
        }

        public ITag this[int index]
        {
            get => _clientPackets[index];
            set => _clientPackets[index] = value;
        }
    }
}