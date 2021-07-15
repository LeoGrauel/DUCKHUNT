using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Collider[]colliders = Physics.OverlapBox(gameObject.transform.position, gameObject.transform.localScale, gameObject.transform.rotation);

        foreach(Collider c in colliders)
        {
            PlayerController p = c.gameObject.gameObject.GetComponent<PlayerController>();
            if (p != null && Gamemode.instance.playoffline == false)
            {
                ClientSend.PlayerDamage(Client.instance.myId, Client.instance.myId, 1000000);
            }
            if (p != null && Gamemode.instance.playoffline)
            {
                p.gameObject.GetComponent<OFFPlayerHealth>().getDamage(5);
                Debug.Log("Damage");
            }
            else
            {
            }
        }
    }
}
