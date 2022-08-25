using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopperOreManager : MonoBehaviour
{
    [SerializeField]
    GameObject NPCStone;


    /// <summary>
    /// If this object that enters the trigger is an NPC, then the NPC will get the item and move to it then destroy it
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            InventoryManager.instance.GetItem(gameObject);
            other.gameObject.GetComponent<NPCCopperAndQuartMovingAndAnimation>().SetEatingAnimation();
            Destroy(gameObject);
            if (SwitchSceneManager.instance.getNowScene() == 1)
                SoundManager.instance.PlayPickUpItem();
        }
    }

    /// <summary>
    /// When the player clicks on the object, the NPC will move to the position of the object 
    /// </summary>
    private void OnMouseDown()
    {
        NPCStone.GetComponent<NPCCopperAndQuartMovingAndAnimation>().MoveToPoint(new Vector2(transform.position.x, transform.position.y));
    }
}
