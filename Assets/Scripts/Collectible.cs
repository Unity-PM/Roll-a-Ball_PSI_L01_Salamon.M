using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Collectible : MonoBehaviour
{
    float m_rotate = 70;
    AudioSource m_audioSource;

    public static event Action CoinCollectedHandler;

    void Start()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime* m_rotate, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_audioSource.Play();
        CoinCollectedHandler?.Invoke();
        Invoke(nameof(CoinCollected), 0.25f);
    }
    
    private void CoinCollected()
    {
        gameObject.SetActive(false);
    }
}
