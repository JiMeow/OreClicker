using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBrickManager : MonoBehaviour
{
    [SerializeField]
    GameObject stoneBarPrefab;
    public int durable;
    public float dropRate;
    bool notDestroyed = true;

    /// <summary>
    /// When the mouse is clicked, the function HitStone is called with the parameter 1
    /// </summary>
    private void OnMouseDown()
    {
        HitStone(1);
    }

    /// <summary>
    /// If the power is equal 0 or the durable equal to 0, then the power is set to 1, otherwise the power is
    /// set to the old power then reduce durable by power if durable is 0 drop item then tranparent the stone
    /// else shake 
    /// </summary>
    /// <param name="power">The power of click.</param>
    public void HitStone(int power)
    {
        power = Mathf.Max(power, durable) == 0 ? 1 : power;
        durable -= power;
        if (durable <= 0 && notDestroyed)
        {
            notDestroyed = false;
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
    /// It spawns an Item gameobject position. (StoneBar)
    /// </summary>
    private void DropItem()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        int randomGoldApple = Random.Range(0, 100);
        if (randomGoldApple < dropRate)
        {
            Instantiate(stoneBarPrefab, spawnPoint, Quaternion.identity);
        }
    }

    /// <summary>
    /// This function makes the game object transparent and then destroys it
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
}
