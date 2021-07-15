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

    public Slider mastervolumeslider;
    public Dropdown resolutiondropdown;

    public Button fullscreenbutton;

    // Start is called before the first frame update
    void Start()
    {
        mastervolumeslider.value = PlayerPrefs.GetFloat("MasterVolume");
        fullscreenbutton.GetComponentInChildren<Text>().text = GameInstance.instance.fullscreen.ToString();

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


    public void updateMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", mastervolumeslider.value);
        GameInstance.instance.MasterVolume = mastervolumeslider.value;

        Debug.Log(GameInstance.instance.MasterVolume);
    }

    public void toggleFullscreen()
    {
        if (GameInstance.instance.fullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            GameInstance.instance.fullscreen = false;
            fullscreenbutton.GetComponentInChildren<Text>().text = GameInstance.instance.fullscreen.ToString();
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            GameInstance.instance.fullscreen = true;
            fullscreenbutton.GetComponentInChildren<Text>().text = GameInstance.instance.fullscreen.ToString();
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }
    public void setResolution()
    {
        int[] res = stringToResolution(resolutiondropdown.options[resolutiondropdown.value].text);

        Screen.SetResolution(res[0], res[1], true);

        PlayerPrefs.SetInt("resX", res[0]);
        PlayerPrefs.SetInt("resY", res[1]);
    }

    public int[] stringToResolution(string resolution)
    {
        int[] res = new int[2];
        string[] parts = resolution.Split('*');

        if (parts[0] == "848" && parts[1] == "480")
        {
            res[0] = 848;
            res[1] = 480;
        }
        if (parts[0] == "1280" && parts[1] == "720")
        {
            res[0] = 1280;
            res[1] = 720;
        }
        if (parts[0] == "1920" && parts[1] == "1080")
        {
            res[0] = 1920;
            res[1] = 1080;
        }
        if (parts[0] == "2560" && parts[1] == "1440")
        {
            res[0] = 2560;
            res[1] = 1440;
        }

        return res;
    }
    


}
