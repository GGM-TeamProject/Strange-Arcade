using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    protected Stage3_Car _player;

    protected virtual void Awake() {
        _player = GameObject.Find("Screen/Stages/Stage_3/PlayerCar").GetComponent<Stage3_Car>();   
    }

    private void Update() {
        MoveItem();
    }

    public void MoveItem(){
        transform.position += -Vector3.forward * (_speed * (_player.PlayerSpeed / 10)) * Time.deltaTime;
        if(transform.position.z <= -3f || !_player.gameObject.activeSelf || _player.PlayerState == CarState.Die || 
        SceneTransManager.Instance.IsChangeScene || GameManager.Instance.UIManager.IsGameClear || GameManager.Instance.UIManager.IsGameOver) {
            PoolManager.Instance.Push(gameObject);
        }
    }

    public abstract void OnUseItem();
}
