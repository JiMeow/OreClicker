using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        float nowVersion = 4;
        // if not this version delete save
        float version = PlayerPrefs.GetFloat("version", 0);
        if (version != nowVersion)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("version", nowVersion);
        }
    }
    void Update()
    {
        // if press escape, then quit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
