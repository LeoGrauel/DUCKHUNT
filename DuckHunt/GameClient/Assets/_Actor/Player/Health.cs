using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum damagetype
{
    normal, fire, electric, voId
}

public class Health : MonoBehaviour
{
    public int health = 100;
    private int maxhealth;

    private int playerid;

    // Start is called before the first frame update
    void Start()
    {
        maxhealth = health;

        PlayerManager pm = this.GetComponent<PlayerManager>();
        playerid = pm.id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int ammount, damagetype type = damagetype.normal)
    {

        ClientSend.PlayerDamage(Client.instance.myId, playerid, ammount);

        /*
        health = health - ammount;

        if (health <= 0)
        {
            Debug.Log("DEATH");
        }
        Replicate();
        Debug.Log("Health: " + health);
        */
    }


}
