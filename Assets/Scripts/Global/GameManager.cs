using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        // if not this version delete save
        float version = PlayerPrefs.GetFloat("version", 0);
        if (version != 1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("version", 1);
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
