using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject m_camera;
    Vector3 m_transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");
        m_transform = m_camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_camera.transform.position = transform.position + m_transform;
    }
}
