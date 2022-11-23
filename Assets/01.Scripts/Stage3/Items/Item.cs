using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    public void MoveItem(){
        transform.position += -Vector3.forward * _speed * Time.deltaTime;
    }

    private void Update() {
        MoveItem();
    }

    public abstract void OnUseItem();
}
