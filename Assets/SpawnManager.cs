using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField]
    GameObject treePrefab;
    public float spawnTime;
    public float countTime;
    float topleftx = -2.65f;
    float toplefty = 1.00f;
    float bottomrightx = 2.75f;
    float bottomrighty = -1.0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        spawnTime = 5.0f;
    }

    private void Update()
    {
        // every spawnTime seconds, spawn a tree
        if (Time.time - countTime > spawnTime)
        {
            SpawnTree();
            countTime += spawnTime;
        }
    }

    /// <summary>
    /// It creates a random position within the bounds of the map, and then spawns a tree at that
    /// position
    /// </summary>
    void SpawnTree()
    {
        float x = Random.Range(topleftx, bottomrightx);
        float y = Random.Range(toplefty, bottomrighty);
        Vector2 spawnPoint = new Vector2(x, y);
        Instantiate(treePrefab, spawnPoint, Quaternion.identity);
    }

    public void SetSpawnTimeDown(float percent)
    {
        spawnTime *= (1.0f - percent / 100f);
    }
}
