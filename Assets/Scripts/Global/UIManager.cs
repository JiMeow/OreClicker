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

    [Header("Upgrades Window On Each Scene")]
    [SerializeField]
    GameObject[] UpgradesWindow;

    [Header("Upgrades scene1")]
    [SerializeField]
    GameObject[] chestTreeSpeed50LevelScales;
    [SerializeField]
    GameObject[] treeSpawn50LevelScales;
    [SerializeField]
    GameObject[] UpgradeChestAutoDestroyTree;
    [SerializeField]
    GameObject[] UpgradeGoldenApple;

    [SerializeField]
    GameObject[] UpgradeGoNextStageTree;

    [Header("Upgrades scene2")]
    [SerializeField]
    GameObject[] chestStoneSpeed50LevelScales;
    [SerializeField]
    GameObject[] stoneSpawn50LevelScales;
    [SerializeField]
    GameObject[] UpgradeChestAutoDestroyStone;
    [SerializeField]
    GameObject[] UpgradeGoldenStone;

    [SerializeField]
    GameObject[] UpgradeGoNextStageStone;


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
            Vector3 endscale = new Vector3(startscale.x * 1.75f, startscale.y * 1.75f, startscale.z * 1.75f);
            Vector3 maxscale = endscale;
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
        int nowScene = SwitchSceneManager.instance.getNowScene();
        for (int i = 0; i < UpgradesWindow.Length; i++)
        {
            if (i == nowScene)
            {
                UpgradesWindow[i].SetActive(true);
            }
            else
            {
                UpgradesWindow[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// close the window with the upgrades
    /// </summary>
    public void CloseWindowUpgradesUI()
    {
        WindowUpgradesUI.SetActive(false);
    }


    //// SCENE TREE
    //// SCENE TREE
    //// SCENE TREE
    //// SCENE TREE
    //// SCENE TREE
    //// SCENE TREE
    //// SCENE TREE
    //// SCENE TREE

    /// <summary>
    /// If upgrade is not at max level, it increases the upgrade level by 1 (add scale) and returns true
    /// then if it was max level, change the upgrade text to 'MAX' and value to '-', but if not max level return false
    /// </summary>
    /// <returns>
    /// a boolean value.
    /// </returns>
    public bool CanBuyChestTreeSpeed50()
    {
        /* Checking if scale is not max or if it will max set text to MAX and -. */
        for (int i = 0; i < chestTreeSpeed50LevelScales.Length - 2; i++)
        {
            GameObject scale = chestTreeSpeed50LevelScales[i];
            if (scale.activeSelf)
            {
                if (i == chestTreeSpeed50LevelScales.Length - 3) // last levelgrade
                {
                    chestTreeSpeed50LevelScales[i + 1].GetComponent<Text>().text = "MAX";
                    chestTreeSpeed50LevelScales[i + 2].GetComponent<Text>().text = "-";
                }
                scale.SetActive(false); // hide white show black
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// If upgrade is not at max level, it increases the upgrade level by 1 (add scale) and returns true
    /// then if it was max level, change the upgrade text to 'MAX' and value to '-', but if not max level return false
    /// </summary>
    /// <returns>
    /// a boolean value.
    /// </returns>
    public bool CanBuyTreeSpawn50()
    {
        /* Checking if scale is not max or if it will max set text to MAX and -. */
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
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    /// <param name="nowIndex">The current index of the upgrade chest.</param>
    public void ShowNextUpgradeChestAutoDestroyTree(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeChestAutoDestroyTree[i].SetActive(false);
        }
        UpgradeChestAutoDestroyTree[nowIndex + 1].SetActive(true);
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
    public void ShowNextUpgradeGoNextStageTree(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeGoNextStageTree[i].SetActive(false);
        }
        UpgradeGoNextStageTree[nowIndex + 1].SetActive(true);
    }


    /// SCENE STONE
    /// SCENE STONE
    /// SCENE STONE
    /// SCENE STONE
    /// SCENE STONE
    /// SCENE STONE
    /// SCENE STONE
    /// SCENE STONE

    /// <summary>
    /// If chestStoneSpeed upgrade is not at max level, it increases the upgrade level by 1 (add scale) and returns true
    /// then if it was max level, change the upgrade text to 'MAX' and value to '-', but if not max level return false
    /// </summary>
    public bool CanBuyChestStoneSpeed50()
    {
        /* Checking if scale is not max or if it will max set text to MAX and -. */
        for (int i = 0; i < chestStoneSpeed50LevelScales.Length - 2; i++)
        {
            GameObject scale = chestStoneSpeed50LevelScales[i];
            if (scale.activeSelf)
            {
                if (i == chestStoneSpeed50LevelScales.Length - 3) // last levelgrade
                {
                    chestStoneSpeed50LevelScales[i + 1].GetComponent<Text>().text = "MAX";
                    chestStoneSpeed50LevelScales[i + 2].GetComponent<Text>().text = "-";
                }
                scale.SetActive(false); // hide white show black
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// If StoneSpawn upgrade is not at max level, it increases the upgrade level by 1 (add scale) and returns true
    /// then if it was max level, change the upgrade text to 'MAX' and value to '-', but if not max level return false
    /// </summary>
    public bool CanBuyStoneSpawn30()
    {
        /* Checking if scale is not max or if it will max set text to MAX and -. */
        for (int i = 0; i < stoneSpawn50LevelScales.Length - 2; i++)
        {
            GameObject scale = stoneSpawn50LevelScales[i];
            if (scale.activeSelf)
            {
                if (i == stoneSpawn50LevelScales.Length - 3) // last levelgrade
                {
                    stoneSpawn50LevelScales[i + 1].GetComponent<Text>().text = "MAX";
                    stoneSpawn50LevelScales[i + 2].GetComponent<Text>().text = "-";
                }
                scale.SetActive(false); // hide white show black
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    /// <param name="nowIndex">The current index of the upgrade.</param>
    public void ShowNextUpgradeChestAutoDestroyStone(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeChestAutoDestroyStone[i].SetActive(false);
        }
        UpgradeChestAutoDestroyStone[nowIndex + 1].SetActive(true);
    }


    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    public void ShowNextUpgradeGoldenStone(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeGoldenStone[i].SetActive(false);
        }
        UpgradeGoldenStone[nowIndex + 1].SetActive(true);
    }

    /// <summary>
    /// It takes an integer as an argument and sets the active state of the game objects in the array to
    /// false, then sets the active state of the next game object in the array to true
    /// </summary>
    public void ShowNextUpgradeGoNextStageStone(int nowIndex)
    {
        for (int i = 0; i <= nowIndex; i++)
        {
            UpgradeGoNextStageStone[i].SetActive(false);
        }
        UpgradeGoNextStageStone[nowIndex + 1].SetActive(true);
    }
}
