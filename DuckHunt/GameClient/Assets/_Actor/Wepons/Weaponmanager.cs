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

    private bool canswitch = true;
    private float delay = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(null);
        weapons.Add(mp5);
        weapons.Add(m14);

        switchtoWeapon(0);
    }

    public void switchtoWeapon(int value)
    {
        if (canswitch == false)
        {
            return;
        }

        int index = 0;
        foreach (GameObject w in weapons)
        {
            if (index == 0)
            {
                index++;
                continue;
            }

            if (index == value)
            {
                w.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
                w.gameObject.GetComponentInChildren<WeaponFunc>().shotenabled = true;
                HUD.instance.setAmmo(w.gameObject.GetComponentInChildren<WeaponFunc>().getRounds());

                canswitch = false;
                StartCoroutine(reset(delay));
            }
            else
            {
                w.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                w.gameObject.GetComponentInChildren<WeaponFunc>().shotenabled = false;
            }

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator reset(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        canswitch = true;
    }

}
