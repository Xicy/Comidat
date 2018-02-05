using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

    public static GameObject Find(this GameObject go, string nameToFind, bool bSearchInChildren)
    {
        if (bSearchInChildren)
        {
            var transform = go.transform;
            for (int i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i);
                if (child.gameObject.name == nameToFind)
                    return child.gameObject;
                GameObject result = child.gameObject.Find(nameToFind, bSearchInChildren);
                if (result != null) return result;
            }
            return null;
        }
        else
        {
            return GameObject.Find(nameToFind);
        }
    }
}
