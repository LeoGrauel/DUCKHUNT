using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    #region send Mothods
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }
    #endregion

#region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(GameInstance.instance.username);

            SendTCPData(_packet);
        }
    }

    public static void PlayerTransform()
    {
        if (GameManager.players[Client.instance.myId] == null)
        {
            Debug.LogError("Player dosnt exist");
            return;
        }

        using (Packet _packet = new Packet((int)ClientPackets.playerTransform))
        {
            _packet.Write(GameManager.players[Client.instance.myId].transform.position);
            _packet.Write(GameManager.players[Client.instance.myId].transform.rotation);

            SendTCPData(_packet);
        }
    }

    public static void PlayerDamage(int myid, int targetid, int damage)
    {
        Debug.Log("Sending PlayerDamage");
        using (Packet packet = new Packet((int)ClientPackets.damagePlayer))
        {
            packet.Write(myid); //instigator id
            packet.Write(targetid); //tartget id
            packet.Write(damage);

            SendTCPData(packet);
        }
    }

    public static void playershot(int myid,int shotid ,Vector3 location, Quaternion rotation)
    {
        using (Packet packet = new Packet((int)ClientPackets.playershot))
        {
            packet.Write(location);
            packet.Write(rotation);
            packet.Write(shotid);

            SendTCPData(packet);
        }
    }

#endregion
}