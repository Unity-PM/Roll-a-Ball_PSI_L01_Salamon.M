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
    GameObject panelTextOptions;
    public TMP_Text t_score;
    private GameManager gameManager;
    public TMP_Text t_timer;
    int i_time;
    public TMP_Text t_savepoint;
    public TMP_Text t_lifes;
    public TMP_Text t_savepointcounter;
    RawImage dashImg;
    RawImage dashImg2;
    RawImage dJumpImg;
    RawImage dJumpImg2;

    public void Start()
    {
        panelTextOptions = GameObject.FindGameObjectWithTag("Options");
        panelTextOptions?.SetActive(false);
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
            dashImg2 = GameObject.Find("DashUI2").GetComponent<RawImage>();
            dJumpImg = GameObject.Find("DoubleJumpUI").GetComponent<RawImage>();
            dJumpImg2 = GameObject.Find("DoubleJumpUI2").GetComponent<RawImage>();
        }
    }
    public void Update()
    {
        Timer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panelTextOptions.activeInHierarchy) 
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
            dJumpImg2.enabled = true;
        }
        else
        {
            dJumpImg.enabled = true;
            dJumpImg2.enabled = false;
        }
    }

    private void DashImg(bool obj)
    { 
        if (obj)
        {
            dashImg.enabled = false;
            dashImg2.enabled = true;
        }
        else
        {
            dashImg.enabled = true;
            dashImg2.enabled = false;
        }
    }

    private void SavepointsCounter(int savepointcounter)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            t_savepointcounter.text = savepointcounter.ToString();
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
        t_score.text = score + "/" + pointspossible;
    }

    public void Timer()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            i_time = (int)Time.timeSinceLevelLoad;
            t_timer.text = (i_time / 60).ToString("00") + ":" + (i_time % 60).ToString("00");
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
        panelTextOptions.SetActive(isActive);
    }

    public void SavepointEnabled()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            t_savepoint.text = "Enabled";
    }

    public void Lifes(int lifes)
    {
        t_lifes.text = lifes.ToString();
    }

}