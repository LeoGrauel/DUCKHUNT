using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBuffersize = 4096;

    public string ip = "127.0.0.1";
    public int port = 26950;
    public int myId = 0;
    public TCP tcp;
    public UDP udp;

    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
        udp = new UDP();
    }

    public void ConnectToServer()
    {
        InitializeClientData();
        tcp.Connect();

    }

    public class TCP
    {
        public TcpClient socket;

        private NetworkStream stream;
        private Packet receivedData;
        private byte[] recieveBuffer;

        public void Connect()
        {
            socket = new TcpClient()
            {
                ReceiveBufferSize = dataBuffersize,
                SendBufferSize = dataBuffersize
            };

            recieveBuffer = new byte[dataBuffersize];
            socket.BeginConnect(instance.ip, instance.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            socket.EndConnect(result);

            if (!socket.Connected)
            {
                return;
            }

            stream = socket.GetStream();

            receivedData = new Packet();

            stream.BeginRead(recieveBuffer, 0, dataBuffersize, ReceiveCallback, null);
        }

        public void SendData(Packet packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = stream.EndRead(result);
                if (byteLength <= 0)
                {
                    //TODO: disconnnec
                }

                byte[] data = new byte[byteLength];
                Array.Copy(recieveBuffer, data, byteLength);

                receivedData.Reset(HandleData(data));

                stream.BeginRead(recieveBuffer, 0, dataBuffersize, ReceiveCallback, null);
            }
            catch (Exception)
            {
                //TODO: Disconnect
            }
        }

        private bool HandleData(byte[] data)
        {
            int packetlength = 0;
            receivedData.SetBytes(data);

            if (receivedData.UnreadLength() >= 4)
            {
                packetlength = receivedData.ReadInt();
                if (packetlength <= 0)
                {
                    return true;
                }
            }

            while (packetlength > 0 && packetlength <= receivedData.UnreadLength())
            {
                byte[] packetbytes = receivedData.ReadBytes(packetlength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetbytes)) 
                    {
                        int packetid = packet.ReadInt();
                        packetHandlers[packetid](packet);
                    }
                });

                packetlength = 0;
                if (receivedData.UnreadLength() >= 4)
                {
                    packetlength = receivedData.ReadInt();
                    if (packetlength <= 0)
                    {
                        return true;
                    }
                }
            }

            if (packetlength <= 1)
            {
                return true;
            }

            return false;
        }

        
    }

    public class UDP
    {
        public UdpClient socket;
        public IPEndPoint endPoint;

        public UDP()
        {
            endPoint = new IPEndPoint(IPAddress.Parse(instance.ip), instance.port);
        }

        /// <summary>Attempts to connect to the server via UDP.</summary>
        /// <param name="_localPort">The port number to bind the UDP socket to.</param>
        public void Connect(int _localPort)
        {
            socket = new UdpClient(_localPort);

            socket.Connect(endPoint);
            socket.BeginReceive(ReceiveCallback, null);

            using (Packet _packet = new Packet())
            {
                SendData(_packet);
            }
        }

        /// <summary>Sends data to the client via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        public void SendData(Packet _packet)
        {
            try
            {
                _packet.InsertInt(instance.myId); // Insert the client's ID at the start of the packet
                if (socket != null)
                {
                    socket.BeginSend(_packet.ToArray(), _packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via UDP: {_ex}");
            }
        }

        /// <summary>Receives incoming UDP data.</summary>
        private void ReceiveCallback(IAsyncResult _result)
        {
            try
            {
                byte[] _data = socket.EndReceive(_result, ref endPoint);
                socket.BeginReceive(ReceiveCallback, null);

                if (_data.Length < 4)
                {
                    //instance.Disconnect();
                    return;
                }

                HandleData(_data);
            }
            catch
            {
                //Disconnect();
            }
        }

        /// <summary>Prepares received data to be used by the appropriate packet handler methods.</summary>
        /// <param name="_data">The recieved data.</param>
        private void HandleData(byte[] _data)
        {
            using (Packet _packet = new Packet(_data))
            {
                int _packetLength = _packet.ReadInt();
                _data = _packet.ReadBytes(_packetLength);
            }

            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet _packet = new Packet(_data))
                {
                    int _packetId = _packet.ReadInt();
                    packetHandlers[_packetId](_packet); // Call appropriate method to handle the packet
                }
            });
        }

        /// <summary>Disconnects from the server and cleans up the UDP connection.</summary>
        private void Disconnect()
        {
            //instance.Disconnect();

            endPoint = null;
            socket = null;
        }
    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int)ServerPackets.welcome, CLientHandle.Welcome},
            { (int)ServerPackets.udpTest, CLientHandle.UDPTest}
        };
        Debug.Log("Initialized Packets");
    }
}
