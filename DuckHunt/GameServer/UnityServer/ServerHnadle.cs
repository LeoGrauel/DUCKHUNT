using NiloxUniversalLib.Logging;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Log.Info($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Log.Info($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void PlayerTransform(int fromClient, Packet packet)
        {
            if (checkID(fromClient))
            {
                return;
            }

            Vector3 position = packet.ReadVector3();
            Quaternion quaternion = packet.ReadQuaternion();

            if (Server.clients[fromClient].player == null)
            {
                Log.Error($"Player of {fromClient} is null");
            }

            if (Server.clients[fromClient].player == null)
            {
                Log.Error("Player with id" + fromClient + " doesnt exist");
                return;
            }

            Server.clients[fromClient].player.updateTransform(position, quaternion);
        }

        public static void damagePlayer(int fromclient, Packet packet)
        {
            int instigatorid = packet.ReadInt();
            int targetid = packet.ReadInt();
            int ammount = packet.ReadInt();

            if (checkID(targetid))
            {
                return;
            }

            if (fromclient != instigatorid)
            {
                Log.Warning("Instigator and fromclient didnt match!!!");
                return;
            }

            Server.clients[targetid].player.damage(ammount, instigatorid);
        }

        public static void playershot(int fromclient, Packet packet)
        {
            if (checkID(fromclient))
            {
                return;
            }

            Vector3 location = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();
            int shotid = packet.ReadInt();

            ServerSend.playershot(fromclient, location, rotation, shotid);
        } 





        /// <summary>
        /// Is true if check failed and false if it was good
        /// </summary>
        /// <param name="id">id that needs to be checked</param>
        /// <returns></returns>
        public static bool checkID(int id)
        {
            if (Server.clients[id] == null)
            {
                ServerSend.despawnplayer(id);

                return true;
            }
            else
            {
                if (Server.clients[id].player == null)
                {
                    ServerSend.despawnplayer(id);
                    return true;
                }
            }

            return false;
        }
    }
}