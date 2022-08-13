using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    int[] quantity = new int[5];
    [SerializeField]
    GameObject NPC;
    void Start()
    {
        quantity = InventoryManager.instance.GetQuantity();
    }
    public void BuyChestSpeed50()
    {
        if (quantity[0] >= 4)
        {
            quantity[0] -= 4;
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCMoveAndAnimation>().SetSpeedUp(50);
        }
    }

    public void BuyTreeSpawn50()
    {
        if (quantity[1] >= 10)
        {
            quantity[1] -= 10;
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetSpawnTimeDown(50);
        }
    }
}
