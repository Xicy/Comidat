using System;
using Comidat.Model;

namespace Comidat.Runtime
{
    public static partial class Helper
    {
        public static Vector3d NearestPointOnLine(Vector3d[] arr, Vector3d point)
        {
            Vector3d ret = Vector3d.Zero;
            var rn = double.MaxValue;
            var count = arr.Length;
            if (count < 2) throw new Exception("Vectors not correct");
            for (var i = count - 1; i >= 1; i--)
            {
                Vector3d r;

                var end = arr[i];
                var start = arr[i - 1];

                var ps = point - start;
                var norm = (end - start);
                double leng = norm.Length;
                norm.Normalize();
                double dot = Vector3d.Dot(norm, ps);

                if (dot <= 0f)
                    r = start;
                else if (dot >= leng)
                    r = end;
                else
                    r = start + norm * dot;

                var mag = (r - point).Length;
                if (!(rn > mag)) continue;
                ret = r;
                rn = mag;
            }
            return ret;
        }
    }
}