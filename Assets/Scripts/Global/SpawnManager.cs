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
    public bool isCanDropCoal;
    public int coalDropRate;
    public int maxStoneInScene = 25;
    public float spawnStoneTime;
    public float countStoneTime;
    float topleftxStone = -2.65f + 9.98f;
    float topleftyStone = 1.00f;
    float bottomrightxStone = 2.75f + 9.98f;
    float bottomrightyStone = -1.0f;

    [Header("Spawn Copper Settings")]
    [SerializeField]
    GameObject[] copperStonePrefab;
    [SerializeField]
    GameObject[] quartStonePrefab;
    public int maxCopperAndQuartInScene = 25;
    public float spawnCopperStoneTime;
    public float countCopperStoneTime;
    public float spawnQuartStoneTime;
    public float countQuartStoneTime;
    float topleftxCopperAndQuartStone = -2.65f + 20.57f;
    float topleftyCopperAndQuartStone = 1.00f;
    float bottomrightxCopperAndQuartStone = 2.75f + 20.57f;
    float bottomrightyCopperAndQuartStone = -1.0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        goldenAppleDropRate = 2;
        coalDropRate = 25;
        spawnTreeTime = 5.0f;
        spawnStoneTime = 10.0f;
        spawnCopperStoneTime = 7.5f;
        spawnQuartStoneTime = 10f;
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
        countCopperStoneTime += Time.deltaTime;
        // every spawnTime seconds, spawn a copper stone
        if (countCopperStoneTime > spawnCopperStoneTime)
        {
            if (GameObject.FindGameObjectsWithTag("Quart").Length + GameObject.FindGameObjectsWithTag("Copper").Length <
                maxCopperAndQuartInScene)
            {
                SpawnCopperStone();
                countCopperStoneTime = 0;
            }
        }
        countQuartStoneTime += Time.deltaTime;
        // every spawnTime seconds, spawn a quartstone
        if (countQuartStoneTime > spawnQuartStoneTime)
        {
            if (GameObject.FindGameObjectsWithTag("Quart").Length + GameObject.FindGameObjectsWithTag("Copper").Length <
                maxCopperAndQuartInScene)
            {
                SpawnQuartStone();
                countQuartStoneTime = 0;
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

        float multipleScale = Random.Range(1f, 1.2f);
        newTree.transform.localScale *= multipleScale;
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
        Vector3 spawnPoint = new Vector3(x, y, -1f);
        GameObject newStone = Instantiate(stonePrefab, spawnPoint, Quaternion.identity);

        float multipleScale = Random.Range(1f, 1.25f);
        newStone.transform.localScale *= multipleScale;

        // if stone can drop coal, then set it to can drop coal
        if (isCanDropCoal)
        {
            newStone.GetComponent<StoneBrickManager>().SetCoalDropRate(coalDropRate);
        }
    }

    /// <summary>
    /// It creates a random position within the bounds of the map, and then spawns a copper stone at that
    /// position
    /// </summary>
    private void SpawnCopperStone()
    {
        if (SwitchSceneManager.instance.GetMapUnlocked() < 3)
            return;
        float x = Random.Range(topleftxCopperAndQuartStone, bottomrightxCopperAndQuartStone);
        float y = Random.Range(topleftyCopperAndQuartStone, bottomrightyCopperAndQuartStone);
        Vector3 spawnPoint = new Vector3(x, y, -1f);
        GameObject newStone = Instantiate(copperStonePrefab[Random.Range(0, copperStonePrefab.Length)], spawnPoint, Quaternion.identity);

        float multipleScale = Random.Range(1f, 1.25f);
        newStone.transform.localScale *= multipleScale;
    }

    /// <summary>
    /// It creates a random position within the bounds of the map, and then spawns a quart stone at that
    /// position
    /// </summary>
    private void SpawnQuartStone()
    {
        if (SwitchSceneManager.instance.GetMapUnlocked() < 3)
            return;
        float x = Random.Range(topleftxCopperAndQuartStone, bottomrightxCopperAndQuartStone);
        float y = Random.Range(topleftyCopperAndQuartStone, bottomrightyCopperAndQuartStone);
        Vector3 spawnPoint = new Vector3(x, y, -1f);
        GameObject newStone = Instantiate(quartStonePrefab[Random.Range(0, quartStonePrefab.Length)], spawnPoint, Quaternion.identity);

        float multipleScale = Random.Range(1f, 1.25f);
        newStone.transform.localScale *= multipleScale;
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
    /// <summary>
    /// This function sets the boolean variable isCanDropCoal to true
    /// </summary>
    public void SetCanDropCoal()
    {
        isCanDropCoal = true;
    }
}
