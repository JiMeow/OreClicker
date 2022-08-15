using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStoneGetItemManager : MonoBehaviour
{
    public float hitDelayTime;
    public float countTime;
    NPCStoneMoveAndAnimation NPCmove;

    private void Start()
    {
        // max int
        hitDelayTime = 2147483647;
        NPCmove = GetComponent<NPCStoneMoveAndAnimation>();
    }


    private void Update()
    {
        // increase countTime by time in frame
        countTime += Time.deltaTime;
        // if countTime is greater than hitDelayTime, and NPC is not moving, then set NPC to cut the closest stone (auto)
        if (countTime >= hitDelayTime)
        {
            if (!NPCmove.Moving())
            {
                if (HitClosetStone())
                {
                    countTime = 0;
                }
            }
        }
    }


    /// <summary>
    /// This function takes in a GameObject and adds one to the quantity of the item that the GameObject
    /// represents
    /// </summary>
    /// <param name="GameObject">The item that is being picked up.</param>
    public void GetItem(GameObject item)
    {
        InventoryManager.instance.GetItem(item);
    }


    /// <summary>
    /// Find the closest stone to the player, and move the player to that stone and hit it when at the stone
    /// </summary>
    private bool HitClosetStone()
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");
        GameObject minimumDistanceStone = null;
        // have 1 prototype stone in scene for instantiate so ignore the this stone
        if (stones.Length == 1)
            return false;
        float minimumDistance = float.MaxValue;
        foreach (GameObject stone in stones)
        {
            // have 1 prototype stone in scene for instantiate so ignore the this stone
            if (stone.transform.position.x < -5) continue;
            float distance = EuclideanDistance(stone.transform.position, transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                minimumDistanceStone = stone;
            }
        }
        if (minimumDistanceStone != null)
        {
            Vector3 stonePosition = minimumDistanceStone.transform.position;
            NPCmove.MoveToPoint(stonePosition, whenStopHitStone: true);
            return true;
        }
        return false;
    }


    /// <summary>
    /// "The Euclidean distance between two points is the length of the path connecting them."
    /// </summary>
    /// <param name="Vector3">A 3D vector.</param>
    /// <param name="Vector3">A 3D vector.</param>
    /// <returns>
    /// The distance between two points with no dimention z.
    /// </returns>
    private float EuclideanDistance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2));
    }


    /// <summary>
    /// This function sets the hitDelayTime variable to the value of the time parameter
    /// </summary>
    /// <param name="time">The time to delay the hit.</param>
    public void SetHitDelayTime(float time)
    {
        hitDelayTime = Mathf.Min(time, hitDelayTime);
    }
}
