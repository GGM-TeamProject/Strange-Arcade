using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<GameObject> _poolList = new List<GameObject>();

    public ChallengeManager ChallengeManager;
    public UIManager UIManager;
    public CursorManager CursorManager;
    public CameraManager CameraManager;
    public ItemManager ItemManager;

    private Transform _bulletStore;
    public Transform BulletStore => _bulletStore;

    private void Awake() {
        _bulletStore = GameObject.Find("Screen/Stages/Stage_2/RollObjects").transform.Find("Bullets");

        foreach(GameObject obj in _poolList){
            Transform parent = (obj.name == "Bullet") ? _bulletStore : transform;
            PoolManager.Instance.CreatePool(obj, parent);
            //Create Pooling Object
        }
        
        ChallengeManager = transform.parent.GetComponentInChildren<ChallengeManager>();
        UIManager = transform.parent.GetComponentInChildren<UIManager>();
        CursorManager = transform.parent.GetComponentInChildren<CursorManager>();
        CameraManager = transform.parent.GetComponentInChildren<CameraManager>();
        ItemManager = transform.parent.GetComponentInChildren<ItemManager>();
    }
}
