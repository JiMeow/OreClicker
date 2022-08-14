using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager : MonoBehaviour
{
    // [0] -> apple, [1] -> coal
    int[] quantity = new int[5];
    [SerializeField]
    GameObject NPC;
    private void Start()
    {
        quantity = InventoryManager.instance.GetQuantity();
        LoadBuyItem();
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

            SaveBuy("BuyChestSpeed50", "Apple", 4);
        }
    }

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 10, then decrease apple quantity by 10 and increase chest speed by 50 percents
    /// </summary>
    public void BuyTreeSpawn50()
    {
        if (quantity[0] >= 10)
        {
            quantity[0] -= 10;
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetSpawnTimeDown(50);

            SaveBuy("BuyTreeSpawn50", "Apple", 10);
        }
    }

    /// <summary>
    /// If the player has 25 of the apple, then subtract 25 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 4
    /// </summary>
    public void BuyChestAutoCut4()
    {
        if (quantity[0] >= 25)
        {
            quantity[0] -= 25;
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(4);

            SaveBuy("BuyChestAutoCut4", "Apple", 25);
        }
    }

    /// <summary>
    /// If the player has 50 of the apple, then subtract 50 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 2
    /// </summary>
    public void BuyChestAutoCut2()
    {
        if (quantity[0] >= 50)
        {
            quantity[0] -= 50;
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(2);

            SaveBuy("BuyChestAutoCut2", "Apple", 50);
        }
    }

    /// <summary>
    /// If the player has 75 of the apple, then subtract 75 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 1
    /// </summary>
    public void BuyChestAutoCut1()
    {
        if (quantity[0] >= 75)
        {
            quantity[0] -= 75;
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(1);

            SaveBuy("BuyChestAutoCut1", "Apple", 75);
        }
    }

    /// <summary>
    /// This function takes in a string for the item name, a string for the money type, and an int for
    /// the value of the item. then it adds the item to the player's inventory and subtracts the money (in save game)
    /// type
    /// </summary>
    /// <param name="itemname">The name of the item you want to buy.</param>
    /// <param name="moneytype">The name of the currency you want to use.</param>
    /// <param name="value">The amount of money that will be deducted from the player's
    /// moneytype.</param>
    private void SaveBuy(string itemname, string moneytype, int value)
    {
        SaveGameManager.instance.SaveGameInt(itemname, valueInt: 1, add: true);
        SaveGameManager.instance.SaveGameInt(moneytype, valueInt: -value, add: true);
    }

    /// <summary>
    /// It loads the amount of items bought from the save file and then sets the NPC's speed, tree spawn
    /// time, and cut delay time based on the amount of items bought
    /// </summary>
    private void LoadBuyItem()
    {
        int amoutOfBuy;
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyChestSpeed50");
        while (amoutOfBuy > 0)
        {
            NPC.GetComponent<NPCMoveAndAnimation>().SetSpeedUp(50);
            amoutOfBuy--;
        }
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyTreeSpawn50");
        while (amoutOfBuy > 0)
        {
            SpawnManager.instance.SetSpawnTimeDown(50);
            amoutOfBuy--;
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoCut4") > 0)
        {
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(4);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoCut2") > 0)
        {
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(2);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoCut1") > 0)
        {
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(1);
        }
    }
}
