using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class Collectible : MonoBehaviour
{
    float m_rotate = 70;
    AudioSource m_audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime* m_rotate, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<GameManager>().m_score += 1;
        m_audioSource.Play();
        Invoke(nameof(CoinCollected),0.2f);
    }
    
    private void CoinCollected()
    {
        gameObject.SetActive(false);
    }
}
