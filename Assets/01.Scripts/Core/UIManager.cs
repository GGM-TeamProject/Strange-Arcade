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
    private TextMeshProUGUI _challengeText;

    private const int IN_POS_X = -2;
    private const int OUT_POS_X = 512;

    private bool _isMovePanel = false;
    #endregion
    
    private void Awake() {
        _challengePanel = GameObject.Find("Screen/ScreenCanvas/ChallengeClearPanel").GetComponent<RectTransform>();
        _challengeText = _challengePanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start() {
        _challengePanel.anchoredPosition3D = new Vector3(OUT_POS_X, -5, 0);
    }

    public void PopUpChallengePanel(string challengeName){
        StartCoroutine(PopUpChallengeCoroutine(challengeName));
    }

    IEnumerator PopUpChallengeCoroutine(string challengeName){
        yield return new WaitUntil(() => _isMovePanel == false);

        _challengeText.text = challengeName;
        _isMovePanel = true;
        _challengePanel.DOAnchorPos3DX(IN_POS_X, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSecondsRealtime(_onChallengePanelReleaseTime);
        _challengePanel.DOAnchorPos3DX(OUT_POS_X, 0.5f).SetEase(Ease.OutQuad);
        yield return new WaitForSecondsRealtime(0.5f);
        _isMovePanel = false;
    }
}
