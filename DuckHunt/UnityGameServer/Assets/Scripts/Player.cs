using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        ServerSend.PlayerTransform(this);
    }

    public void UpdateTransform(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}
