using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DummyServer
{

    public int MaxPlayers { get; set; }
    public int Port { get; set; }

    private TcpListener tcpListener;
    private UdpClient udpListeneer;


    public void Start()
    {
        MaxPlayers = 1;
        Port = 26950;

        tcpListener = new TcpListener(IPAddress.Any, Port);
        tcpListener.Start();
        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);

        udpListeneer = new UdpClient(Port);
        udpListeneer.BeginReceive(UDPReceiveCallback, null);

        Debug.Log("Dummy start");

        tcpListener.Stop();
        tcpListener = null;

        udpListeneer.Close();
        udpListeneer = null;
        Debug.Log("Dummy end");
    }

    private void TCPConnectionCallback(IAsyncResult result)
    {

    }

    private void UDPReceiveCallback(IAsyncResult result)
    {

    }


}

