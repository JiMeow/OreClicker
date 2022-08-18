using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnlockedOre : MonoBehaviour
{
    public static ShowUnlockedOre instance;

    [SerializeField]
    GameObject[] allOre;

    int unlocked = 1;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// If the index of the ore is less than the number of unlocked ores, then show the ore. Otherwise,
    /// hide it
    /// </summary>
    public void UpdateShow()
    {
        for (int i = 0; i < allOre.Length; i++)
        {
            if (i < unlocked)
            {
                allOre[i].SetActive(true);
            }
            else
            {
                allOre[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// UnlockOre(int index) is a function that takes in an integer and sets the unlocked variable to
    /// that integer
    /// </summary>
    /// <param name="index">The index of the ore to unlock.</param>
    public void UnlockOre(int index)
    {
        unlocked = index;
        UpdateShow();
    }
}
