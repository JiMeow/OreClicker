using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCGetItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] QuantitiesText;
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

    public void GetItem(GameObject item)
    {
        int index = NameToIndex[item.tag];
        quantity[index]++;
        SetQuantityText();
    }

    private void SetQuantityText()
    {
        for (int i = 0; i < QuantitiesText.Length; i++)
        {
            QuantitiesText[i].GetComponent<Text>().text = quantity[i].ToString();
        }
    }

}
