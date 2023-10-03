using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Object = System.Object;

public class User : MonoBehaviour
{
    
    public TextMeshProUGUI username;
    public TextMeshProUGUI email;
    public TextMeshProUGUI gender;
    public Button viewDetailsButton;
    public UsersScreen _screen;
    public string thumbnailUrl; 
    public Image thumbnail;

    public void Initialize(UserData user, UsersScreen screen)
    {
        username.text = user.first + " " + user.last;
        email.text = user.email;
        gender.text = user.gender;
        _screen = screen;
        thumbnailUrl = user.thumbnailUrl;
        AssetManager assetManager = ServiceLocator.Instance.Get<AssetManager>();
        ThumbNailSendProperties data = new ThumbNailSendProperties();
        assetManager.GetSprite(thumbnailUrl, data, ThumbNailCallBack);
    }

    private void ThumbNailCallBack(ISpriteProperties temp)
    {
        ThumbNailSendProperties data = (ThumbNailSendProperties) temp;
        thumbnail.sprite = data.Sprite;
    }
    
    IEnumerator ViewDetails()
    {
        AudioManager audioManager = ServiceLocator.Instance.Get<AudioManager>();
        audioManager.PlayEffect(_screen.viewDetailButtonAudioclip);
        yield return new WaitForSeconds(0.4f);
        _screen.ShowUserDetailScreen(email.text);
    }
    public void OnViewDetailsClick()
    {
        StartCoroutine(ViewDetails());
    }
}
