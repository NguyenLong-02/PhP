using System.Collections;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System;

public class WebLogin : MonoBehaviour
{
    public static WebLogin Instance;
    public Web web;
    public UserInfo userinfo;

    public TMP_InputField UserName_Input;
    public TMP_InputField PassWord_Input;
    public TMP_InputField PassWordCon_Input;

    public TextMeshProUGUI username;
    public TextMeshProUGUI level;
    public TextMeshProUGUI coins;

    public string current_coin;


    public Button Login_Button;
    public Button Res_Button;
    public Button ShowUserItemsID_Button;

    public string current_id = null;

    public Action<string> getusercallback;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        getusercallback = (strinnjson) =>
        {
            StartCoroutine(GetUserInfoCallBack(strinnjson));
        };

        userinfo = new UserInfo();

        Login_Button.onClick.AddListener(() => {
            StartCoroutine(web.Login(UserName_Input.text, PassWord_Input.text));

            StartCoroutine(WaitToGetUSeriD());

        });
        Res_Button.onClick.AddListener(() => { StartCoroutine(web.Register(UserName_Input.text, PassWord_Input.text, PassWordCon_Input.text)); });
        /*ShowUserItemsID_Button.onClick.AddListener(() => { StartCoroutine(web.GetUseritemsID(userinfo.GetID())); });*/
    }



    public IEnumerator WaitToGetUSeriD()
    {
        while(userinfo.userid == null || userinfo.userid == current_id)
        {
            yield return null;
        }

        current_id = userinfo.userid;
        StartCoroutine(web.GetAndSetUser(userinfo.userid, getusercallback));
    }


    IEnumerator GetUserInfoCallBack(string jsondata)
    {
        JSONArray jSONArray = JSON.Parse(jsondata) as JSONArray;
        if(jsondata != "0")
        {
            username.text = jSONArray[0].AsObject["username"];
            level.text = jSONArray[0].AsObject["level"];
            coins.text = jSONArray[0].AsObject["coins"];
            current_coin = coins.text;
        }
         else
        {
            yield return null;

        }
    }

}
