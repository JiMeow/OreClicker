using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SaveGameInt(string key, int valueInt, bool add = false, bool set = false)
    {
        if (add)
        {
            PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key, 0) + valueInt);
        }
        if (set)
        {
            PlayerPrefs.SetInt(key, valueInt);
        }
    }

    public void SaveGameFloat(string key, float valueFloat, bool add = false, bool set = false)
    {
        if (add)
        {
            PlayerPrefs.SetFloat(key, PlayerPrefs.GetFloat(key, 0) + valueFloat);
        }
        if (set)
        {
            PlayerPrefs.SetFloat(key, valueFloat);
        }
    }

    public int LoadGameInt(string key)
    {
        return PlayerPrefs.GetInt(key, 0);
    }

    public float LoadGameFloat(string key)
    {
        return PlayerPrefs.GetFloat(key, 0);
    }

}
