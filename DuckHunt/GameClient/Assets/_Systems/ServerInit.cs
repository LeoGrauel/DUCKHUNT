using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DummyServer dummy = new DummyServer();
        dummy.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
