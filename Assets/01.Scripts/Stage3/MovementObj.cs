using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObj : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private Vector3 _limitPos;
    [SerializeField] private Vector3 _movePos;

    private void Update() {
        transform.position += -Vector3.forward * _moveSpeed * Time.deltaTime;
        if(transform.position.z < _limitPos.z) transform.position = _movePos;
    }
}
