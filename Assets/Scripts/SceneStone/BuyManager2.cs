using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyManager2 : MonoBehaviour
{
    // [0] -> apple, [1] -> golden apple, [2] -> stone bar, [3] -> coal bar;
    int[] quantity = new int[5];
    [SerializeField]
    GameObject NPC;

    private void Start()
    {
        quantity = InventoryManager.instance.GetQuantity();
        LoadBuyItem();
    }

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 5, then decrease apple quantity by 5 and increase chest speed by 50 percents
    /// </summary>
    public void BuyChestStoneSpeed50(bool loaded = false)
    {
        if (quantity[2] >= 10 || loaded)
        {
            if (!UIManager.instance.CanBuyChestStoneSpeed50())
                return;
            if (!loaded)
            {
                quantity[2] -= 10;
                SaveBuy("BuyChestStoneSpeed50", "StoneBar", 10);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCStoneMoveAndAnimation>().SetSpeedUp(50);
        }
    }

    /// <summary>
    /// if player buy stoneSpawn30 and player have stone bar more than 10, then decrease stone bar quantity by 10 and increase stoneSpawn time by 30 percents
    /// </summary>
    public void BuyStoneSpawn30(bool loaded = false)
    {
        if (quantity[2] >= 10 || loaded)
        {
            if (!UIManager.instance.CanBuyStoneSpawn30())
                return;
            if (!loaded)
            {
                quantity[2] -= 10;
                SaveBuy("BuyStoneSpawn30", "StoneBar", 10);
            }
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetStoneSpawnTimeDown(30);
        }
    }

    /// <summary>
    /// if player buy auto destroy stone and player have stone bar more than 25, then decrease stone bar quantity by 25 and set auto destroy stone to true with 10 second cooldown
    /// </summary>
    public void BuyChestAutoDestroyStone1(bool loaded = false)
    {
        if (quantity[2] >= 25 || loaded)
        {
            if (!loaded)
            {
                quantity[2] -= 25;
                SaveBuy("BuyChestAutoDestroyStone1", "StoneBar", 25);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCStoneGetItemManager>().SetHitDelayTime(10);
            UIManager.instance.ShowNextUpgradeChestAutoDestroyStone(0);
        }
    }

    /// <summary>
    /// if player buy auto destroy stone and player have stone bar more than 25, then decrease stone bar quantity by 25 and set auto destroy stone to true with 8 second cooldown
    /// </summary>
    public void BuyChestAutoDestroyStone2(bool loaded = false)
    {
        if (quantity[2] >= 25 || loaded)
        {
            if (!loaded)
            {
                quantity[2] -= 25;
                SaveBuy("BuyChestAutoDestroyStone2", "StoneBar", 25);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCStoneGetItemManager>().SetHitDelayTime(8);
            UIManager.instance.ShowNextUpgradeChestAutoDestroyStone(1);
        }
    }

    /// <summary>
    /// if player buy auto destroy stone and player have stone bar more than 25, then decrease stone bar quantity by 25 and set auto destroy stone to true with 5 second cooldown
    /// </summary>
    public void BuyChestAutoDestroyStone3(bool loaded = false)
    {
        if (quantity[2] >= 25 || loaded)
        {
            if (!loaded)
            {
                quantity[2] -= 25;
                SaveBuy("BuyChestAutoDestroyStone3", "StoneBar", 25);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCStoneGetItemManager>().SetHitDelayTime(5);
            UIManager.instance.ShowNextUpgradeChestAutoDestroyStone(2);
        }
    }

    // /// <summary>
    // /// </summary>
    // public void BuyGoldenAppleRate(bool loaded = false)
    // {
    //     if (quantity[0] >= 90 || loaded)
    //     {
    //         if (!loaded)
    //         {
    //             quantity[0] -= 90;
    //             SaveBuy("BuyGoldenAppleRate", "Apple", 90);
    //         }
    //         UIManager.instance.SetQuantityText();
    //         SpawnManager.instance.SetCanDropGoldenApple();
    //         UIManager.instance.ShowNextUpgradeGoldenApple(0);
    //     }
    // }

    /// <summary>
    /// </summary>
    // public void BuyGoNextStageStone(bool loaded = false)
    // {
    //     if (quantity[3] >= 50 || loaded)
    //     {
    //         if (!loaded)
    //         {
    //             quantity[3] -= 50;
    //             SaveBuy("BuyGoNextStageStone", "CoalBar", 50);
    //         }
    //         UIManager.instance.SetQuantityText();
    //         UIManager.instance.ShowNextUpgradeGoNextStageStone(0);
    //         SwitchSceneManager.instance.UnlockNewScene();
    //     }
    // }

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
    /// It loads the amount of items bought from the save file and then sets the all data
    /// based on the amount of items bought
    /// </summary>
    private void LoadBuyItem()
    {
        int amoutOfBuy;
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyChestStoneSpeed50");
        while (amoutOfBuy > 0)
        {
            BuyChestStoneSpeed50(loaded: true);
            amoutOfBuy--;
        }
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyStoneSpawn30");
        while (amoutOfBuy > 0)
        {
            BuyStoneSpawn30(loaded: true);
            amoutOfBuy--;
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoDestroyStone1") > 0)
        {
            BuyChestAutoDestroyStone1(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoDestroyStone2") > 0)
        {
            BuyChestAutoDestroyStone2(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoDestroyStone3") > 0)
        {
            BuyChestAutoDestroyStone3(loaded: true);
        }
        // if (SaveGameManager.instance.LoadGameInt("BuyGoldenAppleRate") > 0)
        // {
        //     BuyGoldenAppleRate(loaded: true);
        // }
        // if (SaveGameManager.instance.LoadGameInt("BuyGoNextStageStone") > 0)
        // {
        //     BuyGoNextStageStone(loaded: true);
        // }
    }
}
