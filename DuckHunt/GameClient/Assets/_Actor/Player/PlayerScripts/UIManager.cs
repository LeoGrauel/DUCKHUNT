using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;

    public GameObject textfield;
    public GameObject button1;
    public GameObject button2;
    public GameObject loadingtext;

    public GameObject HUD;

    int lstate = 0;
    float lstatedelay = 0.1f;

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

    public void FixedUpdate()
    {
        Debug.Log(Time.deltaTime);

        lstatedelay -= Time.deltaTime;
        if (lstatedelay <= 0)
        {
            lstatedelay = 100;
            switch (lstate)
            {
                case 0:
                    {
                        loadingtext.GetComponent<Text>().text = "Loading";
                        lstate = 1;
                        break;
                    }
                case 1:
                    {
                        loadingtext.GetComponent<Text>().text = "Loading.";
                        lstate = 2;
                        break;
                    }
                case 2:
                    {
                        loadingtext.GetComponent<Text>().text = "Loading..";
                        lstate = 3;
                        break;
                    }
                case 3:
                    {
                        loadingtext.GetComponent<Text>().text = "Loading...";
                        lstate = 0;
                        break;
                    }
            }
        }
        
    }

    private void Start()
    {
        if (Gamemode.instance.playoffline) 
        {
            if (GameInstance.instance == null)
            {
                GameObject gi = new GameObject("GameInstance");
                gi.AddComponent<GameInstance>();
                gi.GetComponent<GameInstance>().username = "user";
            }

            HUD.SetActive(true);

            textfield.SetActive(false);
            button1.SetActive(false);
            button2.SetActive(false);

            startMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            return;
        }

        HUD.SetActive(false);

        textfield.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);

        if (GameInstance.instance != null)
        {
            if (GameInstance.instance.username != null)
            {
                if (GameInstance.instance.username != "")
                {
                    ConnectedToServer();
                }
            }
        }
        else
        {
            GameObject gi = new GameObject("GameInstance");
            gi.AddComponent<GameInstance>();
            gi.GetComponent<GameInstance>().username = "user";
            ConnectedToServer();
        }
    }

    private void OnDisable()
    {
        if (HUD != null)
        {
            HUD.SetActive(true);
        }
    }


    public void ConnectedToServer()
    {
        StartCoroutine(delayedstart());
    }
    private void ConnectedToServer2()
    {
        //startMenu.SetActive(false);
        //usernameField.interactable = false;

        //GameInstance.instance.username = usernameField.text;

        int index = 0;
        while (Client.instance.tcp == null)
        {
            Debug.Log("TCP null " + index);
            index++;
        }

        Client.instance.ConnectToServer();
    }
    IEnumerator delayedstart()
    {
        yield return new WaitForSecondsRealtime(1f);
        ThreadManager.ExecuteOnMainThread(ConnectedToServer2);
    }

    public void CloseMenu()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
