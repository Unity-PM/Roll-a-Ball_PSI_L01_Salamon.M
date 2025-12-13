using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    private static AudioManager instance = null;

    public static AudioManager Instance
    { 
        get 
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<AudioManager>();
                if (instance != null)
                {
                    GameObject singletone = new GameObject(typeof(AudioManager).ToString());
                    instance = singletone.AddComponent<AudioManager>();
                    DontDestroyOnLoad(singletone);
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);   
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            Destroy(gameObject);
        }
    }

}