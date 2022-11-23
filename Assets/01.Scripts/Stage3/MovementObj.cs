using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObj : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private Vector3 _limitPos;
    [SerializeField] private Vector3 _movePos;

    private Stage3_Car _player;

    private void Awake() {
        _player = transform.parent.parent.parent.Find("PlayerCar").GetComponent<Stage3_Car>();
    }

    private void Update() {
        _moveSpeed = _player.PlayerSpeed;
        transform.position += -Vector3.forward * _moveSpeed * Time.deltaTime;
        if(transform.position.z <= _limitPos.z) transform.position += _movePos;
    }
}
