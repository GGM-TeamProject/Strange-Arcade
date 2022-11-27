using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;

    public void VolumeSet(string mixer, float volume){
        _masterMixer.SetFloat(mixer, Mathf.Lerp(-40, 20, volume));
    }

    public void PlayerOneShot(AudioClip Clip){
        _sfxSource.PlayOneShot(Clip);
    }

    public void BGMSetting(AudioClip Clip){
        _bgmSource.clip = Clip;
        _bgmSource.Stop();
        if(!_bgmSource.isPlaying) _bgmSource.Play();
    }
}
