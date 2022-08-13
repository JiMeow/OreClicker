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

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 4, then decrease apple quantity by 4 and increase chest speed by 50 percents
    /// </summary>
    public void BuyChestSpeed50()
    {
        if (quantity[0] >= 4)
        {
            quantity[0] -= 4;
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCMoveAndAnimation>().SetSpeedUp(50);
        }
    }

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 10, then decrease apple quantity by 10 and increase chest speed by 50 percents
    /// </summary>
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
