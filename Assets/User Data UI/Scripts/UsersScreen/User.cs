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
    private Action<string> _onDetailButtonClick;
    public string thumbnailUrl; 
    public Image thumbnail;
    private AssetManager _assetManager;
    
    public void Initialize(UserData user, Action<string> onDetailButtonClick)
    {
        _assetManager = ServiceLocator.Instance.Get<AssetManager>();
        username.text = user.first + " " + user.last;
        email.text = user.email;
        gender.text = user.gender;
        _onDetailButtonClick = onDetailButtonClick;
        thumbnailUrl = user.thumbnailUrl;
        ThumbNailSendProperties data = new ThumbNailSendProperties();
        _assetManager.GetSprite(thumbnailUrl, data, ThumbNailCallBack);
    }

    private void ThumbNailCallBack(ISpriteProperties temp)
    {
        ThumbNailSendProperties data = (ThumbNailSendProperties) temp;
        thumbnail.sprite = data.Sprite;
    }

    private void OnViewDetailsClick()
    {
        _onDetailButtonClick(email.text);
    }
}
