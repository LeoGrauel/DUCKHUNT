using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotautoplay : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletTrail;

    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.Play();
        bulletTrail.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
