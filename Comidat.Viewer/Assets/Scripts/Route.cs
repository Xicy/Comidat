using System.Collections.Generic;
using UnityEngine;

public class Route
{
    public Route(Vector3 pos)
    {
        position = pos;
        prev = new List<int>();
        next = new List<int>();
    }
    public Vector3 position;
    public List<int> prev;
    public List<int> next;
}
