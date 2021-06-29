using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchMaker 
{
    public string ripurl = "http://nilox.network/open/PHP/duckhunt/getip.php";

    public string getRandomIP()
    {
        
        HttpWebRequest wr = WebRequest.CreateHttp(ripurl);
        WebResponse response = wr.GetResponse();

        using (Stream dataStream = response.GetResponseStream())
        {
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            return responseFromServer;
        }
    }

}
