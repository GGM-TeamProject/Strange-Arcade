using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelSelect : MonoBehaviour
{
    [System.Serializable]
    public struct levelCount{
        public int min;
        public int max;
    }

    [field:SerializeField] private levelCount _levelCount;

    [SerializeField] private Vector3 _cursorInitPos;
    [SerializeField] private RectTransform[] _windows;

    private int _currentWindow = 1;
    private int _currentLevel = 1;

    private bool _isMovingWindow = false;

    private RectTransform _cursor;
    private RectTransform _arrow;
    private TextMeshProUGUI _arrowDirect;
    private MainMenuUI _mainMenuUI;

    private Sequence sq;

    private void Awake() {
        _cursor = transform.Find("LevelSelect/Cursor").GetComponent<RectTransform>();
        _arrow = transform.Find("Arrow").GetComponent<RectTransform>();
        _arrowDirect = _arrow.GetComponent<TextMeshProUGUI>();
        _mainMenuUI = GetComponent<MainMenuUI>();
        
        _cursor.anchoredPosition3D = _cursorInitPos;
    }

    private void OnEnable() {
        for(int i = 0; i < DataManager.Instance.User.CurrentPlayStage; i++){
            SceneTransManager.Instance.Levels[i].SetLockState(false);
        }
    }

    private void Update() {
        _arrow.anchoredPosition3D = new Vector3((_currentWindow == 1) ? 520f : -520f, 0f, 0f);
        _arrowDirect.text = (_currentWindow == 1) ? ">" : "<";
        SelectLevel();
        SelectWindow();
    }

    private void SelectWindow(){
        if(_mainMenuUI.IsSelected || _isMovingWindow) return;

        if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && (_currentWindow > 1 || _currentWindow < 2)){
            sq = DOTween.Sequence();
            _isMovingWindow = true;
            
            _currentWindow += (_currentWindow == 1) ? 1 : -1;
            foreach(RectTransform r in _windows){
                sq.Join(r.DOAnchorPos3DX(r.anchoredPosition3D.x + ((_currentWindow == 1) ? 1500 : -1500), 0.5f));
            }

            sq.OnComplete(() => _isMovingWindow = false);
        }
    }

    private void SelectLevel(){
        if(_mainMenuUI.IsSelected || _currentWindow != 1 || _isMovingWindow) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(_currentLevel > _levelCount.min && !SceneTransManager.Instance.Levels[(_currentLevel - 1) - 1].IsLock){
                --_currentLevel;
                _cursor.anchoredPosition3D = new Vector3(0, _cursor.anchoredPosition3D.y + 100, 0);
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(_currentLevel < _levelCount.max && !SceneTransManager.Instance.Levels[(_currentLevel - 1) + 1].IsLock){
                ++_currentLevel;
                _cursor.anchoredPosition3D = new Vector3(0, _cursor.anchoredPosition3D.y - 100, 0);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            _mainMenuUI.PopUpLevelPanel(_currentLevel);
        }
    }

    private void SelectTrophy(){
        if(_currentWindow != 2 || _isMovingWindow) return;

        
    }
}
