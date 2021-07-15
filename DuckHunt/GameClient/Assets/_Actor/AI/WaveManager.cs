using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int wave = 1;
    public float multiplier = 1.5f;

    private int spawns = 0;
    private int wavetotal = 0;

    public GameObject[] respawns;
    public GameObject enemieduck;

    public bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    public int countAis()
    {
        return GameObject.FindGameObjectsWithTag("Duck").Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawning == false && countAis() == 0)
        {
            wave++;
            Debug.Log("Started Wave:" + wave);
            wavetotal = (int)(wave * multiplier);
            spawns = 0;
            spawning = true;
            StartCoroutine(spawnAi());
        }

        
    }

    public void startWave()
    {

    }

    IEnumerator spawnAi()
    {
        while (spawns < wavetotal)
        {
            yield return new WaitForSeconds(1f);
            spawnDuck();
            spawns++;
        }

        spawning = false;
        Debug.Log("Wave " + wave + " fully spawned " + spawns);
    }

    public void spawnDuck()
    {
        Instantiate(enemieduck, Gamemode.instance.getrandomSpawnpoint(), new Quaternion(0,0,0,0));
    }

}
