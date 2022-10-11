using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [System.Serializable]
    public struct levelCount{
        public int min;
        public int max;
    }

    [field:SerializeField] private levelCount _levelCount;
    [SerializeField] private Vector3 _cursorInitPos;

    private int _currentLevel = 1;
    private RectTransform _cursor;

    private MainMenuUI _mainMenuUI;

    private void Awake() {
        _cursor = transform.Find("LevelSelect/Cursor").GetComponent<RectTransform>();
        _mainMenuUI = GetComponent<MainMenuUI>();
        
        _cursor.anchoredPosition3D = _cursorInitPos;
    }

    private void Update() {
        SelectLevel();
    }

    private void SelectLevel(){
        if(_mainMenuUI.IsSelected) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(_currentLevel > _levelCount.min){
                --_currentLevel;
                _cursor.anchoredPosition3D = new Vector3(0, _cursor.anchoredPosition3D.y + 100, 0);
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(_currentLevel < _levelCount.max){
                ++_currentLevel;
                _cursor.anchoredPosition3D = new Vector3(0, _cursor.anchoredPosition3D.y - 100, 0);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log(_currentLevel);
            _mainMenuUI.PopUpLevelPanel(_currentLevel);
        }
    }
}
