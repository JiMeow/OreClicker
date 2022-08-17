using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    // reference from "Name of the item" to "index of the item in the array (quantity)"
    // like "Apple" to "0"
    Dictionary<string, int> NameToIndex;
    public int[] quantity = new int[5];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NameToIndex = new Dictionary<string, int>();
        NameToIndex.Add("Apple", 0);
        NameToIndex.Add("GoldenApple", 1);
        NameToIndex.Add("StoneBar", 2);
        LoadInventory();
    }

    /// <summary>
    /// This function takes in a GameObject and adds one to the quantity of the item that the GameObject
    /// represents
    /// </summary>
    /// <param name="GameObject">The item that is being picked up.</param>
    public void GetItem(GameObject item)
    {
        int index = NameToIndex[item.tag];
        quantity[index]++;

        // save the quantity of the item to playerprefs
        SaveGameManager.instance.SaveGameInt(item.tag, valueInt: 1, add: true);

        UIManager.instance.SetQuantityText();
    }

    /// <summary>
    /// This function adds an item to the inventory
    /// </summary>
    /// <param name="itemname">the name of the item to add</param>
    /// <param name="value">the amount of the item to add</param>
    public void AddItem(string itemname, int value = 1)
    {
        int index = NameToIndex[itemname];
        quantity[index] += value;

        // save the quantity of the item to playerprefs
        SaveGameManager.instance.SaveGameInt(itemname, valueInt: value, add: true);

        UIManager.instance.SetQuantityText();
    }

    /// <summary>
    /// It returns the quantity of the items in the array.
    /// </summary>
    /// <returns>
    /// The quantity array.
    /// </returns>
    public int[] GetQuantity()
    {
        return quantity;
    }

    public void LoadInventory()
    {
        quantity[0] = SaveGameManager.instance.LoadGameInt("Apple");
        quantity[1] = SaveGameManager.instance.LoadGameInt("GoldenApple");
        quantity[2] = SaveGameManager.instance.LoadGameInt("StoneBar");
        UIManager.instance.SetQuantityText();
    }
}
