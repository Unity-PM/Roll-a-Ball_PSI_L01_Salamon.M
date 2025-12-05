using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MovementController : MonoBehaviour
{
    Rigidbody m_player;
    GameObject playerGameObj;
    float m_speed = 17f;
    float m_jump = 300f;
    bool b_jump;
    bool b_savepoint = true;
    Vector3 m_dash = new Vector3(0,0,0);
    Vector3 m_savepoint = new Vector3(0,0,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_player = GetComponent<Rigidbody>();
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        InputHandler();
    }

    void InputHandler()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_player.AddForce(0, 0, m_speed);
            if(Input.GetKey(KeyCode.E))
            {
                Dash(0, 0, 0.7f);
                DashOn();
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_player.AddForce(0, 0, -m_speed);
            if (Input.GetKey(KeyCode.E))
            {
                Dash(0, 0, -0.7f);
            }
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Dash(-0.3f, 0, 0.3f);
            }
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Dash(0.3f, 0, 0.3f);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_player.AddForce(-m_speed, 0, 0);
            if (Input.GetKey(KeyCode.E))
            {
                Dash(-0.7f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_player.AddForce(m_speed, 0, 0);
            if (Input.GetKey(KeyCode.E))
            {
                Dash(0.7f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Dash(-0.3f, 0, -0.3f);
            }
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.E))
            {
                m_dash.z = -0.3f;
                m_dash.x = 0.3f;
                Dash(0.3f, 0, -0.3f);
            }
        }
        if (Input.GetKey(KeyCode.Space) && b_jump == true) { m_player.AddForce(0, m_jump, 0);b_jump = false; }
        if (Input.GetKey(KeyCode.R))
        {
            if (b_savepoint)
            {
                b_savepoint = false;
                m_savepoint = m_player.transform.position;
            }
            else
            {
                b_savepoint = true;
                m_player.transform.position = m_savepoint;
            }
        }
    }
    private void Dash(float x, float y, float z)
    {
        m_dash.y = y;
        m_dash.x = x;
        m_dash.z = z;
        DashOn();
        m_player.transform.position += m_dash;
        Invoke(nameof(DashOff), 0.5f);
    }
    private void DashOn()
    {
        playerGameObj.GetComponent<Renderer>().enabled = false;
        playerGameObj.GetComponent<TrailRenderer>().enabled = true;
    }
    private void DashOff()
    {
        playerGameObj.GetComponent<Renderer>().enabled = true;
        playerGameObj.GetComponent<TrailRenderer>().enabled = false;
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
