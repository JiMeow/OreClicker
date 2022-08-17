using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [Header("Spawn Tree Settings")]
    [SerializeField]
    GameObject treePrefab;
    public bool isCanDropGoldenApple;
    public int goldenAppleDropRate;
    public int maxTreeInScene = 25;
    public float spawnTreeTime;
    public float countTreeTime;
    float topleftxTree = -2.65f;
    float topleftyTree = 1.00f;
    float bottomrightxTree = 2.75f;
    float bottomrightyTree = -1.0f;

    [Header("Spawn Stone Settings")]
    [SerializeField]
    GameObject stonePrefab;
    public int maxStoneInScene = 25;
    public float spawnStoneTime;
    public float countStoneTime;
    float topleftxStone = -2.65f + 9.98f;
    float topleftyStone = 1.00f;
    float bottomrightxStone = 2.75f + 9.98f;
    float bottomrightyStone = -1.0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        goldenAppleDropRate = 2;
        spawnTreeTime = 5.0f;
        spawnStoneTime = 10.0f;
    }

    private void Update()
    {
        countTreeTime += Time.deltaTime;
        // every spawnTime seconds, spawn a tree
        if (countTreeTime > spawnTreeTime)
        {
            if (GameObject.FindGameObjectsWithTag("Tree").Length < maxTreeInScene)
            {
                SpawnTree();
                countTreeTime = 0;
            }
        }
        countStoneTime += Time.deltaTime;
        // every spawnTime seconds, spawn a stone
        if (countStoneTime > spawnStoneTime)
        {
            if (GameObject.FindGameObjectsWithTag("Stone").Length < maxStoneInScene)
            {
                SpawnStone();
                countStoneTime = 0;
            }
        }
    }

    /// <summary>
    /// It creates a random position within the bounds of the map, and then spawns a tree at that
    /// position
    /// </summary>
    private void SpawnTree()
    {
        if (SwitchSceneManager.instance.GetMapUnlocked() < 1)
            return;

        float x = Random.Range(topleftxTree, bottomrightxTree);
        float y = Random.Range(topleftyTree, bottomrightyTree);
        Vector2 spawnPoint = new Vector2(x, y);

        GameObject newTree = Instantiate(treePrefab, spawnPoint, Quaternion.identity);
        // if the tree must can drop golden apple, then set it to can drop golden apple
        if (isCanDropGoldenApple)
        {
            newTree.GetComponent<TreeCutManager>().SetGoldenAppleRate(goldenAppleDropRate);
        }
    }

    /// <summary>
    /// It creates a random position within the bounds of the map, and then spawns a stone at that
    /// position
    /// </summary>
    private void SpawnStone()
    {
        if (SwitchSceneManager.instance.GetMapUnlocked() < 2)
            return;

        float x = Random.Range(topleftxStone, bottomrightxStone);
        float y = Random.Range(topleftyStone, bottomrightyStone);
        Vector2 spawnPoint = new Vector2(x, y);

        GameObject newStone = Instantiate(stonePrefab, spawnPoint, Quaternion.identity);
    }

    /// <summary>
    /// It takes a float value (percent) then decrease the spawncooldown of the tree by that percent
    /// </summary>
    /// <param name="percent">The percentage of the current spawn time to decrease by.</param>
    public void SetTreeSpawnTimeDown(float percent)
    {
        spawnTreeTime *= (1.0f - percent / 100f);
    }

    /// <summary>
    /// It takes a float value (percent) then decrease the spawncooldown of the tree by that percent
    /// </summary>
    /// <param name="percent">The percentage of the current spawn time to decrease by.</param>
    public void SetStoneSpawnTimeDown(float percent)
    {
        spawnStoneTime *= (1.0f - percent / 100f);
    }

    /// <summary>
    /// This function sets the boolean variable isCanDropGoldenApple to true
    /// </summary>
    public void SetCanDropGoldenApple()
    {
        isCanDropGoldenApple = true;
    }
}
