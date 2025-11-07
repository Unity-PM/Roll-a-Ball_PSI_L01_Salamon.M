using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MovementController : MonoBehaviour
{
    public Rigidbody m_player;
    public float m_speed = 20f;
    public TMP_Text t_allscored;
    public TMP_Text t_score;
    public GameObject m_coin;
    public int score;
    public int m_scene;
    //float jumpForce = 20;
    //int player_health = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_player = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        InputHandler();
    }

    private void Update()
    {

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
        //if(Input.GetKey(KeyCode.Space))
        //{
        //    m_player.AddForce(0, jumpForce, 0);
        //}
        
    }
    



}
