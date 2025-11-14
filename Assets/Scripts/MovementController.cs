using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MovementController : MonoBehaviour
{
    Rigidbody m_player;
    float m_speed = 17f;
    float m_jump = 300f;
    bool b_jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_player = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        InputHandler();
    }

    void InputHandler()
    {
        if (Input.GetKey(KeyCode.W))
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
        if (Input.GetKey(KeyCode.Space) && b_jump == true) { m_player.AddForce(0, m_jump, 0);b_jump = false; }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            b_jump = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BadWall")
        {
            SceneManager.LoadScene(4);
        }
    }


}
