using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ESCMenu : ButtonSelect
{
    [SerializeField] private RectTransform _mainPanel;
    [SerializeField] private RectTransform _escPanel;
    [SerializeField] private RectTransform _audioSliderPanel;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] private bool _isEsc = false;
    [SerializeField] private bool _isAudioSetting = false;

    private GameObject _mainMenuObj = null;
    private GameObject _gameClearPanel = null;
    private GameObject _gameOverPanel = null;

    private void Awake() {
        _mainMenuObj = transform.parent.parent.Find("Menu").gameObject;
        _gameOverPanel = transform.parent.Find("GameOver").gameObject;
        _gameClearPanel = transform.parent.Find("GameClear").gameObject;
    }

    private void Update() {
        GameManager.Instance.SoundManager.VolumeSet("MASTER", _masterSlider.value);
        GameManager.Instance.SoundManager.VolumeSet("BGM", _bgmSlider.value);
        GameManager.Instance.SoundManager.VolumeSet("SFX", _sfxSlider.value);
        
        if(_mainMenuObj.activeSelf || _gameClearPanel.activeSelf || _gameOverPanel.activeSelf) return;
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(_isAudioSetting){
                _isAudioSetting = false;
                _escPanel.DOAnchorPosY(0f, 0.5f).SetUpdate(true);
                _audioSliderPanel.DOAnchorPosY(-60f, 0.5f).SetUpdate(true);
            }
            else{
                _isEsc = !_isEsc;
                ESCPanel();
            }
        }

        if(!_isAudioSetting){
            ButtonMove();
            if(Input.GetKeyDown(KeyCode.Return)){
                switch(_currentSelectButton){
                    case 0:
                        ContinueBtn();
                        break;
                    case 1:
                        AudioSetting();
                        break;
                    case 2:
                        ExitToMain();
                        break;
                }
            }
        }
    }

    private void ESCPanel(){
        Time.timeScale = (_isEsc) ? 0 : 1;
        _mainPanel.DOAnchorPosX(((_isEsc) ? -80.5f : -130f), 0.5f).SetUpdate(true);
    }

    public void ContinueBtn(){
        _isAudioSetting = false;
        _isEsc = false;
        ESCPanel();
    }

    public void AudioSetting(){
        _isAudioSetting = true;
        _escPanel.DOAnchorPosY(20f, 0.5f).SetUpdate(true);
        _audioSliderPanel.DOAnchorPosY(-10f, 0.5f).SetUpdate(true);
    }

    public void ExitToMain(){
        _isAudioSetting = false;
        _isEsc = false;
        ESCPanel();
        SceneTransManager.Instance.SceneChange("MainMenu");
    }
}
