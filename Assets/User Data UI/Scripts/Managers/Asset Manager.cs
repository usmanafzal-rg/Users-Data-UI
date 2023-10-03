using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class AssetManager : MonoBehaviour, IService
{
    
    private Dictionary<string, Sprite> _assets = new Dictionary<string, Sprite>();
    private Dictionary<string, bool> _underProgress = new Dictionary<string, bool>();
    private Dictionary<string, List<NetworkSendProperties>> _pending = new Dictionary<string, List<NetworkSendProperties>>();
    public void GetSprite(string url, ISpriteProperties data ,Action<ISpriteProperties> callback)
    {
        NetworkSendProperties temp = new NetworkSendProperties();
        temp.URL = url;
        temp.CallBack = callback;
        temp.data = data;
        
        if (_assets.ContainsKey(url))
        {
            data.Sprite = _assets[url];
            callback(data);
        }
        else if(_underProgress.ContainsKey(url))
        {
            if (_pending.ContainsKey(url) == false)
            {
                _pending[url] = new List<NetworkSendProperties>();
            }
            _pending[url].Add(temp);
        }
        else
        {
            NetworkManager networkManager = ServiceLocator.Instance.Get<NetworkManager>();
            _underProgress.Add(url, true);
            networkManager.GetSprite<NetworkSendProperties>(url, temp ,SpriteFromNetwork);
        }
    }

    private void SpriteFromNetwork(NetworkSendProperties data) 
    {
        _assets.Add(data.URL, data.Sprite);
        data.data.Sprite = data.Sprite;
        data.CallBack(data.data);
        _underProgress.Remove(data.URL);

        if (_pending.ContainsKey(data.URL))
        {
            foreach (var request in _pending[data.URL])
            {
                if (request.URL.CompareTo(data.URL) == 0)
                {
                    request.data.Sprite = data.Sprite;
                    request.CallBack(request.data);
                }
            }
            
            _pending.Remove(data.URL);
        }

    }
}
