using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyHunt : MonoBehaviour
{
    public GameObject enemy;
    public GameObject targetPosition;
    public int areaMask = 1;

    NavMeshPath path;
    bool pathFound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pathFound = NavMesh.CalculatePath(enemy.transform.position, targetPosition.transform.position, areaMask, path);

        if (pathFound)
        {
            NavMesh.
        }
        else
        {

        }
    }
}
