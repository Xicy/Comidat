using System;
using System.Collections.Generic;
using System.Linq;
using Comidat.Model;

namespace Comidat.Runtime
{
    public static class Helper
    {
        /// <summary>
        ///     Calculate distance between router and client
        /// </summary>
        /// <param name="flspConst">Constan for unit of meter kilometer <see cref="FSPL" /></param>
        /// <param name="signalLevelInDb">Signal RSSI </param>
        /// <param name="freq">Frequancy of signal</param>
        /// <seealso>
        ///     <cref>https://stackoverflow.com/questions/11217674/how-to-calculate-distance-from-wifi-router-using-signal-strength</cref>
        /// </seealso>
        /// <returns>Distance of wifi to router</returns>
        public static double CalculateDistance(double flspConst, double signalLevelInDb, double freq)
        {
            var exp = (-flspConst - 20 * Math.Log10(freq) + Math.Abs(signalLevelInDb)) / 20.0;
            return Math.Pow(10.0, exp);
        }

        /// <summary>
        ///     Enum flag check
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag(byte value, byte flag)
        {
            return (value & flag) > 0;
        }

#if NET461
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
#endif

        private static bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }

                finished = i == 0;
            }

            return changed;
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> elements, int k)
        {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, k));
        }

        private static double Factorial(double number)
        {
            return number <= 1 ? 1 : number * Factorial(number - 1);
        }

        public static double Combination(double n, double r)
        {
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }
    }
}