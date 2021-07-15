using NiloxUniversalLib.Logging;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

    #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            Log.Info("Sending Welcomemessage");
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }


        public static void SpawnPlayer(int _toClient, Player player)
        {
            Log.Info($"Sending Spawn Player({player.username})");
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                packet.Write(player.id);
                packet.Write(player.username);
                packet.Write(player.teamid);
                packet.Write(player.position);
                packet.Write(player.rotation);

                SendTCPData(_toClient, packet);
            }
        }


        public static void PlayerTransform(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerTransform))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPDataToAll(_player.id, _packet);
            }
        }

        public static void updateHealthodId(Player player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.updateHealthOfID))
            {
                _packet.Write(player.id);
                _packet.Write(player.health);

                SendTCPDataToAll(_packet);
            }
        }

        public static void playerDied(Player player, int instigatorid)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerdied))
            {
                _packet.Write(player.id);
                _packet.Write(instigatorid);

                SendTCPDataToAll(_packet);
            }
        }

        public static void despawnplayer(int id)
        {
            Log.Warning("Sending despawn player for id:" + id);
            using (Packet _packet = new Packet((int)ServerPackets.despawnplayer))
            {
                _packet.Write(id);

                SendTCPDataToAll(_packet);
            }
        }

        public static void playerrespawn(Player player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerrespawn))
            {
                _packet.Write(player.id);

                SendTCPDataToAll(_packet);
            }
        }

        public static void playershot(int instigator, Vector3 location, Quaternion rotation, int shotid)
        {
            using (Packet packet = new Packet((int)ServerPackets.playershot))
            {
                packet.Write(instigator);
                packet.Write(location);
                packet.Write(rotation);
                packet.Write(shotid);

                SendTCPDataToAll(instigator, packet );
            }
        }

        public static void updatePoints()
        {
            using (Packet packet = new Packet((int)ServerPackets.updatepoints))
            {
                packet.Write(Game.pointsblue);
                packet.Write(Game.pointsred);

                SendTCPDataToAll(packet);
            }
        }

    #endregion


    }
}