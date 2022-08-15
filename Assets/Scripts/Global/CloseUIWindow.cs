using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseUIWindow : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if mouse clicked on user interface
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // do nothing
            }
            else
            {
                // else hide the window with the upgrades
                UIManager.instance.ShowWindowUpgradesUI();
            }
        }
    }
}
