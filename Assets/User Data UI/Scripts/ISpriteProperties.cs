using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public interface ISpriteProperties
{
    public Sprite Sprite { set; }
}

public class NetworkSendProperties : ISpriteProperties
{
    private Sprite _sprite;
    public Sprite Sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
    public string URL;
    public Action<ISpriteProperties> CallBack;
    public ISpriteProperties data;
}

public class ThumbNailSendProperties : ISpriteProperties
{
    private Sprite _sprite;
    public Sprite Sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
    
}
