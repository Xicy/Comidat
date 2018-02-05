using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOverMouse : MonoBehaviour
{
    public TagInfoViewer InfoPanel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                TagInfo ti = hitInfo.transform.gameObject.GetComponent<TagInfo>();
                InfoPanel.UpdateInfo(ti.info, hitInfo.transform);
            }
        }
    }
}
