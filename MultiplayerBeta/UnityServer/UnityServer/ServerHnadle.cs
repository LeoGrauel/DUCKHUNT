using System;
using System.Collections.Generic;
using System.Text;

namespace UnityServer
{
    class ServerHandle
    {

        public static void WelcomeReceived(int fromClient, Packet packet)
        {
            int _clientIdCheck = packet.ReadInt();
            string _username = packet.ReadString();

            Console.WriteLine($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}.");
            if (fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            // TODO: send player into game
        }

        public static void UDPTestReceived(int fromClient, Packet packet)
        {
            string msg = packet.ReadString();

            Console.WriteLine($"Receives paclet via UDP. Contains mesage: {msg}");
        }

    }
}