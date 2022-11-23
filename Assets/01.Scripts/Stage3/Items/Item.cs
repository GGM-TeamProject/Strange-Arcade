using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    protected Stage3_Car player;

    protected virtual void Awake() {
        player = GameObject.Find("Screen/Stages/Stage_3/PlayerCar").GetComponent<Stage3_Car>();   
    }

    private void Update() {
        MoveItem();
    }

    public void MoveItem(){
        transform.position += -Vector3.forward * (_speed * (player.PlayerSpeed / 10)) * Time.deltaTime;
        if(transform.position.z <= -3f) PoolManager.Instance.Push(gameObject);
    }

    public abstract void OnUseItem();
}
