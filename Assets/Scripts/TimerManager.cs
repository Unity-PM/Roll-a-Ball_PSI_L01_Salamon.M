using UnityEngine;

public class TimerManager : MonoBehaviour
{
    UIMenager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindFirstObjectByType<UIMenager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.Timer();
    }


}
