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

    private void OnMouseDown()
    {
        HitStone(1);
    }

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

    private void ShakeObject()
    {
        float shakeTime = 0.05f;
        StartCoroutine(Shake(shakeTime));
    }

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

    private void DropItem()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        int randomGoldApple = Random.Range(0, 100);
        if (randomGoldApple < dropRate)
        {
            Instantiate(stoneBarPrefab, spawnPoint, Quaternion.identity);
        }
    }

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
