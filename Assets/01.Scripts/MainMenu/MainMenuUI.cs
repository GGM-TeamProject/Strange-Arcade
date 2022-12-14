using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<StageSO> stageSOs;
    [SerializeField] private AudioClip _stageStartSound;
    [SerializeField] private AudioClip _mainMenuBGM;

    private bool _isSelected = false;
    public bool IsSelected {get => _isSelected; set => _isSelected = value;}

    private RectTransform _levelRectTrm;
    private RectTransform _popUpRectTrm;

    private void Awake() {
        _levelRectTrm = transform.Find("LevelSelect").GetComponent<RectTransform>();
        _popUpRectTrm = transform.Find("LevelPopUp").GetComponent<RectTransform>();
    } 

    private void OnEnable() {
        GameManager.Instance.SoundManager.BGMSetting(_mainMenuBGM);
    }

    public void PopUpLevelPanel(int stage){
        if(!_isSelected){
            _isSelected = true;
            StartCoroutine(PopUpLevelCoroutine(stage));
        }
    }

    IEnumerator PopUpLevelCoroutine(int stage){
        _popUpRectTrm.Find("Image").GetComponent<Image>().sprite = stageSOs[stage - 1].image;
        _popUpRectTrm.Find("Image/StageInfo").GetComponent<TextMeshProUGUI>().text = stageSOs[stage - 1].InfoTxt;

        _levelRectTrm.DOAnchorPos3DX(-256, 0.2f);
        _popUpRectTrm.DOAnchorPos3DX(-284, 0.3f);

        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape));

        _levelRectTrm.DOAnchorPos3DX(0, 0.2f);
        _popUpRectTrm.DOAnchorPos3DX(470, 0.3f);
        if(Input.GetKeyDown(KeyCode.Return)){
            yield return new WaitForSeconds(0.2f);
            GameManager.Instance.SoundManager.PlayerOneShot(_stageStartSound);
            Debug.Log($"{stage} 시작");
            SceneTransManager.Instance.SceneChange($"Stage{stage}");
            _isSelected = false;
        }
        else{
            yield return new WaitForSeconds(0.2f);
            _isSelected = false;
        }
    }
}
