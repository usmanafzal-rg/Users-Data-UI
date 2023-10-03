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
    void Awake()
    {
        defaultUISettings.CreateUIInstance();
        serviceLocator.Initialize();
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.OpenWindow("Start Screen");
    }
}
