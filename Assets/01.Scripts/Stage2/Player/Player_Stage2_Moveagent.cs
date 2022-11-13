using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage2_Moveagent : MonoBehaviour
{
    [Header("Movement Value")]
    [SerializeField] private float _accel = 50f;
    [SerializeField] private float _deAccel = 50f;
    [SerializeField][Range(0.1f, 10f)] private float _maxSpeed = 10f;

    private Rigidbody2D _rigid;

    private float _currentVelocity = 0f;
    private Vector2 _movementDirection;

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 movementInput){
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
        _rigid.velocity = _movementDirection * _currentVelocity;
    }

    public void StopImmediatly(){
        _currentVelocity = 0f;
        _rigid.velocity = Vector2.zero;
    }
}
