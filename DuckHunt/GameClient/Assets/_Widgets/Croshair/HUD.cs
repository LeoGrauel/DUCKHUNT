using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public GameObject ammo;

    public GameObject pointsblue;
    public GameObject pointsred;

    public GameObject top;
    public GameObject down;
    public GameObject right;
    public GameObject left;

    public int maxoffset = 100;
    public float percentage = 0;

    private Vector3 starttop;
    private Vector3 startdown;
    private Vector3 startright;
    private Vector3 startleft;

    private Vector3 maxtop;
    private Vector3 maxdown;
    private Vector3 maxright;
    private Vector3 maxleft;

    // Start is called before the first frame update
    void Start()
    {
        starttop = top.GetComponent<RectTransform>().position;
        startdown = down.GetComponent<RectTransform>().position;
        startright = right.GetComponent<RectTransform>().position;
        startleft = left.GetComponent<RectTransform>().position;

        maxtop =    starttop    +   new Vector3(0,                  maxoffset,          0);
        maxdown =   startdown   +   new Vector3(0,                  maxoffset * -1,     0);
        maxright =  startright  +   new Vector3(maxoffset * -1,     0,                  0);
        maxleft =   startleft   +   new Vector3(maxoffset,          0,                  0);
    }

    // Update is called once per frame
    void Update()
    {
        top.GetComponent<RectTransform>().position = Vector3.Lerp(starttop, maxtop, percentage);
        down.GetComponent<RectTransform>().position = Vector3.Lerp(startdown, maxdown, percentage);
        right.GetComponent<RectTransform>().position = Vector3.Lerp(startright, maxright, percentage);
        left.GetComponent<RectTransform>().position = Vector3.Lerp(startleft, maxleft, percentage);
    }

    public void setAmmo(int value)
    {
        ammo.GetComponent<Text>().text = value.ToString();   
    }

    public void updatePoints(int blue, int red)
    {
        pointsblue.GetComponent<Text>().text = blue.ToString();
        pointsred.GetComponent<Text>().text = red.ToString();
    }
}
