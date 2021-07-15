using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public NavMeshAgent enemy;
    float time = 0.2F;
    float time_tick = 0F;

    public SphereCollider damagecollider;

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


            Collider[] colliders = Physics.OverlapSphere(damagecollider.transform.position, damagecollider.radius * 3);

            foreach (Collider c in colliders)
            {
                PlayerController p = c.gameObject.gameObject.GetComponent<PlayerController>();
                if (p != null)
                {
                    p.gameObject.GetComponent<OFFPlayerHealth>().getDamage(5);
                    Debug.Log("Damage");
                }
                else
                {
                }
            }
        }

        time_tick += Time.deltaTime;

        

    }
}
