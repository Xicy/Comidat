using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ClosestPointLineRenderer : MonoBehaviour
{
    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public Vector3 GetPosition(Vector3 pos)
    {
        return lr.NearestPointOnLine(pos);
    }
}
