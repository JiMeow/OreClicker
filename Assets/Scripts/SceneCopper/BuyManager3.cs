using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager3 : MonoBehaviour
{
    int[] quantity = new int[5];

    // spawn speed
    [SerializeField]
    GameObject buySpawnSpeedbuttonText;
    [SerializeField]
    GameObject buySpawnSpeedbuttonPriceText;
    [SerializeField]
    GameObject buySpawnSpeedbuttonUpgradeText;
    [SerializeField]
    string[] copperSpawnSpeedValue;
    [SerializeField]
    string[] copperSpawnSpeedText;
    [SerializeField]
    int copperSpawnNowLevel;

    private void Start()
    {
        quantity = InventoryManager.instance.GetQuantity();

        // LoadBuyItem();
    }

    public void BuyCopperSpawn30()
    {
        //prase string to int
        int nowcost = int.Parse(copperSpawnSpeedValue[copperSpawnNowLevel]);
        string nowcostText = copperSpawnSpeedText[copperSpawnNowLevel];
    }
}
