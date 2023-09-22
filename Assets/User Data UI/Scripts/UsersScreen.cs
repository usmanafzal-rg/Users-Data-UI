using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;

public class UsersScreen : AWindowController<UsersScreenData>
{
    public GameObject userPrefab;
    public Transform content;
    protected override void OnPropertiesSet() {
        foreach (var userPair in Properties.AllUsers)
        {
            UserData user = userPair.Value;
            GameObject userObject = Instantiate(userPrefab, content);
            User info = userObject.GetComponent<User>();
            info.username.text = user.first + " " + user.last;
            info.email.text = user.email;
            info.gender.text = user.gender;
        }
    }
    
}
