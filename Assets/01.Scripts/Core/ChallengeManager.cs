using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    [SerializeField] private List<Trophy> _trophies;
    public List<Trophy> Trophies {get => _trophies;}

    // 0 - FirstGame
    // 1 - Clear_S1
    // 2 - Clear_S2
    // 3 - Clear_S3
    // 4 - Clear_S4
    // 5 - FirstDeath_S1
    // 6 - FirstDeath_S2
    // 7 - FirstDeath_S3
    // 8 - FirstDeath_S4
    // 9 - AllClear

    private void Start() {
        //CheckClear("FirstGame");

        for(int i = 0; i < _trophies.Count; i++){
            _trophies[i].achiveMission(DataManager.Instance.User.clearChallenge[i]);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) CheckClear("FirstGame");
        if(Input.GetKeyDown(KeyCode.Alpha2)) CheckClear("Clear_S1");
    }

    public void CheckClear(string challengeName){
        int? index = null;
        string _challengeName = null;

        switch(challengeName){
            case "FirstGame":
                _challengeName = "시작은 창대하였으나\n끝은 미약하리라.";
                index = 0;
                break;
            case "Clear_S1":
                _challengeName = "휴 ! \n 다행히 발에 불만 붙었군";
                index = 1;
                break;
            case "Clear_S2":
                _challengeName = "시켜줘.\n정이 명예 소방관";
                index = 2;
                break;
            case "Clear_S3":
                _challengeName = "미개한 인간에게 지다니..";
                index = 3;
                break;
            case "Clear_S4":
                _challengeName = "왕년에 돈키콩 좀 하던 사람";
                index = 4;
                break;
            case "FirstDeath_S1":
                _challengeName = "아이 따뜻해";
                index = 5;
                break;
            case "FirstDeath_S2":
                _challengeName = "벌집 피자";
                index = 6;
                break;
            case "FirstDeath_S3":
                _challengeName = "미개한 인간 따위가\n우릴 이길 순 없지";
                index = 7;
                break;
            case "FirstDeath_S4":
                _challengeName = "털공 롤러다ㅏㅏㅏㅏ";
                index = 8;
                break;
            case "AllClear":
                _challengeName = "[고양이] 그 잡채";
                index = 9;
                break;
        }

        if(index == null || _challengeName == null) return;
        if(!DataManager.Instance.User.clearChallenge[(int)index]){
            DataManager.Instance.User.clearChallenge[(int)index] = true;
            _trophies[(int)index].achiveMission(DataManager.Instance.User.clearChallenge[(int)index]);
            
            GameManager.Instance.UIManager.PopUpChallengePanel(_challengeName);
        }
    }
}
