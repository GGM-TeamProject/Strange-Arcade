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
    // 4 - FirstDeath_S1
    // 5 - FirstDeath_S2
    // 6 - FirstDeath_S3
    // 7 - AllClear

    private void Start() {
        //CheckClear("FirstGame");

        for(int i = 0; i < _trophies.Count; i++){
            _trophies[i].achiveMission(DataManager.Instance.User.clearChallenge[i]);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) CheckClear("FirstGame");
        if(Input.GetKeyDown(KeyCode.Alpha2)) CheckClear("Clear_S1");
        if(Input.GetKeyDown(KeyCode.Alpha3)) CheckClear("Clear_S2");
        if(Input.GetKeyDown(KeyCode.Alpha4)) CheckClear("Clear_S3");
        if(Input.GetKeyDown(KeyCode.Alpha5)) CheckClear("FirstDeath_S1");
        if(Input.GetKeyDown(KeyCode.Alpha6)) CheckClear("FirstDeath_S2");
        if(Input.GetKeyDown(KeyCode.Alpha7)) CheckClear("FirstDeath_S3");
        if(Input.GetKeyDown(KeyCode.Alpha8)) CheckClear("AllClear");
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
            case "FirstDeath_S1":
                index = 4;
                break;
            case "FirstDeath_S2":
                index = 5;
                break;
            case "FirstDeath_S3":
                index = 6;
                break;
            case "AllClear":
                index = 7;
                break;
        }

        if(index == null) return;
        if(!DataManager.Instance.User.clearChallenge[(int)index]){
            DataManager.Instance.User.clearChallenge[(int)index] = true;
            _trophies[(int)index].achiveMission(DataManager.Instance.User.clearChallenge[(int)index]);
            
            GameManager.Instance.UIManager.PopUpChallengePanel(_trophies[(int)index].trophySO.challegeName, _trophies[(int)index].trophySO.unLocked);
        }
    }
}
