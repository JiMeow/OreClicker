using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Quantity")]
    [SerializeField]
    GameObject[] QuantitiesText;
    [SerializeField]
    GameObject[] QuantitiesPhoto;

    [SerializeField]
    GameObject WindowUpgradesUI;

    [Header("Upgrades scene1")]
    [SerializeField]
    GameObject[] UpgradeChestAutoCut;
    [SerializeField]
    GameObject[] UpgradeGoldenApple;
    [SerializeField]
    GameObject[] UpgradeGoNextStage;

    bool isScaleUIPhoto;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isScaleUIPhoto = false;
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
            /*If text is new value*/
            int nowItemCount = quantity[i];
            int textItemCount = int.Parse(QuantitiesText[i].GetComponent<Text>().text);
            if (nowItemCount != textItemCount)
            {
                QuantitiesText[i].GetComponent<Text>().text = nowItemCount.ToString();
                StartCoroutine(ScaleUIPhoto(QuantitiesPhoto[i]));
            }
        }
    }

    /// <summary>
    /// It scales the object up and down in a loop
    /// </summary>
    /// <param name="GameObject">The object you want to scale.</param>
    IEnumerator ScaleUIPhoto(GameObject Obj)
    {
        if (!isScaleUIPhoto)
        {
            isScaleUIPhoto = true;
            Vector3 startscale = Obj.transform.localScale;
            Vector3 maxscale = new Vector3(1.75f, 1.75f, 1.75f);
            while (Obj.transform.localScale.x < maxscale.x)
            {
                Obj.transform.localScale = new Vector3(Obj.transform.localScale.x + 0.035f, Obj.transform.localScale.y + 0.035f, Obj.transform.localScale.z + 0.035f);
                yield return null;
            }
            while (Obj.transform.localScale.x > startscale.x)
            {
                Obj.transform.localScale = new Vector3(Obj.transform.localScale.x - 0.035f, Obj.transform.localScale.y - 0.035f, Obj.transform.localScale.z - 0.035f);
                yield return null;
            }
            isScaleUIPhoto = false;
        }
        else
        {
            yield return null;
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

    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    /// <param name="nowIndex">The current index of the upgrade chest.</param>
    public void ShowNextUpgradeChestAutoCut(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeChestAutoCut[i].SetActive(false);
        }
        UpgradeChestAutoCut[nowIndex + 1].SetActive(true);
    }

    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    /// <param name="nowIndex">The current index of the upgrade chest.</param>
    public void ShowNextUpgradeGoldenApple(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeGoldenApple[i].SetActive(false);
        }
        UpgradeGoldenApple[nowIndex + 1].SetActive(true);
    }

    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    /// <param name="nowIndex">The current index of the upgrade chest.</param>
    public void ShowNextUpgradeGoNextStage(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeGoNextStage[i].SetActive(false);
        }
        UpgradeGoNextStage[nowIndex + 1].SetActive(true);
    }
}
