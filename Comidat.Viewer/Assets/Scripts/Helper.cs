using TriLib;
using UnityEngine;

public static class Helper
{
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

    public static Vector3 NearestPointOnLine(this LineRenderer line, Vector3 point)
    {
        Vector3 ret = Vector3.zero;
        var rn = float.MaxValue;
        var count = line.positionCount;
        if (count < 2) throw new UnityException("Line not correct");
        for (var i = count - 1; i >= 1; i--)
        {
            Vector3 r;

            var end = line.GetPosition(i);
            var start = line.GetPosition(i - 1);
            
            var ps = point - start;
            var norm = (end - start).normalized;
            float leng = Vector3.Distance(start, end);
            float dot = Vector3.Dot(norm, ps);

            if (dot <= 0f)
                r = start;
            else if (dot >= leng)
                r = end;
            else
                r = start + norm * dot;

            var mag = Vector3.Distance(point, r);
            if (!(rn > mag)) continue;
            ret = r;
            rn = mag;
        }
        return ret;
    }


    public static void FitToBounds(this Camera camera, Transform transform, float distance)
    {
        var bounds = transform.EncapsulateBounds();
        var boundRadius = bounds.extents.magnitude;
        var finalDistnace = (boundRadius / (2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad))) * distance;
        camera.farClipPlane = finalDistnace * 2f;
        camera.transform.position = new Vector3(bounds.center.x, bounds.center.y + 100, bounds.center.z + finalDistnace);
        camera.transform.LookAt(transform.position + bounds.extents);
    }

    public static GameObject Find(this GameObject go, string nameToFind, bool bSearchInChildren)
    {
        if (!bSearchInChildren) return GameObject.Find(nameToFind);

        var transform = go.transform;
        for (var i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            if (child.gameObject.name == nameToFind)
                return child.gameObject;
            var result = child.gameObject.Find(nameToFind, true);
            if (result != null) return result;
        }
        return null;

    }
}
