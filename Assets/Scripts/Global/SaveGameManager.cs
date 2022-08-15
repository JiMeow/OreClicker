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

    /// <summary>
    /// This function saves an integer value to the PlayerPrefs
    /// </summary>
    /// <param name="key">The name of the key you want to save the value to.</param>
    /// <param name="valueInt">The value you want to save.</param>
    /// <param name="add">If true, the value will be added to the current value.</param>
    /// <param name="set">If true, the value will be set to the valueInt.</param>
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

    /// <summary>
    /// This function saves a float value to the PlayerPrefs.
    /// </summary>
    /// <param name="key">The name of the key you want to save the value to.</param>
    /// <param name="valueFloat">The float value you want to save.</param>
    /// <param name="add">If true, the value will be added to the current value.</param>
    /// <param name="set">If true, the value will be set to the valueFloat.</param>
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

    /// <summary>
    /// This function will return the value of the key you pass in, or 0 if the key doesn't exist
    /// </summary>
    /// <param name="key">The name of the key you want to load.</param>
    /// <returns>
    /// The value of the key, or 0 if the key does not exist.
    /// </returns>
    public int LoadGameInt(string key)
    {
        return PlayerPrefs.GetInt(key, 0);
    }

    /// <summary>
    ///This function will return the value of the key you pass in, or 0 if the key doesn't exist
    /// </summary>
    /// <param name="key">The name of the key you want to load.</param>
    /// <returns>
    /// The value of the key, or 0 if the key does not exist.
    /// </returns>
    public float LoadGameFloat(string key)
    {
        return PlayerPrefs.GetFloat(key, 0);
    }

}
