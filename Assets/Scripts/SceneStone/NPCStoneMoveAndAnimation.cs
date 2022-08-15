using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStoneMoveAndAnimation : MonoBehaviour
{
    // have to move equal to vectorToGo for go to destination
    public Vector2 vectorToGo;
    public bool isMoving;
    public float NpcSpeed;
    bool whenStopHitStone = false;

    private void Start()
    {
        vectorToGo = new Vector2(0, 0);
        isMoving = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // if have to move to hit stone
        if (isMoving)
        {
            // move towards destination by NpcSpeed
            Vector2 move = vectorToGo.normalized * NpcSpeed * Time.deltaTime;
            // if move is greater than distance to go, then move to destination
            if (move.magnitude > vectorToGo.magnitude)
            {
                move = vectorToGo;
            }
            // move NPC
            transform.Translate(move);
            // subtract move from distance to go
            vectorToGo -= move;
            if (vectorToGo.magnitude <= 0.001f)
            {
                // if NPC is at destination and whenStopHitStone is true, then hit stone
                if (whenStopHitStone)
                {
                    HitClosetStone();
                }
                // else stop moving
                else
                {
                    isMoving = false;
                }
            }
        }
        // if the NPC move to the left, then scale size to 1 for the left animation
        if (vectorToGo.x <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // if the NPC move to the right, then scale size to -1 for the right animation
        if (vectorToGo.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// This function sets the vectorToGo variable to the vector between the current position of the
    /// object and the worldPosition parameter (for go to world position from current position by vectorToGo vector)
    /// </summary>
    /// <param name="Vector2">The position you want the object to move to.</param>
    public void MoveToPoint(Vector2 worldPosition, bool whenStopHitStone = false)
    {
        // if not go for hitting
        if (!isMoving)
        {
            vectorToGo = worldPosition - new Vector2(transform.position.x, transform.position.y);
            this.whenStopHitStone = whenStopHitStone;
            isMoving = true;
        }
    }

    /// <summary>
    /// Set the eating animation to true, then after 0.25 seconds, set the idle animation to true
    /// </summary>
    public void SetEatingAnimation()
    {
        GetComponent<Animator>().SetBool("isEating", true);
        Invoke("SetIdleAnimation", 0.25f);
    }

    /// <summary>
    /// Set the idle animation to true (set false in isEating)
    /// </summary>
    private void SetIdleAnimation()
    {
        GetComponent<Animator>().SetBool("isEating", false);
    }

    /// <summary>
    /// It takes a float value (percent) then increases the speed of the NPC by that percent
    /// </summary>
    /// <param name="percent">The percentage of speed increase.</param>
    public void SetSpeedUp(float percent)
    {
        NpcSpeed *= (1f + percent / 100f);
    }

    /// <summary>
    /// If chest is hitting stone, then return true else return false
    /// </summary>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public bool Moving()
    {
        return isMoving;
    }

    /// <summary>
    /// Find the closest stone to the player, and hit it with power = 5, (for auto destroy after hitting)
    /// </summary>
    private void HitClosetStone()
    {
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Stone");
        // have 1 prototype stone in scene for instantiate so ignore the this stone
        if (stones.Length == 1)
            return;
        GameObject minimumDistanceStone = null;
        float minimumDistance = float.MaxValue;
        foreach (GameObject stone in stones)
        {
            // have 1 prototype stone in scene for instantiate so ignore the this stone
            if (stone.transform.position.x < -5) continue;

            Vector3 stonePosition = stone.transform.position;
            float distance = EuclideanDistance(stonePosition, transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                minimumDistanceStone = stone;
            }
        }
        if (minimumDistanceStone != null)
        {
            minimumDistanceStone.GetComponent<StoneBrickManager>().HitStone(5);
            whenStopHitStone = false;
        }
        isMoving = false;
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
}
