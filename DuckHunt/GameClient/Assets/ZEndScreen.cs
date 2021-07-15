using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZEndScreen : MonoBehaviour
{
    public Text score;
    public Text highscore;

    public Text newHS;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        if (PlayerPrefs.GetInt("ZScore") < Gamemode.instance.kills)
        {
            PlayerPrefs.SetInt("ZScore", Gamemode.instance.kills);
            newHS.text = "New Highscore!!";
        }
        else
        {
            newHS.text = "";
        }

        highscore.text = PlayerPrefs.GetInt("ZScore").ToString();
        score.text = Gamemode.instance.kills.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoMainMenu()
    {
        GameInstance.instance.goToMainMenu();
    }
}
