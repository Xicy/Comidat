using System;
using System.Collections;
using UnityEngine;

public class TagController : MonoBehaviour
{
    public GameObject TagsObject;
    public ClosestPointGroup closePointFinder;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DoCheck());
    }

    IEnumerator DoCheck()
    {
        GameObject prefab = Resources.Load<GameObject>("Man");
        for (; ; )
        {
            GameObject tag;
            MoveController mover;
            foreach (var tagWithLocation in DataContext.Instance.GetTagsLocation())
            {
                tag = TagsObject.Find("Tag_" + tagWithLocation.pos.TagId, true);
                if (tag == null)
                {
                    tag = Instantiate(prefab, TagsObject.transform);
                    var info = tag.GetComponent<TagInfo>();
                    info.info = tagWithLocation.tag;
                    info.LocInfo = tagWithLocation.pos;
                    tag.GetComponentInChildren<TextMesh>().text = tagWithLocation.tag.TagFullName;
                    tag.transform.position = closePointFinder.GetPosition(new Vector3(tagWithLocation.pos.XPosition, tagWithLocation.pos.YPosition - 5, tagWithLocation.pos.ZPosition));
                    tag.name = "Tag_" + tagWithLocation.pos.TagId;
                    tag.GetComponent<MoveController>().IsActive = !((DateTime.Now - tagWithLocation.pos.RecordDateTime).TotalMinutes > 10);
                    continue;
                }
                mover = tag.GetComponent<MoveController>();
                mover.IsActive = !((DateTime.Now - tagWithLocation.pos.RecordDateTime).TotalMinutes > 10);

                if (mover.IsActive && !mover.Moving)
                    mover.Move(closePointFinder.GetPosition(new Vector3(tagWithLocation.pos.XPosition, tagWithLocation.pos.YPosition - 5, tagWithLocation.pos.ZPosition)));
            }

            yield return new WaitForSeconds(5);
        }
    }
}
