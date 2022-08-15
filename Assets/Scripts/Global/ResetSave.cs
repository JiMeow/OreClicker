using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSave : MonoBehaviour
{
    // for reset save (Debug)
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
