using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;


    public GameObject shotprefab;

    public void spawnShot(Vector3 location, Quaternion rotation)
    {
        Instantiate(shotprefab, location, rotation);
    }
}