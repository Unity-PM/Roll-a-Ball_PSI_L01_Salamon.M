using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int m_scene;
    GameObject[] collectibles;
    int m_scorepossible; 
    public int m_score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        m_scorepossible = collectibles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        OnCoinCollected();
    }

    public void OnCoinCollected()
    {
        if (m_score == m_scorepossible)
        {
            m_scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(m_scene + 1);
        }
    }
}
