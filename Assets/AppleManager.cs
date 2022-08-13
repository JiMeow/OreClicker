using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    [SerializeField]
    GameObject NPC;
    float dropDistance;
    void Start()
    {
        dropDistance = 0.44f;
    }

    void Update()
    {
        DropToFloor();
    }

    /// <summary>
    /// If the dropDistance is greater than 0, then subtract time in frame from dropDistance and move
    /// the object down by time in frame
    /// </summary>
    void DropToFloor()
    {
        if (dropDistance > 0)
        {
            dropDistance -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NPC")
        {
            other.gameObject.GetComponent<NPCGetItemManager>().GetItem(gameObject);
            other.gameObject.GetComponent<NPCMoveAndAnimation>().SetEatingAnimation();
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        NPC.GetComponent<NPCMoveAndAnimation>().MoveToPoint(new Vector2(transform.position.x, transform.position.y - dropDistance));
    }

}
