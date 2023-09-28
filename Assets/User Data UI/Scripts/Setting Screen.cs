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
    private void Start()
    {
        AudioManager audioManager = ServiceLocator.Instance.Get<AudioManager>();
        float curVolume = audioManager.GetVolume();
        slider.value = curVolume;
        sound.sprite = audioManager.GetMute() ? soundOff : soundOn;
    }

    public void OnChangeSound(float value)
    {
        AudioManager audioManager = ServiceLocator.Instance.Get<AudioManager>();
        audioManager.ChangeVolumeEffectSource(value);
        audioManager.ChangeVolumeMusicSource(value);
    }
    
    public void OnGoBackClick()
    {
        UIFrame uiFrame = ServiceLocator.Instance.Get<UIFrame>();
        uiFrame.CloseWindow("Setting Screen");
    }

    public void ToggleVolume()
    {
        AudioManager audioManager = ServiceLocator.Instance.Get<AudioManager>();
        audioManager.ToggleEffectSource();
        audioManager.ToggleMusicSource();
        sound.sprite = audioManager.GetMute() ? soundOff : soundOn;
    }
}
