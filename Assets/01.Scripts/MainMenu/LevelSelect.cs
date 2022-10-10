using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private int _currentLevel = 1;
    private RectTransform _cursor;
    private bool _isLevelMove = false;

    private void Awake() {
        _cursor = GameObject.Find("Screen/ScreenCanvas/LevelSelect/Cursor").GetComponent<RectTransform>();
        _cursor.anchoredPosition3D = _cursorInitPos;
    }

    private void Update() {
        SelectLevel();
    }

    private void SelectLevel(){
        float input = Input.GetAxisRaw("Vertical");

        if(input != 0 && !_isLevelMove){
            Sequence sq = DOTween.Sequence();
            if(input > 0){
                if(_currentLevel > _levelCount.min){
                    _isLevelMove = true;
                    _currentLevel--;

                    sq.Append(_cursor.DOAnchorPos3DY(_cursor.anchoredPosition3D.y + 100, 0.5f));
                    sq.OnComplete(() => _isLevelMove = false);
                }
            }
            if(input < 0){
                if(_currentLevel < _levelCount.max){
                    _isLevelMove = true;
                    _currentLevel++;

                    sq.Append(_cursor.DOAnchorPos3DY(_cursor.anchoredPosition3D.y - 100, 0.5f));
                    sq.OnComplete(() => _isLevelMove = false);
                }
            }
        }
    }
}
