using Comidat.Data.Model;
using UnityEngine;
using UnityEngine.UI;

public class TagInfoViewer : MonoBehaviour
{
    public Text Name;
    public Text TC;
    public Text Phone;
    public Text Location;
    public Text Description;
    public Button Ok;
    public bool isActive { set { gameObject.SetActive(value); } get { return gameObject.activeSelf; } }

    void Start()
    {
        Ok.onClick.AddListener(delegate { isActive = false; });
    }

    public void UpdateInfo(TBLTag ti, Transform loc)
    {
        Name.text = ti.TagFirstName + " " + ti.TagLastName;
        TC.text = ti.TagTCNo;
        Phone.text = ti.TagMobilTelephone;
        Location.text = loc.localPosition.ToString();
        Description.text = ti.TagDescription;
        isActive = true;
    }
}
