using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using NiloxUniversalLib.Logging;
using System.Timers;

namespace GameServer
{
    public class Player
    {
        public int id;
        public string username;

        public int teamid;

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

        Timer respawntimer = new Timer();

        public Player(int _id, string _username, int _teamid, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            teamid = _teamid;
            position = _spawnPosition;
            rotation = Quaternion.Identity;
            health = 100;

            if (teamid == 0)
            {
                Game.bluecount++;
            }
            if (teamid == 1)
            {
                Game.redcount++;
            }

            respawntimer.Interval = 2000;
            respawntimer.Elapsed += Respawn;
        }

        private void Respawn(object sender, ElapsedEventArgs e)
        {
            health = 100;
            ServerSend.playerrespawn(this);
            respawntimer.Stop();
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
                    if (teamid == 0)
                    {
                        Game.pointsred++;
                    }
                    if (teamid == 1)
                    {
                        Game.pointsblue++;
                    }

                    Log.Info($"Player {username} died");
                    ServerSend.playerDied(this, instigatorid);
                    respawntimer.Start();

                    ServerSend.updatePoints();
                }
            }
            else
            {
                Log.Warning("Player already dead");
            }
            
        }


    }
}