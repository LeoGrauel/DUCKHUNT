using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Gamemode : MonoBehaviour
{
    public static Gamemode instance;

    // Start is called before the first frame update
    public bool playoffline;
    public GameObject localPlayerPrefab;
    public string username0;

    public Vector3 spawnlocation;
    public Vector3 deathlocation;

    public GameObject[] respawns;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");


        if (playoffline)
        {
            GameManager.instance.SpawnPlayer(0, "local", 0, getrandomSpawnpoint(), new Quaternion(0, 0, 0, 0)); ;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public Vector3 getrandomSpawnpoint()
    {
        if (respawns.Length == 0)
        {
            Debug.LogError("NO RESPAWNS AVAILABLE");
        }

        int index = Random.Range(0, respawns.Length);

        return respawns[index].transform.position;
    }


}
