using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject cameraGameObject;
    Vector3 transformCamPos;
    void Start()
    {
        cameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");
        transformCamPos = cameraGameObject.transform.position - base.transform.position;
    }

    void Update()
    {
        cameraGameObject.transform.position = base.transform.position + transformCamPos;
    }
}
