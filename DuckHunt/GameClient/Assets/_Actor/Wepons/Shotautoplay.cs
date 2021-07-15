using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotautoplay : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletTrail;

    public AudioClip shotaudio;

    public void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.Play();
        bulletTrail.Play();

        this.GetComponent<AudioSource>().PlayOneShot(shotaudio, GameInstance.instance.MasterVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
