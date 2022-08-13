using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleManager : MonoBehaviour
{
    float dropDistance;
    void Start()
    {
        dropDistance = 0.44f;
    }

    void Update()
    {
        DropToFloor();
    }

    void DropToFloor()
    {
        if (dropDistance > 0)
        {
            dropDistance -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
        }
    }
}
