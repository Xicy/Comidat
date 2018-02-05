using System.Collections;
using UnityEngine;


public class CamereMovement : MonoBehaviour
{
    private static Vector3 _positionCameraHome;
    private static Vector3 _rotationCameraHome;
    public float Speed = 100;
    public float Sensitivity = 100;
    private float yaw;
    private float pitch;
    
    void Start()
    {
        _positionCameraHome = transform.position;
        _rotationCameraHome = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw = transform.eulerAngles.y + Time.smoothDeltaTime * Sensitivity * Input.GetAxis("Mouse X");
            pitch = transform.eulerAngles.x - Time.smoothDeltaTime * Sensitivity * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            Vector3 target = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.E))
                target += Vector3.up;

            if (Input.GetKey(KeyCode.Q))
                target += Vector3.down;

            transform.Translate(target * Speed * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.R))
            StartCoroutine(ResetCamera());
    }

    private IEnumerator ResetCamera()
    {
        iTween.RotateTo(gameObject, iTween.Hash("x", _rotationCameraHome.x, "y", _rotationCameraHome.y, "z", _rotationCameraHome.z, "time", 1f, "easetype", "easeOutSine"));
        iTween.MoveTo(gameObject, iTween.Hash("x", _positionCameraHome.x, "y", _positionCameraHome.y, "z", _positionCameraHome.z, "time", 1f, "easetype", "easeOutSine"));
        yield return new WaitForSeconds(1f);
    }

}
