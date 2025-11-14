using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int m_scene;
    public GameObject[] collectibles;
    public int m_scorepossible; 
    int m_score = 0;

    public delegate void ScoreUpdateHandler(int score, int possiblescore);
    public event Action<int, int> ScoreUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        m_scorepossible = collectibles.Length;
        Collectible.CoinCollectedHandler += OnCoinCollected;
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
    }
}
