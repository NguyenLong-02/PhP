using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleJSON;
using TMPro;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    Action<string> _CreateItemCallback;
    // Start is called before the first frame update
    void Start()
    {
        _CreateItemCallback = (jsonstring) =>
        {
            StartCoroutine(CreateItemrRoutine(jsonstring));
        };
    }

    public void CreatedItems()
    {
        string userid = WebLogin.Instance.userinfo.GetID();
        StartCoroutine(WebLogin.Instance.web.GetUseritemsID(userid, _CreateItemCallback));
    }

    IEnumerator CreateItemrRoutine(string jsonarray)
    {
        JSONArray jSONArray = JSON.Parse(jsonarray) as JSONArray;
        if (jsonarray != "0 results")
        {
            for (int i = 0; i < jSONArray.Count; i++)
            {
                bool isDone = false;
                string itemid = jSONArray[i].AsObject["itemid"];
                JSONObject iteminfo = new JSONObject();

                Action<string> getIteminfoCallBack = (jsoniteminfo) =>
                {
                    isDone = true;
                    JSONArray temarray = JSON.Parse(jsoniteminfo) as JSONArray;
                    iteminfo = temarray[0].AsObject;

                };

                StartCoroutine(WebLogin.Instance.web.Getitem(itemid, getIteminfoCallBack));

                yield return new WaitUntil(() => isDone = true);

                GameObject item = Instantiate(Resources.Load<GameObject>("Items"), transform);
                item.transform.localScale = Vector2.one;

                item.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = iteminfo["name"];
                item.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = iteminfo["description"];
                item.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = iteminfo["price"];


                item.GetComponentInChildren<Button>().onClick.AddListener(() => 
                {
                    string itemid_inbutton = itemid;
                    string userid_inbutton = WebLogin.Instance.userinfo.userid;
                    StartCoroutine(WebLogin.Instance.web.SellItem(itemid_inbutton, userid_inbutton));
                    StartCoroutine(WaitToCoinChange());
                    Destroy(item.gameObject);
                });

            }
        }
        
    }


    IEnumerator WaitToCoinChange()
    {
        while (WebLogin.Instance.coins.text == WebLogin.Instance.current_coin)
        {

            yield return null;
        }
        StartCoroutine(WebLogin.Instance.web.GetAndSetUser(WebLogin.Instance.userinfo.GetID(), WebLogin.Instance.getusercallback));
    }
}
