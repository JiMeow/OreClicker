using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutManager : MonoBehaviour
{
    [SerializeField]
    GameObject applePrefab;
    [SerializeField]
    GameObject goldenApplePrefab;
    public int durable; //3
    int dropGoldenAppleRate;
    bool notCuted = true;

    /// <summary>
    /// If the player clicks on the tree, the tree will be cut down with power = 1
    /// </summary>
    private void OnMouseDown()
    {
        CutTree(1);
    }

    /// <summary>
    /// If the power is equal 0 or the durable equal to 0, then the power is set to 1, otherwise the power is
    /// set to the old power then reduce durable by power if durable is 0 drop the apple then tranparent the tree
    /// else shake tree
    /// </summary>
    /// <param name="power">The power of click.</param>
    public void CutTree(int power)
    {
        power = Mathf.Max(power, durable) == 0 ? 1 : power;
        durable -= power;
        if (durable <= 0 && notCuted)
        {
            notCuted = false;
            DropItem();
            StartCoroutine(TransparentThisGameObject());
        }
        else
        {
            ShakeObject();
        }
    }

    /// <summary>
    /// start the coroutine to make the object shake
    /// </summary>
    private void ShakeObject()
    {
        float shakeTime = 0.05f;
        StartCoroutine(Shake(shakeTime));
    }

    /// <summary>
    /// shake by object by little distance then back to normal position for a given time
    /// </summary>
    /// <param name="shaketime">The amount of time the camera will shake for.</param>
    IEnumerator Shake(float shaketime)
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0.0f;
        while (elapsed < shaketime)
        {
            float x = Random.Range(-0.03f, 0.03f);
            float y = Random.Range(0f, 0.03f);
            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }

    /// <summary>
    /// It spawns an apple at the tree's position. 
    /// drop the apple with a random chance of dropping a golden apple
    /// </summary>
    private void DropItem()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        int randomGoldApple = Random.Range(0, 100);
        if (randomGoldApple < dropGoldenAppleRate)
        {
            Instantiate(goldenApplePrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            Instantiate(applePrefab, spawnPoint, Quaternion.identity);
        }
    }

    /// <summary>
    /// While the alpha value of the sprite renderer is greater than 0, subtract the delta time from the
    /// alpha value of the sprite renderer, if the alpha value is lower than 0 then destroy the tree.
    /// </summary>
    IEnumerator TransparentThisGameObject()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.tag = "Untagged";
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// This function sets the drop rate of golden apples
    /// </summary>
    /// <param name="rate">The rate at which the golden apple will drop.</param>
    public void SetGoldenAppleRate(int rate)
    {
        dropGoldenAppleRate = rate;
    }

}
