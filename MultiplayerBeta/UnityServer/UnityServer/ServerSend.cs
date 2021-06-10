using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityServer
{
    public class ServerSend
    {
        #region TCP
        private static void SendTCPData(int toclient, Packet packet)
        {
            packet.WriteLength();
            Server.clients[toclient].tcp.SendData(packet);
        }

        private static void SendTCPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayer; i++)
            {
                Server.clients[i].tcp.SendData(packet);
            }
        }
        private static void SendTCPDataToAll(int exeptionclient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayer; i++)
            {
                if (i != exeptionclient)
                {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }
        #endregion

        #region UDP
        private static void SendUDPData(int toclient, Packet packet)
        {
            packet.WriteLength();
            Server.clients[toclient].udp.SendData(packet);
        }

        private static void SendUDPDataToAll(Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayer; i++)
            {
                Server.clients[i].udp.SendData(packet);
            }
        }
        private static void SendUDPDataToAll(int exeptionclient, Packet packet)
        {
            packet.WriteLength();
            for (int i = 1; i < Server.MaxPlayer; i++)
            {
                if (i != exeptionclient)
                {
                    Server.clients[i].udp.SendData(packet);
                }
            }
        }
        #endregion



        public static void Welcome(int tcpclient, string msg)
        {
            using (Packet packet= new Packet((int)ServerPackets.welcome))
            {
                packet.Write(msg);
                packet.Write(tcpclient);

                SendTCPData(tcpclient, packet);
            }
        }

        public static void UDPTest(int toclient)
        {
            using (Packet packet = new Packet((int)ServerPackets.udpTest))
            {
                packet.Write("A test packet for UDP.");

                SendUDPData(toclient, packet);
            }
        }

    }
}
