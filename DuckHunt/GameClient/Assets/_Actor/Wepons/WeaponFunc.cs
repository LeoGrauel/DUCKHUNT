using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunc : MonoBehaviour
{
    public bool shotenabled = true;

    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletTrail;
    public GameObject bulletHit;
    public GameObject playerHit;
    public Animation recoil;
    //public Animation reloadA;
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
    public float reloadTime = 2.0F;

    int rounds;
    float reload_timer;
    float gun_range;
    float gun_timer;
    float gunShot_delay;

    public int getRounds()
    {
        return rounds;
    }

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
        if (shotenabled == false)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0) && gun_timer >= gunShot_delay)
        {
            trigger = true;
            gun_timer = 0F;
        }
        if(Input.GetKey(KeyCode.R) && rounds != magazine && reload_timer >= reloadTime)
        {
            reload = true;
            //reloadA.Play();
            reload_timer = 0F;
        }
    }

    void FixedUpdate()
    {
        if (shotenabled == false)
        {
            return;
        }

        lookpos = transform.position;
        direction = transform.forward;

        if (reload)
        {
            reload = false;
            rounds = magazine;
            HUD.instance.setAmmo(rounds);
            return;
        }

        if (trigger && reload_timer >= reloadTime)
        {
            trigger = false;
            if(rounds <= 0)
            {
                if (!empty)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(emptyAudio);
                    empty = true;
                }
                return;
            }
            else
            {
                empty = false;
            }

            rounds -= 1;
            HUD.instance.setAmmo(rounds);
            muzzleFlash.Play();
            this.GetComponent<AudioSource>().PlayOneShot(shot);
            bulletTrail.Play();
            recoil.Stop();
            recoil.Play();
            
            if (Physics.Raycast(lookpos, direction, out hitresult, gun_range))
            {
                Health h = hitresult.collider.gameObject.GetComponentInParent<Health>();
                if (h != null)
                {
                    h.Damage(damage);
                    Instantiate(playerHit, hitresult.point, Quaternion.LookRotation(hitresult.normal));
                }
                else
                {
                    Instantiate(bulletHit, hitresult.point, Quaternion.LookRotation(hitresult.normal));
                }
            }
            else
            {
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
