using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunc : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    bool trigger = false;
    bool empty = false;
    bool reload = false;

    Vector3 direction;
    Vector3 lookpos;

    RaycastHit hitresult;

    public AudioClip shot;
    public AudioClip emptyAudio;
    public int rpm = 450;
    public int magazine = 20;
    public int damage = 5;
    public float reloadTime = 3.0F;

    int rounds;
    float reload_timer;
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
        }

        rounds = magazine;
        reload_timer = reloadTime;
        gun_range = 50.0F;
        gun_timer = gunShot_delay + 1F;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && gun_timer >= gunShot_delay)
        {
            trigger = true;
            gun_timer = 0F;
        }
        if(Input.GetKey(KeyCode.R) && rounds != magazine && reload_timer >= reloadTime)
        {
            reload = true;
            reload_timer = 0F;
        }
    }

    void FixedUpdate()
    {
        lookpos = transform.position;
        direction = transform.forward;
        if (empty)
        {
            this.GetComponent<AudioSource>().PlayOneShot(emptyAudio);
            if (reload)
            {
                reload = false;
                rounds = magazine;
            }
        }

        if (reload)
        {
            reload = false;
            rounds = magazine;
            return;
        }

        if (trigger)
        {
            trigger = false;
            if(rounds <= 0)
            {
                return;
            }
            rounds -= 1;
            muzzleFlash.Play();
            this.GetComponent<AudioSource>().PlayOneShot(shot);
            
            if (Physics.Raycast(lookpos, direction, out hitresult, gun_range))
            {
                Health h = hitresult.collider.gameObject.GetComponentInParent<Health>();
                if (h != null)
                {
                    h.Damage(damage);
                    //Debug.Log("HIT");
                }
                else
                {
                    //Debug.Log("Hit doenst have health component");
                }
            }
            else
            {
                //Debug.Log("No hit");
            }
        }

        gun_timer += Time.deltaTime;
        reload_timer += Time.deltaTime;
    }

    /*void OnDrawGizmos()
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
    }*/
}
