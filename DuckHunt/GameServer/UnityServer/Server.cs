using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace GameServer
{
    public class Server
    {
        public static int MaxPlayers { get; set; }
        public static int Port { get; set; }

        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public delegate void PacketHandler(int fromClient, Packet packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        private static TcpListener tcpListener;
        private static UdpClient udpListeneer;

        private static bool shouldclose = false;

        public static void Start(int maxplayers, int port)
        {
            MaxPlayers = maxplayers;
            Port = port;

            entlistInDatabase();

            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);

            udpListeneer = new UdpClient(port);
            udpListeneer.BeginReceive(UDPReceiveCallback, null);

            Console.WriteLine($"Server started on {Port}");
        }

        private static void TCPConnectionCallback(IAsyncResult result)
        {
            if (shouldclose)
            {
                return;
            }

            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);
            Console.WriteLine($"Incomming connection from {client.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(client);
                    return;
                }
            }

            Console.WriteLine($"{client.Client.RemoteEndPoint} failed to connect: Server full!");
        }

        private static void UDPReceiveCallback(IAsyncResult result)
        {
            if (shouldclose)
            {
                return;
            }

            try
            {
                IPEndPoint clientENdpoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpListeneer.EndReceive(result, ref clientENdpoint);
                udpListeneer.BeginReceive(UDPReceiveCallback, null);

                if (data.Length < 4)
                {
                    return;
                }

                using (Packet packet = new Packet(data))
                {
                    int clientid = packet.ReadInt();

                    if (clientid == 0)
                    {
                        return;
                    }

                    if (clients[clientid].udp.endPoint == null)
                    {
                        clients[clientid].udp.Connect(clientENdpoint);
                        return;
                    }

                    if (clients[clientid].udp.endPoint.ToString() == clientENdpoint.ToString())
                    {
                        clients[clientid].udp.HandleData(packet);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving UDP data: {e.Message}");
            }
        }

        public static void SendUDPData(IPEndPoint clientendpoint, Packet packet)
        {
            if (shouldclose)
            {
                return;
            }

            try
            {
                if (clientendpoint != null)
                {
                    udpListeneer.BeginSend(packet.ToArray(), packet.Length(), clientendpoint, null, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending data to {clientendpoint} via UDP: {e}");
            }
        }


        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
                { (int)ClientPackets.playerTransform, ServerHandle.PlayerTransform },
                { (int)ClientPackets.damagePlayer, ServerHandle.damagePlayer}
            };
            Console.WriteLine("Initialized packets: " + packetHandlers.Count);
        }


        #region SQL
        public async static void entlistInDatabase()
        {
            string result = "";
            {
                HttpClient client = new HttpClient();

                var values = new Dictionary<string, string>
                {
                    { "servername", "hello" },
                    { "maxplayercount", "10" },
                    { "currentplayercount", "1" }
                };

                var content = new FormUrlEncodedContent(values);


                var response = await client.PostAsync("http://nilox.network/open/PHP/duckhunt/entlist.php", content);

                result = await response.Content.ReadAsStringAsync();

                if (result == "1")
                {
                    Console.WriteLine("Databse entry was succesfull");
                }
                else
                {
                    Console.WriteLine("Databaseentry Failed stopping server");
                    await Task.Delay(-1);
                }
            }
        }

        public async static void removefromDatabase()
        {
            string result = "";
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync("http://nilox.network/open/PHP/duckhunt/delist.php");

                result = await response.Content.ReadAsStringAsync();

                if (result == "1")
                {
                    Console.WriteLine("Databse entry was removed");
                }
                else
                {
                    Console.WriteLine("Databaseremoval Failed stopping server");
                    await Task.Delay(-1);
                }
            }
        }
        #endregion
    }
}
