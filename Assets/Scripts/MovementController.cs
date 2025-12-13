using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MovementController : MonoBehaviour
{
    public Rigidbody player;
    GameObject playerGameObj;
    float speed = 20f;
    float jumpStrength = 300f;
    bool jumpPossible;
    bool savepointActive = true;
    Vector3 dash = new Vector3(0,0,0);
    Vector3 savepointPosition = new Vector3(0,0,0);
    float timeDash;
    float timeDJump;
    int savepointIndex = 2;
    GameObject savepoint;
    bool dashUsed = false;
    bool doubleJumpUsed = false;
    Vector3 currentMovementInput;

    public static event Action BoxCollisionEnterHandler;
    public static event Action SavepointActive;
    public static event Action<int> SavepointCreate;
    public static event Action<bool> DashUsed;
    public static event Action<bool> DoubleJumpUsed;

    void Start()
    {
        player = GetComponent<Rigidbody>();
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        savepoint = GameObject.FindGameObjectWithTag("Savepoint");
    }

    void Update()
    {
        InputHandler();
        if (Time.timeSinceLevelLoad > timeDash)
        {
            SkillStatusUpdate(false, DashUsed);
            
        }

        if (Time.timeSinceLevelLoad > timeDJump)
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
            player.AddForce(currentMovementInput * speed);
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
        if (Input.GetKey(KeyCode.Space) && jumpPossible == true) 
        {
            player.AddForce(jumpStrength * Vector3.up, ForceMode.Force);
            jumpPossible = false; 
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (savepointActive)
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
        if (SceneManager.GetActiveScene().buildIndex == 2 && Time.timeSinceLevelLoad > timeDash)
        {
            timeDash = Time.timeSinceLevelLoad+5;
            dashUsed = true;
            dash.y = y;
            dash.x = x;
            dash.z = z;
            TrailHandler(false, true);
            player.useGravity = false;
            player.AddForce(x, y, z, ForceMode.Force);
            DashUsed?.Invoke(dashUsed);
            Invoke(nameof(DashOff), 0.5f);
        }
    }

    private void DashOff()
    {
        TrailHandler(true, false);
        player.linearVelocity = Vector3.zero;
        player.useGravity = true;
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
            savepointPosition = player.transform.position;
            savepointActive = true;
            savepoint.transform.position = savepointPosition;
            savepoint.GetComponent<Renderer>().enabled = true;
            SavepointActive?.Invoke();
        }
    }

    private void GoToSavepoint()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            player.transform.position = savepointPosition;
    }

    private void DoubleJump()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && Time.timeSinceLevelLoad > timeDJump)
        {
            timeDJump = Time.timeSinceLevelLoad + 5;
            doubleJumpUsed = true;
            player.linearVelocity = Vector3.zero;
            player.AddForce(3f * jumpStrength * Vector3.up, ForceMode.Force);
            DoubleJumpUsed?.Invoke(doubleJumpUsed);
        } 
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpPossible = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BoxWall"))
            BoxCollisionEnterHandler?.Invoke();
    }
}
