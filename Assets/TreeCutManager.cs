using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutManager : MonoBehaviour
{
    [SerializeField]
    GameObject applePrefab;
    public int durable;

    private void OnMouseDown()
    {
        durable--;
        if (durable == 0)
        {
            DropApple();
            StartCoroutine(TransparentThisGameObject());
        }
        else
        {
            TreeShake();
        }
    }

    private void TreeShake()
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

    private void DropApple()
    {
        Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.y);
        Instantiate(applePrefab, spawnPoint, Quaternion.identity);
    }

    IEnumerator TransparentThisGameObject()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
