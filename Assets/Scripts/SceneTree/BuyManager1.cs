using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager1 : MonoBehaviour
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
    public void BuyChestTreeSpeed50(bool loaded = false)
    {
        if (quantity[0] >= 5 || loaded)
        {
            if (!UIManager.instance.CanBuyChestTreeSpeed50())
                return;
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[0] -= 5;
                SaveBuy("BuyChestTreeSpeed50", "Apple", 5);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCTreeMoveAndAnimation>().SetSpeedUp(50);
        }
    }

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 10, then decrease apple quantity by 10 and increase chest speed by 50 percents
    /// </summary>
    public void BuyTreeSpawn50(bool loaded = false)
    {
        if (quantity[0] >= 10 || loaded)
        {
            if (!UIManager.instance.CanBuyTreeSpawn50())
                return;
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[0] -= 10;
                SaveBuy("BuyTreeSpawn50", "Apple", 10);
            }
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetTreeSpawnTimeDown(50);
        }
    }

    /// <summary>
    /// If the player has 25 of the apple, then subtract 25 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 4
    /// </summary>
    public void BuyChestAutoDestroyTree1(bool loaded = false)
    {
        if (quantity[0] >= 25 || loaded)
        {
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[0] -= 25;
                SaveBuy("BuyChestAutoDestroyTree1", "Apple", 25);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCTreeGetItemManager>().SetCutDelayTime(4);
            UIManager.instance.ShowNextUpgradeChestAutoDestroyTree(0);
        }
    }

    /// <summary>
    /// If the player has 50 of the apple, then subtract 50 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 2
    /// </summary>
    public void BuyChestAutoDestroyTree2(bool loaded = false)
    {
        if (quantity[0] >= 50 || loaded)
        {
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[0] -= 50;
                SaveBuy("BuyChestAutoDestroyTree2", "Apple", 50);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCTreeGetItemManager>().SetCutDelayTime(2);
            UIManager.instance.ShowNextUpgradeChestAutoDestroyTree(1);
        }
    }

    /// <summary>
    /// If the player has 75 of the apple, then subtract 75 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 1
    /// </summary>
    public void BuyChestAutoDestroyTree3(bool loaded = false)
    {
        if (quantity[0] >= 75 || loaded)
        {
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[0] -= 75;
                SaveBuy("BuyChestAutoDestroyTree3", "Apple", 75);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCTreeGetItemManager>().SetCutDelayTime(1);
            UIManager.instance.ShowNextUpgradeChestAutoDestroyTree(2);
        }
    }

    /// <summary>
    /// If the player has 90 of the apple, then subtract 90 from the quantity of the apple,
    /// set all of next tree can drop golden apple
    /// </summary>
    public void BuyGoldenAppleRate(bool loaded = false)
    {
        if (quantity[0] >= 90 || loaded)
        {
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[0] -= 90;
                SaveBuy("BuyGoldenAppleRate", "Apple", 90);
            }
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetCanDropGoldenApple();
            UIManager.instance.ShowNextUpgradeGoldenApple(0);
            ShowUnlockedOre.instance.UnlockOre(2);
        }
    }

    /// <summary>
    /// If the player has 5 golden apples, subtract 5 golden apples from the player's inventory, save the purchase, show the
    /// next upgrade, and unlock the next scene
    /// </summary>
    /// <param name="loaded">This is a boolean that is true if the game is loaded.</param>
    public void BuyGoNextStageTree(bool loaded = false)
    {
        if (quantity[1] >= 5 || loaded)
        {
            if (!loaded)
            {
                SoundManager.instance.PlayUpgradeSuccessSound();
                quantity[1] -= 5;
                SaveBuy("BuyGoNextStageTree", "GoldenApple", 5);
            }
            UIManager.instance.SetQuantityText();
            UIManager.instance.ShowNextUpgradeGoNextStageTree(0);
            SwitchSceneManager.instance.UnlockNewScene();
            ShowUnlockedOre.instance.UnlockOre(3);
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
    /// It loads the amount of items bought from the save file and then sets the all data
    /// based on the amount of items bought
    /// </summary>
    private void LoadBuyItem()
    {
        int amoutOfBuy;
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyChestTreeSpeed50");
        while (amoutOfBuy > 0)
        {
            BuyChestTreeSpeed50(loaded: true);
            amoutOfBuy--;
        }
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyTreeSpawn50");
        while (amoutOfBuy > 0)
        {
            BuyTreeSpawn50(loaded: true);
            amoutOfBuy--;
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoDestroyTree1") > 0)
        {
            BuyChestAutoDestroyTree1(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoDestroyTree2") > 0)
        {
            BuyChestAutoDestroyTree2(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoDestroyTree3") > 0)
        {
            BuyChestAutoDestroyTree3(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyGoldenAppleRate") > 0)
        {
            BuyGoldenAppleRate(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyGoNextStageTree") > 0)
        {
            BuyGoNextStageTree(loaded: true);
        }
    }
}
