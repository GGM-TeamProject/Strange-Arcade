using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Stage2_UI : MonoBehaviour
{
    [SerializeField] private Image[] _skillIcons;
    [SerializeField] private Transform _hpParent = null;
    [SerializeField] private int _timerTime;

    private Stack<Image> _catHp = new Stack<Image>();

    private int _minute = 0;
    private int _second = 0;

    private Player_Stage2 _player;
    private Stage2_Cat _cat;
    private TextMeshProUGUI _timer;

    private void Awake() {
        _hpParent = transform.Find("CatHp");
        _player = transform.parent.Find("Player").GetComponent<Player_Stage2>();
        _cat = transform.parent.Find("Cat").GetComponent<Stage2_Cat>();
        _timer = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        Init();
    }

    public void Init(){
        foreach(Transform child in _hpParent){
            child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _catHp.Push(child.GetComponent<Image>());
        }
        _timerTime = 180; 
        StopAllCoroutines();
        StartCoroutine(TimerPath());
    }

    private void Update() {
        SetPlayerSKillValue();
    }

    IEnumerator TimerPath(){
        while(_cat.CatState != CatState.Die){
            if(_timerTime == 60) _timer.DOColor(Color.red, 60f);
            Second2MinSec(_timerTime, out _minute, out _second);
            _timer.text = $"{_minute.ToString("D2")}:{_second.ToString("D2")}";
            yield return new WaitForSecondsRealtime(1f);
            --_timerTime;
        }
    }

    public void HpDownUI(int count){
        StartCoroutine(HpDownCoroutine(count));
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

    private void Second2MinSec(int second, out int Minute, out int Second){
        Minute = 0; Second = 0;
        while(second > 60){
            second -= 60;
            Minute++;
        }
        Second = second;
    }

    private void SetPlayerSKillValue(){
        for(int i = 0; i < _skillIcons.Length; i++){
            _skillIcons[i].fillAmount = Mathf.Lerp(0f, 1f, (!_player.PlayerSkills[i].CanSkill) ? 1f - (_player.PlayerSkills[i].SkillCool / 10f) : 1f);
        }
    }
}
