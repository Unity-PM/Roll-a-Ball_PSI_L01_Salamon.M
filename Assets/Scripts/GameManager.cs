using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int m_scene;
    public GameObject[] collectibles;
    public int m_scorepossible; 
    int m_score = 0;
    int activeScene;
    GameObject m_savepoint;
    MovementController m_movementcontroller;
    int m_lifes = 3;

    public delegate void ScoreUpdateHandler(int score, int possiblescore);
    public event Action<int, int> ScoreUpdate;
    public event Action<int> LifeUpdate;

    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        m_scorepossible = collectibles.Length;
        Collectible.CoinCollectedHandler += OnCoinCollected;
        MovementController.BoxCollisionEnterHandler += OnBoxCollisionEnter;
        m_savepoint = GameObject.FindGameObjectWithTag("Savepoint");
        m_movementcontroller = FindFirstObjectByType<MovementController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            activeScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeScene);
        }
    }

    public void OnCoinCollected()
    {
        m_score++;
        if (m_score == m_scorepossible)
        {
            m_scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(m_scene + 1);
        }
        ScoreUpdate?.Invoke(m_score, m_scorepossible);

    }
    private void OnDisable()
    {
        Collectible.CoinCollectedHandler -= OnCoinCollected;
        MovementController.BoxCollisionEnterHandler -= OnBoxCollisionEnter;
    }

    private void OnBoxCollisionEnter()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        if (m_savepoint.GetComponent<Renderer>().enabled && m_lifes > 0)
        {

            m_movementcontroller.m_player.transform.position = m_savepoint.transform.position;
            m_lifes--;
            LifeUpdate?.Invoke(m_lifes);
        }
        else
            SceneManager.LoadScene(activeScene);
    }
    
}
