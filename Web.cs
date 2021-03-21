using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using System;

public class Web : MonoBehaviour
{
    public WebLogin webLogin;


    void Start()
    {
        
    }

    public IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackEnd/Sever.php"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
                                        
                webLogin.userinfo.SetInfo(www.downloadHandler.text, username, password);
                Debug.Log(webLogin.userinfo.userid);
            }
        }
    }

    public IEnumerator GetAndSetUser(string userid, Action<string> GetUserCallBack)
    {
        while(webLogin.userinfo.userid == null)
        {
            Debug.Log(webLogin.userinfo.userid == null);
            yield return null;
        }

        WWWForm form = new WWWForm();
        form.AddField("userid", userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/Getuser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonUserData = www.downloadHandler.text;
                GetUserCallBack(jsonUserData);
            }
        }
    }
    public IEnumerator Register(string username, string password, string passwordcon)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginPassConf", passwordcon);



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/Resgister.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }
    }

    public IEnumerator GetUseritemsID(string userid, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userid", userid);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/GetItemsID.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonarray = www.downloadHandler.text;

                callback(jsonarray);
            }
        }
    }

    public IEnumerator Getitem(string id, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemid", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/GetItem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);
                string jsonarray = www.downloadHandler.text;

                callback(jsonarray);
            }
        }
    }

    public IEnumerator SellItem(string itemid, string userid)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemid", itemid);
        form.AddField("userid", userid);


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/SellItem.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                StartCoroutine(GetAndSetUser(WebLogin.Instance.userinfo.userid, WebLogin.Instance.getusercallback));
            }
        }
    }



}
