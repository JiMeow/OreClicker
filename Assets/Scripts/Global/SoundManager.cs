using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    AudioSource chopTreeSound;
    [SerializeField]
    AudioSource treeWasCuttedSound;
    [SerializeField]
    AudioSource hitStoneSound;
    [SerializeField]
    AudioSource stoneWasDestroyedSound;
    [SerializeField]
    AudioSource pickUpItem;
    [SerializeField]
    AudioSource upgradeSuccessSound;

    private void Awake()
    {
        instance = this;
    }

    public void PlayChopTreeSound()
    {
        if (SwitchSceneManager.instance.getNowScene() == 0)
            chopTreeSound.Play();
    }

    public void PlayTreeWasCuttedSound()
    {
        if (SwitchSceneManager.instance.getNowScene() == 0)
            treeWasCuttedSound.Play();
    }

    public void PlayHitStoneSound()
    {
        if (SwitchSceneManager.instance.getNowScene() == 1)
            hitStoneSound.Play();
    }

    public void PlayStoneWasDestroyedSound()
    {
        if (SwitchSceneManager.instance.getNowScene() == 1)
            stoneWasDestroyedSound.Play();
    }

    public void PlayPickUpItem()
    {
        pickUpItem.Play();
    }

    public void PlayUpgradeSuccessSound()
    {
        upgradeSuccessSound.Play();
    }
}
