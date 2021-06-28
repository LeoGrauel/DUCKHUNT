using UnityEngine;
using System.Collections;
using System.Data.Sql;
using System.Data.SqlClient;
using UnityEngine.Networking;

public class DB : MonoBehaviour
{
    public MainMenu mainmenu;

    static string login = "http://nilox.network/open/PHP/login/login.php";
    static string register = "http://nilox.network/open/PHP/login/register.php";

    public void Login(string username, string password)
    {
        StartCoroutine(pLogin(username, password));
    }

    IEnumerator pLogin(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(login, form))
        {
            yield return request.SendWebRequest();

            string result = request.downloadHandler.text;

            switch (result)
            {
                case "Login-Success":
                    {
                        mainmenu.showMainScreen();
                        Debug.Log("User logged in");
                        break;
                    }
                case "Username or Password is Incorrect":
                    {
                        Debug.LogWarning("Username or Password is Incorrect");
                        break;
                    }
                case "Username or Password is Incorrect...":
                    {
                        Debug.LogWarning("Username or Password is Incorrect");
                        break;
                    }
                default:
                    Debug.LogError("UNRESOLVED ERROR WITH WEBREQUEST LOGIN");
                    break;
            }
        }
    }


    public void Register(string username, string password)
    {
        StartCoroutine(pRegister(username, password));
    }

    IEnumerator pRegister(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(register, form))
        {
            yield return request.SendWebRequest();

            string result = request.downloadHandler.text;

            switch (result)
            {
                case "User Created":
                    {
                        mainmenu.showMainScreen();
                        Debug.Log("User Created");
                        break;
                    }
                case "username is taken":
                    {
                        Debug.LogWarning("Username is taken");
                        break;
                    }
                default:
                    Debug.LogError("UNRESOLVED ERROR WITH WEBREQUEST LOGIN");
                    break;
            }
        }
    }

}