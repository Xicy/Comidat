using System;
using System.Collections;
using UnityEngine;

public class TagController : MonoBehaviour
{
    public GameObject TagsObject;
    public ClosestPointGroup ClosePointFinder;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DoCheck());
    }

    private IEnumerator DoCheck()
    {
        var prefab = Resources.Load<GameObject>("Man");
        for (; ; )
        {
            foreach (var tagWithLocation in DataContext.Instance.GetTagsLocation())
            {
                var tagGameObject = TagsObject.Find("Tag_" + tagWithLocation.pos.TagId, true);
                if (tagGameObject == null)
                {
                    tagGameObject = Instantiate(prefab, TagsObject.transform);
                    var info = tagGameObject.GetComponent<TagInfo>();
                    info.info = tagWithLocation.tag;
                    info.LocInfo = tagWithLocation.pos;
                    tagGameObject.GetComponentInChildren<TextMesh>().text = tagWithLocation.tag.TagFullName;
                    tagGameObject.transform.position = ClosePointFinder.GetPosition(new Vector3(tagWithLocation.pos.XPosition, tagWithLocation.pos.YPosition - 5, tagWithLocation.pos.ZPosition));
                    tagGameObject.name = "Tag_" + tagWithLocation.pos.TagId;
                    tagGameObject.GetComponent<MoveController>().IsActive = !((DateTime.Now - tagWithLocation.pos.RecordDateTime).TotalMinutes > 10);
                    continue;
                }
                var mover = tagGameObject.GetComponent<MoveController>();
                mover.IsActive = !((DateTime.Now - tagWithLocation.pos.RecordDateTime).TotalMinutes > 10);

                if (mover.IsActive && !mover.Moving)
                    mover.Move(ClosePointFinder.GetPosition(new Vector3(tagWithLocation.pos.XPosition, tagWithLocation.pos.YPosition - 5, tagWithLocation.pos.ZPosition)));
            }
            yield return new WaitForSeconds(5);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
