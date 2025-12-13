using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int sceneId;
    public GameObject[] collectibles;
    public int scorePossible; 
    int score = 0;
    int activeScene;
    GameObject savepoint;
    MovementController movementController;
    int lifes = 3;

    public delegate void ScoreUpdateHandler(int score, int possiblescore);
    public event Action<int, int> ScoreUpdate;
    public event Action<int> LifeUpdate;

    void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        scorePossible = collectibles.Length;
        Collectible.CoinCollectedHandler += OnCoinCollected;
        MovementController.BoxCollisionEnterHandler += OnBoxCollisionEnter;
        savepoint = GameObject.FindGameObjectWithTag("Savepoint");
        movementController = FindFirstObjectByType<MovementController>();
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
        score++;
        if (score == scorePossible)
        {
            sceneId = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneId + 1);
        }
        ScoreUpdate?.Invoke(score, scorePossible);

    }
    private void OnDisable()
    {
        Collectible.CoinCollectedHandler -= OnCoinCollected;
        MovementController.BoxCollisionEnterHandler -= OnBoxCollisionEnter;
    }

    private void OnBoxCollisionEnter()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        if (savepoint.GetComponent<Renderer>().enabled && lifes > 0)
        {

            movementController.player.transform.position = savepoint.transform.position;
            lifes--;
            LifeUpdate?.Invoke(lifes);
        }
        else
            SceneManager.LoadScene(activeScene);
    }
    


}
