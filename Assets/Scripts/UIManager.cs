using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenager : MonoBehaviour
{
    GameObject panelTextOptions;
    public TMP_Text t_score;

    public void Start()
    {
        panelTextOptions = GameObject.FindGameObjectWithTag("Options");
        panelTextOptions?.SetActive(false);
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.ScoreUpdate += Score;
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickOptions()
    {
        SetOptionsActive(true);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void SetOptionsActive(bool isActive)
    {
        panelTextOptions.SetActive(isActive);
    }

    public void OptionsQuit()
    {
        panelTextOptions.SetActive(false);
    }
    public void EndSceneRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void Score(int score, int pointspossible)
    {
        t_score.text = "Score: " + score + "/" + pointspossible;
    }
}
