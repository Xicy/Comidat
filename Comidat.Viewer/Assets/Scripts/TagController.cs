using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class TagController : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DoCheck());
    }

    IEnumerator DoCheck()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Man.prefab");
        for (; ; )
        {
            GameObject tag;
            MoveController mover;
            foreach (var tagWithLocation in DataContext.Instance.GetTagsLocation())
            {
                tag = GameObject.Find("tag_" + tagWithLocation.TagId);
                if (tag == null)
                {
                    tag = Instantiate(prefab);
                    tag.name = "tag_" + tagWithLocation.TagId;
                }
                mover = tag.GetComponent<MoveController>();
                mover.IsActive = !((DateTime.Now - tagWithLocation.RecordDateTime).TotalMinutes > 10);
                mover.Move(new Vector3((float)tagWithLocation.XPosition, (float)tagWithLocation.YPosition, (float)tagWithLocation.ZPosition));
            }

            yield return new WaitForSeconds(5);
        }
    }
}
