using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveAndAnimation : MonoBehaviour
{
    // have to move equal to vectorToGo for go to destination
    public Vector2 vectorToGo;
    public float NpcSpeed;
    bool whenStopCutTree = false;

    void Start()
    {
        vectorToGo = new Vector2(0, 0);
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // if have to move
        if (vectorToGo.magnitude > 0.01f)
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
            if (vectorToGo.magnitude <= 0.01f)
            {
                // if NPC is at destination and whenStopCutTree is true, then cut closet tree
                if (whenStopCutTree)
                {
                    CutClosetTree();
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
    public void MoveToPoint(Vector2 worldPosition, bool whenStopCutTree = false)
    {
        // if not moving
        if (vectorToGo.magnitude < 0.01f)
        {
            vectorToGo = worldPosition - new Vector2(transform.position.x, transform.position.y);
            this.whenStopCutTree = whenStopCutTree;
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
    public void SetIdleAnimation()
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
    /// If chest is moving, then return true else return false
    /// </summary>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public bool isMoving()
    {
        if (vectorToGo.magnitude >= 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Find the closest tree to the player, and cut it down with power = 3
    /// </summary>
    private void CutClosetTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        // have 1 prototype tree in scene for instantiate so ignore the this tree
        if (trees.Length == 1)
            return;
        GameObject minimumDistanceTree = null;
        float minimumDistance = float.MaxValue;
        foreach (GameObject tree in trees)
        {
            // have 1 prototype tree in scene for instantiate so ignore the this tree
            if (tree.transform.position.x < -5) continue;
            float distance = EuclideanDistance(tree.transform.position, transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                minimumDistanceTree = tree;
            }
        }
        if (minimumDistanceTree != null)
        {
            minimumDistanceTree.GetComponent<TreeCutManager>().CutTree(3);
            whenStopCutTree = false;
        }
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
