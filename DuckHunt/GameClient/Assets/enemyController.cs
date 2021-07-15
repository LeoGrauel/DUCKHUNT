using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public NavMeshAgent enemy;
    float time = 0.2F;
    float time_tick = 0F;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time <= time_tick)
        {
            enemy.SetDestination(PlayerController.instance.gameObject.transform.position);
            time_tick = 0f;
        }

        time_tick += Time.deltaTime;
    }
}
