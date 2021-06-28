using NiloxUniversalLib.SQL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public InputField username;
    public InputField password;


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
        sst.position = hidden;
        lst.position = shown;
        mst.position = hidden;
    }

    public void showMainScreen()
    {
        sst.position = hidden;
        lst.position = hidden;
        mst.position = shown;
    }



    public void tryLogin()
    {
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
        Application.Quit();
    }


}
