using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAgent : MonoBehaviour
{
    [Header("Movement Value")]
    [SerializeField] private float _accel = 50f;
    [SerializeField] private float _deAccel = 50f;
    [SerializeField][Range(0.1f, 100f)] private float _maxSpeed = 10f;

    [Header("Type")]
    [SerializeField] private bool _2D = false;

    private float _currentVelocity = 0f;
    private Vector2 _movementDirection;

    private Rigidbody _rigid;
    private Rigidbody2D _rigid2D;

    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 movementInput){
        if(movementInput.sqrMagnitude > 0){
            if(Vector2.Dot(movementInput, _movementDirection) < 0){
                _currentVelocity = 0f;
            }
            _movementDirection = movementInput.normalized;
        }
        _currentVelocity = CalcSpeed(movementInput);
    }

    private float CalcSpeed(Vector2 movementInput){
        if(movementInput.sqrMagnitude > 0){
            _currentVelocity += _accel * Time.deltaTime;
        }
        else{
            _currentVelocity -= _deAccel * Time.deltaTime;
        }
        return Mathf.Clamp(_currentVelocity, 0, _maxSpeed);
    }

    private void FixedUpdate() {
        if(_2D) _rigid2D.velocity = _movementDirection * _currentVelocity;
        else _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void StopImmediatly(){
        _currentVelocity = 0f;
        _rigid.velocity = Vector2.zero;
    }
}
