using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWarningSound : MonoBehaviour
{
    [SerializeField] private AudioClip _warningLaveSound;
    [SerializeField] private AudioClip _stage1BGM;

    private GameManager _gameManagerInstance;

    private void Awake() {
        _gameManagerInstance = GameManager.Instance;
    }

    private void OnEnable() {
        _gameManagerInstance.SoundManager.BGMSetting(_warningLaveSound);
    }
    
    private void OnDisable() {
        _gameManagerInstance.SoundManager.BGMSetting(_stage1BGM);
    }
}
