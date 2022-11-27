using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWarningSound : MonoBehaviour
{
    [SerializeField] private AudioClip _warningLaveSound;
    [SerializeField] private AudioClip _stage1BGM;

    private void OnEnable() {
        GameManager.Instance.SoundManager.BGMSetting(_warningLaveSound);
    }
    
    private void OnDisable() {
        GameManager.Instance.SoundManager.BGMSetting(_stage1BGM);
    }
}
