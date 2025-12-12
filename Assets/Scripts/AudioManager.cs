using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    private static AudioManager _instance = null;

    public static AudioManager Instance
    { 
        get 
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<AudioManager>();
                if (_instance != null)
                {
                    GameObject singletone = new GameObject(typeof(AudioManager).ToString());
                    _instance = singletone.AddComponent<AudioManager>();
                    DontDestroyOnLoad(singletone);
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
            Destroy(gameObject);
            
    }

    private void Start()
    {
        _instance.audioSource = audioSource;
        audioSource.Play();
    }
}
