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
        CheckClear("FirstGame");

        for(int i = 0; i < _trophies.Count; i++){
            _trophies[i].achiveMission(DataManager.Instance.User.clearChallenge[i]);
        }
    }

    public void CheckClear(string challengeName){
        int? index = null;

        switch(challengeName){
            case "FirstGame":
                index = 0;
                break;
            case "Clear_S1":
                index = 1;
                break;
            case "Clear_S2":
                index = 2;
                break;
            case "Clear_S3":
                index = 3;
                break;
            case "Clear_S4":
                index = 4;
                break;
            case "FirstDeath_S1":
                index = 5;
                break;
            case "FirstDeath_S2":
                index = 6;
                break;
            case "FirstDeath_S3":
                index = 7;
                break;
            case "FirstDeath_S4":
                index = 8;
                break;
            case "AllClear":
                index = 9;
                break;
        }

        if(index == null) return;
        if(!DataManager.Instance.User.clearChallenge[(int)index]){
            DataManager.Instance.User.clearChallenge[(int)index] = true;
            _trophies[(int)index].achiveMission(DataManager.Instance.User.clearChallenge[(int)index]);
            //도전과제 팝업
        }
    }
}
