using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public int teamid;


    public GameObject shotprefab0;
    public GameObject shotprefab1;

    public void spawnShot(Vector3 location, Quaternion rotation, int shotid)
    {
        switch (shotid)
        {
            case 0:
                {
                    Instantiate(shotprefab0, location, rotation);
                    break;
                }
            case 1:
                {
                    Instantiate(shotprefab1, location, rotation);
                    break;
                }
        }

        Debug.Log("Enemy shot at " + location.ToString());
    }
}