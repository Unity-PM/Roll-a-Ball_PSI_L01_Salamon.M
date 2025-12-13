using NUnit.Framework.Constraints;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIMenager : MonoBehaviour
{
    GameObject Options;
    public TMP_Text score;
    private GameManager gameManager;
    public TMP_Text timerText;
    int timeSinceLevelStarted;
    public TMP_Text savepointText;
    public TMP_Text lifesText;
    public TMP_Text savepointCounterText;
    RawImage dashImg;
    RawImage dashImgUnactive;
    RawImage dJumpImg;
    RawImage dJumpImgUnactive;

    public void Start()
    {
        Options = GameObject.FindGameObjectWithTag("Options");
        Options?.SetActive(false);
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.ScoreUpdate += Score;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            MovementController.SavepointActive += SavepointEnabled;
            gameManager.LifeUpdate += Lifes;
            MovementController.SavepointCreate += SavepointsCounter;
            MovementController.DashUsed += DashImg;
            MovementController.DoubleJumpUsed += DJumpImg;
            dashImg = GameObject.Find("DashUI").GetComponent<RawImage>();
            dashImgUnactive = GameObject.Find("DashUI2").GetComponent<RawImage>();
            dJumpImg = GameObject.Find("DoubleJumpUI").GetComponent<RawImage>();
            dJumpImgUnactive = GameObject.Find("DoubleJumpUI2").GetComponent<RawImage>();
        }
    }
    public void Update()
    {
        Timer();
        if (Input.GetKeyDown(KeyCode.Escape) && IsGameScene())
        {
            if (!Options.activeInHierarchy) 
                RowInfoActive(true);
            else 
                RowInfoActive(false);
        }
    }
    private void DJumpImg(bool obj)
    {
        if (obj)
        {
            dJumpImg.enabled = false;
            dJumpImgUnactive.enabled = true;
        }
        else
        {
            dJumpImg.enabled = true;
            dJumpImgUnactive.enabled = false;
        }
    }

    private void DashImg(bool obj)
    { 
        if (obj)
        {
            dashImg.enabled = false;
            dashImgUnactive.enabled = true;
        }
        else
        {
            dashImg.enabled = true;
            dashImgUnactive.enabled = false;
        }
    }

    private void SavepointsCounter(int savepointcounter)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            savepointCounterText.text = savepointcounter.ToString();
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
    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }
    public void SetOptionsActive(bool isActive)
    {
        Options.SetActive(isActive);
    }

    public void OptionsQuit()
    {
        Options.SetActive(false);
    }
    public void EndSceneRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void Score(int score, int pointspossible)
    {
        this.score.text = score + "/" + pointspossible;
    }

    public void Timer()
    {
        if (IsGameScene())
        {
            timeSinceLevelStarted = (int)Time.timeSinceLevelLoad;
            timerText.text = (timeSinceLevelStarted / 60).ToString("00") + ":" + (timeSinceLevelStarted % 60).ToString("00");
        }
    }

    private void OnDisable()
    {
        gameManager.ScoreUpdate -= Score;
        MovementController.SavepointActive -= SavepointEnabled;
        gameManager.LifeUpdate -= Lifes;
        MovementController.SavepointCreate -= SavepointsCounter;
        MovementController.DashUsed -= DashImg;
        MovementController.DoubleJumpUsed -= DJumpImg;
    }

    public void RowInfoActive(bool isActive)
    {
        Options.SetActive(isActive);
    }

    public void SavepointEnabled()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            savepointText.text = "Enabled";
    }

    public void Lifes(int lifes)
    {
        lifesText.text = lifes.ToString();
    }

    private bool IsGameScene()
    {
        return (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2);
    }
}