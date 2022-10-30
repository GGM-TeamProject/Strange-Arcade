using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage1 : MonoBehaviour
{
    [Header("Movement Value")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    [Header("JumpPowerLimitValue")]
    [SerializeField] private float _maxJumpPower;
    [SerializeField] private float _minJumpPower;

    private Rigidbody _rigid;

    private int _jumpCount = 1;
    private bool _isJump = false;
    
    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update() {
        if(_isJump) return; //점프 중에는 움직이기 불가능

        Move();
        Jump();
    }

    private void Move(){
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 moveDir = new Vector3(h * _speed, _rigid.velocity.y);
        
        _rigid.velocity = moveDir;
    }

    private void Jump(){

        if(Input.GetKey(KeyCode.Space)){
            _jumpPower += Time.deltaTime;
            _jumpPower = Mathf.Clamp(_jumpPower, _minJumpPower, _maxJumpPower);
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            if(_jumpCount > 0){
                _jumpPower = 0f;
                _jumpCount--;
                _isJump = true;

                _rigid.velocity = Vector2.zero;
                _rigid.velocity = Vector2.up * _jumpPower;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Stage1_Platform")){
            _jumpCount = 1;
        }
    }
}
