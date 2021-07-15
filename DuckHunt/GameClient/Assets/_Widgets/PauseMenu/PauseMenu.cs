using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Canvas canvas;
    public static PauseMenu instance;
    private bool canswitch = true;

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

        hidePausemenu();
    }

    IEnumerator reset(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        canswitch = true;
    }

    public void showPausemenu()
    {
        if (canswitch)
        {
            canswitch = false;
            gameObject.GetComponent<Canvas>().enabled = true;
            StartCoroutine(reset(0.2f));
        }
        
    }
    public void hidePausemenu()
    {
        if (canswitch)
        {
            canswitch = false;
            gameObject.GetComponent<Canvas>().enabled = false;
            StartCoroutine(reset(0.2f));
        }
    }
    public static void togglePausemenu()
    {
        if (instance.gameObject.GetComponent<Canvas>().enabled)
        {
            instance.hidePausemenu();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            instance.showPausemenu();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
