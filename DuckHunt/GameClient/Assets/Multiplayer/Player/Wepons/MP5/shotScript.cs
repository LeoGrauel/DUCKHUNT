using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotScript : MonoBehaviour
{
    bool trigger;
    bool see_any;

    Vector3 direction;
    Vector3 lookpos;

    RaycastHit gun_line;

    public AudioClip shot;
    float gun_range;
    float gun_timer;
    float gunShot_delay;

    void Start()
    {
        gun_range = 50.0F;
        gunShot_delay = 1.0F;
        gun_timer = gunShot_delay + 1F;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && gun_timer >= gunShot_delay)
        {
            trigger = true;
            gun_timer = 0F;
        }
    }

    void FixedUpdate()
    {
        lookpos = transform.position;
        direction = transform.forward;

        if(Physics.Raycast(lookpos, direction, out gun_line, gun_range))
        {
            see_any = true;
            print("Anvisiert: " + gun_line.transform.tag);
        }
        else
        {
            see_any = false;
            print("Nichts Avisiert");
        }

        if (trigger)
        {
            trigger = false;
            this.GetComponent<AudioSource>().PlayOneShot(shot);

            if (see_any)
            {
                print("Getroffen: " + gun_line.transform.tag);
                if(gun_line.transform.tag == "localPlayer")
                {
                    
                }
                else
                {

                }
            }
        }

        gun_timer += Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        if (see_any)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(lookpos, direction * gun_line.distance);
        }
        else if (!see_any)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(lookpos, direction * gun_range);
        }
    }
}
