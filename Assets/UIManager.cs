using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    GameObject[] QuantitiesText;
    [SerializeField]
    GameObject WindowUpgradesUI;

    [SerializeField]
    GameObject[] UpgradeChestAutoCut;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        WindowUpgradesUI.SetActive(false);
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

    /// <summary>
    /// show the window with the upgrades if it is not already shown else hide it
    /// </summary>
    public void ShowWindowUpgradesUI()
    {
        if (WindowUpgradesUI.activeSelf)
        {
            WindowUpgradesUI.SetActive(false);
        }
        else
        {
            WindowUpgradesUI.SetActive(true);
        }
    }

    public void ShowNextUpgradeChestAutoCut(int nowIndex)
    {
        if (nowIndex == 3)
        {
            return;
        }
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeChestAutoCut[i].SetActive(false);
        }
        UpgradeChestAutoCut[nowIndex + 1].SetActive(true);
    }
}
