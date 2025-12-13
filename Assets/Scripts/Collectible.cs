using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Collectible : MonoBehaviour
{
    float rotate = 70;
    AudioSource audioSource;

    public static event Action CoinCollectedHandler;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime* rotate, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        CoinCollectedHandler?.Invoke();
        Invoke(nameof(CoinCollected), 0.25f);
    }
    
    private void CoinCollected()
    {
        gameObject.SetActive(false);
    }
}
