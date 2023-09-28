using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISpriteProperties
{
    public Sprite Sprite { set; }
}

public class NetworkSendProperties<TProps> : ISpriteProperties
{
    private Sprite _sprite;
    public Sprite Sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
    public string URL;
    public Action<TProps> CallBack;
    public TProps data;
}