using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Stage2_UI : MonoBehaviour
{
    [SerializeField] private Image[] _skillIcons;
    [SerializeField] private int _timerTime;
    private int _minute = 0;
    private int _second = 0;

    private Player_Stage2 _player;
    private TextMeshProUGUI _timer;

    private void Awake() {
        _player = transform.parent.Find("Player").GetComponent<Player_Stage2>();
        _timer = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        StartCoroutine(TimerPath());
    }

    private void Update() {
        SetPlayerSKillValue();
    }

    IEnumerator TimerPath(){
        while(true){
            if(_timerTime == 60) _timer.DOColor(Color.red, 60f);
            TransTime(_timerTime, out _minute, out _second);
            _timer.text = $"{_minute.ToString("D2")}:{_second.ToString("D2")}";
            yield return new WaitForSecondsRealtime(1f);
            --_timerTime;;
        }
    }

    private void TransTime(int second, out int Minute, out int Second){
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
