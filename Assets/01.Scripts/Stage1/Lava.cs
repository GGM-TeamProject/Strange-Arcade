using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private float _lavaMoveSpeed = 0.5f;
    [SerializeField] private Vector3 _initPos;

    private bool _onMove = true;
    public bool OnMove {get => _onMove; set => _onMove = value;}

    private void Update() {
        if(_onMove){
            transform.position += Vector3.up * Time.deltaTime * _lavaMoveSpeed;
        }
    }

    public void InitSet(){
        _onMove = true;
        transform.position = _initPos;
    }
}
