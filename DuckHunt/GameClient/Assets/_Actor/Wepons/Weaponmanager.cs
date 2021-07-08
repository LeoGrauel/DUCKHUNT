using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponmanager : MonoBehaviour
{
    public static Weaponmanager instance;
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

    public GameObject mp5;
    public GameObject m14;

    private List<GameObject> weapons = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(null);
        weapons.Add(mp5);
        weapons.Add(m14);
    }

    public void switchtoWeapon(int value)
    {
        Debug.Log("Switching to " + value + "C:" + weapons.Count);

        int index = 0;
        foreach (GameObject w in weapons)
        {
            if (index == 0)
            {
                Debug.Log("INdex 0");
                continue;
            }

            if (index == value)
            {
                //w.gameObject.GetComponent<MeshRenderer>().gameObject.SetActive(true);
                w.gameObject.GetComponent<WeaponFunc>().shotenabled = true;
            }
            else
            {
                //w.gameObject.GetComponent<MeshRenderer>().gameObject.SetActive(false);
                w.gameObject.GetComponent<WeaponFunc>().shotenabled = false;
            }

            Debug.Log("asdas" + index);


            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
