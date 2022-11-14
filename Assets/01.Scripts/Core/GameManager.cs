using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<GameObject> _poolList = new List<GameObject>();

    public ChallengeManager ChallengeManager;
    public UIManager UIManager;
    public CursorManager CursorManager;
    public CameraManager CameraManager;

    private void Awake() {
        foreach(GameObject obj in _poolList){
            PoolManager.Instance.CreatePool(obj, transform);
            //Create Pooling Object
        }
        
        ChallengeManager = transform.parent.GetComponentInChildren<ChallengeManager>();
        UIManager = transform.parent.GetComponentInChildren<UIManager>();
        CursorManager = transform.parent.GetComponentInChildren<CursorManager>();
        CameraManager = transform.parent.GetComponentInChildren<CameraManager>();
    }

    public IEnumerator TurnObject(){
        while(DataManager.Instance.User.CurrentPlayStage == 2){
            yield return null;
            //오브젝트 회전하는거 구현해야함
        }
    }
}
