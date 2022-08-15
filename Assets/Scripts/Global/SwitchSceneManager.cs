using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneManager : MonoBehaviour
{
    enum Scene
    {
        Grass,
        Stone,
    }

    [SerializeField]
    Scene scene;

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
}
