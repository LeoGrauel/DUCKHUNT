using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
    public GameObject sun;
    public float dayTime = 180F;
    float dayTime_timer = 0F;
    bool night = false;
    GameObject[] lightPosts;

    // Update is called once per frame
    void Update()
    {
        if(dayTime <= dayTime_timer)
        {
            if (!night)
            {
                lightPosts = GameObject.FindGameObjectsWithTag("lightSource");

                foreach(GameObject g in lightPosts)
                {
                    g.GetComponent<Light>().intensity = 0F;
                }
            }
            else
            {
                lightPosts = GameObject.FindGameObjectsWithTag("lightSource");

                foreach (GameObject g in lightPosts)
                {
                    g.GetComponent<Light>().intensity = 1.5F;
                }
            }

            dayTime_timer = 0F;
        }
        

        dayTime_timer += Time.deltaTime;
    }
}
