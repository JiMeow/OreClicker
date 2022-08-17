using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneManager : MonoBehaviour
{
    public static SwitchSceneManager instance;
    public int mapUnlocked;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mapUnlocked = 1;
    }

    enum Scene
    {
        Grass,
        Stone,
    }

    [SerializeField]
    Scene scene;

    /// <summary>
    /// "If the scene is grass, move the camera to the stone scene, and if the scene is stone, move the
    /// camera to the grass scene." - not complete
    /// </summary>
    public void SwitchScene()
    {
        switch (scene)
        {
            case Scene.Grass:
                StartCoroutine(MoveMainCamera(9.98f));
                scene = Scene.Stone;
                break;
            case Scene.Stone:
                StartCoroutine(MoveMainCamera(0f));
                scene = Scene.Grass;
                break;
        }
    }


    /// <summary>
    /// It moves the main camera to the position specified by the parameter
    /// </summary>
    /// <param name="positionX">The x position of the camera.</param>
    IEnumerator MoveMainCamera(float positionX)
    {
        Camera main = Camera.main;
        int direction = main.transform.position.x > positionX ? -1 : 1;
        while (Mathf.Abs(main.transform.position.x - positionX) > 0.05f || Mathf.Abs(main.transform.position.x - positionX) < -0.05f)
        {
            main.transform.position = new Vector3(main.transform.position.x + direction * 0.1f, main.transform.position.y, main.transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
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
}
