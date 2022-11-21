using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credit()
    {
        Application.OpenURL("https://github.com/JiMeow/OreClicker");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
}
