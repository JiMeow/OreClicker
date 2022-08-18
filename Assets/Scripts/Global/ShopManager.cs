using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    // [0] -> apple, [1] -> golden apple, [2] -> stone bar, [3] -> coal bar;
    int[] quantity = new int[5];
    public bool debug = false;

    [SerializeField]
    GameObject[] allTrade;
    [SerializeField]
    GameObject[] debugTrade;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        quantity = InventoryManager.instance.GetQuantity();
        if (debug)
        {
            for (int i = 0; i < debugTrade.Length; i++)
            {
                debugTrade[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// If the player has 150 apples, remove 150 apples from the inventory and add 1 golden apple to the
    /// inventory
    /// </summary>
    public void TradeAppleToGoldenApple()
    {
        if (quantity[0] >= 150)
        {
            quantity[0] -= 150;
            InventoryManager.instance.AddItem("GoldenApple", 1);
            SoundManager.instance.PlayUpgradeSuccessSound();
        }
    }

    /// <summary>
    /// If the player has 100 apples, remove 100 apples from the inventory and add 1 stone bar to the
    /// inventory
    /// </summary>
    public void TradeAppleToStoneBar()
    {
        if (quantity[0] >= 100)
        {
            quantity[0] -= 100;
            InventoryManager.instance.AddItem("StoneBar", 1);
            SoundManager.instance.PlayUpgradeSuccessSound();
        }
    }

    /// <summary>
    /// If the player has 15 stone bar, remove 15 stone bar from the inventory and add 1 coal bar to the
    /// inventory
    /// </summary>
    public void TradeStoneBarToCoalBar()
    {
        if (quantity[2] >= 15)
        {
            quantity[2] -= 15;
            InventoryManager.instance.AddItem("CoalBar", 1);
            SoundManager.instance.PlayUpgradeSuccessSound();
        }
    }

    /// <summary>
    /// This function free adds 10 Apple to the inventory
    /// </summary>
    public void DebugApple()
    {
        InventoryManager.instance.AddItem("Apple", 10);
    }

    /// <summary>
    /// This function free adds 10 GoldenApple to the inventory
    /// </summary>
    public void DebugGoldenApple()
    {
        InventoryManager.instance.AddItem("GoldenApple", 10);
    }

    /// <summary>
    /// This function free adds 10 StoneBar to the inventory
    /// </summary>
    public void DebugStoneBar()
    {
        InventoryManager.instance.AddItem("StoneBar", 10);
    }

    /// <summary>
    /// This function free adds 10 CoalBar to the inventory
    /// </summary>
    public void DebugCoalBar()
    {
        InventoryManager.instance.AddItem("CoalBar", 10);
    }
}
