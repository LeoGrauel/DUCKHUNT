using NiloxUniversalLib.SQL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

using NiloxUniversalLib.EnDecryption;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("[MainMenui] Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public GameObject startScreen;
    public GameObject loginScreen;
    public GameObject mainScreen;
    public GameObject settings;

    public DB db;

    public Text loginerrortext;

    public InputField username;
    public InputField password;

    public VideoPlayer videplayer;
    public AudioSource audioclip;

    // Start is called before the first frame update
    void Start()
    {
        hideLoginScreen();
        hideMainScreen();
        hideSettings();

        showStartScreen();

        autologin();
    }

    // Update is called once per frame
    void Update()
    {
    }

    #region shwo hide
    public void showStartScreen()
    {
        hideLoginScreen();
        hideMainScreen();
        hideSettings();

        startScreen.SetActive(true);
    }
    public void hideStartScreen()
    {
        startScreen.SetActive(false);
    }
    public void showLoginScreen()
    {
        hideStartScreen();
        hideMainScreen();
        hideSettings();

        loginScreen.SetActive(true);
    }
    public void hideLoginScreen()
    {
        loginScreen.SetActive(false);
    }
    public void showMainScreen()
    {
        hideLoginScreen();
        hideStartScreen();
        hideSettings();

        mainScreen.SetActive(true);
    }
    public void hideMainScreen()
    {
        mainScreen.SetActive(false);
    }
    public void showSettings()
    {
        hideLoginScreen();
        hideMainScreen();
        hideStartScreen();

        settings.SetActive(true);
    }
    public void hideSettings()
    {
        settings.SetActive(false);
    }
    #endregion

    #region Login
    private void autologin()
    {
        if (GameInstance.loadAutologin())
        {
            Debug.Log("Autologin is on");

            Dictionary<string, string> load = GameInstance.loadUserCredentials();

            string lusername;
            if (load.TryGetValue("username", out lusername))
            {
                username.text = lusername;
            }
            else
            {
                Debug.LogError("Value 'username' dosnt exist in Directory or is empty");
            }

            string lpassword;
            if (load.TryGetValue("password", out lpassword))
            {
                password.text = lpassword;
            }
            else
            {
                Debug.LogError("Value 'password' dosnt exist in Directory or is empty");
            }
        }
        else
        {
            Debug.Log("Autologin is off");
        }
    }

    public void tryLogin()
    {
        loginerrortext.text = "";

        string username = this.username.text;
        string password = this.password.text;
        
        db.Login(username, password);
    }

    public void tryRegister()
    {
        string username = this.username.text;
        string password = this.password.text;

        db.Register(username, password);
    }
    #endregion

    #region Mathchmaking
    public void quikplay()
    {
        SceneManager.LoadScene("Camp");
    }
    #endregion

    #region quit
    public void quitgame()
    {
        GameInstance.quitGame();
    }
    #endregion
}
