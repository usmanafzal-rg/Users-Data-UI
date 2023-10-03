using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid.UIFramework;
using UnityEngine.UI;

public class SettingScreen : AWindowController
{
    public Slider slider;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Image sound;
    AudioManager _audioManager;
    private void Start()
    {
        _audioManager = ServiceLocator.Instance.Get<AudioManager>();
        float curVolume = _audioManager.GetVolume();
        slider.value = curVolume;
        sound.sprite = _audioManager.GetMute() ? soundOff : soundOn;
    }

    public void OnChangeSound(float value)
    {
        _audioManager.ChangeVolumeEffectSource(value);
        _audioManager.ChangeVolumeMusicSource(value);
    }
    
    public void OnGoBackClick()
    {
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.CloseWindow("Setting Screen");
    }

    public void ToggleVolume()
    {
        _audioManager.ToggleEffectSource();
        _audioManager.ToggleMusicSource();
        sound.sprite = _audioManager.GetMute() ? soundOff : soundOn;
    }
}
