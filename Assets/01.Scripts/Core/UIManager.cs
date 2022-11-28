using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region ChanllengePanelUI
    [SerializeField] private float _onChallengePanelReleaseTime;

    private RectTransform _challengePanel;
    private Image _challengeImage;
    private TextMeshProUGUI _challengeText;

    private const int IN_POS_X = -2;
    private const int OUT_POS_X = 512;

    private bool _isMovePanel = false;
    #endregion

    [SerializeField] private Image _gameOverPanel;
    [SerializeField] private Image _gameClearPanel;

    [SerializeField] private Image _3DGameOverPanel;
    [SerializeField] private Image _3DGameClearPanel;

    [SerializeField] private Image _trophyInfo;
    [SerializeField] private Image _trophyImage;
    [SerializeField] private TextMeshProUGUI _trophyName;
    [SerializeField] private TextMeshProUGUI _trophyInfoText;

    [SerializeField] private AudioClip _challengeClearSound;
    [SerializeField] private AudioClip _gameClearSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _allClearSound;
    [SerializeField] private AudioClip _uiPopUpSound;

    private bool _isOnChallengePanel = false;

    public bool IsGameOver {get; set;} = false;
    public bool IsGameClear {get; set;} = false;
    
    private void Awake() {
        _challengePanel = GameObject.Find("Screen/PublicCanvas/ChallengeClearPanel").GetComponent<RectTransform>();
        _challengeImage = _challengePanel.transform.Find("TrophyImage").GetComponent<Image>();
        _challengeText = _challengePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() {
        _challengePanel.anchoredPosition3D = new Vector3(OUT_POS_X, -5, 0);
    }

    private void Update() {
        if(_isOnChallengePanel){
            if(Input.GetKeyDown(KeyCode.Escape)){
                OffTrophyInfo();
            }
        }
    }

    private void OffTrophyInfo(){
        if(!_isOnChallengePanel) return;
        _isOnChallengePanel = false;

        _trophyInfo.rectTransform.DOScale(0f, 0.3f);
    }

    public void OnTrophyInfo(TrophySO trophySO){
        if(_isOnChallengePanel) return;

        GameManager.Instance.SoundManager.PlayerOneShot(_uiPopUpSound);
        _isOnChallengePanel = true;

        _trophyImage.sprite = trophySO.unLocked;
        _trophyName.text = trophySO.challegeName;
        _trophyInfoText.text = trophySO.explanation;

        Sequence sq = DOTween.Sequence();
        sq.Append(_trophyInfo.rectTransform.DOScale(1.3f, 0.2f));
        sq.Append(_trophyInfo.rectTransform.DOScale(0.9f, 0.1f));
        sq.Append(_trophyInfo.rectTransform.DOScale(1.2f, 0.1f));
    }

    public void OnGameClearPanel(bool is3D){
        IsGameClear = true;
        GameManager.Instance.SoundManager.PlayerOneShot(_gameClearSound);
        ((is3D) ? _3DGameClearPanel : _gameClearPanel).gameObject.SetActive(true);
        ((is3D) ? _3DGameClearPanel : _gameClearPanel).DOFade(1, 0.3f);
        ((is3D) ? _3DGameClearPanel : _gameClearPanel).transform.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.3f);
    }

    public void OffGameClearPanel(bool is3D){
        Sequence sq = DOTween.Sequence();

        sq.Append(((is3D) ? _3DGameClearPanel : _gameClearPanel).DOFade(0, 0.3f));
        sq.Join(((is3D) ? _3DGameClearPanel : _gameClearPanel).transform.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.3f));
        sq.OnComplete(() => {
            ((is3D) ? _3DGameClearPanel : _gameClearPanel).gameObject.SetActive(false);
            IsGameClear = false;
        });
    }

    public void OnGameOverPanel(bool is3D){
        IsGameOver = true;
        GameManager.Instance.SoundManager.PlayerOneShot(_gameOverSound);
        ((is3D) ? _3DGameOverPanel : _gameOverPanel).gameObject.SetActive(true);
        ((is3D) ? _3DGameOverPanel : _gameOverPanel).DOFade(1, 0.3f);
        ((is3D) ? _3DGameOverPanel : _gameOverPanel).transform.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.3f);
    }

    public void OffGameOverPanel(bool is3D){
        Sequence sq = DOTween.Sequence();

        sq.Append(((is3D) ? _3DGameOverPanel : _gameOverPanel).DOFade(0, 0.3f));
        sq.Join(((is3D) ? _3DGameOverPanel : _gameOverPanel).transform.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.3f));
        sq.OnComplete(() => {
            ((is3D) ? _3DGameOverPanel : _gameOverPanel).gameObject.SetActive(false);
            IsGameOver = false;
        });
    }

    public void PopUpChallengePanel(string challengeName, Sprite challengeSprite){
        GameManager.Instance.SoundManager.PlayerOneShot((challengeName == "[고양이] 그 잡채") ? _allClearSound : _challengeClearSound);
        StartCoroutine(PopUpChallengeCoroutine(challengeName, challengeSprite));
    }

    IEnumerator PopUpChallengeCoroutine(string challengeName, Sprite challengeSprite){
        yield return new WaitUntil(() => _isMovePanel == false);

        _challengeText.text = challengeName;
        _challengeImage.sprite = challengeSprite;
        _isMovePanel = true;
        _challengePanel.DOAnchorPos3DX(IN_POS_X, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSecondsRealtime(_onChallengePanelReleaseTime);
        _challengePanel.DOAnchorPos3DX(OUT_POS_X, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSecondsRealtime(0.5f);
        _isMovePanel = false;
    }
}
