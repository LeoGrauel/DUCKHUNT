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
        Debug.Log("START");
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
        print("Still here");
    }





    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
