using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    public class Player
    {
        public int id;
        public string username;

        public int health;

        public Vector3 position;
        public Quaternion rotation;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            health = 100;
        }

        public void Update()
        {
            ServerSend.PlayerTransform(this);
        }

        public void updateTransform(Vector3 _position, Quaternion _quaternion)
        {
            position = _position;
            rotation = _quaternion;
        }
        
        public void damage(int ammount)
        {
            health = health - ammount;

            ServerSend.updateHealthodId(this);
        }

    }
}