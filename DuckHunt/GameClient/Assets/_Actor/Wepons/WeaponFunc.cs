using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunc : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    bool trigger;

    Vector3 direction;
    Vector3 lookpos;

    RaycastHit hitresult;

    public AudioClip shot;
    public int rpm = 450;
    public int damage = 5;

    float gun_range;
    float gun_timer;
    float gunShot_delay;

    void Start()
    {
        //Convert RPM to shotdelay
        {
            float e = rpm / 60;
            e = 1 / e;
            gunShot_delay = e;

            Debug.Log("Delay 1:" + gunShot_delay);
        }

        gun_range = 50.0F;
        gun_timer = gunShot_delay + 1F;

        Debug.Log("Delay 2:" + gunShot_delay);
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
            muzzleFlash.Play();
            this.GetComponent<AudioSource>().PlayOneShot(shot);
            
            if (Physics.Raycast(lookpos, direction, out hitresult, gun_range))
            {
                Health h = hitresult.collider.GetComponent<Health>();
                if (h != null)
                {
                    h.Damage(damage);
                }
                else
                {
                    Debug.Log("Hit doenst have health component");
                }
            }
            else
            {
                //Debug.Log("No hit");
            }
        }

        gun_timer += Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        if (trigger)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(lookpos, direction * hitresult.distance);
        }
        else if (!trigger)
        {
            //Gizmos.color = Color.green;
            //Gizmos.DrawRay(lookpos, direction * gun_range);
        }
    }
}
