using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneManager : MonoBehaviour
{
    public static SwitchSceneManager instance;
    public int mapUnlocked;

    string[] sceneList;
    int nowScene;

    bool MovingCamreaCoroutineRunning;

    private void Awake()
    {
        instance = this;
        mapUnlocked = 1;
    }

    private void Start()
    {
        nowScene = 0;
        MovingCamreaCoroutineRunning = false;
        sceneList = new string[10];
        sceneList[0] = "Grass";
        sceneList[1] = "Stone";
        sceneList[1] = "Copper";
    }

    // Grass at 0, 0f
    // Stone at 1, 9.98f

    /// <summary>
    /// If the camera is not moving, then move the camera to the next left scene
    /// </summary>
    public void SwitchSceneLeft()
    {
        if (MovingCamreaCoroutineRunning)
            return;

        int nextScene = nowScene - 1;
        if (nextScene < 0)
        {
            nextScene = mapUnlocked - 1;
        }
        Goto(nextScene);
    }

    /// <summary>
    /// If the camera is not moving, then move the camera to the next right scene
    /// </summary>
    public void SwitchSceneRight()
    {
        if (MovingCamreaCoroutineRunning)
            return;

        int nextScene = nowScene + 1;
        if (nextScene > mapUnlocked - 1)
        {
            nextScene = 0;
        }
        Goto(nextScene);
    }

    /// <summary>
    /// Move the camera to the scene with index sceneIndex
    /// </summary>
    /// <param name="index">The index of the scene you want to go to.</param>
    private void Goto(int index)
    {
        switch (index)
        {
            case 0:
                StartCoroutine(MoveMainCamera(0f));
                break;
            case 1:
                StartCoroutine(MoveMainCamera(9.98f));
                break;
            case 2:
                StartCoroutine(MoveMainCamera(20.57f));
                break;
        }
        nowScene = index;
    }

    /// <summary>
    /// It moves the main camera to the position specified by the parameter and also close windowUI
    /// </summary>
    /// <param name="positionX">The x position of the camera.</param>
    IEnumerator MoveMainCamera(float positionX)
    {
        MovingCamreaCoroutineRunning = true;
        UIManager.instance.CloseWindowUpgradesUI();
        UIManager.instance.CloseWindowShopUI();
        Camera main = Camera.main;
        int direction = main.transform.position.x > positionX ? -1 : 1;
        while (Mathf.Abs(main.transform.position.x - positionX) > 0.05f || Mathf.Abs(main.transform.position.x - positionX) < -0.05f)
        {
            main.transform.position = new Vector3(main.transform.position.x + direction * 0.1f, main.transform.position.y, main.transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        MovingCamreaCoroutineRunning = false;
    }

    /// <summary>
    /// It make another scene active
    /// </summary>
    public void UnlockNewScene()
    {
        mapUnlocked++;
    }

    /// <summary>
    /// Get the number of unlocked scene
    /// </summary>
    public int GetMapUnlocked()
    {
        return mapUnlocked;
    }

    /// <summary>
    /// It returns the current scene's index
    /// </summary>
    /// <returns>
    /// The current scene.
    /// </returns>
    public int getNowScene()
    {
        return nowScene;
    }
}
