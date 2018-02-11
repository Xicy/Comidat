using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Transform nameField;

    public bool IsActive
    {
        set { gameObject.SetActive(value); }
        get { return gameObject.activeSelf; }
    }

    public bool Moving = false;
    public float MSpeed = 2.0f;

    public void MoveDone()
    {
        if (!Moving) return;
        GetComponent<Animator>().Play("Idle");
        Moving = false;
    }

    void LateUpdate()
    {
        iTween.LookTo(nameField.gameObject, Camera.main.transform.position, 1);
    }

    public void Move(Vector3 target)
    {
        if (!IsActive || Moving || (transform.position - target).magnitude < 3) return;
        Moving = true;
        GetComponent<Animator>().Play("Walk");
        iTween.MoveTo(gameObject, iTween.Hash("position", target, "looktarget", target, "speed", MSpeed, "easetype", "linear", "oncompletetarget", gameObject, "oncomplete", "MoveDone"));
    }
}