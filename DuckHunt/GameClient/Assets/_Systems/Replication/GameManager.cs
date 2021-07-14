using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("[GameManager] Instance already exists, destroying object!");
            Destroy(this);
        }

    }

    public void SpawnPlayer(int _id, string _username, int teamid, Vector3 _position, Quaternion _rotation)
    {
        GameObject player;
        if (_id == Client.instance.myId)
        {
           player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            player = Instantiate(playerPrefab, _position, _rotation);
        }

        player.GetComponent<PlayerManager>().id = _id;
        player.GetComponent<PlayerManager>().username = _username;
        player.GetComponent<PlayerManager>().teamid = teamid;
        players.Add(_id, player.GetComponent<PlayerManager>());
    }




}