using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public bool islocalprojectile;
    public GameObject front;

    public void Awake()
    {
        
    }



    void FixedUpdate()
    {
        bool hit = Physics.Raycast(front.transform.position, front.transform.forward, 100, 0, QueryTriggerInteraction.Collide);
        if (hit)
        {
            
        }
    }
}
