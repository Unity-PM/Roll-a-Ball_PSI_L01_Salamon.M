using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MovementController : MonoBehaviour
{
    public Rigidbody m_player;
    GameObject playerGameObj;
    float m_speed = 20f;
    float m_jump = 300f;
    bool b_jump;
    bool b_savepoint = true;
    Vector3 m_dash = new Vector3(0,0,0);
    Vector3 m_savepoint = new Vector3(0,0,0);
    float m_timeDash;
    float m_timeDJump;
    int savepointIndex = 2;
    GameObject g_savepoint;
    bool b_dashUsed = false;
    bool b_doubleJumpUsed = false;
    Vector3 currentMovementInput;

    public static event Action BoxCollisionEnterHandler;
    public static event Action SavepointActive;
    public static event Action<int> SavepointCreate;
    public static event Action<bool> DashUsed;
    public static event Action<bool> DoubleJumpUsed;

    void Start()
    {
        m_player = GetComponent<Rigidbody>();
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        g_savepoint = GameObject.FindGameObjectWithTag("Savepoint");
    }

    void Update()
    {
        InputHandler();
        if (Time.timeSinceLevelLoad > m_timeDash)
        {
            SkillStatusUpdate(false, DashUsed);
            
        }

        if (Time.timeSinceLevelLoad > m_timeDJump)
        {
            SkillStatusUpdate(false, DoubleJumpUsed);
        }
    }

    private void FixedUpdate()
    {
        ApplyForce();
    }

    private void ApplyForce()
    {
        if (currentMovementInput != Vector3.zero)
        {
            m_player.AddForce(currentMovementInput * m_speed);
        }
    }

    private void SkillStatusUpdate(bool isUsed, Action<bool> skillUsed)
    {
        skillUsed?.Invoke(isUsed);
    }

    void InputHandler()
    {
        currentMovementInput = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            currentMovementInput += Vector3.forward;
            if(Input.GetKeyDown(KeyCode.J))
            {
                Dash(0, 0, 1000f);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentMovementInput += Vector3.back;
            if (Input.GetKeyDown(KeyCode.J))
            {
                Dash(0, 0, -1000f);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentMovementInput += Vector3.left;
            if (Input.GetKeyDown(KeyCode.J))
            {
                Dash(-1000f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentMovementInput += Vector3.right;
            if (Input.GetKeyDown(KeyCode.J))
            {
                Dash(1000f, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.Space) && b_jump == true) 
        {
            m_player.AddForce(m_jump * Vector3.up, ForceMode.Force);
            b_jump = false; 
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (b_savepoint)
            {
                GoToSavepoint();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (savepointIndex > 0 )
            {
                
                CreateSavepoint();
                savepointIndex -= 1;
                SavepointCreate?.Invoke(savepointIndex);
            }
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DoubleJump();
        }
    }

    private void Dash(float x, float y, float z)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && Time.timeSinceLevelLoad > m_timeDash)
        {
            m_timeDash = Time.timeSinceLevelLoad+5;
            b_dashUsed = true;
            m_dash.y = y;
            m_dash.x = x;
            m_dash.z = z;
            TrailHandler(false, true);
            m_player.useGravity = false;
            m_player.AddForce(x, y, z, ForceMode.Force);
            DashUsed?.Invoke(b_dashUsed);
            Invoke(nameof(DashOff), 0.5f);
        }
    }

    private void DashOff()
    {
        TrailHandler(true, false);
        m_player.linearVelocity = Vector3.zero;
        m_player.useGravity = true;
    }

    private void TrailHandler(bool renderer, bool trailRenderer)
    {
        playerGameObj.GetComponent<Renderer>().enabled = renderer;
        playerGameObj.GetComponent<TrailRenderer>().enabled = trailRenderer;
    }

    private void CreateSavepoint()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            m_savepoint = m_player.transform.position;
            b_savepoint = true;
            g_savepoint.transform.position = m_savepoint;
            g_savepoint.GetComponent<Renderer>().enabled = true;
            SavepointActive?.Invoke();
        }
    }

    private void GoToSavepoint()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            m_player.transform.position = m_savepoint;
    }

    private void DoubleJump()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && Time.timeSinceLevelLoad > m_timeDJump)
        {
            m_timeDJump = Time.timeSinceLevelLoad + 5;
            b_doubleJumpUsed = true;
            m_player.linearVelocity = Vector3.zero;
            m_player.AddForce(3f * m_jump * Vector3.up, ForceMode.Force);
            DoubleJumpUsed?.Invoke(b_doubleJumpUsed);
        } 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            b_jump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BoxWall"))
            BoxCollisionEnterHandler?.Invoke();
    }
}
