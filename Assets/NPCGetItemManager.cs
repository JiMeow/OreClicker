using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCGetItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] QuantitiesText;
    // reference from "Name of the item" to "index of the item in the array (quantity)"
    // like "Apple" to "0"
    Dictionary<string, int> NameToIndex;
    int[] quantity = new int[5];


    void Start()
    {
        NameToIndex = new Dictionary<string, int>();
        NameToIndex.Add("Apple", 0);
        quantity[0] = 0;
    }

    void Update()
    {

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
        SetQuantityText();
    }

    /// <summary>
    /// It loops through the array of text objects and sets the text of each text object to the
    /// corresponding value in the quantity array
    /// </summary>
    private void SetQuantityText()
    {
        for (int i = 0; i < QuantitiesText.Length; i++)
        {
            QuantitiesText[i].GetComponent<Text>().text = quantity[i].ToString();
        }
    }

}
