using UnityEngine;
using UnityEngine.UIElements;

public class MapBehavior : MonoBehaviour
{
    GameObject m_spiningwall;
    float m_rotate = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_spiningwall = GameObject.FindGameObjectWithTag("Spiningwall");
    }

    // Update is called once per frame
    void Update()
    {
        m_spiningwall.transform.Rotate(0, Time.deltaTime * m_rotate, 0, Space.World);
    }

}
