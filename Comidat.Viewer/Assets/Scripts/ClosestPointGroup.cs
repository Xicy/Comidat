using UnityEngine;

public class ClosestPointGroup : MonoBehaviour
{
    private ClosestPointLineRenderer[] cp;

    public void UpdateFunction()
    {
        cp = GetComponentsInChildren<ClosestPointLineRenderer>();
    }

    public Vector3 GetPosition(Vector3 pos)
    {
        Vector3 r = default(Vector3), p;
        float len, mag = float.MaxValue;
        foreach (var closestPointLineRenderer in cp)
        {
            p = closestPointLineRenderer.GetPosition(pos);
            len = Vector3.Distance(p, pos);
            if (!(mag > len)) continue;
            mag = len;
            r = p;
        }

        RaycastHit hitInfo;
        if (Physics.Raycast(r, Vector3.down, out hitInfo, Mathf.Infinity, 1 << 9))
            return hitInfo.point;
        return r;
    }
}
