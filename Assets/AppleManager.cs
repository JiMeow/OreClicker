using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    [SerializeField]
    GameObject NPC;
    float dropDistance;
    private void Start()
    {
        dropDistance = 0.44f;
    }

    private void Update()
    {
        DropToFloor();
    }

    /// <summary>
    /// If the dropDistance is greater than 0, then subtract time in frame from dropDistance and move
    /// the object down by time in frame
    /// </summary>
    private void DropToFloor()
    {
        if (dropDistance > 0)
        {
            dropDistance -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
        }
    }

    /// <summary>
    /// If this object that enters the trigger is an NPC, then the NPC will get the item and move to it then destroy it
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPCGetItemManager>().GetItem(gameObject);
            other.gameObject.GetComponent<NPCMoveAndAnimation>().SetEatingAnimation();
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// When the player clicks on the object, the NPC will move to the position of the object but position is at on the floor (dropDistance=0)
    /// </summary>
    private void OnMouseDown()
    {
        NPC.GetComponent<NPCMoveAndAnimation>().MoveToPoint(new Vector2(transform.position.x, transform.position.y - dropDistance));
    }

}
