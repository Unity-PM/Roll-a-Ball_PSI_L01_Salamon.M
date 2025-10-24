using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenager : MonoBehaviour
{
    public GameObject panelTextOptions;
    public void Start()
    {
        panelTextOptions.SetActive(false);
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
}
