using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<GameObject> _poolList = new List<GameObject>();

    public ChallengeManager ChallengeManager;
    public UIManager UIManager;
    public CursorManager CursorManager;

    private void Awake() {
        foreach(GameObject obj in _poolList){
            PoolManager.Instance.CreatePool(obj, transform);
            //Create Pooling Object
        }
        
        ChallengeManager = GetComponent<ChallengeManager>();
        UIManager = GetComponent<UIManager>();
        CursorManager = GetComponent<CursorManager>();
    }
}
