using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    float m_speed = 0.5f;
    GameObject platform;
    Vector3 m_position;
    Vector3 m_vector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platform = GameObject.FindGameObjectWithTag("Platfrom");
        m_position = platform.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionStay(Collision collision)
    {
            if(collision.gameObject.name == "DownWall")
            {
                m_vector =  new Vector3(0, 0, -m_speed);
                platform.transform.position = platform.transform.position + m_vector;
            }
            if (collision.gameObject.name == "TopWall")
            {
                m_vector = new Vector3(0, 0, m_speed);
                platform.transform.position = platform.transform.position + m_vector;
            }
            if (collision.gameObject.name == "LeftWall")
            {
                m_vector = new Vector3(-m_speed, 0, 0);
                platform.transform.position = platform.transform.position + m_vector;
            }
            if (collision.gameObject.name == "RightWall")
            {
                m_vector = new Vector3(m_speed, 0, 0);
                platform.transform.position = platform.transform.position + m_vector;
            }
    }
}
