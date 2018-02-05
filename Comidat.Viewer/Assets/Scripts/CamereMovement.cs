using System.Collections;
using TriLib;
using UnityEngine;

public class CamereMovement : MonoBehaviour
{
    private static Vector3 _positionCameraHome;
    private static Vector3 _rotationCameraHome;
    public float Speed = 100;
    public float Sensitivity = 100;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Awake()
    {
        _positionCameraHome = transform.position;
        _rotationCameraHome = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += Time.smoothDeltaTime * Sensitivity * Input.GetAxis("Mouse X");
            pitch -= Time.smoothDeltaTime * Sensitivity * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            Vector3 target = Vector3.zero;

            if (Input.GetKey(KeyCode.D))
                target += Vector3.right;

            if (Input.GetKey(KeyCode.A))
                target += Vector3.left;

            if (Input.GetKey(KeyCode.W))
                target += Vector3.forward;

            if (Input.GetKey(KeyCode.S))
                target += Vector3.back;

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
