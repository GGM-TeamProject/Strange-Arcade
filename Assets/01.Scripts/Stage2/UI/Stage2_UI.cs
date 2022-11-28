using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Stage2_UI : Timer
{
    [SerializeField] private Image[] _skillIcons;
    [SerializeField] private Transform _hpParent = null;
    [SerializeField] private Image _skillInfoPanel;

    private Stack<Image> _catHp = new Stack<Image>();

    private Player_Stage2 _player;
    private Stage2_Cat _cat;

    private void OnEnable()
    {
        _player = transform.parent.Find("Player").GetComponent<Player_Stage2>();
        _cat = transform.parent.Find("Cat").GetComponent<Stage2_Cat>();

        StartCoroutine(InfoCoroutine());
    }

    public void Init(){
        foreach(Transform child in _hpParent){
            child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _catHp.Push(child.GetComponent<Image>());
        }
        _timerTime = 120; 
        StopAllCoroutines();
        StartCoroutine(TimerPath());
    }

    private void Update() {
        SetPlayerSKillValue();
    }

    public void HpDownUI(int count){
        StartCoroutine(HpDownCoroutine(count));
    }

    IEnumerator InfoCoroutine(){
        yield return new WaitForSeconds(1.8f);
        _skillInfoPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        _skillInfoPanel.DOFade(0.6f, 0.5f).SetUpdate(true);
        yield return new WaitUntil(() => Input.anyKeyDown);
        _skillInfoPanel.DOFade(0f, 0.5f).SetUpdate(true);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);
        _skillInfoPanel.gameObject.SetActive(false);
    }

    IEnumerator HpDownCoroutine(int count){
        for(int i = 0; i < count; i++){
            if(_catHp.Count > 0){
                Sequence sq = DOTween.Sequence();
                Image image = _catHp.Pop();
                sq.Append(image.rectTransform.DOScale(1.5f, 0.2f));
                sq.Join(image.DOColor(Color.red, 0.2f));
                sq.Append(image.rectTransform.DOScale(1f, 0.1f));
                sq.OnComplete(() => {
                    image.color = new Color(1, 1, 1, 0);
                });
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void SetPlayerSKillValue(){
        for(int i = 0; i < _skillIcons.Length; i++){
            _skillIcons[i].fillAmount = Mathf.Lerp(0f, 1f, (!_player.PlayerSkills[i].CanSkill) ? 1f - (_player.PlayerSkills[i].SkillCool / 10f) : 1f);
        }
    }

    protected override IEnumerator StageClearCoroutine()
    {
        GameManager.Instance.ChallengeManager.CheckClear("Clear_S2");
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameClearPanel(false);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape)); //나중에 수정
        GameManager.Instance.UIManager.OffGameClearPanel(false);
        SceneTransManager.Instance.SceneChange("MainMenu");
    }
}
