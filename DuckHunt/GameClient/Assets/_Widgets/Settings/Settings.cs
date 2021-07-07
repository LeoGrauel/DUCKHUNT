using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    
    public static Settings instance;
    #region Instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("[Settings] Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    #endregion

    public string rAccount = "account";
    public GameObject autologinstatustext;
    public GameObject Account;
    public GameObject Video;
    public GameObject Audio;


    // Start is called before the first frame update
    void Start()
    {
        hideAccount();
        hideAudio();
        hideVideo();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region show hide
    public static void closeSettings()
    {
        Settings.instance.gameObject.SetActive(false);
    }
    public static void showSettings()
    {
        Settings.instance.gameObject.SetActive(true);
    }
    public void hide()
    {
        closeSettings();

        if (MainMenu.instance != null)
        {
            MainMenu.instance.showMainScreen();
        }
    }
    #endregion

    #region Widget switcher
    public void showAccount()
    {
        hideVideo();
        hideAudio();

        Account.SetActive(true);

        updateAutoupdateText();
    }
    public void hideAccount()
    {
        Account.SetActive(false);
    }

    public void showVideo()
    {
        hideAudio();
        hideAccount();

        Video.SetActive(true);
    }
    public void hideVideo()
    {
        Video.SetActive(false);
    }

    public void showAudio()
    {
        hideVideo();
        hideAccount();

        Audio.SetActive(true);
    }
    public void hideAudio()
    {
        Audio.SetActive(false);
    }
    #endregion

    #region autologin
    public void toggleAutologin()
    {
        if (GameInstance.loadAutologin())
        {
            GameInstance.saveAutologin(false);
            autologinstatustext.gameObject.GetComponent<Text>().text = "off";
        }
        else
        {
            GameInstance.saveAutologin(true);
            autologinstatustext.gameObject.GetComponent<Text>().text = "on";
        }
    }

    public void updateAutoupdateText()
    {
        if (GameInstance.loadAutologin())
        {
            autologinstatustext.gameObject.GetComponent<Text>().text = "on";
        }
        else
        {
            autologinstatustext.gameObject.GetComponent<Text>().text = "off";
        }
    }

    #endregion






}
