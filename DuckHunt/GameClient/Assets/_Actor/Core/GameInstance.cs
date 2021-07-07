using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    public string username = "localhorst";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("[GameManager] Instance already exists, destroying object!");
            Destroy(this);
        }

        DontDestroyOnLoad(this);
        Debug.Log("Gaminstance initiated");
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        
    }

    #region Save / Load
    public static void saveAutologin(bool autologin)
    {
        int i;

        if (autologin)
        {
            i = 1;
        }
        else
        {
            i = 0;
        }

        PlayerPrefs.SetInt("autologin", i);
    }
    public static bool loadAutologin()
    {
        int i = PlayerPrefs.GetInt("autologin");

        if (i == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static void saveUserCredentials(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
    }
    public static Dictionary<string, string> loadUserCredentials()
    {
        Dictionary<string, string> credentials = new Dictionary<string, string>();
        credentials.Add("username", PlayerPrefs.GetString("username"));
        credentials.Add("password", PlayerPrefs.GetString("password"));

        return credentials;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public static void quitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
        #else
         Application.Quit();
        #endif
    }

    public string getUsername()
    {
        return username;
    }
    public void setUsername(string arg)
    {
        username = arg;
    }


}
