using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stage1 : MonoBehaviour
{
    [Header("Movement Value")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    private Rigidbody _rigid;

    private int _jumpCount = 1;
    
    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update() {
        Move();
        Jump();
    }

    private void Move(){
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 moveDir = new Vector3(h * _speed, _rigid.velocity.y);
        
        _rigid.velocity = moveDir;
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(_jumpCount > 0){
                _jumpCount--;
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
