using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    // reference from "Name of the item" to "index of the item in the array (quantity)"
    // like "Apple" to "0"
    Dictionary<string, int> NameToIndex;
    int[] quantity = new int[5];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NameToIndex = new Dictionary<string, int>();
        NameToIndex.Add("Apple", 0);
        NameToIndex.Add("Coal", 1);
        quantity[0] = 0;
        quantity[1] = 0;
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
}
