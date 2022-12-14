using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    [SerializeField] protected int _timerTime;
    [SerializeField] protected AudioClip _timerWarningClip;

    protected int _second = 0;
    protected int _minute = 0;

    [SerializeField] protected TextMeshProUGUI _timer;

    public void InitTimerSet(){
        _timerTime = 180;
        _timer.color = Color.white;
        StartCoroutine(TimerPath());
    }

    protected virtual IEnumerator TimerPath(){
        while(_timerTime >= 0){
            if(_timerTime == 60){
                _timer.DOColor(Color.red, 60f);
                GameManager.Instance.SoundManager.BGMSetting(_timerWarningClip);
            }
            Second2MinSec(_timerTime, out _minute, out _second);
            _timer.text = $"{_minute.ToString("D2")}:{_second.ToString("D2")}";
            yield return new WaitForSeconds(1f);
            --_timerTime;
        }
        DataManager.Instance.User.CurrentPlayStage++;
        StartCoroutine(StageClearCoroutine());
    }

    private void Second2MinSec(int second, out int Minute, out int Second){
        Minute = 0; Second = 0;
        while(second > 60){
            second -= 60;
            Minute++;
        }
        Second = second;
    }

    protected virtual IEnumerator StageClearCoroutine(){
        GameManager.Instance.ChallengeManager.CheckClear("Clear_S3");
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UIManager.OnGameClearPanel(true);

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape)); //나중에 수정
        SceneTransManager.Instance.SceneChange("MainMenu");
        GameManager.Instance.UIManager.OffGameClearPanel(true);
    }
}
