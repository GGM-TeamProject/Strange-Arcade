using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<StageSO> stageSOs;

    private bool _isSelected = false;
    public bool IsSelected {get => _isSelected; set => _isSelected = value;}

    private RectTransform _levelRectTrm;
    private RectTransform _popUpRectTrm;

    private void Awake() {
        _levelRectTrm = transform.Find("LevelSelect").GetComponent<RectTransform>();
        _popUpRectTrm = transform.Find("LevelPopUp").GetComponent<RectTransform>();
    } 

    public void PopUpLevelPanel(int stage){
        if(!_isSelected){
            _isSelected = true;
            StartCoroutine(PopUpCoroutine(stage));
        }
    }

    IEnumerator PopUpCoroutine(int stage){
        //팝업 스크린 수정하기
        _levelRectTrm.DOAnchorPos3DX(-225, 0.2f);
        _popUpRectTrm.DOAnchorPos3DX(-70, 0.3f);

        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape));

        _levelRectTrm.DOAnchorPos3DX(0, 0.2f);
        _popUpRectTrm.DOAnchorPos3DX(470, 0.3f);
        if(Input.GetKeyDown(KeyCode.Return)){
            yield return new WaitForSeconds(0.2f);
            Debug.Log($"{stage} 시작");
            _isSelected = false;
        }
        else{
            yield return new WaitForSeconds(0.2f);
            _isSelected = false;
        }
    }
}
