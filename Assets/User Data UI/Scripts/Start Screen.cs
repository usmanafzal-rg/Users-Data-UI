using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
public class StartScreen : AWindowController
{
   private void OnStartClick()
    {
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        UsersScreen screen = (UsersScreen) uiFrame.FindWindow("Users Screen");
        
        screen.ReadUsersData().Then(() =>
        {
            UsersScreenData data = screen.GetUsersData();
            uiFrame.OpenWindow("Users Screen", data);
        });
    }
}
