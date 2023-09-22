using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
        image.sprite = Properties.image;
    }


    public void OnGoBackClick()
    {
        Controller.instance.GoBack();
    }
}
