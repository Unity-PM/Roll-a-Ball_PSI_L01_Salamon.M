using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MovementController : MonoBehaviour
{
    public Rigidbody m_player;
    public float m_speed = 20f;
    public int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_player = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            m_player.AddForce(0, 0, m_speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_player.AddForce(0, 0, -m_speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_player.AddForce(-m_speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_player.AddForce(m_speed, 0, 0);
        }
    }
}
