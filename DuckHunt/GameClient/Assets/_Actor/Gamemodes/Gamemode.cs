using System.Collections;
using System.Collections.Generic;
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
        if (playoffline)
        {
            GameManager.instance.SpawnPlayer(0, "local", spawnlocation, new Quaternion(0, 0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
