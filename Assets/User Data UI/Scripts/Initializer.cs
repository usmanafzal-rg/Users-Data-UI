using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
public class Initializer : MonoBehaviour
{
    public UISettings defaultUISettings = null;
    public ServiceLocator serviceLocator;
    async void Start()
    {
        GameObject first = defaultUISettings.CreateUIInstance("Users Screen(Clone)");
        serviceLocator.Initialize();
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        UsersScreen screen = first.GetComponent<UsersScreen>();
        await screen.ReadUsersData();
        UsersScreenData data = screen.GetUsersData();
        uiFrame.OpenWindow("Users Screen", data);
    }
}
