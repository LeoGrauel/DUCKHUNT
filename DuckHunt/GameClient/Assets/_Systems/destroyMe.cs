using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMe : MonoBehaviour
{
    public float time = 5.0F;
    float timeTick = 0F;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(time <= timeTick)
        {
            Destroy(this.gameObject);
        }

        timeTick += Time.deltaTime;
    }
}
