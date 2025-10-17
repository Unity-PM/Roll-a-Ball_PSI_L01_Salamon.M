using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Collectible : MonoBehaviour
{
    public float m_rotate = 70;
    public GameObject m_coin;
    public GameObject m_player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_coin.transform.Rotate(0, Time.deltaTime* m_rotate, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<MovementController>().score += 1;
        Debug.Log("You scored a point!!!!");
        Debug.Log("You have: " + m_player.GetComponent<MovementController>().score + " coins");
        m_coin.SetActive(false);
    }
    
}
