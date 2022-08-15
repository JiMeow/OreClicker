using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTreeGetItemManager : MonoBehaviour
{
    public float cutDelayTime;
    public float countTime;
    NPCTreeMoveAndAnimation NPCmove;

    private void Start()
    {
        // max int
        cutDelayTime = 2147483647;
        NPCmove = GetComponent<NPCTreeMoveAndAnimation>();
    }

    private void Update()
    {
        // increase countTime by time in frame
        countTime += Time.deltaTime;
        // if countTime is greater than cutDelayTime, and NPC is not moving, then set NPC to cut the cloest tree (auto)
        if (countTime >= cutDelayTime)
        {
            if (!NPCmove.Moving())
            {
                if (CutClosetTree())
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
    /// Find the closest tree to the player, and move the player to that tree and cut it down when at the tree
    /// </summary>
    private bool CutClosetTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        GameObject minimumDistanceTree = null;
        // have 1 prototype tree in scene for instantiate so ignore the this tree
        if (trees.Length == 1)
            return false;
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
            Vector3 treePosition = minimumDistanceTree.transform.position;
            treePosition.y -= 0.44f; // from leaf to stem
            NPCmove.MoveToPoint(treePosition, whenStopCutTree: true);
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
    /// This function sets the cutDelayTime variable to the value of the time parameter
    /// </summary>
    /// <param name="time">The time to delay the cut.</param>
    public void SetCutDelayTime(float time)
    {
        cutDelayTime = Mathf.Min(time, cutDelayTime);
    }
}
