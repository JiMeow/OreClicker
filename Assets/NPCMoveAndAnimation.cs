using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveAndAnimation : MonoBehaviour
{
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
        if (vectorToGo.magnitude > 0.01f)
        {
            Vector2 move = vectorToGo.normalized * NpcSpeed * Time.deltaTime;
            if (move.magnitude > vectorToGo.magnitude)
            {
                move = vectorToGo;
            }
            transform.Translate(move);
            vectorToGo -= move;
        }
        if (vectorToGo.x <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (vectorToGo.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void MoveToPoint(Vector2 worldPosition)
    {
        if (vectorToGo.magnitude < 0.01f)
        {
            vectorToGo = worldPosition - new Vector2(transform.position.x, transform.position.y);
        }
    }

    public void SetEatingAnimation()
    {
        GetComponent<Animator>().SetBool("isEating", true);
        Invoke("SetIdleAnimation", 0.25f);
    }

    public void SetIdleAnimation()
    {
        GetComponent<Animator>().SetBool("isEating", false);
    }
}
