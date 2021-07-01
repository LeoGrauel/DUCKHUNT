using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotScriptmp55 : MonoBehaviour
{
    bool trigger;

    Vector3 direction;
    Vector3 lookpos;


    RaycastHit gun_line;

    public bool Debug = false;
    public AudioClip shot;
    float gun_range;
    float gun_timer;
    float gunShot_delay;

    void Start()
    {
        gun_range = 50.0F;
        gunShot_delay = 0.1F;
        gun_timer = gunShot_delay + 1F;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && gun_timer >= gunShot_delay)
        {
            trigger = true;
            gun_timer = 0F;
        }
    }

    void FixedUpdate()
    {
        lookpos = transform.position;
        direction = transform.forward;



        if (trigger)
        {
            trigger = false;
            this.GetComponent<AudioSource>().PlayOneShot(shot);

            if (Physics.Raycast(lookpos, direction, out gun_line, gun_range))
            {
                print("Getroffen: " + gun_line.transform.tag);
            }
            else
            {
                print("Nichts Getroffen");
            }
        }

        gun_timer += Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        if (trigger)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(lookpos, direction * gun_line.distance);
        }
        else if (!trigger)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(lookpos, direction * gun_range);
        }
    }
}
