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

    public PlayerManager pm;

    // Start is called before the first frame update
    void Start()
    {
        maxhealth = health;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int ammount, damagetype type = damagetype.normal)
    {
        ClientSend.PlayerDamage(Client.instance.myId, pm.id, ammount);
    }


}
