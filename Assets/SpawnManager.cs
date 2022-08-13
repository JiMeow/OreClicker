using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject treePrefab;
    public float spawnTime;
    public float countTime;
    float topleftx = -2.65f;
    float toplefty = 1.25f;
    float bottomrightx = 2.75f;
    float bottomrighty = -1.0f;

    private void Start()
    {
        spawnTime = 5.0f;
    }

    private void Update()
    {
        if (Time.time - countTime > spawnTime)
        {
            SpawnTree();
            countTime += spawnTime;
        }
    }

    void SpawnTree()
    {
        float x = Random.Range(topleftx, bottomrightx);
        float y = Random.Range(toplefty, bottomrighty);
        Vector2 spawnPoint = new Vector2(x, y);
        Instantiate(treePrefab, spawnPoint, Quaternion.identity);
    }

}
