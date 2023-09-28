using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour , IService
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    public void ToggleMusicSource()
    {
        _musicSource.mute = !_musicSource.mute;
    }
    
    public void ToggleEffectSource()
    {
        _effectSource.mute = !_musicSource.mute;
    }

    public void ChangeVolumeMusicSource(float volume)
    {
        _musicSource.volume = volume;
    }
    
    public void ChangeVolumeEffectSource(float volume)
    {
        _effectSource.volume = volume;
    }

    public void PlayEffect(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void SetMusicSource(AudioClip clip)
    {
        _musicSource.clip = clip;
    }

    public float GetVolume()
    {
        return _musicSource.volume;
    }

    public bool GetMute()
    {
        return _musicSource.mute;
    }
}
