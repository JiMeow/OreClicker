using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    // [0] -> apple, [1] -> coal
    int[] quantity = new int[5];
    [SerializeField]
    GameObject NPC;

    [SerializeField]
    GameObject[] chestSpeed50LevelScales;
    [SerializeField]
    GameObject[] treeSpawn50LevelScales;
    private void Start()
    {
        quantity = InventoryManager.instance.GetQuantity();
        LoadBuyItem();
    }

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 5, then decrease apple quantity by 5 and increase chest speed by 50 percents
    /// </summary>
    public void BuyChestSpeed50(bool loaded = false)
    {
        if (quantity[0] >= 5 || loaded)
        {
            /* Checking if scale is not max or if it will max set text to MAX and -. */
            bool complete = false;
            for (int i = 0; i < chestSpeed50LevelScales.Length - 2; i++)
            {
                GameObject scale = chestSpeed50LevelScales[i];
                if (scale.activeSelf)
                {
                    if (i == chestSpeed50LevelScales.Length - 3) // last levelgrade
                    {
                        chestSpeed50LevelScales[i + 1].GetComponent<Text>().text = "MAX";
                        chestSpeed50LevelScales[i + 2].GetComponent<Text>().text = "-";
                    }
                    scale.SetActive(false); // hide white show black
                    complete = true;
                    break;
                }
            }
            if (!complete)
            {
                return;
            }
            /**/
            if (!loaded)
            {
                quantity[0] -= 5;
                SaveBuy("BuyChestSpeed50", "Apple", 5);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCMoveAndAnimation>().SetSpeedUp(50);
        }
    }

    /// <summary>
    /// if player buy increase chest speed by 50 and player have apple more than 10, then decrease apple quantity by 10 and increase chest speed by 50 percents
    /// </summary>
    public void BuyTreeSpawn50(bool loaded = false)
    {
        if (quantity[0] >= 10 || loaded)
        {
            /* Checking if scale is not max or if it will max set text to MAX and -. */
            bool complete = false;
            for (int i = 0; i < treeSpawn50LevelScales.Length - 2; i++)
            {
                GameObject scale = treeSpawn50LevelScales[i];
                if (scale.activeSelf)
                {
                    if (i == treeSpawn50LevelScales.Length - 3) // last levelgrade
                    {
                        treeSpawn50LevelScales[i + 1].GetComponent<Text>().text = "MAX";
                        treeSpawn50LevelScales[i + 2].GetComponent<Text>().text = "-";
                    }
                    scale.SetActive(false); // hide white show black
                    complete = true;
                    break;
                }
            }
            if (!complete)
            {
                return;
            }
            /**/
            if (!loaded)
            {
                quantity[0] -= 10;
                SaveBuy("BuyTreeSpawn50", "Apple", 10);
            }
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetSpawnTimeDown(50);
        }
    }

    /// <summary>
    /// If the player has 25 of the apple, then subtract 25 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 4
    /// </summary>
    public void BuyChestAutoCut4(bool loaded = false)
    {
        if (quantity[0] >= 25 || loaded)
        {
            if (!loaded)
            {
                quantity[0] -= 25;
                SaveBuy("BuyChestAutoCut4", "Apple", 25);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(4);
            UIManager.instance.ShowNextUpgradeChestAutoCut(0);
        }
    }

    /// <summary>
    /// If the player has 50 of the apple, then subtract 50 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 2
    /// </summary>
    public void BuyChestAutoCut2(bool loaded = false)
    {
        if (quantity[0] >= 50 || loaded)
        {
            if (!loaded)
            {
                quantity[0] -= 50;
                SaveBuy("BuyChestAutoCut2", "Apple", 50);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(2);
            UIManager.instance.ShowNextUpgradeChestAutoCut(1);
        }
    }

    /// <summary>
    /// If the player has 75 of the apple, then subtract 75 from the quantity of the apple,
    /// update the UI, and set the NPC's cut delay time to 1
    /// </summary>
    public void BuyChestAutoCut1(bool loaded = false)
    {
        if (quantity[0] >= 75 || loaded)
        {
            if (!loaded)
            {
                quantity[0] -= 75;
                SaveBuy("BuyChestAutoCut1", "Apple", 75);
            }
            UIManager.instance.SetQuantityText();
            NPC.GetComponent<NPCGetItemManager>().SetCutDelayTime(1);
            UIManager.instance.ShowNextUpgradeChestAutoCut(2);
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
                quantity[0] -= 90;
                SaveBuy("BuyGoldenAppleRate", "Apple", 90);
            }
            UIManager.instance.SetQuantityText();
            SpawnManager.instance.SetCanDropGoldenApple();
            UIManager.instance.ShowNextUpgradeGoldenApple(0);
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
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyChestSpeed50");
        while (amoutOfBuy > 0)
        {
            BuyChestSpeed50(loaded: true);
            amoutOfBuy--;
        }
        amoutOfBuy = SaveGameManager.instance.LoadGameInt("BuyTreeSpawn50");
        while (amoutOfBuy > 0)
        {
            BuyTreeSpawn50(loaded: true);
            amoutOfBuy--;
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoCut4") > 0)
        {
            BuyChestAutoCut4(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoCut2") > 0)
        {
            BuyChestAutoCut2(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyChestAutoCut1") > 0)
        {
            BuyChestAutoCut1(loaded: true);
        }
        if (SaveGameManager.instance.LoadGameInt("BuyGoldenAppleRate") > 0)
        {
            BuyGoldenAppleRate(loaded: true);
        }
    }
}
