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
        var syncContext = System.Threading.SynchronizationContext.Current;
        screen.ReadUsersData().Then(() =>
        {
            UsersScreenData data = screen.GetUsersData();
            syncContext.Post(_ =>
            {
                UIFrame uiFrame1 = ServiceLocator.Instance.Get<UIFrame>();
                uiFrame1.OpenWindow("Users Screen", data);
            }, null);
            
        });
    }
}
