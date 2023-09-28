using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;
using System.Threading.Tasks;
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
public class UsersScreen : AWindowController<UsersScreenData>
{
    public GameObject userPrefab;
    public Transform content;
    public AudioClip viewDetailButtonAudioclip;
    public async Task ReadUsersData()
    {
        NetworkManager request = ServiceLocator.Instance.Get<NetworkManager>();
        UsersData data = await request.Get<UsersData>("https://randomuser.me/api/?results=50&inc=name,email,gender,phone,dob,picture");
        foreach(Result user in data.results)
        {
            Properties.AllUsers.Add(user.email ,new UserData(user.name.first, user.name.last, user.email, user.gender, user.phone, user.dob.age, user.picture.large ));
        }
    }

    public UsersScreenData GetUsersData()
    {
        return Properties;
    }

    public void ShowUserDetailScreen(string email)
    {
        StartCoroutine(ShowUserDetailCoroutine(email));
    }
    
    private IEnumerator ShowUserDetailCoroutine(string email)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(Properties.AllUsers[email].imageUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            Properties.AllUsers[email].image = sprite;
        }
        
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.OpenWindow("UserDetail Screen", Properties.AllUsers[email]);
    }
    
    protected override void OnPropertiesSet() {
        foreach (var userPair in Properties.AllUsers)
        {
            UserData user = userPair.Value;
            GameObject userObject = Instantiate(userPrefab, content);
            User info = userObject.GetComponent<User>();
            info.username.text = user.first + " " + user.last;
            info.email.text = user.email;
            info.gender.text = user.gender;
            info._screen = this;
        }
    }
    
    public void OnSettingClick()
    {
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.OpenWindow("Setting Screen");
    }
}
