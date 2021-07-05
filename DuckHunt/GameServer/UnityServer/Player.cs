using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using NiloxUniversalLib.Logging;

namespace GameServer
{
    public class Player
    {
        public int id;
        public string username;

        public int health;
        public bool alive
        {
            get
            {
                if (health > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

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
            if (alive)
            {
                ServerSend.PlayerTransform(this);
            }
        }

        public void updateTransform(Vector3 _position, Quaternion _quaternion)
        {
            if (alive)
            {
                position = _position;
                rotation = _quaternion;
            }
        }
        
        public void damage(int ammount, int instigatorid)
        {
            if (alive)
            {
                health = health - ammount;

                Log.Debug($"Player {username} took {ammount} Damage and has now {health}HP");

                ServerSend.updateHealthodId(this);

                if (alive == false)
                {
                    Log.Info($"Player {username} died");
                    ServerSend.playerDied(this, instigatorid);
                }
            }
            else
            {
                Log.Warning("Player already dead");
            }
            
        }


    }
}