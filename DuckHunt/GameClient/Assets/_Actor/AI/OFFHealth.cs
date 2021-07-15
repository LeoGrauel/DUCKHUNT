using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OFFHealth : MonoBehaviour
{
    public bool ALIVE
    {
        get
        {
            if (health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public int health;
    public int maxhealth;

    public bool dorespawn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void getDamage(int value)
    {
        health -= value;

        if (ALIVE == false)
        {
            die();
        }
    }


    public void die()
    {
        Destroy(gameObject);
    }

}
