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
    
    private void Awake() {
        _challengePanel = GameObject.Find("Screen/BackGroundCanvas/ChallengeClearPanel").GetComponent<RectTransform>();
        _challengeImage = _challengePanel.transform.Find("TrophyImage").GetComponent<Image>();
        _challengeText = _challengePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() {
        _challengePanel.anchoredPosition3D = new Vector3(OUT_POS_X, -5, 0);
    }

    public void OnGameOverPanel(){
        _gameOverPanel.gameObject.SetActive(true);
        _gameOverPanel.DOFade(1, 0.3f);
        _gameOverPanel.transform.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.3f);
    }

    public void OffGameOverPanel(){
        Sequence sq = DOTween.Sequence();

        sq.Append(_gameOverPanel.DOFade(0, 0.3f));
        sq.Join(_gameOverPanel.transform.GetComponentInChildren<TextMeshProUGUI>().DOFade(1, 0.3f));
        sq.OnComplete(() => {
            _gameOverPanel.gameObject.SetActive(false);
        });
    }

    public void PopUpChallengePanel(string challengeName, Sprite challengeSprite){
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
