using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    GameObject[] QuantitiesText;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// It loops through the array of text objects and sets the text of each text object to the
    /// corresponding value in the quantity array
    /// </summary>
    public void SetQuantityText()
    {
        int[] quantity = InventoryManager.instance.GetQuantity();
        for (int i = 0; i < QuantitiesText.Length; i++)
        {
            QuantitiesText[i].GetComponent<Text>().text = quantity[i].ToString();
        }
    }
}
