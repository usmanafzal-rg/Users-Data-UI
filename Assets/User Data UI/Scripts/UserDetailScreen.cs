using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading.Tasks;

[Serializable]

public class UserDetailScreen : AWindowController<UserData>
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI phone;
    public TextMeshProUGUI age;
    public TextMeshProUGUI gender;
    public Image image;
    
    protected override void OnPropertiesSet()
    {
        username.text = Properties.first + " " + Properties.last;
        phone.text = Properties.phone;
        age.text = Properties.age.ToString();
        gender.text = Properties.gender;
        if(Properties.image != null)
             image.sprite = Properties.image;
    }
    public void OnGoBackClick()
    {
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.CloseWindow("UserDetail Screen");
    }

    public void OnSettingClick()
    {
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.OpenWindow("Setting Screen");
    }
}
