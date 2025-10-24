using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject m_camera;
    private Vector3 m_transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_transform = m_camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_camera.transform.position = transform.position + m_transform;
    }
}
