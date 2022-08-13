using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveAndAnimation : MonoBehaviour
{
    // have to move equal to vectorToGo for go to destination
    public Vector2 vectorToGo;
    public float NpcSpeed = 1f;

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
    public void MoveToPoint(Vector2 worldPosition)
    {
        // if not moving
        if (vectorToGo.magnitude < 0.01f)
        {
            vectorToGo = worldPosition - new Vector2(transform.position.x, transform.position.y);
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
}
