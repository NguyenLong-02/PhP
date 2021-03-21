

using System;

[System.Serializable]
public class UserInfo
{
    public string userid { get; private set; }
    public string username { get; private set; }
    public string userpassword { get; private set; }

    public void SetInfo(string id, string name, string pass)
    {
        userid = id; username = name; userpassword = pass;
    }

    public string GetID()
    {
        return userid;
    }
}