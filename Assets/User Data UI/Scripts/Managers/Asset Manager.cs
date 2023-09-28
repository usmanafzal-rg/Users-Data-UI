using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssetManager : MonoBehaviour, IService
{
    private Dictionary<string, Sprite> _assets = new Dictionary<string, Sprite>();

    public void GetSprite<TProps>(string url, TProps data ,Action<TProps> callback) where TProps : ISpriteProperties
    {
        if (_assets.ContainsKey(url))
        {
            data.Sprite = _assets[url];
            callback(data);
            return;
        }

        NetworkManager networkManager = ServiceLocator.Instance.Get<NetworkManager>();
        NetworkSendProperties<TProps> temp = new NetworkSendProperties<TProps>();
        temp.URL = url;
        temp.CallBack = callback;
        temp.data = data;
        networkManager.GetSprite(url, temp ,SpriteFromNetwork);
    }

    private void SpriteFromNetwork<TProps>(NetworkSendProperties<TProps> data) where TProps : ISpriteProperties
    {
        _assets.Add(data.URL, data.Sprite);
        data.data.Sprite = data.Sprite;
        data.CallBack(data.data);
    }
}
