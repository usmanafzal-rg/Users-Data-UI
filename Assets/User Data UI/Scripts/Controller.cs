using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
[Serializable]
public class Name
{
    public string title = "";
    public string first = "";
    public string last = "";
}

[Serializable]
public class DOB
{
    public string date;
    public int age;
}

[Serializable]
public class Picture
{
    public string large;
    public string medium;
    public string thumbnail;
}

[Serializable]
public class Result
{
    public string gender = "";
    public Name name;
    public string email = "";
    public DOB dob;
    public string phone;
    public Picture picture;
}
[Serializable]
public class Info
{
    public string seed = "";
    public int results;
    public int page;
    public string version = "";
}
[Serializable]
public class UsersData
{
    public List<Result> results;
    public Info info;
}
[Serializable]
public class UserData : WindowProperties
{
    public string first;
    public string last;
    public string email;
    public string gender;
    public string phone;
    public int age;
    public string imageUrl;
    public Sprite image = null;
    public UserData(string fname, string lname, string email, string gender, string phone, int age, string imageUrl)
    {
        first = fname;
        last = lname;
        this.email = email;
        this.gender = gender;
        this.phone = phone;
        this.age = age;
        this.imageUrl = imageUrl;
    }
}
[Serializable]
public class UsersScreenData : WindowProperties
{
    public Dictionary<string,UserData> AllUsers = new Dictionary<string,UserData>();
}


public class Controller : MonoBehaviour
{
    public string URL = "";
    public UsersScreenData usersScreenData = new UsersScreenData();
    public UISettings defaultUISettings = null;
    private UIFrame uiFrame = null;
    public static Controller instance = null;
    private bool dataRecieved = false;
    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(request());
        uiFrame = defaultUISettings.CreateUIInstance();
        
    }

    IEnumerator request()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(URL);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            
           string responseText = webRequest.downloadHandler.text;
           UsersData data;
           try
           { 
               data = JsonUtility.FromJson<UsersData>(responseText);
           }
           catch (Exception e)
           {
               Debug.LogError("Deserialization Error: " + e.Message);
               throw;
           }
           foreach(Result user in data.results)
           {
               usersScreenData.AllUsers.Add(user.email ,new UserData(user.name.first, user.name.last, user.email, user.gender, user.phone, user.dob.age, user.picture.large ));
           }

           dataRecieved = true;
        }
    }
    private void Update()
    {
        if (dataRecieved == true)
        {
            uiFrame.OpenWindow<UsersScreenData>("Users Screen", usersScreenData);
            dataRecieved = false;
        }
    }

    public void ViewDetails(User caller)
    {
        StartCoroutine(ViewDetailsCoRoutine(caller.email.text));
    }

    public void GoBack()
    {
        uiFrame.CloseWindow("UserDetail Screen");
    }
    
    
    IEnumerator ViewDetailsCoRoutine(string email)
    {
        UserData user = usersScreenData.AllUsers[email];
        if (user.image == null)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(user.imageUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                user.image = sprite;
            }
        }

        uiFrame.OpenWindow<UserData>("UserDetail Screen", usersScreenData.AllUsers[email]);
    }
}
