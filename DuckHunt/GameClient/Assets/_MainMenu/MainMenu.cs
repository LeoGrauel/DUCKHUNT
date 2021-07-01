using NiloxUniversalLib.SQL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas startScreen;
    RectTransform sst;

    public Canvas loginScreen;
    RectTransform lst;

    public Canvas mainScreen;
    RectTransform mst;

    Vector3 hidden = new Vector3(10000, 0, 10000);
    Vector3 shown;

    public DB db;

    public Text loginerrortext;

    public InputField username;
    public InputField password;


    public VideoPlayer videplayer;

    public AudioSource audioclip;

    // Start is called before the first frame update
    void Start()
    {
        sst = startScreen.GetComponent<RectTransform>();
        lst = loginScreen.GetComponent<RectTransform>();
        mst = mainScreen.GetComponent<RectTransform>();

        shown = sst.position;

        sst.position = shown;
        lst.position = hidden;
        mst.position = hidden;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void showLoginScreen()
    {
        loginerrortext.text = "";
        sst.position = hidden;
        lst.position = shown;
        mst.position = hidden;
    }

    public void showMainScreen()
    {
        sst.position = hidden;
        lst.position = hidden;
        mst.position = shown;

        videplayer.Play();

        audioclip.Play();
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

    public void quitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
        #else
         Application.Quit();
        #endif
    }

    public void quikplay()
    {
        SceneManager.LoadScene("Game");
    }
}
