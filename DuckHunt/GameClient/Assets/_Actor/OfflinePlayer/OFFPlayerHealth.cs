using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OFFPlayerHealth : MonoBehaviour
{
    public static OFFPlayerHealth instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Instance already exists destroying OFFPlayerHealth2");
            Destroy(gameObject);
        }
    }

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

    public float health;
    public int maxhealth;

    public bool dorespawn = true;

    public GameObject endscreen;

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

        Debug.Log("Took Damage " + value + " Health: " + health);


        HUD.instance.setHealth((float)health / 100);

        if (ALIVE == false)
        {
            die();
        }
    }


    public void die()
    {
        gameObject.transform.position = Gamemode.instance.deathlocation;
        Instantiate(endscreen);
        gameObject.SetActive(false);
    }

    

}
